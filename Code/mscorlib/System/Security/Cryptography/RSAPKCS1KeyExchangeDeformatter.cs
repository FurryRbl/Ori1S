using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Decrypts the PKCS #1 key exchange data.</summary>
	// Token: 0x020005D5 RID: 1493
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> class.</summary>
		// Token: 0x06003928 RID: 14632 RVA: 0x000C34C0 File Offset: 0x000C16C0
		public RSAPKCS1KeyExchangeDeformatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003929 RID: 14633 RVA: 0x000C34C8 File Offset: 0x000C16C8
		public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		/// <summary>Gets the parameters for the PKCS #1 key exchange.</summary>
		/// <returns>An XML string containing the parameters of the PKCS #1 key exchange operation.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000C34D8 File Offset: 0x000C16D8
		// (set) Token: 0x0600392B RID: 14635 RVA: 0x000C34DC File Offset: 0x000C16DC
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

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000C34E0 File Offset: 0x000C16E0
		// (set) Token: 0x0600392D RID: 14637 RVA: 0x000C34E8 File Offset: 0x000C16E8
		public RandomNumberGenerator RNG
		{
			get
			{
				return this.random;
			}
			set
			{
				this.random = value;
			}
		}

		/// <summary>Extracts secret information from the encrypted key exchange data.</summary>
		/// <returns>The secret information derived from the key exchange data.</returns>
		/// <param name="rgbIn">The key exchange data within which the secret information is hidden. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600392E RID: 14638 RVA: 0x000C34F4 File Offset: 0x000C16F4
		public override byte[] DecryptKeyExchange(byte[] rgbIn)
		{
			if (this.rsa == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("No key pair available."));
			}
			byte[] array = PKCS1.Decrypt_v15(this.rsa, rgbIn);
			if (array != null)
			{
				return array;
			}
			throw new CryptographicException(Locale.GetText("PKCS1 decoding error."));
		}

		/// <summary>Sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600392F RID: 14639 RVA: 0x000C3540 File Offset: 0x000C1740
		public override void SetKey(AsymmetricAlgorithm key)
		{
			this.rsa = (RSA)key;
		}

		// Token: 0x040018C6 RID: 6342
		private RSA rsa;

		// Token: 0x040018C7 RID: 6343
		private RandomNumberGenerator random;
	}
}
