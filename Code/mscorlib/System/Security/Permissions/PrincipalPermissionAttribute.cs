using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PrincipalPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000611 RID: 1553
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003B21 RID: 15137 RVA: 0x000CB018 File Offset: 0x000C9218
		public PrincipalPermissionAttribute(SecurityAction action) : base(action)
		{
			this.authenticated = true;
		}

		/// <summary>Gets or sets a value indicating whether the current principal has been authenticated by the underlying role-based security provider.</summary>
		/// <returns>true if the current principal has been authenticated; otherwise, false.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000CB028 File Offset: 0x000C9228
		// (set) Token: 0x06003B23 RID: 15139 RVA: 0x000CB030 File Offset: 0x000C9230
		public bool Authenticated
		{
			get
			{
				return this.authenticated;
			}
			set
			{
				this.authenticated = value;
			}
		}

		/// <summary>Gets or sets the name of the identity associated with the current principal.</summary>
		/// <returns>A name to match against that provided by the underlying role-based security provider.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x000CB03C File Offset: 0x000C923C
		// (set) Token: 0x06003B25 RID: 15141 RVA: 0x000CB044 File Offset: 0x000C9244
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

		/// <summary>Gets or sets membership in a specified security role.</summary>
		/// <returns>The name of a role from the underlying role-based security provider.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000CB050 File Offset: 0x000C9250
		// (set) Token: 0x06003B27 RID: 15143 RVA: 0x000CB058 File Offset: 0x000C9258
		public string Role
		{
			get
			{
				return this.role;
			}
			set
			{
				this.role = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.PrincipalPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PrincipalPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003B28 RID: 15144 RVA: 0x000CB064 File Offset: 0x000C9264
		public override IPermission CreatePermission()
		{
			PrincipalPermission result;
			if (base.Unrestricted)
			{
				result = new PrincipalPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new PrincipalPermission(this.name, this.role, this.authenticated);
			}
			return result;
		}

		// Token: 0x040019BC RID: 6588
		private bool authenticated;

		// Token: 0x040019BD RID: 6589
		private string name;

		// Token: 0x040019BE RID: 6590
		private string role;
	}
}
