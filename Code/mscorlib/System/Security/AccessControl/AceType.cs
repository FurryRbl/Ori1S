using System;

namespace System.Security.AccessControl
{
	/// <summary>Defines the available access control entry (ACE) types.</summary>
	// Token: 0x0200055C RID: 1372
	public enum AceType
	{
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x040016A1 RID: 5793
		AccessAllowed,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x040016A2 RID: 5794
		AccessDenied,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x040016A3 RID: 5795
		SystemAudit,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040016A4 RID: 5796
		SystemAlarm,
		/// <summary>Defined but never used. Included here for completeness.</summary>
		// Token: 0x040016A5 RID: 5797
		AccessAllowedCompound,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x040016A6 RID: 5798
		AccessAllowedObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x040016A7 RID: 5799
		AccessDeniedObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x040016A8 RID: 5800
		SystemAuditObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040016A9 RID: 5801
		SystemAlarmObject,
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type may contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x040016AA RID: 5802
		AccessAllowedCallback,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x040016AB RID: 5803
		AccessDeniedCallback,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type may contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x040016AC RID: 5804
		AccessAllowedCallbackObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x040016AD RID: 5805
		AccessDeniedCallbackObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x040016AE RID: 5806
		SystemAuditCallback,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040016AF RID: 5807
		SystemAlarmCallback,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x040016B0 RID: 5808
		SystemAuditCallbackObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040016B1 RID: 5809
		SystemAlarmCallbackObject,
		/// <summary>Tracks the maximum defined ACE type in the enumeration.</summary>
		// Token: 0x040016B2 RID: 5810
		MaxDefinedAceType = 16
	}
}
