using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.DataProtectionPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000072 RID: 114
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DataProtectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06000352 RID: 850 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public DataProtectionPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the data protection permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.DataProtectionPermissionFlags.NoFlags" />.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000EE58 File Offset: 0x0000D058
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000EE60 File Offset: 0x0000D060
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & DataProtectionPermissionFlags.AllFlags) != value)
				{
					string message = string.Format(Locale.GetText("Invalid flags {0}"), value);
					throw new ArgumentException(message, "DataProtectionPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>true if data can be encrypted; otherwise, false.  </returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public bool ProtectData
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.ProtectData) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.ProtectData;
				}
				else
				{
					this._flags &= ~DataProtectionPermissionFlags.ProtectData;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>true if data can be unencrypted; otherwise, false.  </returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public bool UnprotectData
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.UnprotectData) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.UnprotectData;
				}
				else
				{
					this._flags &= ~DataProtectionPermissionFlags.UnprotectData;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>true if memory can be encrypted; otherwise, false.  </returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000EF30 File Offset: 0x0000D130
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000EF40 File Offset: 0x0000D140
		public bool ProtectMemory
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.ProtectMemory) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.ProtectMemory;
				}
				else
				{
					this._flags &= ~DataProtectionPermissionFlags.ProtectMemory;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>true if memory can be unencrypted; otherwise, false.  </returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000EF78 File Offset: 0x0000D178
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000EF88 File Offset: 0x0000D188
		public bool UnprotectMemory
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.UnprotectMemory) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.UnprotectMemory;
				}
				else
				{
					this._flags &= ~DataProtectionPermissionFlags.UnprotectMemory;
				}
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.DataProtectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.DataProtectionPermission" /> that corresponds to the attribute.</returns>
		// Token: 0x0600035D RID: 861 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public override IPermission CreatePermission()
		{
			DataProtectionPermission result;
			if (base.Unrestricted)
			{
				result = new DataProtectionPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new DataProtectionPermission(this._flags);
			}
			return result;
		}

		// Token: 0x040001A4 RID: 420
		private DataProtectionPermissionFlags _flags;
	}
}
