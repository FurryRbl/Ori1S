using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	internal enum AlertDescription : byte
	{
		// Token: 0x0400021A RID: 538
		CloseNotify,
		// Token: 0x0400021B RID: 539
		UnexpectedMessage = 10,
		// Token: 0x0400021C RID: 540
		BadRecordMAC = 20,
		// Token: 0x0400021D RID: 541
		DecryptionFailed,
		// Token: 0x0400021E RID: 542
		RecordOverflow,
		// Token: 0x0400021F RID: 543
		DecompressionFailiure = 30,
		// Token: 0x04000220 RID: 544
		HandshakeFailiure = 40,
		// Token: 0x04000221 RID: 545
		NoCertificate,
		// Token: 0x04000222 RID: 546
		BadCertificate,
		// Token: 0x04000223 RID: 547
		UnsupportedCertificate,
		// Token: 0x04000224 RID: 548
		CertificateRevoked,
		// Token: 0x04000225 RID: 549
		CertificateExpired,
		// Token: 0x04000226 RID: 550
		CertificateUnknown,
		// Token: 0x04000227 RID: 551
		IlegalParameter,
		// Token: 0x04000228 RID: 552
		UnknownCA,
		// Token: 0x04000229 RID: 553
		AccessDenied,
		// Token: 0x0400022A RID: 554
		DecodeError,
		// Token: 0x0400022B RID: 555
		DecryptError,
		// Token: 0x0400022C RID: 556
		ExportRestriction = 60,
		// Token: 0x0400022D RID: 557
		ProtocolVersion = 70,
		// Token: 0x0400022E RID: 558
		InsuficientSecurity,
		// Token: 0x0400022F RID: 559
		InternalError = 80,
		// Token: 0x04000230 RID: 560
		UserCancelled = 90,
		// Token: 0x04000231 RID: 561
		NoRenegotiation = 100
	}
}
