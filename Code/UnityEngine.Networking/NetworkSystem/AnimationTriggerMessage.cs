using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200002D RID: 45
	internal class AnimationTriggerMessage : MessageBase
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x0000531C File Offset: 0x0000351C
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.hash = (int)reader.ReadPackedUInt32();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005338 File Offset: 0x00003538
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			writer.WritePackedUInt32((uint)this.hash);
		}

		// Token: 0x04000083 RID: 131
		public NetworkInstanceId netId;

		// Token: 0x04000084 RID: 132
		public int hash;
	}
}
