using System;
using System.Security;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Creates a <see cref="T:System.Configuration.ConfigurationPermission" /> object that either grants or denies the marked target permission to access sections of configuration files.</summary>
	// Token: 0x0200002F RID: 47
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ConfigurationPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPermissionAttribute" /> class.</summary>
		/// <param name="action">The security action represented by an enumeration member of <see cref="T:System.Security.Permissions.SecurityAction" />. Determines the permission state of the attribute.</param>
		// Token: 0x060001D8 RID: 472 RVA: 0x00006B84 File Offset: 0x00004D84
		public ConfigurationPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Creates and returns an object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <returns>Returns an object that implements <see cref="T:System.Security.IPermission" />.</returns>
		// Token: 0x060001D9 RID: 473 RVA: 0x00006B90 File Offset: 0x00004D90
		public override IPermission CreatePermission()
		{
			return new ConfigurationPermission((!base.Unrestricted) ? PermissionState.None : PermissionState.Unrestricted);
		}
	}
}
