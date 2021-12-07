using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000063 RID: 99
	public class SyncListStruct<T> : SyncList<T> where T : struct
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0001A474 File Offset: 0x00018674
		public new void AddInternal(T item)
		{
			base.AddInternal(item);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001A480 File Offset: 0x00018680
		protected override void SerializeItem(NetworkWriter writer, T item)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001A484 File Offset: 0x00018684
		protected override T DeserializeItem(NetworkReader reader)
		{
			return default(T);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001A49C File Offset: 0x0001869C
		public T GetItem(int i)
		{
			return base[i];
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0001A4A8 File Offset: 0x000186A8
		public new ushort Count
		{
			get
			{
				return (ushort)base.Count;
			}
		}
	}
}
