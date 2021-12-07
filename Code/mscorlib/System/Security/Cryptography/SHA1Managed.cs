using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash for the input data using the managed library. </summary>
	// Token: 0x020005DC RID: 1500
	[ComVisible(true)]
	public class SHA1Managed : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1Managed" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">This class is not compliant with the FIPS algorithm.</exception>
		// Token: 0x06003955 RID: 14677 RVA: 0x000C43D0 File Offset: 0x000C25D0
		public SHA1Managed()
		{
			this.sha = new SHA1Internal();
		}

		/// <summary>Routes data written to the object into the <see cref="T:System.Security.Cryptography.SHA1Managed" /> hash algorithm for computing the hash.</summary>
		/// <param name="rgb">The input data. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the array to use as data. </param>
		// Token: 0x06003956 RID: 14678 RVA: 0x000C43E4 File Offset: 0x000C25E4
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this.State = 1;
			this.sha.HashCore(rgb, ibStart, cbSize);
		}

		/// <summary>Returns the computed <see cref="T:System.Security.Cryptography.SHA1" /> hash value after all data has been written to the object.</summary>
		/// <returns>The computed hash code.</returns>
		// Token: 0x06003957 RID: 14679 RVA: 0x000C43FC File Offset: 0x000C25FC
		protected override byte[] HashFinal()
		{
			this.State = 0;
			return this.sha.HashFinal();
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA1Managed" />.</summary>
		// Token: 0x06003958 RID: 14680 RVA: 0x000C4410 File Offset: 0x000C2610
		public override void Initialize()
		{
			this.sha.Initialize();
		}

		// Token: 0x040018D6 RID: 6358
		private SHA1Internal sha;
	}
}
