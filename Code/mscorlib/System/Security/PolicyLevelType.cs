using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the type of a managed code policy level.</summary>
	// Token: 0x0200053E RID: 1342
	[ComVisible(true)]
	[Serializable]
	public enum PolicyLevelType
	{
		/// <summary>Security policy for all managed code that is run by the user.</summary>
		// Token: 0x0400162F RID: 5679
		User,
		/// <summary>Security policy for all managed code that is run on the computer.</summary>
		// Token: 0x04001630 RID: 5680
		Machine,
		/// <summary>Security policy for all managed code in an enterprise.</summary>
		// Token: 0x04001631 RID: 5681
		Enterprise,
		/// <summary>Security policy for all managed code in an application.</summary>
		// Token: 0x04001632 RID: 5682
		AppDomain
	}
}
