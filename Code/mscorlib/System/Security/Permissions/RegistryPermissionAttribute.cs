using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.RegistryPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000619 RID: 1561
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x06003B6C RID: 15212 RVA: 0x000CC528 File Offset: 0x000CA728
		public RegistryPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets full access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for full access. </returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called; it is only provided for C# compiler compatibility.</exception>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x000CC534 File Offset: 0x000CA734
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x000CC540 File Offset: 0x000CA740
		[Obsolete("use newer properties")]
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.create = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets create-level access for the specified registry keys. </summary>
		/// <returns>A semicolon-separated list of registry key paths, for create-level access. </returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000CC558 File Offset: 0x000CA758
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x000CC560 File Offset: 0x000CA760
		public string Create
		{
			get
			{
				return this.create;
			}
			set
			{
				this.create = value;
			}
		}

		/// <summary>Gets or sets read access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for read access. </returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000CC56C File Offset: 0x000CA76C
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x000CC574 File Offset: 0x000CA774
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		/// <summary>Gets or sets write access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for write access. </returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000CC580 File Offset: 0x000CA780
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000CC588 File Offset: 0x000CA788
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		/// <summary>Gets or sets change access control for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for change access control. .</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000CC594 File Offset: 0x000CA794
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x000CC59C File Offset: 0x000CA79C
		public string ChangeAccessControl
		{
			get
			{
				return this.changeAccessControl;
			}
			set
			{
				this.changeAccessControl = value;
			}
		}

		/// <summary>Gets or sets view access control for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for view access control.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000CC5A8 File Offset: 0x000CA7A8
		// (set) Token: 0x06003B78 RID: 15224 RVA: 0x000CC5B0 File Offset: 0x000CA7B0
		public string ViewAccessControl
		{
			get
			{
				return this.viewAccessControl;
			}
			set
			{
				this.viewAccessControl = value;
			}
		}

		/// <summary>Gets or sets a specified set of registry keys that can be viewed and modified.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for create, read, and write access. </returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called; it is only provided for C# compiler compatibility. </exception>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000CC5BC File Offset: 0x000CA7BC
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000CC5C4 File Offset: 0x000CA7C4
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.create = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.RegistryPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.RegistryPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003B7B RID: 15227 RVA: 0x000CC5DC File Offset: 0x000CA7DC
		public override IPermission CreatePermission()
		{
			RegistryPermission registryPermission;
			if (base.Unrestricted)
			{
				registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			}
			else
			{
				registryPermission = new RegistryPermission(PermissionState.None);
				if (this.create != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Create, this.create);
				}
				if (this.read != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Write, this.write);
				}
			}
			return registryPermission;
		}

		// Token: 0x040019DC RID: 6620
		private string create;

		// Token: 0x040019DD RID: 6621
		private string read;

		// Token: 0x040019DE RID: 6622
		private string write;

		// Token: 0x040019DF RID: 6623
		private string changeAccessControl;

		// Token: 0x040019E0 RID: 6624
		private string viewAccessControl;
	}
}
