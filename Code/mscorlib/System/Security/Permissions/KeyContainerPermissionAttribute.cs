using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.KeyContainerPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200060B RID: 1547
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003AED RID: 15085 RVA: 0x000CA0B4 File Offset: 0x000C82B4
		public KeyContainerPermissionAttribute(SecurityAction action) : base(action)
		{
			this._spec = -1;
			this._type = -1;
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000CA0CC File Offset: 0x000C82CC
		// (set) Token: 0x06003AEF RID: 15087 RVA: 0x000CA0D4 File Offset: 0x000C82D4
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the name of the key container.</summary>
		/// <returns>The name of the key container.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000CA0E0 File Offset: 0x000C82E0
		// (set) Token: 0x06003AF1 RID: 15089 RVA: 0x000CA0E8 File Offset: 0x000C82E8
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
		/// <returns>One of the AT_ values defined in the Wincrypt.h header file. </returns>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000CA0F4 File Offset: 0x000C82F4
		// (set) Token: 0x06003AF3 RID: 15091 RVA: 0x000CA0FC File Offset: 0x000C82FC
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
		/// <returns>The name of the key store. The default is "*".</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000CA108 File Offset: 0x000C8308
		// (set) Token: 0x06003AF5 RID: 15093 RVA: 0x000CA110 File Offset: 0x000C8310
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
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000CA11C File Offset: 0x000C831C
		// (set) Token: 0x06003AF7 RID: 15095 RVA: 0x000CA124 File Offset: 0x000C8324
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
		/// <returns>One of the PROV_ values defined in the Wincrypt.h header file. </returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000CA130 File Offset: 0x000C8330
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x000CA138 File Offset: 0x000C8338
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

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermission" /> that corresponds to the attribute.</returns>
		// Token: 0x06003AFA RID: 15098 RVA: 0x000CA144 File Offset: 0x000C8344
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			if (this.EmptyEntry())
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] accessList = new KeyContainerPermissionAccessEntry[]
			{
				new KeyContainerPermissionAccessEntry(this._store, this._providerName, this._type, this._containerName, this._spec, this._flags)
			};
			return new KeyContainerPermission(this._flags, accessList);
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000CA1BC File Offset: 0x000C83BC
		private bool EmptyEntry()
		{
			return this._containerName == null && this._spec == 0 && this._store == null && this._providerName == null && this._type == 0;
		}

		// Token: 0x0400199D RID: 6557
		private KeyContainerPermissionFlags _flags;

		// Token: 0x0400199E RID: 6558
		private string _containerName;

		// Token: 0x0400199F RID: 6559
		private int _spec;

		// Token: 0x040019A0 RID: 6560
		private string _store;

		// Token: 0x040019A1 RID: 6561
		private string _providerName;

		// Token: 0x040019A2 RID: 6562
		private int _type;
	}
}
