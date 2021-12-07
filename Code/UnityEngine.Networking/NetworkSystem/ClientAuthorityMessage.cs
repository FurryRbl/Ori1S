using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000029 RID: 41
	internal class ClientAuthorityMessage : MessageBase
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00005144 File Offset: 0x00003344
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.authority = reader.ReadBoolean();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005160 File Offset: 0x00003360
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			writer.Write(this.authority);
		}

		// Token: 0x04000077 RID: 119
		public NetworkInstanceId netId;

		// Token: 0x04000078 RID: 120
		public bool authority;
	}
}
