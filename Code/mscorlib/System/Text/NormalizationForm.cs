using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	/// <summary>Defines the type of normalization to perform.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000689 RID: 1673
	[ComVisible(true)]
	public enum NormalizationForm
	{
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x04001B78 RID: 7032
		FormC = 1,
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition.</summary>
		// Token: 0x04001B79 RID: 7033
		FormD,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x04001B7A RID: 7034
		FormKC = 5,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition.</summary>
		// Token: 0x04001B7B RID: 7035
		FormKD
	}
}
