using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x02000564 RID: 1380
	public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="rawSecurityDescriptor">The <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object from which to create the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		// Token: 0x060035CB RID: 13771 RVA: 0x000B1870 File Offset: 0x000AFA70
		public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="sddlForm">The SDDL string from which to create the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		// Token: 0x060035CC RID: 13772 RVA: 0x000B1880 File Offset: 0x000AFA80
		public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified array of byte values.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="binaryForm">The array of byte values from which to create the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="offset">The offset in the <paramref name="binaryForm" /> array at which to begin copying.</param>
		// Token: 0x060035CD RID: 13773 RVA: 0x000B1890 File Offset: 0x000AFA90
		public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified information.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="flags">Flags that specify behavior of the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="owner">The owner for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="group">The primary group for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="systemAcl">The System Access Control List (SACL) for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="discretionaryAcl">The Discretionary Access Control List (DACL) for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		// Token: 0x060035CE RID: 13774 RVA: 0x000B18A0 File Offset: 0x000AFAA0
		public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.isContainer = isContainer;
			this.isDS = isDS;
			this.flags = flags;
			this.owner = owner;
			this.group = group;
			this.systemAcl = systemAcl;
			this.discretionaryAcl = discretionaryAcl;
			throw new NotImplementedException();
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000B18F0 File Offset: 0x000AFAF0
		public override ControlFlags ControlFlags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets or sets the discretionary access control list (DACL) for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. The DACL contains access rules.</summary>
		/// <returns>The DACL for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000B18F8 File Offset: 0x000AFAF8
		// (set) Token: 0x060035D1 RID: 13777 RVA: 0x000B1900 File Offset: 0x000AFB00
		public DiscretionaryAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionaryAcl;
			}
			set
			{
				if (value == null)
				{
				}
				this.discretionaryAcl = value;
			}
		}

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000B1910 File Offset: 0x000AFB10
		// (set) Token: 0x060035D3 RID: 13779 RVA: 0x000B1918 File Offset: 0x000AFB18
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a container object.</summary>
		/// <returns>true if the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a container object; otherwise, false.</returns>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x000B1924 File Offset: 0x000AFB24
		public bool IsContainer
		{
			get
			{
				return this.isContainer;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order.</summary>
		/// <returns>true if the DACL associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order; otherwise, false.</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000B192C File Offset: 0x000AFB2C
		public bool IsDiscretionaryAclCanonical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a directory object.</summary>
		/// <returns>true if the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a directory object; otherwise, false.</returns>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000B1934 File Offset: 0x000AFB34
		public bool IsDS
		{
			get
			{
				return this.isDS;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order.</summary>
		/// <returns>true if the SACL associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order; otherwise, false.</returns>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000B193C File Offset: 0x000AFB3C
		public bool IsSystemAclCanonical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x000B1944 File Offset: 0x000AFB44
		// (set) Token: 0x060035D9 RID: 13785 RVA: 0x000B194C File Offset: 0x000AFB4C
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		/// <summary>Gets or sets the System Access Control List (SACL) for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. The SACL contains audit rules.</summary>
		/// <returns>The SACL for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x000B1958 File Offset: 0x000AFB58
		// (set) Token: 0x060035DB RID: 13787 RVA: 0x000B1960 File Offset: 0x000AFB60
		public SystemAcl SystemAcl
		{
			get
			{
				return this.systemAcl;
			}
			set
			{
				this.systemAcl = value;
			}
		}

		/// <summary>Removes all access rules for the specified security identifier from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <param name="sid">The security identifier for which to remove access rules.</param>
		// Token: 0x060035DC RID: 13788 RVA: 0x000B196C File Offset: 0x000AFB6C
		public void PurgeAccessControl(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules for the specified security identifier from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <param name="sid">The security identifier for which to remove audit rules.</param>
		// Token: 0x060035DD RID: 13789 RVA: 0x000B1974 File Offset: 0x000AFB74
		public void PurgeAudit(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the inheritance protection for the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. DACLs that are protected do not inherit access rules from parent containers.</summary>
		/// <param name="isProtected">true to protect the DACL from inheritance.</param>
		/// <param name="preserveInheritance">true to keep inherited access rules in the DACL; false to remove inherited access rules from the DACL.</param>
		// Token: 0x060035DE RID: 13790 RVA: 0x000B197C File Offset: 0x000AFB7C
		public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the inheritance protection for the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. SACLs that are protected do not inherit audit rules from parent containers.</summary>
		/// <param name="isProtected">true to protect the SACL from inheritance.</param>
		/// <param name="preserveInheritance">true to keep inherited audit rules in the SACL; false to remove inherited audit rules from the SACL.</param>
		// Token: 0x060035DF RID: 13791 RVA: 0x000B1984 File Offset: 0x000AFB84
		public void SetSystemAclProtection(bool isProtected, bool preserveInheritance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040016C4 RID: 5828
		private bool isContainer;

		// Token: 0x040016C5 RID: 5829
		private bool isDS;

		// Token: 0x040016C6 RID: 5830
		private ControlFlags flags;

		// Token: 0x040016C7 RID: 5831
		private SecurityIdentifier owner;

		// Token: 0x040016C8 RID: 5832
		private SecurityIdentifier group;

		// Token: 0x040016C9 RID: 5833
		private SystemAcl systemAcl;

		// Token: 0x040016CA RID: 5834
		private DiscretionaryAcl discretionaryAcl;
	}
}
