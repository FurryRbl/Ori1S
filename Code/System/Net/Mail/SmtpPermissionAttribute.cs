using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x0200034C RID: 844
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SmtpPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermissionAttribute" /> class. </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values that specifies the permission behavior.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06001E06 RID: 7686 RVA: 0x0005BDE0 File Offset: 0x00059FE0
		public SmtpPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the level of access to SMTP servers controlled by the attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> value. Valid values are "Connect" and "None".</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0005BDEC File Offset: 0x00059FEC
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0005BDF4 File Offset: 0x00059FF4
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0005BE00 File Offset: 0x0005A000
		private SmtpAccess GetSmtpAccess()
		{
			if (this.access == null)
			{
				return SmtpAccess.None;
			}
			string text = this.access.ToLowerInvariant();
			switch (text)
			{
			case "connecttounrestrictedport":
				return SmtpAccess.ConnectToUnrestrictedPort;
			case "connect":
				return SmtpAccess.Connect;
			case "none":
				return SmtpAccess.None;
			}
			string text2 = Locale.GetText("Invalid Access='{0}' value.", new object[]
			{
				this.access
			});
			throw new ArgumentException("Access", text2);
		}

		/// <summary>Creates a permission object that can be stored with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> instance.</returns>
		// Token: 0x06001E0A RID: 7690 RVA: 0x0005BEBC File Offset: 0x0005A0BC
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.GetSmtpAccess());
		}

		// Token: 0x04001292 RID: 4754
		private string access;
	}
}
