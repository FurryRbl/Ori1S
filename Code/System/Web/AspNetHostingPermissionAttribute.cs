using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Allows security actions for <see cref="T:System.Web.AspNetHostingPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020004BC RID: 1212
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class AspNetHostingPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> enumeration values. </param>
		// Token: 0x06002BAD RID: 11181 RVA: 0x00098848 File Offset: 0x00096A48
		public AspNetHostingPermissionAttribute(SecurityAction action) : base(action)
		{
			this._level = AspNetHostingPermissionLevel.None;
		}

		/// <summary>Creates a new <see cref="T:System.Web.AspNetHostingPermission" /> with the permission level previously set by the <see cref="P:System.Web.AspNetHostingPermissionAttribute.Level" /> property.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that is the new <see cref="T:System.Web.AspNetHostingPermission" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002BAE RID: 11182 RVA: 0x0009885C File Offset: 0x00096A5C
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new AspNetHostingPermission(PermissionState.Unrestricted);
			}
			return new AspNetHostingPermission(this._level);
		}

		/// <summary>Gets or sets the current hosting permission level.</summary>
		/// <returns>One of the <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration values.</returns>
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x0009887C File Offset: 0x00096A7C
		// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x00098884 File Offset: 0x00096A84
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value < AspNetHostingPermissionLevel.None || value > AspNetHostingPermissionLevel.Unrestricted)
				{
					string text = Locale.GetText("Invalid enum {0}.");
					throw new ArgumentException(string.Format(text, value), "Level");
				}
				this._level = value;
			}
		}

		// Token: 0x04001B86 RID: 7046
		private AspNetHostingPermissionLevel _level;
	}
}
