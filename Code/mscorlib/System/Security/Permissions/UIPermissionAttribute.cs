using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.UIPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000625 RID: 1573
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UIPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003BFC RID: 15356 RVA: 0x000CE5E8 File Offset: 0x000CC7E8
		public UIPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the type of access to the clipboard that is permitted.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000CE5F4 File Offset: 0x000CC7F4
		// (set) Token: 0x06003BFE RID: 15358 RVA: 0x000CE5FC File Offset: 0x000CC7FC
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this.clipboard;
			}
			set
			{
				this.clipboard = value;
			}
		}

		/// <summary>Gets or sets the type of access to the window resources that is permitted.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x000CE608 File Offset: 0x000CC808
		// (set) Token: 0x06003C00 RID: 15360 RVA: 0x000CE610 File Offset: 0x000CC810
		public UIPermissionWindow Window
		{
			get
			{
				return this.window;
			}
			set
			{
				this.window = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.UIPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UIPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003C01 RID: 15361 RVA: 0x000CE61C File Offset: 0x000CC81C
		public override IPermission CreatePermission()
		{
			UIPermission result;
			if (base.Unrestricted)
			{
				result = new UIPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new UIPermission(this.window, this.clipboard);
			}
			return result;
		}

		// Token: 0x04001A11 RID: 6673
		private UIPermissionClipboard clipboard;

		// Token: 0x04001A12 RID: 6674
		private UIPermissionWindow window;
	}
}
