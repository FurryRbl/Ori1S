﻿using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the type of hash algorithm to use with the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class.</summary>
	// Token: 0x02000459 RID: 1113
	public enum X509SubjectKeyIdentifierHashAlgorithm
	{
		/// <summary>The SKI is composed of the 160-bit SHA-1 hash of the value of the public key (excluding the tag, length, and number of unused bits).</summary>
		// Token: 0x040018A7 RID: 6311
		Sha1,
		/// <summary>The SKI is composed of a four-bit type field with the value 0100, followed by the least significant 60 bits of the SHA-1 hash of the value of the public key (excluding the tag, length, and number of unused bit string bits)</summary>
		// Token: 0x040018A8 RID: 6312
		ShortSha1,
		/// <summary>The subject key identifier (SKI) is composed of a 160-bit SHA-1 hash of the encoded public key (including the tag, length, and number of unused bits).</summary>
		// Token: 0x040018A9 RID: 6313
		CapiSha1
	}
}
