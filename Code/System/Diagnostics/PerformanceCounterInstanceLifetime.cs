using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the lifetime of a performance counter instance.</summary>
	// Token: 0x02000239 RID: 569
	public enum PerformanceCounterInstanceLifetime
	{
		/// <summary>Remove the performance counter instance when no counters are using the process category.</summary>
		// Token: 0x040005AA RID: 1450
		Global,
		/// <summary>Remove the performance counter instance when the process is closed.</summary>
		// Token: 0x040005AB RID: 1451
		Process
	}
}
