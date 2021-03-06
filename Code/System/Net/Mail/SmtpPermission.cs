using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x0200034B RID: 843
	[Serializable]
	public sealed class SmtpPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class with the specified state.</summary>
		/// <param name="unrestricted">true if the new permission is unrestricted; otherwise, false.</param>
		// Token: 0x06001DF8 RID: 7672 RVA: 0x0005BAD4 File Offset: 0x00059CD4
		public SmtpPermission(bool unrestricted)
		{
			this.unrestricted = unrestricted;
			this.access = ((!unrestricted) ? SmtpAccess.None : SmtpAccess.ConnectToUnrestrictedPort);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified permission state value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06001DF9 RID: 7673 RVA: 0x0005BB04 File Offset: 0x00059D04
		public SmtpPermission(PermissionState state)
		{
			this.unrestricted = (state == PermissionState.Unrestricted);
			this.access = ((!this.unrestricted) ? SmtpAccess.None : SmtpAccess.ConnectToUnrestrictedPort);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified access level.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x06001DFA RID: 7674 RVA: 0x0005BB3C File Offset: 0x00059D3C
		public SmtpPermission(SmtpAccess access)
		{
			this.access = access;
		}

		/// <summary>Gets the level of access to SMTP servers controlled by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values. </returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0005BB4C File Offset: 0x00059D4C
		public SmtpAccess Access
		{
			get
			{
				return this.access;
			}
		}

		/// <summary>Adds the specified access level value to the permission. </summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x06001DFC RID: 7676 RVA: 0x0005BB54 File Offset: 0x00059D54
		public void AddPermission(SmtpAccess access)
		{
			if (!this.unrestricted && access > this.access)
			{
				this.access = access;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission. </summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that is identical to the current permission.</returns>
		// Token: 0x06001DFD RID: 7677 RVA: 0x0005BB74 File Offset: 0x00059D74
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.access);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that represents the intersection of the current permission and the specified permission. Returns null if the intersection is empty or <paramref name="target" /> is null.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x06001DFE RID: 7678 RVA: 0x0005BB94 File Offset: 0x00059D94
		public override IPermission Intersect(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return null;
			}
			if (this.unrestricted && smtpPermission.unrestricted)
			{
				return new SmtpPermission(true);
			}
			if (this.access > smtpPermission.access)
			{
				return new SmtpPermission(smtpPermission.access);
			}
			return new SmtpPermission(this.access);
		}

		/// <summary>Returns a value indicating whether the current permission is a subset of the specified permission. </summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x06001DFF RID: 7679 RVA: 0x0005BBF8 File Offset: 0x00059DF8
		public override bool IsSubsetOf(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.unrestricted)
			{
				return smtpPermission.unrestricted;
			}
			return this.access <= smtpPermission.access;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06001E00 RID: 7680 RVA: 0x0005BC40 File Offset: 0x00059E40
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Creates an XML encoding of the state of the permission. </summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the current permission.</returns>
		// Token: 0x06001E01 RID: 7681 RVA: 0x0005BC48 File Offset: 0x00059E48
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(SmtpPermission), 1);
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				SmtpAccess smtpAccess = this.access;
				if (smtpAccess != SmtpAccess.Connect)
				{
					if (smtpAccess == SmtpAccess.ConnectToUnrestrictedPort)
					{
						securityElement.AddAttribute("Access", "ConnectToUnrestrictedPort");
					}
				}
				else
				{
					securityElement.AddAttribute("Access", "Connect");
				}
			}
			return securityElement;
		}

		/// <summary>Sets the state of the permission using the specified XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to set the state of the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> does not describe an <see cref="T:System.Net.Mail.SmtpPermission" /> object.-or-<paramref name="securityElement" /> does not contain the required state information to reconstruct the permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null.</exception>
		// Token: 0x06001E02 RID: 7682 RVA: 0x0005BCCC File Offset: 0x00059ECC
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			if (PermissionHelper.IsUnrestricted(securityElement))
			{
				this.access = SmtpAccess.Connect;
			}
			else
			{
				this.access = SmtpAccess.None;
			}
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission. </summary>
		/// <returns>A new <see cref="T:System.Net.Mail.SmtpPermission" /> permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to combine with the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x06001E03 RID: 7683 RVA: 0x0005BD28 File Offset: 0x00059F28
		public override IPermission Union(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return this.Copy();
			}
			if (this.unrestricted || smtpPermission.unrestricted)
			{
				return new SmtpPermission(true);
			}
			if (this.access > smtpPermission.access)
			{
				return new SmtpPermission(this.access);
			}
			return new SmtpPermission(smtpPermission.access);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0005BD90 File Offset: 0x00059F90
		private bool IsEmpty()
		{
			return !this.unrestricted && this.access == SmtpAccess.None;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0005BDAC File Offset: 0x00059FAC
		private SmtpPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(SmtpPermission));
			}
			return smtpPermission;
		}

		// Token: 0x0400128F RID: 4751
		private const int version = 1;

		// Token: 0x04001290 RID: 4752
		private bool unrestricted;

		// Token: 0x04001291 RID: 4753
		private SmtpAccess access;
	}
}
