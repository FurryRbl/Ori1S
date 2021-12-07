using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine.Networking
{
	// Token: 0x02000064 RID: 100
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class SyncList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001A4C8 File Offset: 0x000186C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001A4D0 File Offset: 0x000186D0
		public int Count
		{
			get
			{
				return this.m_Objects.Count;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0001A4E0 File Offset: 0x000186E0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001A4E4 File Offset: 0x000186E4
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001A4EC File Offset: 0x000186EC
		public SyncList<T>.SyncListChanged Callback
		{
			get
			{
				return this.m_Callback;
			}
			set
			{
				this.m_Callback = value;
			}
		}

		// Token: 0x0600050E RID: 1294
		protected abstract void SerializeItem(NetworkWriter writer, T item);

		// Token: 0x0600050F RID: 1295
		protected abstract T DeserializeItem(NetworkReader reader);

		// Token: 0x06000510 RID: 1296 RVA: 0x0001A4F8 File Offset: 0x000186F8
		public void InitializeBehaviour(NetworkBehaviour beh, int cmdHash)
		{
			this.m_Behaviour = beh;
			this.m_CmdHash = cmdHash;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001A508 File Offset: 0x00018708
		private void SendMsg(SyncList<T>.Operation op, int itemIndex, T item)
		{
			if (this.m_Behaviour == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("SyncList not initialized");
				}
				return;
			}
			NetworkIdentity component = this.m_Behaviour.GetComponent<NetworkIdentity>();
			if (component == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("SyncList no NetworkIdentity");
				}
				return;
			}
			if (!component.isServer)
			{
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(9);
			networkWriter.Write(component.netId);
			networkWriter.WritePackedUInt32((uint)this.m_CmdHash);
			networkWriter.Write((byte)op);
			networkWriter.WritePackedUInt32((uint)itemIndex);
			this.SerializeItem(networkWriter, item);
			networkWriter.FinishMessage();
			NetworkServer.SendWriterToReady(component.gameObject, networkWriter, this.m_Behaviour.GetNetworkChannel());
			if (this.m_Behaviour.isServer && this.m_Behaviour.isClient && this.m_Callback != null)
			{
				this.m_Callback(op, itemIndex);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001A608 File Offset: 0x00018808
		private void SendMsg(SyncList<T>.Operation op, int itemIndex)
		{
			this.SendMsg(op, itemIndex, default(T));
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001A628 File Offset: 0x00018828
		public void HandleMsg(NetworkReader reader)
		{
			byte op = reader.ReadByte();
			int num = (int)reader.ReadPackedUInt32();
			T t = this.DeserializeItem(reader);
			switch (op)
			{
			case 0:
				this.m_Objects.Add(t);
				break;
			case 1:
				this.m_Objects.Clear();
				break;
			case 2:
				this.m_Objects.Insert(num, t);
				break;
			case 3:
				this.m_Objects.Remove(t);
				break;
			case 4:
				this.m_Objects.RemoveAt(num);
				break;
			case 5:
			case 6:
				this.m_Objects[num] = t;
				break;
			}
			if (this.m_Callback != null)
			{
				this.m_Callback((SyncList<T>.Operation)op, num);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001A6F4 File Offset: 0x000188F4
		internal void AddInternal(T item)
		{
			this.m_Objects.Add(item);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001A704 File Offset: 0x00018904
		public void Add(T item)
		{
			this.m_Objects.Add(item);
			this.SendMsg(SyncList<T>.Operation.OP_ADD, this.m_Objects.Count - 1, item);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001A734 File Offset: 0x00018934
		public void Clear()
		{
			this.m_Objects.Clear();
			this.SendMsg(SyncList<T>.Operation.OP_CLEAR, 0);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001A74C File Offset: 0x0001894C
		public bool Contains(T item)
		{
			return this.m_Objects.Contains(item);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001A75C File Offset: 0x0001895C
		public void CopyTo(T[] array, int index)
		{
			this.m_Objects.CopyTo(array, index);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001A76C File Offset: 0x0001896C
		public int IndexOf(T item)
		{
			return this.m_Objects.IndexOf(item);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001A77C File Offset: 0x0001897C
		public void Insert(int index, T item)
		{
			this.m_Objects.Insert(index, item);
			this.SendMsg(SyncList<T>.Operation.OP_INSERT, index, item);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001A794 File Offset: 0x00018994
		public bool Remove(T item)
		{
			bool flag = this.m_Objects.Remove(item);
			if (flag)
			{
				this.SendMsg(SyncList<T>.Operation.OP_REMOVE, 0, item);
			}
			return flag;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001A7C0 File Offset: 0x000189C0
		public void RemoveAt(int index)
		{
			this.m_Objects.RemoveAt(index);
			this.SendMsg(SyncList<T>.Operation.OP_REMOVEAT, index);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001A7D8 File Offset: 0x000189D8
		public void Dirty(int index)
		{
			this.SendMsg(SyncList<T>.Operation.OP_DIRTY, index, this.m_Objects[index]);
		}

		// Token: 0x170000D8 RID: 216
		public T this[int i]
		{
			get
			{
				return this.m_Objects[i];
			}
			set
			{
				this.m_Objects[i] = value;
				this.SendMsg(SyncList<T>.Operation.OP_SET, i, value);
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001A818 File Offset: 0x00018A18
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_Objects.GetEnumerator();
		}

		// Token: 0x04000203 RID: 515
		private List<T> m_Objects = new List<T>();

		// Token: 0x04000204 RID: 516
		private NetworkBehaviour m_Behaviour;

		// Token: 0x04000205 RID: 517
		private int m_CmdHash;

		// Token: 0x04000206 RID: 518
		private SyncList<T>.SyncListChanged m_Callback;

		// Token: 0x02000065 RID: 101
		public enum Operation
		{
			// Token: 0x04000208 RID: 520
			OP_ADD,
			// Token: 0x04000209 RID: 521
			OP_CLEAR,
			// Token: 0x0400020A RID: 522
			OP_INSERT,
			// Token: 0x0400020B RID: 523
			OP_REMOVE,
			// Token: 0x0400020C RID: 524
			OP_REMOVEAT,
			// Token: 0x0400020D RID: 525
			OP_SET,
			// Token: 0x0400020E RID: 526
			OP_DIRTY
		}

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x0600053E RID: 1342
		public delegate void SyncListChanged(SyncList<T>.Operation op, int itemIndex);
	}
}
