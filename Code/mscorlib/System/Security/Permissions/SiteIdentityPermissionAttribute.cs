using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200061F RID: 1567
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003BBA RID: 15290 RVA: 0x000CD1CC File Offset: 0x000CB3CC
		public SiteIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the site name of the calling code.</summary>
		/// <returns>The site name to compare against the site name specified by the security provider.</returns>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000CD1D8 File Offset: 0x000CB3D8
		// (set) Token: 0x06003BBC RID: 15292 RVA: 0x000CD1E0 File Offset: 0x000CB3E0
		public string Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.SiteIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003BBD RID: 15293 RVA: 0x000CD1EC File Offset: 0x000CB3EC
		public override IPermission CreatePermission()
		{
			SiteIdentityPermission result;
			if (base.Unrestricted)
			{
				result = new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			else if (this.site == null)
			{
				result = new SiteIdentityPermission(PermissionState.None);
			}
			else
			{
				result = new SiteIdentityPermission(this.site);
			}
			return result;
		}

		// Token: 0x04001A02 RID: 6658
		private string site;
	}
}
