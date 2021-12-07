using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000017 RID: 23
	public class StringMessage : MessageBase
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00004A8C File Offset: 0x00002C8C
		public StringMessage()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004A94 File Offset: 0x00002C94
		public StringMessage(string v)
		{
			this.value = v;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public override void Deserialize(NetworkReader reader)
		{
			this.value = reader.ReadString();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(this.value);
		}

		// Token: 0x04000052 RID: 82
		public string value;
	}
}
