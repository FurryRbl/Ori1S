using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the current execution state of the thread.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000256 RID: 598
	public enum ThreadState
	{
		/// <summary>A state that indicates the thread has been initialized, but has not yet started.</summary>
		// Token: 0x04000667 RID: 1639
		Initialized,
		/// <summary>A state that indicates the thread is waiting to use a processor because no processor is free. The thread is prepared to run on the next available processor.</summary>
		// Token: 0x04000668 RID: 1640
		Ready,
		/// <summary>A state that indicates the thread is currently using a processor.</summary>
		// Token: 0x04000669 RID: 1641
		Running,
		/// <summary>A state that indicates the thread is about to use a processor. Only one thread can be in this state at a time.</summary>
		// Token: 0x0400066A RID: 1642
		Standby,
		/// <summary>A state that indicates the thread has finished executing and has exited.</summary>
		// Token: 0x0400066B RID: 1643
		Terminated,
		/// <summary>A state that indicates the thread is waiting for a resource, other than the processor, before it can execute. For example, it might be waiting for its execution stack to be paged in from disk.</summary>
		// Token: 0x0400066C RID: 1644
		Transition = 6,
		/// <summary>The state of the thread is unknown.</summary>
		// Token: 0x0400066D RID: 1645
		Unknown,
		/// <summary>A state that indicates the thread is not ready to use the processor because it is waiting for a peripheral operation to complete or a resource to become free. When the thread is ready, it will be rescheduled.</summary>
		// Token: 0x0400066E RID: 1646
		Wait = 5
	}
}
