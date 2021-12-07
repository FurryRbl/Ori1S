using System;

namespace System.Security.AccessControl
{
	/// <summary>These flags affect the security descriptor behavior.</summary>
	// Token: 0x02000567 RID: 1383
	[Flags]
	public enum ControlFlags
	{
		/// <summary>No control flags.</summary>
		// Token: 0x040016CF RID: 5839
		None = 0,
		/// <summary>Specifies that the owner <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.  </summary>
		// Token: 0x040016D0 RID: 5840
		OwnerDefaulted = 1,
		/// <summary>Specifies that the group <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.</summary>
		// Token: 0x040016D1 RID: 5841
		GroupDefaulted = 2,
		/// <summary>Specifies that the DACL is not null. Set by resource managers or users.  </summary>
		// Token: 0x040016D2 RID: 5842
		DiscretionaryAclPresent = 4,
		/// <summary>Specifies that the DACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x040016D3 RID: 5843
		DiscretionaryAclDefaulted = 8,
		/// <summary>Specifies that the SACL is not null. Set by resource managers or users.</summary>
		// Token: 0x040016D4 RID: 5844
		SystemAclPresent = 16,
		/// <summary>Specifies that the SACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x040016D5 RID: 5845
		SystemAclDefaulted = 32,
		/// <summary>Ignored.</summary>
		// Token: 0x040016D6 RID: 5846
		DiscretionaryAclUntrusted = 64,
		/// <summary>Ignored.</summary>
		// Token: 0x040016D7 RID: 5847
		ServerSecurity = 128,
		/// <summary>Ignored.</summary>
		// Token: 0x040016D8 RID: 5848
		DiscretionaryAclAutoInheritRequired = 256,
		/// <summary>Ignored.</summary>
		// Token: 0x040016D9 RID: 5849
		SystemAclAutoInheritRequired = 512,
		/// <summary>Specifies that the Discretionary Access Control List (DACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x040016DA RID: 5850
		DiscretionaryAclAutoInherited = 1024,
		/// <summary>Specifies that the System Access Control List (SACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x040016DB RID: 5851
		SystemAclAutoInherited = 2048,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.  </summary>
		// Token: 0x040016DC RID: 5852
		DiscretionaryAclProtected = 4096,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.</summary>
		// Token: 0x040016DD RID: 5853
		SystemAclProtected = 8192,
		/// <summary>Specifies that the contents of the Reserved field are valid.</summary>
		// Token: 0x040016DE RID: 5854
		RMControlValid = 16384,
		/// <summary>Specifies that the security descriptor binary representation is in the self-relative format.  This flag is always set.</summary>
		// Token: 0x040016DF RID: 5855
		SelfRelative = 32768
	}
}
