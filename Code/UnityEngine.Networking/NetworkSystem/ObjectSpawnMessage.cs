using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000024 RID: 36
	internal class ObjectSpawnMessage : MessageBase
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004FA4 File Offset: 0x000031A4
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.assetId = reader.ReadNetworkHash128();
			this.position = reader.ReadVector3();
			this.payload = reader.ReadBytesAndSize();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004FE4 File Offset: 0x000031E4
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			writer.Write(this.assetId);
			writer.Write(this.position);
			writer.WriteBytesFull(this.payload);
		}

		// Token: 0x0400006B RID: 107
		public NetworkInstanceId netId;

		// Token: 0x0400006C RID: 108
		public NetworkHash128 assetId;

		// Token: 0x0400006D RID: 109
		public Vector3 position;

		// Token: 0x0400006E RID: 110
		public byte[] payload;
	}
}
