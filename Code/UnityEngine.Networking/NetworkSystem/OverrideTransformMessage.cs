using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200002A RID: 42
	internal class OverrideTransformMessage : MessageBase
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00005184 File Offset: 0x00003384
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.payload = reader.ReadBytesAndSize();
			this.teleport = reader.ReadBoolean();
			this.time = (int)reader.ReadPackedUInt32();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000051C4 File Offset: 0x000033C4
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			writer.WriteBytesFull(this.payload);
			writer.Write(this.teleport);
			writer.WritePackedUInt32((uint)this.time);
		}

		// Token: 0x04000079 RID: 121
		public NetworkInstanceId netId;

		// Token: 0x0400007A RID: 122
		public byte[] payload;

		// Token: 0x0400007B RID: 123
		public bool teleport;

		// Token: 0x0400007C RID: 124
		public int time;
	}
}
