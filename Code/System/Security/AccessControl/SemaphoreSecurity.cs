using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a named semaphore. This class cannot be inherited.</summary>
	// Token: 0x02000429 RID: 1065
	[ComVisible(false)]
	public sealed class SemaphoreSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class with default values.</summary>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06002672 RID: 9842 RVA: 0x000774B8 File Offset: 0x000756B8
		public SemaphoreSecurity() : base(false, ResourceType.Unknown)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class with the specified sections of the access control security rules from the system semaphore with the specified name.</summary>
		/// <param name="name">The name of the system semaphore whose access control security rules are to be retrieved.</param>
		/// <param name="includeSections">A combination of <see cref="T:System.Security.AccessControl.AccessControlSections" /> flags specifying the sections to retrieve.</param>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06002673 RID: 9843 RVA: 0x000774C4 File Offset: 0x000756C4
		public SemaphoreSecurity(string name, AccessControlSections includesections) : base(false, ResourceType.Unknown, name, includesections)
		{
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreRights" /> enumeration.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000774D0 File Offset: 0x000756D0
		public override Type AccessRightType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> class.</returns>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000774D8 File Offset: 0x000756D8
		public override Type AccessRuleType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> class.</returns>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x000774E0 File Offset: 0x000756E0
		public override Type AuditRuleType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06002677 RID: 9847 RVA: 0x000774E8 File Offset: 0x000756E8
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for a matching rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06002678 RID: 9848 RVA: 0x000774F0 File Offset: 0x000756F0
		public void AddAccessRule(SemaphoreAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x06002679 RID: 9849 RVA: 0x000774F8 File Offset: 0x000756F8
		public void AddAuditRule(SemaphoreAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, and the outcome that triggers the audit rule.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> object representing the specified audit rule for the specified user. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the access rights to audit, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specify whether to audit successful access, failed access, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600267A RID: 9850 RVA: 0x00077500 File Offset: 0x00075700
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600267B RID: 9851 RVA: 0x00077508 File Offset: 0x00075708
		public bool RemoveAccessRule(SemaphoreAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600267C RID: 9852 RVA: 0x00077510 File Offset: 0x00075710
		public void RemoveAccessRuleAll(SemaphoreAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600267D RID: 9853 RVA: 0x00077518 File Offset: 0x00075718
		public void RemoveAccessRuleSpecific(SemaphoreAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise, false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600267E RID: 9854 RVA: 0x00077520 File Offset: 0x00075720
		public bool RemoveAuditRule(SemaphoreAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> that specifies the user to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x0600267F RID: 9855 RVA: 0x00077528 File Offset: 0x00075728
		public void RemoveAuditRuleAll(SemaphoreAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06002680 RID: 9856 RVA: 0x00077530 File Offset: 0x00075730
		public void RemoveAuditRuleSpecific(SemaphoreAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06002681 RID: 9857 RVA: 0x00077538 File Offset: 0x00075738
		public void ResetAccessRule(SemaphoreAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06002682 RID: 9858 RVA: 0x00077540 File Offset: 0x00075740
		public void SetAccessRule(SemaphoreAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06002683 RID: 9859 RVA: 0x00077548 File Offset: 0x00075748
		public void SetAuditRule(SemaphoreAuditRule rule)
		{
			throw new NotImplementedException();
		}
	}
}
