using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000067 RID: 103
	public class NetworkMessage
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0001AA48 File Offset: 0x00018C48
		public static string Dump(byte[] payload, int sz)
		{
			string text = "[";
			for (int i = 0; i < sz; i++)
			{
				text = text + payload[i] + " ";
			}
			return text + "]";
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001AA90 File Offset: 0x00018C90
		public TMsg ReadMessage<TMsg>() where TMsg : MessageBase, new()
		{
			TMsg result = Activator.CreateInstance<TMsg>();
			result.Deserialize(this.reader);
			return result;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		public void ReadMessage<TMsg>(TMsg msg) where TMsg : MessageBase
		{
			msg.Deserialize(this.reader);
		}

		// Token: 0x04000238 RID: 568
		public const int MaxMessageSize = 65535;

		// Token: 0x04000239 RID: 569
		public short msgType;

		// Token: 0x0400023A RID: 570
		public NetworkConnection conn;

		// Token: 0x0400023B RID: 571
		public NetworkReader reader;

		// Token: 0x0400023C RID: 572
		public int channelId;
	}
}
