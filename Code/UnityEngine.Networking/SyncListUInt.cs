using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000061 RID: 97
	public class SyncListUInt : SyncList<uint>
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x0001A274 File Offset: 0x00018474
		protected override void SerializeItem(NetworkWriter writer, uint item)
		{
			writer.WritePackedUInt32(item);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001A280 File Offset: 0x00018480
		protected override uint DeserializeItem(NetworkReader reader)
		{
			return reader.ReadPackedUInt32();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001A288 File Offset: 0x00018488
		[Obsolete("ReadReference is now used instead")]
		public static SyncListUInt ReadInstance(NetworkReader reader)
		{
			ushort num = reader.ReadUInt16();
			SyncListUInt syncListUInt = new SyncListUInt();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncListUInt.AddInternal(reader.ReadPackedUInt32());
			}
			return syncListUInt;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001A2C4 File Offset: 0x000184C4
		public static void ReadReference(NetworkReader reader, SyncListUInt syncList)
		{
			ushort num = reader.ReadUInt16();
			syncList.Clear();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncList.AddInternal(reader.ReadPackedUInt32());
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001A300 File Offset: 0x00018500
		public static void WriteInstance(NetworkWriter writer, SyncListUInt items)
		{
			writer.Write((ushort)items.Count);
			foreach (uint value in items)
			{
				writer.WritePackedUInt32(value);
			}
		}
	}
}
