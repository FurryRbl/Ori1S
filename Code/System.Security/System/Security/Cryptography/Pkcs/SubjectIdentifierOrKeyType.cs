using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" /> enumeration defines how a subject is identified.</summary>
	// Token: 0x02000030 RID: 48
	public enum SubjectIdentifierOrKeyType
	{
		/// <summary>The type is unknown.</summary>
		// Token: 0x0400009C RID: 156
		Unknown,
		/// <summary>The subject is identified by the certificate issuer and serial number.</summary>
		// Token: 0x0400009D RID: 157
		IssuerAndSerialNumber,
		/// <summary>The subject is identified by the hash of the subject key.</summary>
		// Token: 0x0400009E RID: 158
		SubjectKeyIdentifier,
		/// <summary>The subject is identified by the public key.</summary>
		// Token: 0x0400009F RID: 159
		PublicKeyInfo
	}
}
