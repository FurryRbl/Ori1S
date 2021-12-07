using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000025 RID: 37
	internal class ObjectSpawnSceneMessage : MessageBase
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000502C File Offset: 0x0000322C
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.sceneId = reader.ReadSceneId();
			this.position = reader.ReadVector3();
			this.payload = reader.ReadBytesAndSize();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000506C File Offset: 0x0000326C
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			writer.Write(this.sceneId);
			writer.Write(this.position);
			writer.WriteBytesFull(this.payload);
		}

		// Token: 0x0400006F RID: 111
		public NetworkInstanceId netId;

		// Token: 0x04000070 RID: 112
		public NetworkSceneId sceneId;

		// Token: 0x04000071 RID: 113
		public Vector3 position;

		// Token: 0x04000072 RID: 114
		public byte[] payload;
	}
}
