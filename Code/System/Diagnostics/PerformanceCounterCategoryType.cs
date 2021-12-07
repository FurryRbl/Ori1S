using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether the performance counter category can have multiple instances.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000236 RID: 566
	public enum PerformanceCounterCategoryType
	{
		/// <summary>The performance counter category can have only a single instance.</summary>
		// Token: 0x04000599 RID: 1433
		SingleInstance,
		/// <summary>The performance counter category can have multiple instances.</summary>
		// Token: 0x0400059A RID: 1434
		MultiInstance,
		/// <summary>The instance functionality for the performance counter category is unknown. </summary>
		// Token: 0x0400059B RID: 1435
		Unknown = -1
	}
}
