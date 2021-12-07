﻿using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity, an access mask, and an access control type (allow or deny). An <see cref="T:System.Security.AccessControl.AccessRule" /> object also contains information about the how the rule is inherited by child objects and how that inheritance is propagated.</summary>
	// Token: 0x02000558 RID: 1368
	public abstract class AccessRule : AuthorizationRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">The valid access control type.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />, or the <paramref name="type" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is zero, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x06003597 RID: 13719 RVA: 0x000B1318 File Offset: 0x000AF518
		protected AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
		{
			if (!(identity is SecurityIdentifier))
			{
				throw new ArgumentException("identity");
			}
			if (type < AccessControlType.Allow || type > AccessControlType.Deny)
			{
				throw new ArgumentException("type");
			}
			if (accessMask == 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.type = type;
		}

		/// <summary>Gets the <see cref="T:System.Security.AccessControl.AccessControlType" /> value associated with this <see cref="T:System.Security.AccessControl.AccessRule" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessControlType" /> value associated with this <see cref="T:System.Security.AccessControl.AccessRule" /> object.</returns>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x000B1378 File Offset: 0x000AF578
		public AccessControlType AccessControlType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0400168D RID: 5773
		private AccessControlType type;
	}
}
