using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to a cryptographic key object without direct manipulation of  an Access Control List (ACL).</summary>
	// Token: 0x0200056B RID: 1387
	public sealed class CryptoKeySecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> class.</summary>
		// Token: 0x060035EB RID: 13803 RVA: 0x000B1A44 File Offset: 0x000AFC44
		[MonoTODO]
		public CryptoKeySecurity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> class by using the specified security descriptor.</summary>
		/// <param name="securityDescriptor">The security descriptor from which to create the new <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</param>
		// Token: 0x060035EC RID: 13804 RVA: 0x000B1A4C File Offset: 0x000AFC4C
		[MonoTODO]
		public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor)
		{
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <returns>The type of the securable object associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000B1A54 File Offset: 0x000AFC54
		public override Type AccessRightType
		{
			get
			{
				return typeof(CryptoKeyRights);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060035EE RID: 13806 RVA: 0x000B1A60 File Offset: 0x000AFC60
		public override Type AccessRuleType
		{
			get
			{
				return typeof(CryptoKeyAccessRule);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the audit rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000B1A6C File Offset: 0x000AFC6C
		public override Type AuditRuleType
		{
			get
			{
				return typeof(CryptoKeyAuditRule);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		// Token: 0x060035F0 RID: 13808 RVA: 0x000B1A78 File Offset: 0x000AFC78
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new CryptoKeyAccessRule(identityReference, (CryptoKeyRights)accessMask, type);
		}

		/// <summary>Adds the specified access rule to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		// Token: 0x060035F1 RID: 13809 RVA: 0x000B1A84 File Offset: 0x000AFC84
		[MonoTODO]
		public void AddAccessRule(CryptoKeyAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes access rules that contain the same security identifier and access mask as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <returns>true if the access rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060035F2 RID: 13810 RVA: 0x000B1A8C File Offset: 0x000AFC8C
		[MonoTODO]
		public bool RemoveAccessRule(CryptoKeyAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that have the same security identifier as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060035F3 RID: 13811 RVA: 0x000B1A94 File Offset: 0x000AFC94
		[MonoTODO]
		public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060035F4 RID: 13812 RVA: 0x000B1A9C File Offset: 0x000AFC9C
		[MonoTODO]
		public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to reset.</param>
		// Token: 0x060035F5 RID: 13813 RVA: 0x000B1AA4 File Offset: 0x000AFCA4
		[MonoTODO]
		public void ResetAccessRule(CryptoKeyAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that contain the same security identifier and qualifier as the specified access rule in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to set.</param>
		// Token: 0x060035F6 RID: 13814 RVA: 0x000B1AAC File Offset: 0x000AFCAC
		[MonoTODO]
		public void SetAccessRule(CryptoKeyAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the audit rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited audit rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">Specifies the conditions for which the rule is audited.</param>
		// Token: 0x060035F7 RID: 13815 RVA: 0x000B1AB4 File Offset: 0x000AFCB4
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new CryptoKeyAuditRule(identityReference, (CryptoKeyRights)accessMask, flags);
		}

		/// <summary>Adds the specified audit rule to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		// Token: 0x060035F8 RID: 13816 RVA: 0x000B1AC0 File Offset: 0x000AFCC0
		[MonoTODO]
		public void AddAuditRule(CryptoKeyAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <returns>true if the audit rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060035F9 RID: 13817 RVA: 0x000B1AC8 File Offset: 0x000AFCC8
		[MonoTODO]
		public bool RemoveAuditRule(CryptoKeyAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060035FA RID: 13818 RVA: 0x000B1AD0 File Offset: 0x000AFCD0
		[MonoTODO]
		public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060035FB RID: 13819 RVA: 0x000B1AD8 File Offset: 0x000AFCD8
		[MonoTODO]
		public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that contain the same security identifier and qualifier as the specified audit rule in the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object and then adds the specified audit rule.</summary>
		/// <param name="rule">The audit rule to set.</param>
		// Token: 0x060035FC RID: 13820 RVA: 0x000B1AE0 File Offset: 0x000AFCE0
		[MonoTODO]
		public void SetAuditRule(CryptoKeyAuditRule rule)
		{
			throw new NotImplementedException();
		}
	}
}
