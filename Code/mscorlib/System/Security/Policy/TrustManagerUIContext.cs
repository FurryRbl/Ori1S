using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the type of user interface (UI) the trust manager should use for trust decisions. </summary>
	// Token: 0x02000656 RID: 1622
	[ComVisible(true)]
	public enum TrustManagerUIContext
	{
		/// <summary>An Install UI.</summary>
		// Token: 0x04001AA5 RID: 6821
		Install,
		/// <summary>An Upgrade UI.</summary>
		// Token: 0x04001AA6 RID: 6822
		Upgrade,
		/// <summary>A Run UI.</summary>
		// Token: 0x04001AA7 RID: 6823
		Run
	}
}
