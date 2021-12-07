using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200002C RID: 44
	internal class AnimationParametersMessage : MessageBase
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000052B8 File Offset: 0x000034B8
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.parameters = reader.ReadBytesAndSize();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000052D4 File Offset: 0x000034D4
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			if (this.parameters == null)
			{
				writer.WriteBytesAndSize(this.parameters, 0);
			}
			else
			{
				writer.WriteBytesAndSize(this.parameters, this.parameters.Length);
			}
		}

		// Token: 0x04000081 RID: 129
		public NetworkInstanceId netId;

		// Token: 0x04000082 RID: 130
		public byte[] parameters;
	}
}
