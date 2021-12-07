using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000629 RID: 1577
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UrlIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003C10 RID: 15376 RVA: 0x000CE9D8 File Offset: 0x000CCBD8
		public UrlIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the full URL of the calling code.</summary>
		/// <returns>The URL to match with the URL specified by the host.</returns>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000CE9E4 File Offset: 0x000CCBE4
		// (set) Token: 0x06003C12 RID: 15378 RVA: 0x000CE9EC File Offset: 0x000CCBEC
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.UrlIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003C13 RID: 15379 RVA: 0x000CE9F8 File Offset: 0x000CCBF8
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.url);
		}

		// Token: 0x04001A1E RID: 6686
		private string url;
	}
}
