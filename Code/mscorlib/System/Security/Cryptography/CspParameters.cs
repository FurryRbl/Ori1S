using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	/// <summary>Contains parameters that are passed to the cryptographic service provider (CSP) that performs cryptographic computations. This class cannot be inherited.</summary>
	// Token: 0x020005A3 RID: 1443
	[ComVisible(true)]
	public sealed class CspParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class.</summary>
		// Token: 0x0600379C RID: 14236 RVA: 0x000B4498 File Offset: 0x000B2698
		public CspParameters() : this(1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create. </param>
		// Token: 0x0600379D RID: 14237 RVA: 0x000B44A4 File Offset: 0x000B26A4
		public CspParameters(int dwTypeIn) : this(dwTypeIn, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name. </param>
		// Token: 0x0600379E RID: 14238 RVA: 0x000B44B0 File Offset: 0x000B26B0
		public CspParameters(int dwTypeIn, string strProviderNameIn) : this(dwTypeIn, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name, and the specified container name.</summary>
		/// <param name="dwTypeIn">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name. </param>
		/// <param name="strContainerNameIn">A container name. </param>
		// Token: 0x0600379F RID: 14239 RVA: 0x000B44BC File Offset: 0x000B26BC
		public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
		{
			this.ProviderType = dwTypeIn;
			this.ProviderName = strProviderNameIn;
			this.KeyContainerName = strContainerNameIn;
			this.KeyNumber = -1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a handle to an unmanaged smart card password dialog. </summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name. </param>
		/// <param name="keyContainerName">A container name. </param>
		/// <param name="cryptoKeySecurity">A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for the container.</param>
		/// <param name="parentWindowHandle">A handle to the parent window for a smart card password dialog.</param>
		// Token: 0x060037A0 RID: 14240 RVA: 0x000B44EC File Offset: 0x000B26EC
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle) : this(providerType, providerName, keyContainerName)
		{
			if (cryptoKeySecurity != null)
			{
				this.CryptoKeySecurity = cryptoKeySecurity;
			}
			this._windowHandle = parentWindowHandle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a password associated with a smart card key.</summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name. </param>
		/// <param name="keyContainerName">A container name. </param>
		/// <param name="cryptoKeySecurity">A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container. </param>
		/// <param name="keyPassword">A password associated with a smart card key.</param>
		// Token: 0x060037A1 RID: 14241 RVA: 0x000B451C File Offset: 0x000B271C
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword) : this(providerType, providerName, keyContainerName)
		{
			if (cryptoKeySecurity != null)
			{
				this.CryptoKeySecurity = cryptoKeySecurity;
			}
			this._password = keyPassword;
		}

		/// <summary>Represents the flags for <see cref="T:System.Security.Cryptography.CspParameters" /> that modify the behavior of the cryptographic service provider (CSP).</summary>
		/// <returns>An enumeration value, or a bitwise combination of enumeration values.</returns>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000B454C File Offset: 0x000B274C
		// (set) Token: 0x060037A3 RID: 14243 RVA: 0x000B4554 File Offset: 0x000B2754
		public CspProviderFlags Flags
		{
			get
			{
				return this._Flags;
			}
			set
			{
				this._Flags = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container. </summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000B4560 File Offset: 0x000B2760
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x000B4568 File Offset: 0x000B2768
		[MonoTODO("access control isn't implemented")]
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a password associated with a smart card key. </summary>
		/// <returns>A password associated with a smart card key.</returns>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000B4570 File Offset: 0x000B2770
		// (set) Token: 0x060037A7 RID: 14247 RVA: 0x000B4578 File Offset: 0x000B2778
		public SecureString KeyPassword
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		/// <summary>Gets or sets a handle to the unmanaged parent window for a smart card password dialog.</summary>
		/// <returns>A handle to the parent window for a smart card password dialog.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000B4584 File Offset: 0x000B2784
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x000B458C File Offset: 0x000B278C
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this._windowHandle;
			}
			set
			{
				this._windowHandle = value;
			}
		}

		// Token: 0x0400181D RID: 6173
		private CspProviderFlags _Flags;

		/// <summary>Represents the key container name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x0400181E RID: 6174
		public string KeyContainerName;

		/// <summary>Specifies whether an asymmetric key is created as a signature key or an exchange key.</summary>
		// Token: 0x0400181F RID: 6175
		public int KeyNumber;

		/// <summary>Represents the provider name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04001820 RID: 6176
		public string ProviderName;

		/// <summary>Represents the provider type code for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04001821 RID: 6177
		public int ProviderType;

		// Token: 0x04001822 RID: 6178
		private SecureString _password;

		// Token: 0x04001823 RID: 6179
		private IntPtr _windowHandle;
	}
}
