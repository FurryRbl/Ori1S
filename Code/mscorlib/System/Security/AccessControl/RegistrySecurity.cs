using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a registry key. This class cannot be inherited.</summary>
	// Token: 0x02000590 RID: 1424
	public sealed class RegistrySecurity : NativeObjectSecurity
	{
		/// <summary>Gets the enumeration type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryRights" /> enumeration.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000B2874 File Offset: 0x000B0A74
		public override Type AccessRightType
		{
			get
			{
				return typeof(RegistryRights);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> class.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000B2880 File Offset: 0x000B0A80
		public override Type AccessRuleType
		{
			get
			{
				return typeof(RegistryAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> class.</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x000B288C File Offset: 0x000B0A8C
		public override Type AuditRuleType
		{
			get
			{
				return typeof(RegistryAuditRule);
			}
		}

		/// <summary>Creates a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">A Boolean value specifying whether the rule is inherited.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values specifying how the rule is inherited by subkeys.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that modify the way the rule is inherited by subkeys. Meaningless if the value of <paramref name="inheritanceFlags" /> is <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06003719 RID: 14105 RVA: 0x000B2898 File Offset: 0x000B0A98
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new RegistryAccessRule(identityReference, (RegistryRights)accessMask, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Searches for a matching access control with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600371A RID: 14106 RVA: 0x000B28A8 File Offset: 0x000B0AA8
		public void AddAccessRule(RegistryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x0600371B RID: 14107 RVA: 0x000B28B0 File Offset: 0x000B0AB0
		public void AddAuditRule(RegistryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, the inheritance and propagation of the rule, and the outcome that triggers the rule.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> object representing the specified audit rule for the specified user, with the specified flags. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the access rights to audit, cast to an integer.</param>
		/// <param name="isInherited">A Boolean value specifying whether the rule is inherited.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values specifying how the rule is inherited by subkeys.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that modify the way the rule is inherited by subkeys. Meaningless if the value of <paramref name="inheritanceFlags" /> is <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values specifying whether to audit successful access, failed access, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600371C RID: 14108 RVA: 0x000B28B8 File Offset: 0x000B0AB8
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new RegistryAuditRule(identityReference, (RegistryRights)accessMask, inheritanceFlags, propagationFlags, flags);
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified access rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600371D RID: 14109 RVA: 0x000B28C8 File Offset: 0x000B0AC8
		public bool RemoveAccessRule(RegistryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights, inheritance flags, or propagation flags specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600371E RID: 14110 RVA: 0x000B28D0 File Offset: 0x000B0AD0
		public void RemoveAccessRuleAll(RegistryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600371F RID: 14111 RVA: 0x000B28D8 File Offset: 0x000B0AD8
		public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise, false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06003720 RID: 14112 RVA: 0x000B28E0 File Offset: 0x000B0AE0
		public bool RemoveAuditRule(RegistryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> that specifies the user to search for. Any rights, inheritance flags, or propagation flags specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06003721 RID: 14113 RVA: 0x000B28E8 File Offset: 0x000B0AE8
		public void RemoveAuditRuleAll(RegistryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06003722 RID: 14114 RVA: 0x000B28F0 File Offset: 0x000B0AF0
		public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		// Token: 0x06003723 RID: 14115 RVA: 0x000B28F8 File Offset: 0x000B0AF8
		public void ResetAccessRule(RegistryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06003724 RID: 14116 RVA: 0x000B2900 File Offset: 0x000B0B00
		public void SetAccessRule(RegistryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06003725 RID: 14117 RVA: 0x000B2908 File Offset: 0x000B0B08
		public void SetAuditRule(RegistryAuditRule rule)
		{
			throw new NotImplementedException();
		}
	}
}
