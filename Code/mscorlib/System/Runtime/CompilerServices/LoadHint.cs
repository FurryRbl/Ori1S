using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the preferred default binding for a dependent assembly.</summary>
	// Token: 0x0200033E RID: 830
	[Serializable]
	public enum LoadHint
	{
		/// <summary>No preference specified.</summary>
		// Token: 0x0400108C RID: 4236
		Default,
		/// <summary>The dependency is always loaded.</summary>
		// Token: 0x0400108D RID: 4237
		Always,
		/// <summary>The dependency is sometimes loaded.</summary>
		// Token: 0x0400108E RID: 4238
		Sometimes
	}
}
