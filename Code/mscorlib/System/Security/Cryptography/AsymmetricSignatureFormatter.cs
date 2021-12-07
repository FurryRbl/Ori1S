using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of asymmetric signature formatters derive.</summary>
	// Token: 0x02000598 RID: 1432
	[ComVisible(true)]
	public abstract class AsymmetricSignatureFormatter
	{
		/// <summary>When overridden in a derived class, sets the hash algorithm to use for creating the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for creating the signature. </param>
		// Token: 0x0600374F RID: 14159
		public abstract void SetHashAlgorithm(string strName);

		/// <summary>When overridden in a derived class, sets the asymmetric algorithm to use to create the signature.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> to use to create the signature. </param>
		// Token: 0x06003750 RID: 14160
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, creates the signature for the specified data.</summary>
		/// <returns>The digital signature for the <paramref name="rgbHash" /> parameter.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		// Token: 0x06003751 RID: 14161
		public abstract byte[] CreateSignature(byte[] rgbHash);

		/// <summary>Creates the signature from the specified hash value.</summary>
		/// <returns>The signature for the specified hash value.</returns>
		/// <param name="hash">The hash algorithm to use to create the signature. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is null. </exception>
		// Token: 0x06003752 RID: 14162 RVA: 0x000B2ACC File Offset: 0x000B0CCC
		public virtual byte[] CreateSignature(HashAlgorithm hash)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.CreateSignature(hash.Hash);
		}
	}
}
