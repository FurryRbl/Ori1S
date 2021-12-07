using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200005E RID: 94
	public sealed class SyncListString : SyncList<string>
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x00019F74 File Offset: 0x00018174
		protected override void SerializeItem(NetworkWriter writer, string item)
		{
			writer.Write(item);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00019F80 File Offset: 0x00018180
		protected override string DeserializeItem(NetworkReader reader)
		{
			return reader.ReadString();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00019F88 File Offset: 0x00018188
		[Obsolete("ReadReference is now used instead")]
		public static SyncListString ReadInstance(NetworkReader reader)
		{
			ushort num = reader.ReadUInt16();
			SyncListString syncListString = new SyncListString();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncListString.AddInternal(reader.ReadString());
			}
			return syncListString;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00019FC4 File Offset: 0x000181C4
		public static void ReadReference(NetworkReader reader, SyncListString syncList)
		{
			ushort num = reader.ReadUInt16();
			syncList.Clear();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncList.AddInternal(reader.ReadString());
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001A000 File Offset: 0x00018200
		public static void WriteInstance(NetworkWriter writer, SyncListString items)
		{
			writer.Write((ushort)items.Count);
			foreach (string value in items)
			{
				writer.Write(value);
			}
		}
	}
}
