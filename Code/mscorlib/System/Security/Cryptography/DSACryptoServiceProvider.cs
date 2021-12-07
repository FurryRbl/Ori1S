using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.DSA" /> algorithm. This class cannot be inherited. </summary>
	// Token: 0x020005AA RID: 1450
	[ComVisible(true)]
	public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class.</summary>
		// Token: 0x060037CD RID: 14285 RVA: 0x000B54C4 File Offset: 0x000B36C4
		public DSACryptoServiceProvider() : this(1024, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="parameters">The parameters for the CSP. </param>
		// Token: 0x060037CE RID: 14286 RVA: 0x000B54D4 File Offset: 0x000B36D4
		public DSACryptoServiceProvider(CspParameters parameters) : this(1024, parameters)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key for the asymmetric algorithm in bits. </param>
		// Token: 0x060037CF RID: 14287 RVA: 0x000B54E4 File Offset: 0x000B36E4
		public DSACryptoServiceProvider(int dwKeySize) : this(dwKeySize, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size and parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="dwKeySize">The size of the key for the cryptographic algorithm in bits. </param>
		/// <param name="parameters">The parameters for the CSP. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.-or- The key cannot be created. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dwKeySize" /> is out of range.</exception>
		// Token: 0x060037D0 RID: 14288 RVA: 0x000B54F0 File Offset: 0x000B36F0
		public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(512, 1024, 64);
			this.KeySize = dwKeySize;
			this.dsa = new DSAManaged(dwKeySize);
			this.dsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = (parameters != null);
			if (parameters == null)
			{
				parameters = new CspParameters(13);
				if (DSACryptoServiceProvider.useMachineKeyStore)
				{
					parameters.Flags |= CspProviderFlags.UseMachineKeyStore;
				}
				this.store = new KeyPairPersistence(parameters);
			}
			else
			{
				this.store = new KeyPairPersistence(parameters);
				this.store.Load();
				if (this.store.KeyValue != null)
				{
					this.persisted = true;
					this.FromXmlString(this.store.KeyValue);
				}
			}
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000B55E4 File Offset: 0x000B37E4
		~DSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>The name of the key exchange algorithm.</returns>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000B5620 File Offset: 0x000B3820
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the size of the key used by the asymmetric algorithm in bits.</summary>
		/// <returns>The size of the key used by the asymmetric algorithm.</returns>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000B5624 File Offset: 0x000B3824
		public override int KeySize
		{
			get
			{
				return this.dsa.KeySize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the cryptographic service provider (CSP).</summary>
		/// <returns>true if the key should be persisted in the CSP; otherwise, false.</returns>
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000B5634 File Offset: 0x000B3834
		// (set) Token: 0x060037D6 RID: 14294 RVA: 0x000B563C File Offset: 0x000B383C
		public bool PersistKeyInCsp
		{
			get
			{
				return this.persistKey;
			}
			set
			{
				this.persistKey = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key; otherwise, false.</returns>
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000B5648 File Offset: 0x000B3848
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.dsa.PublicOnly;
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000B5658 File Offset: 0x000B3858
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
		/// <returns>true if the key should be persisted in the computer key store; otherwise, false.</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x000B5660 File Offset: 0x000B3860
		// (set) Token: 0x060037DA RID: 14298 RVA: 0x000B5668 File Offset: 0x000B3868
		public static bool UseMachineKeyStore
		{
			get
			{
				return DSACryptoServiceProvider.useMachineKeyStore;
			}
			set
			{
				DSACryptoServiceProvider.useMachineKeyStore = value;
			}
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037DB RID: 14299 RVA: 0x000B5670 File Offset: 0x000B3870
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException(Locale.GetText("Cannot export private key"));
			}
			return this.dsa.ExportParameters(includePrivateParameters);
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.DSA" />. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="parameters" /> parameter has missing fields. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037DC RID: 14300 RVA: 0x000B56A0 File Offset: 0x000B38A0
		public override void ImportParameters(DSAParameters parameters)
		{
			this.dsa.ImportParameters(parameters);
		}

		/// <summary>Creates the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037DD RID: 14301 RVA: 0x000B56B0 File Offset: 0x000B38B0
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Computes the hash value of the specified byte array and signs the resulting hash value.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		/// <param name="buffer">The input data for which to compute the hash. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037DE RID: 14302 RVA: 0x000B56C0 File Offset: 0x000B38C0
		public byte[] SignData(byte[] buffer)
		{
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] rgbHash = hashAlgorithm.ComputeHash(buffer);
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Signs a byte array from the specified start point to the specified end point.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		/// <param name="buffer">The input data to sign. </param>
		/// <param name="offset">The offset into the array from which to begin using data. </param>
		/// <param name="count">The number of bytes in the array to use as data. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037DF RID: 14303 RVA: 0x000B56E8 File Offset: 0x000B38E8
		public byte[] SignData(byte[] buffer, int offset, int count)
		{
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] rgbHash = hashAlgorithm.ComputeHash(buffer, offset, count);
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Computes the hash value of the specified input stream and signs the resulting hash value.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		/// <param name="inputStream">The input data for which to compute the hash. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037E0 RID: 14304 RVA: 0x000B5714 File Offset: 0x000B3914
		public byte[] SignData(Stream inputStream)
		{
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] rgbHash = hashAlgorithm.ComputeHash(inputStream);
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified hash value.</returns>
		/// <param name="rgbHash">The hash value of the data to be signed. </param>
		/// <param name="str">The name of the hash algorithm used to create the hash value of the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- There is no private key. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037E1 RID: 14305 RVA: 0x000B573C File Offset: 0x000B393C
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Verifies the specified data using the specified signature.</summary>
		/// <returns>true if the signature verifies the data; otherwise, false.</returns>
		/// <param name="rgbData">The data that was signed. </param>
		/// <param name="rgbSignature">The signature data to verify. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037E2 RID: 14306 RVA: 0x000B577C File Offset: 0x000B397C
		public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
		{
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] rgbHash = hashAlgorithm.ComputeHash(rgbData);
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		/// <summary>Verifies the specified hash data using the specified signature.</summary>
		/// <returns>true if the signature verifies the hash; otherwise, false.</returns>
		/// <param name="rgbHash">The hash value of the data to be signed. </param>
		/// <param name="str">The name of the hash algorithm used to create the hash value of the data. </param>
		/// <param name="rgbSignature">The signature data to verify. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null.-or- The <paramref name="rgbSignature" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The signature cannot be verified. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037E3 RID: 14307 RVA: 0x000B57A4 File Offset: 0x000B39A4
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (str == null)
			{
				str = "SHA1";
			}
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		/// <summary>Verifies the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature that is computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to verify for <paramref name="rgbData" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037E4 RID: 14308 RVA: 0x000B57F4 File Offset: 0x000B39F4
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060037E5 RID: 14309 RVA: 0x000B5804 File Offset: 0x000B3A04
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.dsa != null)
				{
					this.dsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000B585C File Offset: 0x000B3A5C
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.dsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.  </summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x000B58B0 File Offset: 0x000B3AB0
		[MonoTODO("call into KeyPairPersistence to get details")]
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			get
			{
				return null;
			}
		}

		/// <summary>Exports a blob containing the key information associated with a <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object.  </summary>
		/// <returns>A byte array containing the key information associated with a <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object.</returns>
		/// <param name="includePrivateParameters">true to include the private key; otherwise, false.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037E8 RID: 14312 RVA: 0x000B58B4 File Offset: 0x000B3AB4
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			byte[] result;
			if (includePrivateParameters)
			{
				result = CryptoConvert.ToCapiPrivateKeyBlob(this);
			}
			else
			{
				result = CryptoConvert.ToCapiPublicKeyBlob(this);
			}
			return result;
		}

		/// <summary>Imports a blob that represents DSA key information.</summary>
		/// <param name="keyBlob">A byte array that represents a DSA key blob.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060037E9 RID: 14313 RVA: 0x000B58E0 File Offset: 0x000B3AE0
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			DSA dsa = CryptoConvert.FromCapiKeyBlobDSA(keyBlob);
			if (dsa is DSACryptoServiceProvider)
			{
				DSAParameters parameters = dsa.ExportParameters(!(dsa as DSACryptoServiceProvider).PublicOnly);
				this.ImportParameters(parameters);
			}
			else
			{
				try
				{
					DSAParameters parameters2 = dsa.ExportParameters(true);
					this.ImportParameters(parameters2);
				}
				catch
				{
					DSAParameters parameters3 = dsa.ExportParameters(false);
					this.ImportParameters(parameters3);
				}
			}
		}

		// Token: 0x0400183D RID: 6205
		private const int PROV_DSS_DH = 13;

		// Token: 0x0400183E RID: 6206
		private KeyPairPersistence store;

		// Token: 0x0400183F RID: 6207
		private bool persistKey;

		// Token: 0x04001840 RID: 6208
		private bool persisted;

		// Token: 0x04001841 RID: 6209
		private bool privateKeyExportable = true;

		// Token: 0x04001842 RID: 6210
		private bool m_disposed;

		// Token: 0x04001843 RID: 6211
		private DSAManaged dsa;

		// Token: 0x04001844 RID: 6212
		private static bool useMachineKeyStore;
	}
}
