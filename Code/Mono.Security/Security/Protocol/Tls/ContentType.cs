using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	internal enum ContentType : byte
	{
		// Token: 0x0400025F RID: 607
		ChangeCipherSpec = 20,
		// Token: 0x04000260 RID: 608
		Alert,
		// Token: 0x04000261 RID: 609
		Handshake,
		// Token: 0x04000262 RID: 610
		ApplicationData
	}
}
