using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StorePermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200045E RID: 1118
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StorePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06002825 RID: 10277 RVA: 0x0007F71C File Offset: 0x0007D91C
		public StorePermissionAttribute(SecurityAction action) : base(action)
		{
			this._flags = StorePermissionFlags.NoFlags;
		}

		/// <summary>Gets or sets the store permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.StorePermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x0007F72C File Offset: 0x0007D92C
		// (set) Token: 0x06002827 RID: 10279 RVA: 0x0007F734 File Offset: 0x0007D934
		public StorePermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & StorePermissionFlags.AllFlags) != value)
				{
					string message = string.Format(Locale.GetText("Invalid flags {0}"), value);
					throw new ArgumentException(message, "StorePermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to add to a store.</summary>
		/// <returns>true if the ability to add to a store is allowed; otherwise, false.</returns>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x0007F778 File Offset: 0x0007D978
		// (set) Token: 0x06002829 RID: 10281 RVA: 0x0007F78C File Offset: 0x0007D98C
		public bool AddToStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.AddToStore) != StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.AddToStore;
				}
				else
				{
					this._flags &= ~StorePermissionFlags.AddToStore;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to create a store.</summary>
		/// <returns>true if the ability to create a store is allowed; otherwise, false.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x0007F7B8 File Offset: 0x0007D9B8
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x0007F7C8 File Offset: 0x0007D9C8
		public bool CreateStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.CreateStore) != StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.CreateStore;
				}
				else
				{
					this._flags &= ~StorePermissionFlags.CreateStore;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to delete a store.</summary>
		/// <returns>true if the ability to delete a store is allowed; otherwise, false.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x0007F800 File Offset: 0x0007DA00
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x0007F810 File Offset: 0x0007DA10
		public bool DeleteStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.DeleteStore) != StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.DeleteStore;
				}
				else
				{
					this._flags &= ~StorePermissionFlags.DeleteStore;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate the certificates in a store.</summary>
		/// <returns>true if the ability to enumerate certificates is allowed; otherwise, false.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x0007F848 File Offset: 0x0007DA48
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x0007F85C File Offset: 0x0007DA5C
		public bool EnumerateCertificates
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateCertificates) != StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateCertificates;
				}
				else
				{
					this._flags &= ~StorePermissionFlags.EnumerateCertificates;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate stores.</summary>
		/// <returns>true if the ability to enumerate stores is allowed; otherwise, false.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0007F890 File Offset: 0x0007DA90
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x0007F8A0 File Offset: 0x0007DAA0
		public bool EnumerateStores
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateStores) != StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateStores;
				}
				else
				{
					this._flags &= ~StorePermissionFlags.EnumerateStores;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to open a store.</summary>
		/// <returns>true if the ability to open a store is allowed; otherwise, false.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x0007F8D8 File Offset: 0x0007DAD8
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x0007F8EC File Offset: 0x0007DAEC
		public bool OpenStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.OpenStore) != StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.OpenStore;
				}
				else
				{
					this._flags &= ~StorePermissionFlags.OpenStore;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to remove a certificate from a store.</summary>
		/// <returns>true if the ability to remove a certificate from a store is allowed; otherwise, false.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x0007F918 File Offset: 0x0007DB18
		// (set) Token: 0x06002835 RID: 10293 RVA: 0x0007F92C File Offset: 0x0007DB2C
		public bool RemoveFromStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.RemoveFromStore) != StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.RemoveFromStore;
				}
				else
				{
					this._flags &= ~StorePermissionFlags.RemoveFromStore;
				}
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StorePermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StorePermission" /> that corresponds to the attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002836 RID: 10294 RVA: 0x0007F958 File Offset: 0x0007DB58
		public override IPermission CreatePermission()
		{
			StorePermission result;
			if (base.Unrestricted)
			{
				result = new StorePermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new StorePermission(this._flags);
			}
			return result;
		}

		// Token: 0x040018C3 RID: 6339
		private StorePermissionFlags _flags;
	}
}
