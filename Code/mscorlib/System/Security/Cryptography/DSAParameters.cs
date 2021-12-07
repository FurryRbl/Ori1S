using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains the typical parameters for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
	// Token: 0x020005AB RID: 1451
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		/// <summary>Specifies the counter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04001845 RID: 6213
		public int Counter;

		/// <summary>Specifies the G parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04001846 RID: 6214
		public byte[] G;

		/// <summary>Specifies the J parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04001847 RID: 6215
		public byte[] J;

		/// <summary>Specifies the P parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04001848 RID: 6216
		public byte[] P;

		/// <summary>Specifies the Q parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04001849 RID: 6217
		public byte[] Q;

		/// <summary>Specifies the seed for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400184A RID: 6218
		public byte[] Seed;

		/// <summary>Specifies the X parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400184B RID: 6219
		[NonSerialized]
		public byte[] X;

		/// <summary>Specifies the Y parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400184C RID: 6220
		public byte[] Y;
	}
}
