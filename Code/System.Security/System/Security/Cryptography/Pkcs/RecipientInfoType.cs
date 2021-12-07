using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration defines the types of recipient information.</summary>
	// Token: 0x02000029 RID: 41
	public enum RecipientInfoType
	{
		/// <summary>The recipient information type is unknown.</summary>
		// Token: 0x04000085 RID: 133
		Unknown,
		/// <summary>Key transport recipient information.</summary>
		// Token: 0x04000086 RID: 134
		KeyTransport,
		/// <summary>Key agreement recipient information.</summary>
		// Token: 0x04000087 RID: 135
		KeyAgreement
	}
}
