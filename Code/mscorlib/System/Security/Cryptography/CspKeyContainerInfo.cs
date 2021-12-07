using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	/// <summary>Provides additional information about a cryptographic key pair. This class cannot be inherited.</summary>
	// Token: 0x020005A2 RID: 1442
	[ComVisible(true)]
	public sealed class CspKeyContainerInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> class using the specified parameters.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that provides information about the key.</param>
		// Token: 0x0600378E RID: 14222 RVA: 0x000B43EC File Offset: 0x000B25EC
		public CspKeyContainerInfo(CspParameters parameters)
		{
			this._params = parameters;
			this._random = true;
		}

		/// <summary>Gets a value indicating whether a key in a key container is accessible.</summary>
		/// <returns>true if the key is accessible; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000B4404 File Offset: 0x000B2604
		public bool Accessible
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container. </summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key type is not supported.</exception>
		/// <exception cref="T:System.NotSupportedException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x000B4408 File Offset: 0x000B2608
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether a key can be exported from a key container.</summary>
		/// <returns>true if the key can be exported; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000B440C File Offset: 0x000B260C
		public bool Exportable
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether a key is a hardware key.</summary>
		/// <returns>true if the key is a hardware key; otherwise, false.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.</exception>
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000B4410 File Offset: 0x000B2610
		public bool HardwareDevice
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a key container name.</summary>
		/// <returns>The key container name.</returns>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000B4414 File Offset: 0x000B2614
		public string KeyContainerName
		{
			get
			{
				return this._params.KeyContainerName;
			}
		}

		/// <summary>Gets a value that describes whether an asymmetric key was created as a signature key or an exchange key.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.KeyNumber" /> values that describes whether an asymmetric key was created as a signature key or an exchange key.</returns>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000B4424 File Offset: 0x000B2624
		public KeyNumber KeyNumber
		{
			get
			{
				return (KeyNumber)this._params.KeyNumber;
			}
		}

		/// <summary>Gets a value indicating whether a key is from a machine key set.</summary>
		/// <returns>true if the key is from the machine key set; otherwise, false.</returns>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000B4434 File Offset: 0x000B2634
		public bool MachineKeyStore
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a key pair is protected.</summary>
		/// <returns>true if the key pair is protected; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000B4438 File Offset: 0x000B2638
		public bool Protected
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the provider name of a key.</summary>
		/// <returns>The provider name.</returns>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000B443C File Offset: 0x000B263C
		public string ProviderName
		{
			get
			{
				return this._params.ProviderName;
			}
		}

		/// <summary>Gets the provider type of a key.</summary>
		/// <returns>The provider type. The default is 1.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000B444C File Offset: 0x000B264C
		public int ProviderType
		{
			get
			{
				return this._params.ProviderType;
			}
		}

		/// <summary>Gets a value indicating whether a key container was randomly generated by a managed cryptography class.</summary>
		/// <returns>true if the key container was randomly generated; otherwise, false.</returns>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000B445C File Offset: 0x000B265C
		public bool RandomlyGenerated
		{
			get
			{
				return this._random;
			}
		}

		/// <summary>Gets a value indicating whether a key can be removed from a key container.</summary>
		/// <returns>true if the key is removable; otherwise, false.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) was not found.</exception>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000B4464 File Offset: 0x000B2664
		public bool Removable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a unique key container name.</summary>
		/// <returns>The unique key container name.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000B4468 File Offset: 0x000B2668
		public string UniqueKeyContainerName
		{
			get
			{
				return this._params.ProviderName + "\\" + this._params.KeyContainerName;
			}
		}

		// Token: 0x0400181B RID: 6171
		private CspParameters _params;

		// Token: 0x0400181C RID: 6172
		internal bool _random;
	}
}
