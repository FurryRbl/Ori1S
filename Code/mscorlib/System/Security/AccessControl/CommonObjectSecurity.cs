using System;
using System.Collections.Generic;

namespace System.Security.AccessControl
{
	/// <summary>Controls access to objects without direct manipulation of access control lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class.</summary>
	// Token: 0x02000563 RID: 1379
	[MonoTODO("required for NativeObjectSecurity - implementation is missing")]
	public abstract class CommonObjectSecurity : ObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> class.</summary>
		/// <param name="isContainer">true if the new object is a container object.</param>
		// Token: 0x060035BB RID: 13755 RVA: 0x000B15F8 File Offset: 0x000AF7F8
		protected CommonObjectSecurity(bool isContainer) : base(isContainer, false)
		{
		}

		/// <summary>Gets a collection of the access rules associated with the specified security identifier.</summary>
		/// <returns>The collection of access rules associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <param name="includeExplicit">true to include access rules explicitly set for the object.</param>
		/// <param name="includeInherited">true to include inherited access rules.</param>
		/// <param name="targetType">Specifies whether the security identifier for which to retrieve access rules is of type T:System.Security.Principal.SecurityIdentifier or type T:System.Security.Principal.NTAccount. The value of this parameter must be a type that can be translated to  the <see cref="T:System.Security.Principal.SecurityIdentifier" /> type.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060035BC RID: 13756 RVA: 0x000B1618 File Offset: 0x000AF818
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
		// Token: 0x060035BD RID: 13757 RVA: 0x000B1620 File Offset: 0x000AF820
		public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the specified access rule to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		// Token: 0x060035BE RID: 13758 RVA: 0x000B1628 File Offset: 0x000AF828
		protected void AddAccessRule(AccessRule rule)
		{
			this.access_rules.Add(rule);
			base.AccessRulesModified = true;
		}

		/// <summary>Removes access rules that contain the same security identifier and access mask as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the access rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060035BF RID: 13759 RVA: 0x000B1640 File Offset: 0x000AF840
		protected bool RemoveAccessRule(AccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that have the same security identifier as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060035C0 RID: 13760 RVA: 0x000B1648 File Offset: 0x000AF848
		protected void RemoveAccessRuleAll(AccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060035C1 RID: 13761 RVA: 0x000B1650 File Offset: 0x000AF850
		protected void RemoveAccessRuleSpecific(AccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to reset.</param>
		// Token: 0x060035C2 RID: 13762 RVA: 0x000B1658 File Offset: 0x000AF858
		protected void ResetAccessRule(AccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that contain the same security identifier and qualifier as the specified access rule in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to set.</param>
		// Token: 0x060035C3 RID: 13763 RVA: 0x000B1660 File Offset: 0x000AF860
		protected void SetAccessRule(AccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x060035C4 RID: 13764 RVA: 0x000B1668 File Offset: 0x000AF868
		protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			foreach (AccessRule accessRule in this.access_rules)
			{
				if (rule == accessRule)
				{
					switch (modification)
					{
					case AccessControlModification.Add:
						this.AddAccessRule(rule);
						break;
					case AccessControlModification.Set:
						this.SetAccessRule(rule);
						break;
					case AccessControlModification.Reset:
						this.ResetAccessRule(rule);
						break;
					case AccessControlModification.Remove:
						this.RemoveAccessRule(rule);
						break;
					case AccessControlModification.RemoveAll:
						this.RemoveAccessRuleAll(rule);
						break;
					case AccessControlModification.RemoveSpecific:
						this.RemoveAccessRuleSpecific(rule);
						break;
					}
					modified = true;
					return true;
				}
			}
			modified = false;
			return false;
		}

		/// <summary>Adds the specified audit rule to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		// Token: 0x060035C5 RID: 13765 RVA: 0x000B1754 File Offset: 0x000AF954
		protected void AddAuditRule(AuditRule rule)
		{
			this.audit_rules.Add(rule);
			base.AuditRulesModified = true;
		}

		/// <summary>Removes audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the audit rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060035C6 RID: 13766 RVA: 0x000B176C File Offset: 0x000AF96C
		protected bool RemoveAuditRule(AuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060035C7 RID: 13767 RVA: 0x000B1774 File Offset: 0x000AF974
		protected void RemoveAuditRuleAll(AuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060035C8 RID: 13768 RVA: 0x000B177C File Offset: 0x000AF97C
		protected void RemoveAuditRuleSpecific(AuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that contain the same security identifier and qualifier as the specified audit rule in the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified audit rule.</summary>
		/// <param name="rule">The audit rule to set.</param>
		// Token: 0x060035C9 RID: 13769 RVA: 0x000B1784 File Offset: 0x000AF984
		protected void SetAuditRule(AuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x060035CA RID: 13770 RVA: 0x000B178C File Offset: 0x000AF98C
		protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			foreach (AuditRule auditRule in this.audit_rules)
			{
				if (rule == auditRule)
				{
					switch (modification)
					{
					case AccessControlModification.Add:
						this.AddAuditRule(rule);
						break;
					case AccessControlModification.Set:
						this.SetAuditRule(rule);
						break;
					case AccessControlModification.Remove:
						this.RemoveAuditRule(rule);
						break;
					case AccessControlModification.RemoveAll:
						this.RemoveAuditRuleAll(rule);
						break;
					case AccessControlModification.RemoveSpecific:
						this.RemoveAuditRuleSpecific(rule);
						break;
					}
					base.AuditRulesModified = true;
					modified = true;
					return true;
				}
			}
			modified = false;
			return false;
		}

		// Token: 0x040016C2 RID: 5826
		private List<AccessRule> access_rules = new List<AccessRule>();

		// Token: 0x040016C3 RID: 5827
		private List<AuditRule> audit_rules = new List<AuditRule>();
	}
}
