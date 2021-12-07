using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the section of a security descriptor to be queried or set.</summary>
	// Token: 0x02000592 RID: 1426
	[Flags]
	public enum SecurityInfos
	{
		/// <summary>Specifies the owner identifier.</summary>
		// Token: 0x0400176A RID: 5994
		Owner = 1,
		/// <summary>Specifies the primary group identifier.</summary>
		// Token: 0x0400176B RID: 5995
		Group = 2,
		/// <summary>Specifies the discretionary access control list (DACL).</summary>
		// Token: 0x0400176C RID: 5996
		DiscretionaryAcl = 4,
		/// <summary>Specifies the system access control list (SACL).</summary>
		// Token: 0x0400176D RID: 5997
		SystemAcl = 8
	}
}
