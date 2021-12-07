using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200005F RID: 95
	public sealed class SyncListFloat : SyncList<float>
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0001A074 File Offset: 0x00018274
		protected override void SerializeItem(NetworkWriter writer, float item)
		{
			writer.Write(item);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001A080 File Offset: 0x00018280
		protected override float DeserializeItem(NetworkReader reader)
		{
			return reader.ReadSingle();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001A088 File Offset: 0x00018288
		[Obsolete("ReadReference is now used instead")]
		public static SyncListFloat ReadInstance(NetworkReader reader)
		{
			ushort num = reader.ReadUInt16();
			SyncListFloat syncListFloat = new SyncListFloat();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncListFloat.AddInternal(reader.ReadSingle());
			}
			return syncListFloat;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001A0C4 File Offset: 0x000182C4
		public static void ReadReference(NetworkReader reader, SyncListFloat syncList)
		{
			ushort num = reader.ReadUInt16();
			syncList.Clear();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				syncList.AddInternal(reader.ReadSingle());
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001A100 File Offset: 0x00018300
		public static void WriteInstance(NetworkWriter writer, SyncListFloat items)
		{
			writer.Write((ushort)items.Count);
			foreach (float num in items)
			{
				float value = num;
				writer.Write(value);
			}
		}
	}
}
