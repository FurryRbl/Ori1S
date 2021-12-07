using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000026 RID: 38
	internal class ObjectSpawnFinishedMessage : MessageBase
	{
		// Token: 0x060000BD RID: 189 RVA: 0x000050B4 File Offset: 0x000032B4
		public override void Deserialize(NetworkReader reader)
		{
			this.state = reader.ReadPackedUInt32();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000050C4 File Offset: 0x000032C4
		public override void Serialize(NetworkWriter writer)
		{
			writer.WritePackedUInt32(this.state);
		}

		// Token: 0x04000073 RID: 115
		public uint state;
	}
}
