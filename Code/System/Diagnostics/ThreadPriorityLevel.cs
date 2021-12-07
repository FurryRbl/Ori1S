using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the priority level of a thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000255 RID: 597
	public enum ThreadPriorityLevel
	{
		/// <summary>Specifies one step above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x0400065F RID: 1631
		AboveNormal = 1,
		/// <summary>Specifies one step below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000660 RID: 1632
		BelowNormal = -1,
		/// <summary>Specifies highest priority. This is two steps above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000661 RID: 1633
		Highest = 2,
		/// <summary>Specifies idle priority. This is the lowest possible priority value of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000662 RID: 1634
		Idle = -15,
		/// <summary>Specifies lowest priority. This is two steps below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000663 RID: 1635
		Lowest = -2,
		/// <summary>Specifies normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000664 RID: 1636
		Normal = 0,
		/// <summary>Specifies time-critical priority. This is the highest priority of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000665 RID: 1637
		TimeCritical = 15
	}
}
