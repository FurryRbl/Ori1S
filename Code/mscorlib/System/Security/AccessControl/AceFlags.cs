using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the inheritance and auditing behavior of an access control entry (ACE).</summary>
	// Token: 0x0200055A RID: 1370
	[Flags]
	public enum AceFlags : byte
	{
		/// <summary>No ACE flags are set.</summary>
		// Token: 0x04001691 RID: 5777
		None = 0,
		/// <summary>The access mask is propagated onto child leaf objects.</summary>
		// Token: 0x04001692 RID: 5778
		ObjectInherit = 1,
		/// <summary>The access mask is propagated to child container objects.</summary>
		// Token: 0x04001693 RID: 5779
		ContainerInherit = 2,
		/// <summary>The access checks do not apply to the object; they only apply to its children.</summary>
		// Token: 0x04001694 RID: 5780
		NoPropagateInherit = 4,
		/// <summary>The access mask is propagated only to child objects. This includes both container and leaf child objects.</summary>
		// Token: 0x04001695 RID: 5781
		InheritOnly = 8,
		/// <summary>A logical OR of <see cref="F:System.Security.AccessControl.AceFlags.ObjectInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.ContainerInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.NoPropagateInherit" />, and <see cref="F:System.Security.AccessControl.AceFlags.InheritOnly" />.</summary>
		// Token: 0x04001696 RID: 5782
		InheritanceFlags = 15,
		/// <summary>An ACE is inherited from a parent container rather than being explicitly set for an object.</summary>
		// Token: 0x04001697 RID: 5783
		Inherited = 16,
		/// <summary>Successful access attempts are audited.</summary>
		// Token: 0x04001698 RID: 5784
		SuccessfulAccess = 64,
		/// <summary>Failed access attempts are audited.</summary>
		// Token: 0x04001699 RID: 5785
		FailedAccess = 128,
		/// <summary>All access attempts are audited.</summary>
		// Token: 0x0400169A RID: 5786
		AuditFlags = 192
	}
}
