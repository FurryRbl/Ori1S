using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200002E RID: 46
	internal class LobbyReadyToBeginMessage : MessageBase
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x0000535C File Offset: 0x0000355C
		public override void Deserialize(NetworkReader reader)
		{
			this.slotId = reader.ReadByte();
			this.readyState = reader.ReadBoolean();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005378 File Offset: 0x00003578
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.slotId);
			writer.Write(this.readyState);
		}

		// Token: 0x04000085 RID: 133
		public byte slotId;

		// Token: 0x04000086 RID: 134
		public bool readyState;
	}
}
