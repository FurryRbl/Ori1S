using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000023 RID: 35
	public class ReconnectMessage : MessageBase
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004EF4 File Offset: 0x000030F4
		public override void Deserialize(NetworkReader reader)
		{
			this.oldConnectionId = (int)reader.ReadPackedUInt32();
			this.playerControllerId = (short)reader.ReadPackedUInt32();
			this.netId = reader.ReadNetworkId();
			this.msgData = reader.ReadBytesAndSize();
			if (this.msgData == null)
			{
				this.msgSize = 0;
			}
			else
			{
				this.msgSize = this.msgData.Length;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004F58 File Offset: 0x00003158
		public override void Serialize(NetworkWriter writer)
		{
			writer.WritePackedUInt32((uint)this.oldConnectionId);
			writer.WritePackedUInt32((uint)this.playerControllerId);
			writer.Write(this.netId);
			writer.WriteBytesAndSize(this.msgData, this.msgSize);
		}

		// Token: 0x04000066 RID: 102
		public int oldConnectionId;

		// Token: 0x04000067 RID: 103
		public short playerControllerId;

		// Token: 0x04000068 RID: 104
		public NetworkInstanceId netId;

		// Token: 0x04000069 RID: 105
		public int msgSize;

		// Token: 0x0400006A RID: 106
		public byte[] msgData;
	}
}
