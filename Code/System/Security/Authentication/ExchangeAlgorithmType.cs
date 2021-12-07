using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used to create keys shared by the client and server.</summary>
	// Token: 0x0200042C RID: 1068
	public enum ExchangeAlgorithmType
	{
		/// <summary>No key exchange algorithm is used.</summary>
		// Token: 0x040017AA RID: 6058
		None,
		/// <summary>The Diffie Hellman ephemeral key exchange algorithm.</summary>
		// Token: 0x040017AB RID: 6059
		DiffieHellman = 43522,
		/// <summary>The RSA public-key exchange algorithm.</summary>
		// Token: 0x040017AC RID: 6060
		RsaKeyX = 41984,
		/// <summary>The RSA public-key signature algorithm.</summary>
		// Token: 0x040017AD RID: 6061
		RsaSign = 9216
	}
}
