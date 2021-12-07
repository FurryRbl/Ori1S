using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies permission to request information from Domain Name Servers.</summary>
	// Token: 0x020002FD RID: 765
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DnsPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x06001A3F RID: 6719 RVA: 0x000491F0 File Offset: 0x000473F0
		public DnsPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.DnsPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> that corresponds to the security declaration.</returns>
		// Token: 0x06001A40 RID: 6720 RVA: 0x000491FC File Offset: 0x000473FC
		public override IPermission CreatePermission()
		{
			return new DnsPermission((!base.Unrestricted) ? PermissionState.None : PermissionState.Unrestricted);
		}
	}
}
