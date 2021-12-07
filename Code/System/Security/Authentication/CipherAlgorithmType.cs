using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible cipher algorithms for the <see cref="T:System.Net.Security.SslStream" /> class.</summary>
	// Token: 0x0200042B RID: 1067
	public enum CipherAlgorithmType
	{
		/// <summary>No encryption algorithm is used.</summary>
		// Token: 0x040017A0 RID: 6048
		None,
		/// <summary>The Advanced Encryption Standard (AES) algorithm.</summary>
		// Token: 0x040017A1 RID: 6049
		Aes = 26129,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 128 bit key.</summary>
		// Token: 0x040017A2 RID: 6050
		Aes128 = 26126,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 192 bit key.</summary>
		// Token: 0x040017A3 RID: 6051
		Aes192,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 256 bit key.</summary>
		// Token: 0x040017A4 RID: 6052
		Aes256,
		/// <summary>The Data Encryption Standard (DES) algorithm.</summary>
		// Token: 0x040017A5 RID: 6053
		Des = 26113,
		/// <summary>Rivest's Code 2 (RC2) algorithm.</summary>
		// Token: 0x040017A6 RID: 6054
		Rc2,
		/// <summary>Rivest's Code 4 (RC4) algorithm.</summary>
		// Token: 0x040017A7 RID: 6055
		Rc4 = 26625,
		/// <summary>The Triple Data Encryption Standard (3DES) algorithm.</summary>
		// Token: 0x040017A8 RID: 6056
		TripleDes = 26115
	}
}
