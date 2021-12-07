using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000622 RID: 1570
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003BDC RID: 15324 RVA: 0x000CDF24 File Offset: 0x000CC124
		public StrongNameIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the name of the strong name identity.</summary>
		/// <returns>A name to compare against the name specified by the security provider.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x000CDF30 File Offset: 0x000CC130
		// (set) Token: 0x06003BDE RID: 15326 RVA: 0x000CDF38 File Offset: 0x000CC138
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the public key value of the strong name identity expressed as a hexadecimal string.</summary>
		/// <returns>The public key value of the strong name identity expressed as a hexadecimal string.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000CDF44 File Offset: 0x000CC144
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x000CDF4C File Offset: 0x000CC14C
		public string PublicKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		/// <summary>Gets or sets the version of the strong name identity.</summary>
		/// <returns>The version number of the strong name identity.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x000CDF58 File Offset: 0x000CC158
		// (set) Token: 0x06003BE2 RID: 15330 RVA: 0x000CDF60 File Offset: 0x000CC160
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The method failed because the key is null.</exception>
		// Token: 0x06003BE3 RID: 15331 RVA: 0x000CDF6C File Offset: 0x000CC16C
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.name == null && this.key == null && this.version == null)
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			if (this.key == null)
			{
				throw new ArgumentException(Locale.GetText("PublicKey is required"));
			}
			StrongNamePublicKeyBlob blob = StrongNamePublicKeyBlob.FromString(this.key);
			Version version = null;
			if (this.version != null)
			{
				version = new Version(this.version);
			}
			return new StrongNameIdentityPermission(blob, this.name, version);
		}

		// Token: 0x04001A0A RID: 6666
		private string name;

		// Token: 0x04001A0B RID: 6667
		private string key;

		// Token: 0x04001A0C RID: 6668
		private string version;
	}
}
