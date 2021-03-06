using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to directory objects without direct manipulation of Access Control Lists (ACLs).</summary>
	// Token: 0x0200056D RID: 1389
	public abstract class DirectoryObjectSecurity : ObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> class.</summary>
		// Token: 0x06003603 RID: 13827 RVA: 0x000B1B60 File Offset: 0x000AFD60
		protected DirectoryObjectSecurity() : base(false, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> class with the specified security descriptor.</summary>
		/// <param name="securityDescriptor">The security descriptor to be associated with the new <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" />object.</param>
		// Token: 0x06003604 RID: 13828 RVA: 0x000B1B6C File Offset: 0x000AFD6C
		protected DirectoryObjectSecurity(CommonSecurityDescriptor securityDescriptor) : base(securityDescriptor != null && securityDescriptor.IsContainer, true)
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
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
		/// <param name="objectType">The identity of the class of objects to which the new access rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new access rule.</param>
		// Token: 0x06003605 RID: 13829 RVA: 0x000B1B98 File Offset: 0x000AFD98
		public virtual AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type, Guid objectType, Guid inheritedObjectType)
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
		/// <param name="objectType">The identity of the class of objects to which the new audit rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new audit rule.</param>
		// Token: 0x06003606 RID: 13830 RVA: 0x000B1BA0 File Offset: 0x000AFDA0
		public virtual AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags, Guid objectType, Guid inheritedObjectType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of the access rules associated with the specified security identifier.</summary>
		/// <returns>The collection of access rules associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <param name="includeExplicit">true to include access rules explicitly set for the object.</param>
		/// <param name="includeInherited">true to include inherited access rules.</param>
		/// <param name="targetType">The security identifier for which to retrieve access rules. This must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003607 RID: 13831 RVA: 0x000B1BA8 File Offset: 0x000AFDA8
		public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of the audit rules associated with the specified security identifier.</summary>
		/// <returns>The collection of audit rules associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <param name="includeExplicit">true to include audit rules explicitly set for the object.</param>
		/// <param name="includeInherited">true to include inherited audit rules.</param>
		/// <param name="targetType">The security identifier for which to retrieve audit rules. This must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003608 RID: 13832 RVA: 0x000B1BB0 File Offset: 0x000AFDB0
		public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the specified access rule to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		// Token: 0x06003609 RID: 13833 RVA: 0x000B1BB8 File Offset: 0x000AFDB8
		protected void AddAccessRule(ObjectAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the specified audit rule to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		// Token: 0x0600360A RID: 13834 RVA: 0x000B1BC0 File Offset: 0x000AFDC0
		protected void AddAuditRule(ObjectAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x0600360B RID: 13835 RVA: 0x000B1BC8 File Offset: 0x000AFDC8
		protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x0600360C RID: 13836 RVA: 0x000B1BD0 File Offset: 0x000AFDD0
		protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes access rules that contain the same security identifier and access mask as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <returns>true if the access rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x0600360D RID: 13837 RVA: 0x000B1BD8 File Offset: 0x000AFDD8
		protected bool RemoveAccessRule(ObjectAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that have the same security identifier as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x0600360E RID: 13838 RVA: 0x000B1BE0 File Offset: 0x000AFDE0
		protected void RemoveAccessRuleAll(ObjectAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x0600360F RID: 13839 RVA: 0x000B1BE8 File Offset: 0x000AFDE8
		protected void RemoveAccessRuleSpecific(ObjectAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the audit rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x06003610 RID: 13840 RVA: 0x000B1BF0 File Offset: 0x000AFDF0
		protected bool RemoveAuditRule(ObjectAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x06003611 RID: 13841 RVA: 0x000B1BF8 File Offset: 0x000AFDF8
		protected void RemoveAuditRuleAll(ObjectAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x06003612 RID: 13842 RVA: 0x000B1C00 File Offset: 0x000AFE00
		protected void RemoveAuditRuleSpecific(ObjectAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to reset.</param>
		// Token: 0x06003613 RID: 13843 RVA: 0x000B1C08 File Offset: 0x000AFE08
		protected void ResetAccessRule(ObjectAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that contain the same security identifier and qualifier as the specified access rule in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to set.</param>
		// Token: 0x06003614 RID: 13844 RVA: 0x000B1C10 File Offset: 0x000AFE10
		protected void SetAccessRule(ObjectAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that contain the same security identifier and qualifier as the specified audit rule in the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> object and then adds the specified audit rule.</summary>
		/// <param name="rule">The audit rule to set.</param>
		// Token: 0x06003615 RID: 13845 RVA: 0x000B1C18 File Offset: 0x000AFE18
		protected void SetAuditRule(ObjectAuditRule rule)
		{
			throw new NotImplementedException();
		}
	}
}
