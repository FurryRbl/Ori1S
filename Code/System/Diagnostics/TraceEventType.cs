using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Identifies the type of event that has caused the trace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200025A RID: 602
	public enum TraceEventType
	{
		/// <summary>Fatal error or application crash.</summary>
		// Token: 0x04000685 RID: 1669
		Critical = 1,
		/// <summary>Recoverable error.</summary>
		// Token: 0x04000686 RID: 1670
		Error,
		/// <summary>Noncritical problem.</summary>
		// Token: 0x04000687 RID: 1671
		Warning = 4,
		/// <summary>Informational message.</summary>
		// Token: 0x04000688 RID: 1672
		Information = 8,
		/// <summary>Debugging trace.</summary>
		// Token: 0x04000689 RID: 1673
		Verbose = 16,
		/// <summary>Starting of a logical operation.</summary>
		// Token: 0x0400068A RID: 1674
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		Start = 256,
		/// <summary>Stopping of a logical operation.</summary>
		// Token: 0x0400068B RID: 1675
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		Stop = 512,
		/// <summary>Suspension of a logical operation.</summary>
		// Token: 0x0400068C RID: 1676
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		Suspend = 1024,
		/// <summary>Resumption of a logical operation.</summary>
		// Token: 0x0400068D RID: 1677
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		Resume = 2048,
		/// <summary>Changing of correlation identity.</summary>
		// Token: 0x0400068E RID: 1678
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		Transfer = 4096
	}
}
