using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000027 RID: 39
	internal class ObjectDestroyMessage : MessageBase
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000050DC File Offset: 0x000032DC
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000050EC File Offset: 0x000032EC
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
		}

		// Token: 0x04000074 RID: 116
		public NetworkInstanceId netId;
	}
}
