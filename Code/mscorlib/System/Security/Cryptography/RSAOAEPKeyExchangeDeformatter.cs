using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Decrypts Optimal Asymmetric Encryption Padding (OAEP) key exchange data.</summary>
	// Token: 0x020005D2 RID: 1490
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" /> class.</summary>
		// Token: 0x06003918 RID: 14616 RVA: 0x000C3374 File Offset: 0x000C1574
		public RSAOAEPKeyExchangeDeformatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x06003919 RID: 14617 RVA: 0x000C337C File Offset: 0x000C157C
		public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		/// <summary>Gets the parameters for the Optimal Asymmetric Encryption Padding (OAEP) key exchange.</summary>
		/// <returns>An XML string containing the parameters of the OAEP key exchange operation.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x0600391A RID: 14618 RVA: 0x000C338C File Offset: 0x000C158C
		// (set) Token: 0x0600391B RID: 14619 RVA: 0x000C3390 File Offset: 0x000C1590
		public override string Parameters
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>Extracts secret information from the encrypted key exchange data.</summary>
		/// <returns>The secret information derived from the key exchange data.</returns>
		/// <param name="rgbData">The key exchange data within which the secret information is hidden. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key exchange data verification has failed. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600391C RID: 14620 RVA: 0x000C3394 File Offset: 0x000C1594
		public override byte[] DecryptKeyExchange(byte[] rgbData)
		{
			if (this.rsa == null)
			{
				string text = Locale.GetText("No RSA key specified");
				throw new CryptographicUnexpectedOperationException(text);
			}
			SHA1 hash = SHA1.Create();
			byte[] array = PKCS1.Decrypt_OAEP(this.rsa, hash, rgbData);
			if (array != null)
			{
				return array;
			}
			throw new CryptographicException(Locale.GetText("OAEP decoding error."));
		}

		/// <summary>Sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600391D RID: 14621 RVA: 0x000C33EC File Offset: 0x000C15EC
		public override void SetKey(AsymmetricAlgorithm key)
		{
			this.rsa = (RSA)key;
		}

		// Token: 0x040018BA RID: 6330
		private RSA rsa;
	}
}
