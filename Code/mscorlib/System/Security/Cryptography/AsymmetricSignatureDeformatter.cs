using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of asymmetric signature deformatters derive.</summary>
	// Token: 0x02000597 RID: 1431
	[ComVisible(true)]
	public abstract class AsymmetricSignatureDeformatter
	{
		/// <summary>When overridden in a derived class, sets the hash algorithm to use for verifying the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for verifying the signature. </param>
		// Token: 0x0600374A RID: 14154
		public abstract void SetHashAlgorithm(string strName);

		/// <summary>When overridden in a derived class, sets the public key to use for verifying the signature.</summary>
		/// <param name="key">The instance of an implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key. </param>
		// Token: 0x0600374B RID: 14155
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, verifies the signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />. </param>
		// Token: 0x0600374C RID: 14156
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);

		/// <summary>Verifies the signature from the specified hash value.</summary>
		/// <returns>true if the signature is valid for the hash; otherwise, false.</returns>
		/// <param name="hash">The hash algorithm to use to verify the signature. </param>
		/// <param name="rgbSignature">The signature to be verified. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is null. </exception>
		// Token: 0x0600374D RID: 14157 RVA: 0x000B2A8C File Offset: 0x000B0C8C
		public virtual bool VerifySignature(HashAlgorithm hash, byte[] rgbSignature)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.VerifySignature(hash.Hash, rgbSignature);
		}
	}
}
