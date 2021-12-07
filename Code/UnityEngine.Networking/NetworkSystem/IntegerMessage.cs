using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000018 RID: 24
	public class IntegerMessage : MessageBase
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public IntegerMessage()
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004ACC File Offset: 0x00002CCC
		public IntegerMessage(int v)
		{
			this.value = v;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004ADC File Offset: 0x00002CDC
		public override void Deserialize(NetworkReader reader)
		{
			this.value = (int)reader.ReadPackedUInt32();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004AEC File Offset: 0x00002CEC
		public override void Serialize(NetworkWriter writer)
		{
			writer.WritePackedUInt32((uint)this.value);
		}

		// Token: 0x04000053 RID: 83
		public int value;
	}
}
