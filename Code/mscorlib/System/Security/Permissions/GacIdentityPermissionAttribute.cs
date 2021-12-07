using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.GacIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005FB RID: 1531
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value. </exception>
		// Token: 0x06003A7A RID: 14970 RVA: 0x000C8E58 File Offset: 0x000C7058
		public GacIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Creates a new <see cref="T:System.Security.Permissions.GacIdentityPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.GacIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003A7B RID: 14971 RVA: 0x000C8E64 File Offset: 0x000C7064
		public override IPermission CreatePermission()
		{
			return new GacIdentityPermission();
		}
	}
}
