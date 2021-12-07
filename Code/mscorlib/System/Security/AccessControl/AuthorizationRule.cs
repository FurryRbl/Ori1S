using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Determines access to securable objects. The derived classes <see cref="T:System.Security.AccessControl.AccessRule" /> and <see cref="T:System.Security.AccessControl.AuditRule" /> offer specializations for access and audit functionality.</summary>
	// Token: 0x0200055F RID: 1375
	public abstract class AuthorizationRule
	{
		// Token: 0x060035A0 RID: 13728 RVA: 0x000B1444 File Offset: 0x000AF644
		internal AuthorizationRule()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AuthorizationControl.AccessRule" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.  This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true to inherit this rule from a parent container.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is zero, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x060035A1 RID: 13729 RVA: 0x000B144C File Offset: 0x000AF64C
		protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			if (!(identity is SecurityIdentifier))
			{
				throw new ArgumentException("identity");
			}
			if (accessMask == 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.identity = identity;
			this.accessMask = accessMask;
			this.isInherited = isInherited;
			this.inheritanceFlags = inheritanceFlags;
			this.propagationFlags = propagationFlags;
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</returns>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000B14A8 File Offset: 0x000AF6A8
		public IdentityReference IdentityReference
		{
			get
			{
				return this.identity;
			}
		}

		/// <summary>Gets the value of flags that determine how this rule is inherited by child objects.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000B14B0 File Offset: 0x000AF6B0
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				return this.inheritanceFlags;
			}
		}

		/// <summary>Gets a value indicating whether this rule is explicitly set or is inherited from a parent container object.</summary>
		/// <returns>true if this rule is not explicitly set but is instead inherited from a parent container.</returns>
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000B14B8 File Offset: 0x000AF6B8
		public bool IsInherited
		{
			get
			{
				return this.isInherited;
			}
		}

		/// <summary>Gets the value of the propagation flags, which determine how inheritance of this rule is propagated to child objects. This property is significant only when the value of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> enumeration is not <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000B14C0 File Offset: 0x000AF6C0
		public PropagationFlags PropagationFlags
		{
			get
			{
				return this.propagationFlags;
			}
		}

		/// <summary>Gets the access mask for this rule.</summary>
		/// <returns>The access mask for this rule.</returns>
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000B14C8 File Offset: 0x000AF6C8
		protected internal int AccessMask
		{
			get
			{
				return this.accessMask;
			}
		}

		// Token: 0x040016B8 RID: 5816
		private IdentityReference identity;

		// Token: 0x040016B9 RID: 5817
		private int accessMask;

		// Token: 0x040016BA RID: 5818
		private bool isInherited;

		// Token: 0x040016BB RID: 5819
		private InheritanceFlags inheritanceFlags;

		// Token: 0x040016BC RID: 5820
		private PropagationFlags propagationFlags;
	}
}
