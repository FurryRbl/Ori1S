using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000062 RID: 98
	public class SyncListBool : SyncList<bool>
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x0001A374 File Offset: 0x00018574
		protected override void SerializeItem(NetworkWriter writer, bool item)
		{
			writer.Write(item);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001A380 File Offset: 0x00018580
		protected override bool DeserializeItem(NetworkReader reader)
		{
			return reader.ReadBoolean();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001A388 File Offset: 0x00018588
		[Obsolete("ReadReference is now used instead")]
		public static SyncListBool ReadInstance(NetworkReader reader)
		{
			ushort num = reader.ReadUInt16();
			SyncListBool syncListBool = new SyncListBool();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncListBool.AddInternal(reader.ReadBoolean());
			}
			return syncListBool;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001A3C4 File Offset: 0x000185C4
		public static void ReadReference(NetworkReader reader, SyncListBool syncList)
		{
			ushort num = reader.ReadUInt16();
			syncList.Clear();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncList.AddInternal(reader.ReadBoolean());
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001A400 File Offset: 0x00018600
		public static void WriteInstance(NetworkWriter writer, SyncListBool items)
		{
			writer.Write((ushort)items.Count);
			foreach (bool value in items)
			{
				writer.Write(value);
			}
		}
	}
}
