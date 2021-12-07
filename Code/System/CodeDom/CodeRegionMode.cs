using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Specifies the start or end of a code region.</summary>
	// Token: 0x0200005B RID: 91
	[ComVisible(true)]
	[Serializable]
	public enum CodeRegionMode
	{
		/// <summary>Not used.</summary>
		// Token: 0x040000E9 RID: 233
		None,
		/// <summary>Start of the region.</summary>
		// Token: 0x040000EA RID: 234
		Start,
		/// <summary>End of the region.</summary>
		// Token: 0x040000EB RID: 235
		End
	}
}
