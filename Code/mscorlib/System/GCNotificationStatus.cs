using System;

namespace System
{
	/// <summary>Provides information about the current registration for notification of the next full garbage collection. </summary>
	// Token: 0x0200013C RID: 316
	public enum GCNotificationStatus
	{
		/// <summary>The notification was successful and the registration was not canceled.</summary>
		// Token: 0x04000505 RID: 1285
		Succeeded,
		/// <summary>The notification failed for any reason.</summary>
		// Token: 0x04000506 RID: 1286
		Failed,
		/// <summary>The current registration was canceled by the user. </summary>
		// Token: 0x04000507 RID: 1287
		Canceled,
		/// <summary>The time specified by the <paramref name="millisecondsTimeout" /> parameter for either <see cref="M:System.GC.WaitForFullGCApproach(System.Int32)" /> or <see cref="M:System.GC.WaitForFullGCComplete(System.Int32)" /> has elapsed.</summary>
		// Token: 0x04000508 RID: 1288
		Timeout,
		/// <summary>This result can be caused by the following: there is no current registration for a garbage collection notification, concurrent garbage collection is enabled, or the time specified for the <paramref name="millisecondsTimeout" /> parameter has expired and no garbage collection notification was obtained. (See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.)</summary>
		// Token: 0x04000509 RID: 1289
		NotApplicable
	}
}
