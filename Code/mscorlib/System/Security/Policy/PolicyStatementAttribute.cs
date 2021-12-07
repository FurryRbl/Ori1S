using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines special attribute flags for security policy on code groups.</summary>
	// Token: 0x0200064E RID: 1614
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum PolicyStatementAttribute
	{
		/// <summary>No flags are set.</summary>
		// Token: 0x04001A8D RID: 6797
		Nothing = 0,
		/// <summary>The exclusive code group flag. When a code group has this flag set, only the permissions associated with that code group are granted to code belonging to the code group. At most, one code group matching a given piece of code can be set as exclusive.</summary>
		// Token: 0x04001A8E RID: 6798
		Exclusive = 1,
		/// <summary>The flag representing a policy statement that causes lower policy levels to not be evaluated as part of the resolve operation, effectively allowing the policy level to override lower levels.</summary>
		// Token: 0x04001A8F RID: 6799
		LevelFinal = 2,
		/// <summary>All attribute flags are set.</summary>
		// Token: 0x04001A90 RID: 6800
		All = 3
	}
}
