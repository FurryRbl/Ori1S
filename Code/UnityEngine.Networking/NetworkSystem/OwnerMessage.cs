using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000028 RID: 40
	internal class OwnerMessage : MessageBase
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00005104 File Offset: 0x00003304
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.playerControllerId = (short)reader.ReadPackedUInt32();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005120 File Offset: 0x00003320
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			writer.WritePackedUInt32((uint)this.playerControllerId);
		}

		// Token: 0x04000075 RID: 117
		public NetworkInstanceId netId;

		// Token: 0x04000076 RID: 118
		public short playerControllerId;
	}
}
