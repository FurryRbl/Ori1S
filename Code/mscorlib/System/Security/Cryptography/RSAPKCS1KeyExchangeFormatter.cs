using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Creates the PKCS#1 key exchange data using <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	// Token: 0x020005D6 RID: 1494
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> class.</summary>
		// Token: 0x06003930 RID: 14640 RVA: 0x000C3550 File Offset: 0x000C1750
		public RSAPKCS1KeyExchangeFormatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x06003931 RID: 14641 RVA: 0x000C3558 File Offset: 0x000C1758
		public RSAPKCS1KeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			this.SetRSAKey(key);
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x000C3568 File Offset: 0x000C1768
		// (set) Token: 0x06003933 RID: 14643 RVA: 0x000C3570 File Offset: 0x000C1770
		public RandomNumberGenerator Rng
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

		/// <summary>Gets the parameters for the PKCS #1 key exchange.</summary>
		/// <returns>An XML string containing the parameters of the PKCS #1 key exchange operation.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x000C357C File Offset: 0x000C177C
		public override string Parameters
		{
			get
			{
				return "<enc:KeyEncryptionMethod enc:Algorithm=\"http://www.microsoft.com/xml/security/algorithm/PKCS1-v1.5-KeyEx\" xmlns:enc=\"http://www.microsoft.com/xml/security/encryption/v1.0\" />";
			}
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="rgbData">The secret information to be passed in the key exchange. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="rgbData " />is too big.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003935 RID: 14645 RVA: 0x000C3584 File Offset: 0x000C1784
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (rgbData == null)
			{
				throw new ArgumentNullException("rgbData");
			}
			if (this.rsa == null)
			{
				string text = Locale.GetText("No RSA key specified");
				throw new CryptographicUnexpectedOperationException(text);
			}
			if (this.random == null)
			{
				this.random = RandomNumberGenerator.Create();
			}
			return PKCS1.Encrypt_v15(this.rsa, this.random, rgbData);
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="rgbData">The secret information to be passed in the key exchange. </param>
		/// <param name="symAlgType">This parameter is not used in the current version. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003936 RID: 14646 RVA: 0x000C35E8 File Offset: 0x000C17E8
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000C35F4 File Offset: 0x000C17F4
		private void SetRSAKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.rsa = (RSA)key;
		}

		/// <summary>Sets the public key to use for encrypting the key exchange data.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x06003938 RID: 14648 RVA: 0x000C3614 File Offset: 0x000C1814
		public override void SetKey(AsymmetricAlgorithm key)
		{
			this.SetRSAKey(key);
		}

		// Token: 0x040018C8 RID: 6344
		private RSA rsa;

		// Token: 0x040018C9 RID: 6345
		private RandomNumberGenerator random;
	}
}
