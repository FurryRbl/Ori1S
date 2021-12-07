using System;

namespace Mono.Security.Protocol.Tls.Handshake
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	internal enum ClientCertificateType
	{
		// Token: 0x04000306 RID: 774
		RSA = 1,
		// Token: 0x04000307 RID: 775
		DSS,
		// Token: 0x04000308 RID: 776
		RSAFixed,
		// Token: 0x04000309 RID: 777
		DSSFixed,
		// Token: 0x0400030A RID: 778
		Unknown = 255
	}
}
