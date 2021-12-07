using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited. </summary>
	// Token: 0x0200062B RID: 1579
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003C20 RID: 15392 RVA: 0x000CEC8C File Offset: 0x000CCE8C
		public ZoneIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
			this.zone = SecurityZone.NoZone;
		}

		/// <summary>Gets or sets membership in the content zone specified by the property value.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000CEC9C File Offset: 0x000CCE9C
		// (set) Token: 0x06003C22 RID: 15394 RVA: 0x000CECA4 File Offset: 0x000CCEA4
		public SecurityZone Zone
		{
			get
			{
				return this.zone;
			}
			set
			{
				this.zone = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003C23 RID: 15395 RVA: 0x000CECB0 File Offset: 0x000CCEB0
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new ZoneIdentityPermission(PermissionState.Unrestricted);
			}
			return new ZoneIdentityPermission(this.zone);
		}

		// Token: 0x04001A21 RID: 6689
		private SecurityZone zone;
	}
}
