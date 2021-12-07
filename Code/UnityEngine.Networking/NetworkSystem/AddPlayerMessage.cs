using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200001D RID: 29
	public class AddPlayerMessage : MessageBase
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004B4C File Offset: 0x00002D4C
		public override void Deserialize(NetworkReader reader)
		{
			this.playerControllerId = (short)reader.ReadUInt16();
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

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B98 File Offset: 0x00002D98
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write((ushort)this.playerControllerId);
			writer.WriteBytesAndSize(this.msgData, this.msgSize);
		}

		// Token: 0x04000055 RID: 85
		public short playerControllerId;

		// Token: 0x04000056 RID: 86
		public int msgSize;

		// Token: 0x04000057 RID: 87
		public byte[] msgData;
	}
}
