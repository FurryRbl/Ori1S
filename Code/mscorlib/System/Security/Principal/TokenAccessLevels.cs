using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the privileges of the user account associated with the access token. </summary>
	// Token: 0x02000666 RID: 1638
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TokenAccessLevels
	{
		/// <summary>The user can attach a primary token to a process.</summary>
		// Token: 0x04001ABE RID: 6846
		AssignPrimary = 1,
		/// <summary>The user can duplicate the token.</summary>
		// Token: 0x04001ABF RID: 6847
		Duplicate = 2,
		/// <summary>The user can impersonate a client.</summary>
		// Token: 0x04001AC0 RID: 6848
		Impersonate = 4,
		/// <summary>The user can query the token.</summary>
		// Token: 0x04001AC1 RID: 6849
		Query = 8,
		/// <summary>The user can query the source of the token.</summary>
		// Token: 0x04001AC2 RID: 6850
		QuerySource = 16,
		/// <summary>The user can enable or disable privileges in the token.</summary>
		// Token: 0x04001AC3 RID: 6851
		AdjustPrivileges = 32,
		/// <summary>The user can change the attributes of the groups in the token.</summary>
		// Token: 0x04001AC4 RID: 6852
		AdjustGroups = 64,
		/// <summary>The user can change the default owner, primary group, or discretionary access control list (DACL) of the token.</summary>
		// Token: 0x04001AC5 RID: 6853
		AdjustDefault = 128,
		/// <summary>The user can adjust the session identifier of the token.</summary>
		// Token: 0x04001AC6 RID: 6854
		AdjustSessionId = 256,
		/// <summary>The user has standard read rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.Query" /> privilege for the token.</summary>
		// Token: 0x04001AC7 RID: 6855
		Read = 131080,
		/// <summary>The user has standard write rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustPrivileges, F:System.Security.Principal.TokenAccessLevels.AdjustGroups" />, and <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustDefault" /> privileges for the token.</summary>
		// Token: 0x04001AC8 RID: 6856
		Write = 131296,
		/// <summary>The user has all possible access to the token.</summary>
		// Token: 0x04001AC9 RID: 6857
		AllAccess = 983551,
		/// <summary>The maximum value that can be assigned for the <see cref="T:System.Security.Principal.TokenAccessLevels" /> enumeration.</summary>
		// Token: 0x04001ACA RID: 6858
		MaximumAllowed = 33554432
	}
}
