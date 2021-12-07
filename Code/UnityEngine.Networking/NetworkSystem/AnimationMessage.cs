using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200002B RID: 43
	internal class AnimationMessage : MessageBase
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000520C File Offset: 0x0000340C
		public override void Deserialize(NetworkReader reader)
		{
			this.netId = reader.ReadNetworkId();
			this.stateHash = (int)reader.ReadPackedUInt32();
			this.normalizedTime = reader.ReadSingle();
			this.parameters = reader.ReadBytesAndSize();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000524C File Offset: 0x0000344C
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.netId);
			writer.WritePackedUInt32((uint)this.stateHash);
			writer.Write(this.normalizedTime);
			if (this.parameters == null)
			{
				writer.WriteBytesAndSize(this.parameters, 0);
			}
			else
			{
				writer.WriteBytesAndSize(this.parameters, this.parameters.Length);
			}
		}

		// Token: 0x0400007D RID: 125
		public NetworkInstanceId netId;

		// Token: 0x0400007E RID: 126
		public int stateHash;

		// Token: 0x0400007F RID: 127
		public float normalizedTime;

		// Token: 0x04000080 RID: 128
		public byte[] parameters;
	}
}
