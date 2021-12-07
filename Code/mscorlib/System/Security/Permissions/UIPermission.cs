using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the permissions related to user interfaces and the clipboard. This class cannot be inherited.</summary>
	// Token: 0x02000624 RID: 1572
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with either fully restricted or unrestricted access, as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003BEA RID: 15338 RVA: 0x000CE1CC File Offset: 0x000CC3CC
		public UIPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._clipboard = UIPermissionClipboard.AllClipboard;
				this._window = UIPermissionWindow.AllWindows;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the permissions for the clipboard, and no access to windows.</summary>
		/// <param name="clipboardFlag">One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="clipboardFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> value. </exception>
		// Token: 0x06003BEB RID: 15339 RVA: 0x000CE1F0 File Offset: 0x000CC3F0
		public UIPermission(UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the permissions for windows, and no access to the clipboard.</summary>
		/// <param name="windowFlag">One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="windowFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionWindow" /> value. </exception>
		// Token: 0x06003BEC RID: 15340 RVA: 0x000CE200 File Offset: 0x000CC400
		public UIPermission(UIPermissionWindow windowFlag)
		{
			this.Window = windowFlag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the specified permissions for windows and the clipboard.</summary>
		/// <param name="windowFlag">One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values. </param>
		/// <param name="clipboardFlag">One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="windowFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionWindow" /> value.-or- The <paramref name="clipboardFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> value. </exception>
		// Token: 0x06003BED RID: 15341 RVA: 0x000CE210 File Offset: 0x000CC410
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
			this.Window = windowFlag;
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x000CE228 File Offset: 0x000CC428
		int IBuiltInPermission.GetTokenIndex()
		{
			return 7;
		}

		/// <summary>Gets or sets the clipboard access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000CE22C File Offset: 0x000CC42C
		// (set) Token: 0x06003BF0 RID: 15344 RVA: 0x000CE234 File Offset: 0x000CC434
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this._clipboard;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionClipboard), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "UIPermissionClipboard");
				}
				this._clipboard = value;
			}
		}

		/// <summary>Gets or sets the window access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x000CE284 File Offset: 0x000CC484
		// (set) Token: 0x06003BF2 RID: 15346 RVA: 0x000CE28C File Offset: 0x000CC48C
		public UIPermissionWindow Window
		{
			get
			{
				return this._window;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionWindow), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "UIPermissionWindow");
				}
				this._window = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003BF3 RID: 15347 RVA: 0x000CE2DC File Offset: 0x000CC4DC
		public override IPermission Copy()
		{
			return new UIPermission(this._window, this._clipboard);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06003BF4 RID: 15348 RVA: 0x000CE2F0 File Offset: 0x000CC4F0
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._window = UIPermissionWindow.AllWindows;
				this._clipboard = UIPermissionClipboard.AllClipboard;
			}
			else
			{
				string text = esd.Attribute("Window");
				if (text == null)
				{
					this._window = UIPermissionWindow.NoWindows;
				}
				else
				{
					this._window = (UIPermissionWindow)((int)Enum.Parse(typeof(UIPermissionWindow), text));
				}
				string text2 = esd.Attribute("Clipboard");
				if (text2 == null)
				{
					this._clipboard = UIPermissionClipboard.NoClipboard;
				}
				else
				{
					this._clipboard = (UIPermissionClipboard)((int)Enum.Parse(typeof(UIPermissionClipboard), text2));
				}
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003BF5 RID: 15349 RVA: 0x000CE39C File Offset: 0x000CC59C
		public override IPermission Intersect(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return null;
			}
			UIPermissionWindow uipermissionWindow = (this._window >= uipermission._window) ? uipermission._window : this._window;
			UIPermissionClipboard uipermissionClipboard = (this._clipboard >= uipermission._clipboard) ? uipermission._clipboard : this._clipboard;
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003BF6 RID: 15350 RVA: 0x000CE418 File Offset: 0x000CC618
		public override bool IsSubsetOf(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.IsEmpty(this._window, this._clipboard);
			}
			return uipermission.IsUnrestricted() || (this._window <= uipermission._window && this._clipboard <= uipermission._clipboard);
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003BF7 RID: 15351 RVA: 0x000CE478 File Offset: 0x000CC678
		public bool IsUnrestricted()
		{
			return this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003BF8 RID: 15352 RVA: 0x000CE494 File Offset: 0x000CC694
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				if (this._window != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", this._window.ToString());
				}
				if (this._clipboard != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", this._clipboard.ToString());
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003BF9 RID: 15353 RVA: 0x000CE524 File Offset: 0x000CC724
		public override IPermission Union(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.Copy();
			}
			UIPermissionWindow uipermissionWindow = (this._window <= uipermission._window) ? uipermission._window : this._window;
			UIPermissionClipboard uipermissionClipboard = (this._clipboard <= uipermission._clipboard) ? uipermission._clipboard : this._clipboard;
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000CE5A4 File Offset: 0x000CC7A4
		private bool IsEmpty(UIPermissionWindow w, UIPermissionClipboard c)
		{
			return w == UIPermissionWindow.NoWindows && c == UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000CE5B4 File Offset: 0x000CC7B4
		private UIPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UIPermission uipermission = target as UIPermission;
			if (uipermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UIPermission));
			}
			return uipermission;
		}

		// Token: 0x04001A0E RID: 6670
		private const int version = 1;

		// Token: 0x04001A0F RID: 6671
		private UIPermissionWindow _window;

		// Token: 0x04001A10 RID: 6672
		private UIPermissionClipboard _clipboard;
	}
}
