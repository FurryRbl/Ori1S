using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to objects without direct manipulation of Access Control Lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> and <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> classes.</summary>
	// Token: 0x02000587 RID: 1415
	public abstract class ObjectSecurity
	{
		// Token: 0x060036BA RID: 14010 RVA: 0x000B24A4 File Offset: 0x000B06A4
		internal ObjectSecurity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</param>
		/// <param name="isDS">True if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</param>
		// Token: 0x060036BB RID: 14011 RVA: 0x000B24AC File Offset: 0x000B06AC
		protected ObjectSecurity(bool isContainer, bool isDS)
		{
			this.is_container = isContainer;
			this.is_ds = isDS;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>The type of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060036BC RID: 14012
		public abstract Type AccessRightType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060036BD RID: 14013
		public abstract Type AccessRuleType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060036BE RID: 14014
		public abstract Type AuditRuleType { get; }

		/// <summary>Gets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>true if the access rules are in canonical order; otherwise, false.</returns>
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000B24C4 File Offset: 0x000B06C4
		[MonoTODO]
		public bool AreAccessRulesCanonical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the DACL is protected; otherwise, false.</returns>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000B24CC File Offset: 0x000B06CC
		[MonoTODO]
		public bool AreAccessRulesProtected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>true if the audit rules are in canonical order; otherwise, false.</returns>
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000B24D4 File Offset: 0x000B06D4
		[MonoTODO]
		public bool AreAuditRulesCanonical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the SACL is protected; otherwise, false.</returns>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x000B24DC File Offset: 0x000B06DC
		[MonoTODO]
		public bool AreAuditRulesProtected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>true if the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, false.</returns>
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000B24E4 File Offset: 0x000B06E4
		// (set) Token: 0x060036C4 RID: 14020 RVA: 0x000B24EC File Offset: 0x000B06EC
		protected bool AccessRulesModified
		{
			get
			{
				return this.access_rules_modified;
			}
			set
			{
				this.access_rules_modified = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>true if the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, false.</returns>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000B24F8 File Offset: 0x000B06F8
		// (set) Token: 0x060036C6 RID: 14022 RVA: 0x000B2500 File Offset: 0x000B0700
		protected bool AuditRulesModified
		{
			get
			{
				return this.audit_rules_modified;
			}
			set
			{
				this.audit_rules_modified = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the group associated with the securable object has been modified. </summary>
		/// <returns>true if the group associated with the securable object has been modified; otherwise, false.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000B250C File Offset: 0x000B070C
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x000B2514 File Offset: 0x000B0714
		protected bool GroupModified
		{
			get
			{
				return this.group_modified;
			}
			set
			{
				this.group_modified = value;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object; otherwise, false.</returns>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000B2520 File Offset: 0x000B0720
		protected bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object; otherwise, false.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000B2528 File Offset: 0x000B0728
		protected bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the owner of the securable object has been modified.</summary>
		/// <returns>true if the owner of the securable object has been modified; otherwise, false.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000B2530 File Offset: 0x000B0730
		// (set) Token: 0x060036CC RID: 14028 RVA: 0x000B2538 File Offset: 0x000B0738
		protected bool OwnerModified
		{
			get
			{
				return this.owner_modified;
			}
			set
			{
				this.owner_modified = value;
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
		// Token: 0x060036CD RID: 14029
		public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the audit rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited audit rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">Specifies the conditions for which the rule is audited.</param>
		// Token: 0x060036CE RID: 14030
		public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);

		/// <summary>Gets the primary group associated with the specified owner.</summary>
		/// <returns>The primary group associated with the specified owner.</returns>
		/// <param name="targetType">The owner for which to get the primary group. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060036CF RID: 14031 RVA: 0x000B2544 File Offset: 0x000B0744
		[MonoTODO]
		public IdentityReference GetGroup(Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the owner associated with the specified primary group.</summary>
		/// <returns>The owner associated with the specified group.</returns>
		/// <param name="targetType">The primary group for which to get the owner.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060036D0 RID: 14032 RVA: 0x000B254C File Offset: 0x000B074C
		[MonoTODO]
		public IdentityReference GetOwner(Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an array of byte values that represents the security descriptor information for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>An array of byte values that represents the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. This method returns null if there is no security information in this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x060036D1 RID: 14033 RVA: 0x000B2554 File Offset: 0x000B0754
		[MonoTODO]
		public byte[] GetSecurityDescriptorBinaryForm()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the Security Descriptor Definition Language (SDDL) representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>The SDDL representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		/// <param name="includeSections">Specifies which sections (access rules, audit rules, primary group, owner) of the security descriptor to get.</param>
		// Token: 0x060036D2 RID: 14034 RVA: 0x000B255C File Offset: 0x000B075C
		[MonoTODO]
		public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a Boolean value that specifies whether the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format.</summary>
		/// <returns>true if the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format; otherwise, false.</returns>
		// Token: 0x060036D3 RID: 14035 RVA: 0x000B2564 File Offset: 0x000B0764
		[MonoTODO]
		public static bool IsSddlConversionSupported()
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x060036D4 RID: 14036 RVA: 0x000B256C File Offset: 0x000B076C
		[MonoTODO]
		public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x060036D5 RID: 14037 RVA: 0x000B2574 File Offset: 0x000B0774
		[MonoTODO]
		public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all access rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All access rules are not in canonical order.</exception>
		// Token: 0x060036D6 RID: 14038 RVA: 0x000B257C File Offset: 0x000B077C
		[MonoTODO]
		public virtual void PurgeAccessRules(IdentityReference identity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all audit rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All audit rules are not in canonical order.</exception>
		// Token: 0x060036D7 RID: 14039 RVA: 0x000B2584 File Offset: 0x000B0784
		[MonoTODO]
		public virtual void PurgeAuditRules(IdentityReference identity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets or removes protection of the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected access rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">true to protect the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; false to allow inheritance.</param>
		/// <param name="preserveInheritance">true to preserve inherited access rules; false to remove inherited access rules. This parameter is ignored if <paramref name="isProtected" /> is false.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical Discretionary Access Control List (DACL).</exception>
		// Token: 0x060036D8 RID: 14040 RVA: 0x000B258C File Offset: 0x000B078C
		[MonoTODO]
		public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets or removes protection of the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected audit rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">true to protect the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; false to allow inheritance.</param>
		/// <param name="preserveInheritance">true to preserve inherited audit rules; false to remove inherited audit rules. This parameter is ignored if <paramref name="isProtected" /> is false.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical System Access Control List (SACL).</exception>
		// Token: 0x060036D9 RID: 14041 RVA: 0x000B2594 File Offset: 0x000B0794
		[MonoTODO]
		public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the primary group for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The primary group to set.</param>
		// Token: 0x060036DA RID: 14042 RVA: 0x000B259C File Offset: 0x000B079C
		[MonoTODO]
		public void SetGroup(IdentityReference identity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the owner for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The owner to set.</param>
		// Token: 0x060036DB RID: 14043 RVA: 0x000B25A4 File Offset: 0x000B07A4
		[MonoTODO]
		public void SetOwner(IdentityReference identity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		// Token: 0x060036DC RID: 14044 RVA: 0x000B25AC File Offset: 0x000B07AC
		[MonoTODO]
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x060036DD RID: 14045 RVA: 0x000B25B4 File Offset: 0x000B07B4
		[MonoTODO]
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		// Token: 0x060036DE RID: 14046 RVA: 0x000B25BC File Offset: 0x000B07BC
		[MonoTODO]
		public void SetSecurityDescriptorSddlForm(string sddlForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x060036DF RID: 14047 RVA: 0x000B25C4 File Offset: 0x000B07C4
		[MonoTODO]
		public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x060036E0 RID: 14048
		protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x060036E1 RID: 14049
		protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="handle">The handle used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x060036E2 RID: 14050 RVA: 0x000B25CC File Offset: 0x000B07CC
		[MonoTODO]
		protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x060036E3 RID: 14051 RVA: 0x000B25D4 File Offset: 0x000B07D4
		[MonoTODO]
		protected virtual void Persist(string name, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="enableOwnershipPrivilege">true to enable the privilege that allows the caller to take ownership of the object.</param>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x060036E4 RID: 14052 RVA: 0x000B25DC File Offset: 0x000B07DC
		[MonoTODO]
		protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x060036E5 RID: 14053 RVA: 0x000B25E4 File Offset: 0x000B07E4
		[MonoTODO]
		protected void ReadLock()
		{
			throw new NotImplementedException();
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x060036E6 RID: 14054 RVA: 0x000B25EC File Offset: 0x000B07EC
		[MonoTODO]
		protected void ReadUnlock()
		{
			throw new NotImplementedException();
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x060036E7 RID: 14055 RVA: 0x000B25F4 File Offset: 0x000B07F4
		[MonoTODO]
		protected void WriteLock()
		{
			throw new NotImplementedException();
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x060036E8 RID: 14056 RVA: 0x000B25FC File Offset: 0x000B07FC
		[MonoTODO]
		protected void WriteUnlock()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400173B RID: 5947
		private bool is_container;

		// Token: 0x0400173C RID: 5948
		private bool is_ds;

		// Token: 0x0400173D RID: 5949
		private bool access_rules_modified;

		// Token: 0x0400173E RID: 5950
		private bool audit_rules_modified;

		// Token: 0x0400173F RID: 5951
		private bool group_modified;

		// Token: 0x04001740 RID: 5952
		private bool owner_modified;
	}
}
