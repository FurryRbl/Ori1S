using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the reason a thread is waiting.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000257 RID: 599
	public enum ThreadWaitReason
	{
		/// <summary>The thread is waiting for event pair high.</summary>
		// Token: 0x04000670 RID: 1648
		EventPairHigh = 7,
		/// <summary>The thread is waiting for event pair low.</summary>
		// Token: 0x04000671 RID: 1649
		EventPairLow,
		/// <summary>Thread execution is delayed.</summary>
		// Token: 0x04000672 RID: 1650
		ExecutionDelay = 4,
		/// <summary>The thread is waiting for the scheduler.</summary>
		// Token: 0x04000673 RID: 1651
		Executive = 0,
		/// <summary>The thread is waiting for a free virtual memory page.</summary>
		// Token: 0x04000674 RID: 1652
		FreePage,
		/// <summary>The thread is waiting for a local procedure call to arrive.</summary>
		// Token: 0x04000675 RID: 1653
		LpcReceive = 9,
		/// <summary>The thread is waiting for reply to a local procedure call to arrive.</summary>
		// Token: 0x04000676 RID: 1654
		LpcReply,
		/// <summary>The thread is waiting for a virtual memory page to arrive in memory.</summary>
		// Token: 0x04000677 RID: 1655
		PageIn = 2,
		/// <summary>The thread is waiting for a virtual memory page to be written to disk.</summary>
		// Token: 0x04000678 RID: 1656
		PageOut = 12,
		/// <summary>Thread execution is suspended.</summary>
		// Token: 0x04000679 RID: 1657
		Suspended = 5,
		/// <summary>The thread is waiting for system allocation.</summary>
		// Token: 0x0400067A RID: 1658
		SystemAllocation = 3,
		/// <summary>The thread is waiting for an unknown reason.</summary>
		// Token: 0x0400067B RID: 1659
		Unknown = 13,
		/// <summary>The thread is waiting for a user request.</summary>
		// Token: 0x0400067C RID: 1660
		UserRequest = 6,
		/// <summary>The thread is waiting for the system to allocate virtual memory.</summary>
		// Token: 0x0400067D RID: 1661
		VirtualMemory = 11
	}
}
