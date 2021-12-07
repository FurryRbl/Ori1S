using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies how to match versions when locating application trusts in a collection.</summary>
	// Token: 0x02000634 RID: 1588
	[ComVisible(true)]
	public enum ApplicationVersionMatch
	{
		/// <summary>Match on the exact version.</summary>
		// Token: 0x04001A36 RID: 6710
		MatchExactVersion,
		/// <summary>Match on all versions.</summary>
		// Token: 0x04001A37 RID: 6711
		MatchAllVersions
	}
}
