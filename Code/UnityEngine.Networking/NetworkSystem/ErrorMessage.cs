using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x0200001A RID: 26
	public class ErrorMessage : MessageBase
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00004B14 File Offset: 0x00002D14
		public override void Deserialize(NetworkReader reader)
		{
			this.errorCode = (int)reader.ReadUInt16();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004B24 File Offset: 0x00002D24
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write((ushort)this.errorCode);
		}

		// Token: 0x04000054 RID: 84
		public int errorCode;
	}
}
