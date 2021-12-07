using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Computes masks according to PKCS #1 for use by key exchange algorithms.</summary>
	// Token: 0x020005C3 RID: 1475
	[ComVisible(true)]
	public class PKCS1MaskGenerationMethod : MaskGenerationMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PKCS1MaskGenerationMethod" /> class.</summary>
		// Token: 0x0600387E RID: 14462 RVA: 0x000B821C File Offset: 0x000B641C
		public PKCS1MaskGenerationMethod()
		{
			this.hashName = "SHA1";
		}

		/// <summary>Gets or sets the name of the hash algorithm type to use for generating the mask.</summary>
		/// <returns>The name of the type that implements the hash algorithm to use for computing the mask.</returns>
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000B8230 File Offset: 0x000B6430
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x000B8238 File Offset: 0x000B6438
		public string HashName
		{
			get
			{
				return this.hashName;
			}
			set
			{
				this.hashName = ((value != null) ? value : "SHA1");
			}
		}

		/// <summary>Generates and returns a mask from the specified random seed of the specified length.</summary>
		/// <returns>A randomly generated mask whose length is equal to the <paramref name="cbReturn" /> parameter.</returns>
		/// <param name="rgbSeed">The random seed to use for computing the mask. </param>
		/// <param name="cbReturn">The length of the generated mask in bytes. </param>
		// Token: 0x06003881 RID: 14465 RVA: 0x000B8254 File Offset: 0x000B6454
		public override byte[] GenerateMask(byte[] rgbSeed, int cbReturn)
		{
			HashAlgorithm hash = HashAlgorithm.Create(this.hashName);
			return PKCS1.MGF1(hash, rgbSeed, cbReturn);
		}

		// Token: 0x04001887 RID: 6279
		private string hashName;
	}
}
