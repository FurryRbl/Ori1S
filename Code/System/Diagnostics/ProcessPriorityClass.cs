using System;

namespace System.Diagnostics
{
	/// <summary>Indicates the priority that the system associates with a process. This value, together with the priority value of each thread of the process, determines each thread's base priority level.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000248 RID: 584
	public enum ProcessPriorityClass
	{
		/// <summary>Specifies that the process has priority above <see cref="F:System.Diagnostics.ProcessPriorityClass.Normal" /> but below <see cref="F:System.Diagnostics.ProcessPriorityClass.High" />.</summary>
		// Token: 0x04000623 RID: 1571
		AboveNormal = 32768,
		/// <summary>Specifies that the process has priority above <see cref="F:System.Diagnostics.ProcessPriorityClass.Idle" /> but below <see cref="F:System.Diagnostics.ProcessPriorityClass.Normal" />.</summary>
		// Token: 0x04000624 RID: 1572
		BelowNormal = 16384,
		/// <summary>Specifies that the process performs time-critical tasks that must be executed immediately, such as the Task List dialog, which must respond quickly when called by the user, regardless of the load on the operating system. The threads of the process preempt the threads of normal or idle priority class processes.</summary>
		// Token: 0x04000625 RID: 1573
		High = 128,
		/// <summary>Specifies that the threads of this process run only when the system is idle, such as a screen saver. The threads of the process are preempted by the threads of any process running in a higher priority class.</summary>
		// Token: 0x04000626 RID: 1574
		Idle = 64,
		/// <summary>Specifies that the process has no special scheduling needs.</summary>
		// Token: 0x04000627 RID: 1575
		Normal = 32,
		/// <summary>Specifies that the process has the highest possible priority.</summary>
		// Token: 0x04000628 RID: 1576
		RealTime = 256
	}
}
