using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used for generating message authentication codes (MACs).</summary>
	// Token: 0x0200042D RID: 1069
	public enum HashAlgorithmType
	{
		/// <summary>No hashing algorithm is used.</summary>
		// Token: 0x040017AF RID: 6063
		None,
		/// <summary>The Message Digest 5 (MD5) hashing algorithm.</summary>
		// Token: 0x040017B0 RID: 6064
		Md5 = 32771,
		/// <summary>The Secure Hashing Algorithm (SHA1).</summary>
		// Token: 0x040017B1 RID: 6065
		Sha1
	}
}
