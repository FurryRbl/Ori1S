using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines how the certificate key can be used. If this value is not defined, the key can be used for any purpose.</summary>
	// Token: 0x02000453 RID: 1107
	[Flags]
	public enum X509KeyUsageFlags
	{
		/// <summary>No key usage parameters.</summary>
		// Token: 0x04001882 RID: 6274
		None = 0,
		/// <summary>The key can be used for encryption only.</summary>
		// Token: 0x04001883 RID: 6275
		EncipherOnly = 1,
		/// <summary>The key can be used to sign a certificate revocation list (CRL).</summary>
		// Token: 0x04001884 RID: 6276
		CrlSign = 2,
		/// <summary>The key can be used to sign certificates.</summary>
		// Token: 0x04001885 RID: 6277
		KeyCertSign = 4,
		/// <summary>The key can be used to determine key agreement, such as a key created using the Diffie-Hellman key agreement algorithm.</summary>
		// Token: 0x04001886 RID: 6278
		KeyAgreement = 8,
		/// <summary>The key can be used for data encryption.</summary>
		// Token: 0x04001887 RID: 6279
		DataEncipherment = 16,
		/// <summary>The key can be used for key encryption.</summary>
		// Token: 0x04001888 RID: 6280
		KeyEncipherment = 32,
		/// <summary>The key can be used for authentication.</summary>
		// Token: 0x04001889 RID: 6281
		NonRepudiation = 64,
		/// <summary>The key can be used as a digital signature.</summary>
		// Token: 0x0400188A RID: 6282
		DigitalSignature = 128,
		/// <summary>The key can be used for decryption only.</summary>
		// Token: 0x0400188B RID: 6283
		DecipherOnly = 32768
	}
}
