using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000030 RID: 48
	internal class CRCMessage : MessageBase
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x0000539C File Offset: 0x0000359C
		public override void Deserialize(NetworkReader reader)
		{
			int num = (int)reader.ReadUInt16();
			this.scripts = new CRCMessageEntry[num];
			for (int i = 0; i < this.scripts.Length; i++)
			{
				CRCMessageEntry crcmessageEntry = default(CRCMessageEntry);
				crcmessageEntry.name = reader.ReadString();
				crcmessageEntry.channel = reader.ReadByte();
				this.scripts[i] = crcmessageEntry;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000540C File Offset: 0x0000360C
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write((ushort)this.scripts.Length);
			foreach (CRCMessageEntry crcmessageEntry in this.scripts)
			{
				writer.Write(crcmessageEntry.name);
				writer.Write(crcmessageEntry.channel);
			}
		}

		// Token: 0x04000089 RID: 137
		public CRCMessageEntry[] scripts;
	}
}
