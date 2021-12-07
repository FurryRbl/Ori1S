using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies whether applicable <see cref="Overload:System.String.Split" /> method overloads include or omit empty substrings from the return value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000173 RID: 371
	[Flags]
	[ComVisible(false)]
	public enum StringSplitOptions
	{
		/// <summary>The return value includes array elements that contain an empty string</summary>
		// Token: 0x040005AA RID: 1450
		None = 0,
		/// <summary>The return value does not include array elements that contain an empty string</summary>
		// Token: 0x040005AB RID: 1451
		RemoveEmptyEntries = 1
	}
}
