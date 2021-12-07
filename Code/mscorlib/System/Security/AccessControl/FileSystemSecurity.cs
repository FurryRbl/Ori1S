using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents the access control and audit security for a file or directory.</summary>
	// Token: 0x02000578 RID: 1400
	public abstract class FileSystemSecurity : NativeObjectSecurity
	{
		// Token: 0x06003645 RID: 13893 RVA: 0x000B1F00 File Offset: 0x000B0100
		internal FileSystemSecurity(bool isContainer) : base(isContainer, ResourceType.FileObject)
		{
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000B1F0C File Offset: 0x000B010C
		internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections) : base(isContainer, ResourceType.FileObject, name, includeSections)
		{
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemRights" /> enumeration.</returns>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000B1F18 File Offset: 0x000B0118
		public override Type AccessRightType
		{
			get
			{
				return typeof(FileSystemRights);
			}
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class.</returns>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x000B1F24 File Offset: 0x000B0124
		public override Type AccessRuleType
		{
			get
			{
				return typeof(FileSystemAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class.</returns>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x000B1F30 File Offset: 0x000B0130
		public override Type AuditRuleType
		{
			get
			{
				return typeof(FileSystemAuditRule);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">true if the access rule is inherited; otherwise, false.    </param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether access is allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> parameters specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> parameter is null. -or-The <paramref name="accessMask" /> parameter is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> parameter is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600364A RID: 13898 RVA: 0x000B1F3C File Offset: 0x000B013C
		[MonoTODO]
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new FileSystemAccessRule(identityReference, (FileSystemRights)accessMask, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x0600364B RID: 13899 RVA: 0x000B1F4C File Offset: 0x000B014C
		[MonoTODO]
		public void AddAccessRule(FileSystemAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all matching allow or deny access control list (ACL) permissions from the current file or directory.</summary>
		/// <returns>true if the access rule was removed; otherwise, false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x0600364C RID: 13900 RVA: 0x000B1F54 File Offset: 0x000B0154
		[MonoTODO]
		public bool RemoveAccessRule(FileSystemAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access control list (ACL) permissions for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x0600364D RID: 13901 RVA: 0x000B1F5C File Offset: 0x000B015C
		[MonoTODO]
		public void RemoveAccessRuleAll(FileSystemAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a single matching allow or deny access control list (ACL) permission from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x0600364E RID: 13902 RVA: 0x000B1F64 File Offset: 0x000B0164
		[MonoTODO]
		public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory and removes all matching ACL permissions.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x0600364F RID: 13903 RVA: 0x000B1F6C File Offset: 0x000B016C
		[MonoTODO]
		public void ResetAccessRule(FileSystemAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the specified access control list (ACL) permission for the current file or directory. </summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06003650 RID: 13904 RVA: 0x000B1F74 File Offset: 0x000B0174
		[MonoTODO]
		public void SetAccessRule(FileSystemAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class representing the specified audit rule for the specified user.</summary>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object representing the specified audit rule for the specified user.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">true if the access rule is inherited; otherwise, false.    </param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies the type of auditing to perform.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> properties specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> property is null. -or-The <paramref name="accessMask" /> property is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> property is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06003651 RID: 13905 RVA: 0x000B1F7C File Offset: 0x000B017C
		[MonoTODO]
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new FileSystemAuditRule(identityReference, (FileSystemRights)accessMask, inheritanceFlags, propagationFlags, flags);
		}

		/// <summary>Adds the specified audit rule to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  object that represents an audit rule to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06003652 RID: 13906 RVA: 0x000B1F8C File Offset: 0x000B018C
		[MonoTODO]
		public void AddAuditRule(FileSystemAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all matching allow or deny audit rules from the current file or directory.</summary>
		/// <returns>true if the audit rule was removed; otherwise, false</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  object that represents an audit rule to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06003653 RID: 13907 RVA: 0x000B1F94 File Offset: 0x000B0194
		[MonoTODO]
		public bool RemoveAuditRule(FileSystemAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that specifies a user whose audit rules should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06003654 RID: 13908 RVA: 0x000B1F9C File Offset: 0x000B019C
		[MonoTODO]
		public void RemoveAuditRuleAll(FileSystemAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a single matching allow or deny audit rule from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  object that represents an audit rule to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06003655 RID: 13909 RVA: 0x000B1FA4 File Offset: 0x000B01A4
		[MonoTODO]
		public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the specified audit rule for the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06003656 RID: 13910 RVA: 0x000B1FAC File Offset: 0x000B01AC
		[MonoTODO]
		public void SetAuditRule(FileSystemAuditRule rule)
		{
			throw new NotImplementedException();
		}
	}
}
