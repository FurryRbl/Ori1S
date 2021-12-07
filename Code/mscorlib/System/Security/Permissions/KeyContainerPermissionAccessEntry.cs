using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Specifies access rights for specific key containers. This class cannot be inherited.</summary>
	// Token: 0x02000608 RID: 1544
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified cryptographic service provider (CSP) parameters and access permissions.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that contains the cryptographic service provider (CSP) parameters. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x06003AC9 RID: 15049 RVA: 0x000C9C64 File Offset: 0x000C7E64
		public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.ProviderName = parameters.ProviderName;
			this.ProviderType = parameters.ProviderType;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeySpec = parameters.KeyNumber;
			this.Flags = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified key container name and access permissions.</summary>
		/// <param name="keyContainerName">The name of the key container. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x06003ACA RID: 15050 RVA: 0x000C9CC0 File Offset: 0x000C7EC0
		public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags)
		{
			this.KeyContainerName = keyContainerName;
			this.Flags = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class with the specified property values.</summary>
		/// <param name="keyStore">The name of the key store. </param>
		/// <param name="providerName">The name of the provider. </param>
		/// <param name="providerType">The type code for the provider. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.ProviderType" /> property for values. </param>
		/// <param name="keyContainerName">The name of the key container. </param>
		/// <param name="keySpec">The key specification. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.KeySpec" /> property for values. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x06003ACB RID: 15051 RVA: 0x000C9CD8 File Offset: 0x000C7ED8
		public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
		{
			this.KeyStore = keyStore;
			this.ProviderName = providerName;
			this.ProviderType = providerType;
			this.KeyContainerName = keyContainerName;
			this.KeySpec = keySpec;
			this.Flags = flags;
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000C9D18 File Offset: 0x000C7F18
		// (set) Token: 0x06003ACD RID: 15053 RVA: 0x000C9D20 File Offset: 0x000C7F20
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.NoFlags)
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "KeyContainerPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the key container name.</summary>
		/// <returns>The name of the key container.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000C9D64 File Offset: 0x000C7F64
		// (set) Token: 0x06003ACF RID: 15055 RVA: 0x000C9D6C File Offset: 0x000C7F6C
		public string KeyContainerName
		{
			get
			{
				return this._containerName;
			}
			set
			{
				this._containerName = value;
			}
		}

		/// <summary>Gets or sets the key specification.</summary>
		/// <returns>One of the AT_ values defined in the Wincrypt.h header file.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000C9D78 File Offset: 0x000C7F78
		// (set) Token: 0x06003AD1 RID: 15057 RVA: 0x000C9D80 File Offset: 0x000C7F80
		public int KeySpec
		{
			get
			{
				return this._spec;
			}
			set
			{
				this._spec = value;
			}
		}

		/// <summary>Gets or sets the name of the key store.</summary>
		/// <returns>The name of the key store.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000C9D8C File Offset: 0x000C7F8C
		// (set) Token: 0x06003AD3 RID: 15059 RVA: 0x000C9D94 File Offset: 0x000C7F94
		public string KeyStore
		{
			get
			{
				return this._store;
			}
			set
			{
				this._store = value;
			}
		}

		/// <summary>Gets or sets the provider name.</summary>
		/// <returns>The name of the provider.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000C9DA0 File Offset: 0x000C7FA0
		// (set) Token: 0x06003AD5 RID: 15061 RVA: 0x000C9DA8 File Offset: 0x000C7FA8
		public string ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				this._providerName = value;
			}
		}

		/// <summary>Gets or sets the provider type.</summary>
		/// <returns>One of the PROV_ values defined in the Wincrypt.h header file.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000C9DB4 File Offset: 0x000C7FB4
		// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x000C9DBC File Offset: 0x000C7FBC
		public int ProviderType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object is equal to the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> is equal to the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to compare with the currentinstance. </param>
		// Token: 0x06003AD8 RID: 15064 RVA: 0x000C9DC8 File Offset: 0x000C7FC8
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = o as KeyContainerPermissionAccessEntry;
			return keyContainerPermissionAccessEntry != null && this._flags == keyContainerPermissionAccessEntry._flags && !(this._containerName != keyContainerPermissionAccessEntry._containerName) && !(this._store != keyContainerPermissionAccessEntry._store) && !(this._providerName != keyContainerPermissionAccessEntry._providerName) && this._type == keyContainerPermissionAccessEntry._type;
		}

		/// <summary>Gets a hash code for the current instance that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object.</returns>
		// Token: 0x06003AD9 RID: 15065 RVA: 0x000C9E5C File Offset: 0x000C805C
		public override int GetHashCode()
		{
			int num = this._type ^ this._spec ^ (int)this._flags;
			if (this._containerName != null)
			{
				num ^= this._containerName.GetHashCode();
			}
			if (this._store != null)
			{
				num ^= this._store.GetHashCode();
			}
			if (this._providerName != null)
			{
				num ^= this._providerName.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001995 RID: 6549
		private KeyContainerPermissionFlags _flags;

		// Token: 0x04001996 RID: 6550
		private string _containerName;

		// Token: 0x04001997 RID: 6551
		private int _spec;

		// Token: 0x04001998 RID: 6552
		private string _store;

		// Token: 0x04001999 RID: 6553
		private string _providerName;

		// Token: 0x0400199A RID: 6554
		private int _type;
	}
}
