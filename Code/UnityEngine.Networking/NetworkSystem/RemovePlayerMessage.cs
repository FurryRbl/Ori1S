using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200001E RID: 30
	public class RemovePlayerMessage : MessageBase
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public override void Deserialize(NetworkReader reader)
		{
			this.playerControllerId = (short)reader.ReadUInt16();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write((ushort)this.playerControllerId);
		}

		// Token: 0x04000058 RID: 88
		public short playerControllerId;
	}
}
