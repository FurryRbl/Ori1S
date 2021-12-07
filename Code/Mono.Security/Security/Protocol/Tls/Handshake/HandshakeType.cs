using System;

namespace Mono.Security.Protocol.Tls.Handshake
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	internal enum HandshakeType : byte
	{
		// Token: 0x04000310 RID: 784
		HelloRequest,
		// Token: 0x04000311 RID: 785
		ClientHello,
		// Token: 0x04000312 RID: 786
		ServerHello,
		// Token: 0x04000313 RID: 787
		Certificate = 11,
		// Token: 0x04000314 RID: 788
		ServerKeyExchange,
		// Token: 0x04000315 RID: 789
		CertificateRequest,
		// Token: 0x04000316 RID: 790
		ServerHelloDone,
		// Token: 0x04000317 RID: 791
		CertificateVerify,
		// Token: 0x04000318 RID: 792
		ClientKeyExchange,
		// Token: 0x04000319 RID: 793
		Finished = 20,
		// Token: 0x0400031A RID: 794
		None = 255
	}
}
