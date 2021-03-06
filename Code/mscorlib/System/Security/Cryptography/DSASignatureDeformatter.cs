using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Verifies a Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) PKCS#1 v1.5 signature.</summary>
	// Token: 0x020005AC RID: 1452
	[ComVisible(true)]
	public class DSASignatureDeformatter : AsymmetricSignatureDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class.</summary>
		// Token: 0x060037EA RID: 14314 RVA: 0x000B5978 File Offset: 0x000B3B78
		public DSASignatureDeformatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060037EB RID: 14315 RVA: 0x000B5980 File Offset: 0x000B3B80
		public DSASignatureDeformatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		/// <summary>Specifies the hash algorithm for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="strName">The name of the hash algorithm to use for the signature deformatter. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strName" /> parameter does not map to the <see cref="T:System.Security.Cryptography.SHA1" /> hash algorithm. </exception>
		// Token: 0x060037EC RID: 14316 RVA: 0x000B5990 File Offset: 0x000B3B90
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == null)
			{
				throw new ArgumentNullException("strName");
			}
			try
			{
				SHA1.Create(strName);
			}
			catch (InvalidCastException)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("DSA requires SHA1"));
			}
		}

		/// <summary>Specifies the key to be used for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.DSA" /> that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060037ED RID: 14317 RVA: 0x000B59EC File Offset: 0x000B3BEC
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key != null)
			{
				this.dsa = (DSA)key;
				return;
			}
			throw new ArgumentNullException("key");
		}

		/// <summary>Verifies the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature on the data.</summary>
		/// <returns>true if the signature is valid for the data; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgbHash" /> is null.-or-<paramref name="rgbSignature" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The DSA key is missing.</exception>
		// Token: 0x060037EE RID: 14318 RVA: 0x000B5A1C File Offset: 0x000B3C1C
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this.dsa == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("missing key"));
			}
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x0400184D RID: 6221
		private DSA dsa;
	}
}
