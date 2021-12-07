using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000060 RID: 96
	public class SyncListInt : SyncList<int>
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0001A174 File Offset: 0x00018374
		protected override void SerializeItem(NetworkWriter writer, int item)
		{
			writer.WritePackedUInt32((uint)item);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001A180 File Offset: 0x00018380
		protected override int DeserializeItem(NetworkReader reader)
		{
			return (int)reader.ReadPackedUInt32();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001A188 File Offset: 0x00018388
		[Obsolete("ReadReference is now used instead")]
		public static SyncListInt ReadInstance(NetworkReader reader)
		{
			ushort num = reader.ReadUInt16();
			SyncListInt syncListInt = new SyncListInt();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncListInt.AddInternal((int)reader.ReadPackedUInt32());
			}
			return syncListInt;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001A1C4 File Offset: 0x000183C4
		public static void ReadReference(NetworkReader reader, SyncListInt syncList)
		{
			ushort num = reader.ReadUInt16();
			syncList.Clear();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncList.AddInternal((int)reader.ReadPackedUInt32());
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001A200 File Offset: 0x00018400
		public static void WriteInstance(NetworkWriter writer, SyncListInt items)
		{
			writer.Write((ushort)items.Count);
			foreach (int value in items)
			{
				writer.WritePackedUInt32((uint)value);
			}
		}
	}
}
