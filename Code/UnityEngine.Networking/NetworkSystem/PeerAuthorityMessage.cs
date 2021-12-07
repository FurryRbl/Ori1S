using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200001F RID: 31
	public class PeerAuthorityMessage : MessageBase
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004BEC File Offset: 0x00002DEC
		public override void Deserialize(NetworkReader reader)
		{
			this.connectionId = (int)reader.ReadPackedUInt32();
			this.netId = reader.ReadNetworkId();
			this.authorityState = reader.ReadBoolean();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004C20 File Offset: 0x00002E20
		public override void Serialize(NetworkWriter writer)
		{
			writer.WritePackedUInt32((uint)this.connectionId);
			writer.Write(this.netId);
			writer.Write(this.authorityState);
		}

		// Token: 0x04000059 RID: 89
		public int connectionId;

		// Token: 0x0400005A RID: 90
		public NetworkInstanceId netId;

		// Token: 0x0400005B RID: 91
		public bool authorityState;
	}
}
