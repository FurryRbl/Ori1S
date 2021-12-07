using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the culture, case, and sort rules to be used by certain overloads of the <see cref="M:System.String.Compare(System.String,System.String)" /> and <see cref="M:System.String.Equals(System.Object)" /> methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000177 RID: 375
	[ComVisible(true)]
	[Serializable]
	public enum StringComparison
	{
		/// <summary>Compare strings using culture-sensitive sort rules and the current culture.</summary>
		// Token: 0x040005B4 RID: 1460
		CurrentCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the current culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x040005B5 RID: 1461
		CurrentCultureIgnoreCase,
		/// <summary>Compare strings using culture-sensitive sort rules and the invariant culture.</summary>
		// Token: 0x040005B6 RID: 1462
		InvariantCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the invariant culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x040005B7 RID: 1463
		InvariantCultureIgnoreCase,
		/// <summary>Compare strings using ordinal sort rules.</summary>
		// Token: 0x040005B8 RID: 1464
		Ordinal,
		/// <summary>Compare strings using ordinal sort rules and ignoring the case of the strings being compared.</summary>
		// Token: 0x040005B9 RID: 1465
		OrdinalIgnoreCase
	}
}
