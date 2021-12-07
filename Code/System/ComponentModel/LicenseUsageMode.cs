using System;

namespace System.ComponentModel
{
	/// <summary>Specifies when the <see cref="T:System.ComponentModel.License" /> can be used.</summary>
	// Token: 0x02000179 RID: 377
	public enum LicenseUsageMode
	{
		/// <summary>Used during design time by a visual designer or the compiler.</summary>
		// Token: 0x04000389 RID: 905
		Designtime = 1,
		/// <summary>Used during runtime.</summary>
		// Token: 0x0400038A RID: 906
		Runtime = 0
	}
}
