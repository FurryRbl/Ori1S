using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the standard parameters for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
	// Token: 0x020005D4 RID: 1492
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		/// <summary>Represents the P parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018BE RID: 6334
		[NonSerialized]
		public byte[] P;

		/// <summary>Represents the Q parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018BF RID: 6335
		[NonSerialized]
		public byte[] Q;

		/// <summary>Represents the D parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018C0 RID: 6336
		[NonSerialized]
		public byte[] D;

		/// <summary>Represents the DP parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018C1 RID: 6337
		[NonSerialized]
		public byte[] DP;

		/// <summary>Represents the DQ parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018C2 RID: 6338
		[NonSerialized]
		public byte[] DQ;

		/// <summary>Represents the InverseQ parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018C3 RID: 6339
		[NonSerialized]
		public byte[] InverseQ;

		/// <summary>Represents the Modulus parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018C4 RID: 6340
		public byte[] Modulus;

		/// <summary>Represents the Exponent parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040018C5 RID: 6341
		public byte[] Exponent;
	}
}
