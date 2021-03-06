using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all mask generator algorithms must derive.</summary>
	// Token: 0x020005BE RID: 1470
	[ComVisible(true)]
	public abstract class MaskGenerationMethod
	{
		/// <summary>When overridden in a derived class, generates a mask with the specified length using the specified random seed.</summary>
		/// <returns>A randomly generated mask whose length is equal to the <paramref name="cbReturn" /> parameter.</returns>
		/// <param name="rgbSeed">The random seed to use to compute the mask. </param>
		/// <param name="cbReturn">The length of the generated mask in bytes. </param>
		// Token: 0x0600385C RID: 14428
		[ComVisible(true)]
		public abstract byte[] GenerateMask(byte[] rgbSeed, int cbReturn);
	}
}
