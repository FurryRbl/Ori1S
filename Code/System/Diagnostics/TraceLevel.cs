using System;

namespace System.Diagnostics
{
	/// <summary>Specifies what messages to output for the <see cref="T:System.Diagnostics.Debug" />, <see cref="T:System.Diagnostics.Trace" /> and <see cref="T:System.Diagnostics.TraceSwitch" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200025E RID: 606
	public enum TraceLevel
	{
		/// <summary>Output no tracing and debugging messages.</summary>
		// Token: 0x0400069C RID: 1692
		Off,
		/// <summary>Output error-handling messages.</summary>
		// Token: 0x0400069D RID: 1693
		Error,
		/// <summary>Output warnings and error-handling messages.</summary>
		// Token: 0x0400069E RID: 1694
		Warning,
		/// <summary>Output informational messages, warnings, and error-handling messages.</summary>
		// Token: 0x0400069F RID: 1695
		Info,
		/// <summary>Output all debugging and tracing messages.</summary>
		// Token: 0x040006A0 RID: 1696
		Verbose
	}
}
