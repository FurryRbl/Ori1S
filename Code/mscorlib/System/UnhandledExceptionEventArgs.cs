using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for the event that is raised when there is an exception that is not handled in any application domain.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000192 RID: 402
	[ComVisible(true)]
	[Serializable]
	public class UnhandledExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnhandledExceptionEventArgs" /> class with the exception object and a common language runtime termination flag.</summary>
		/// <param name="exception">The exception that is not handled. </param>
		/// <param name="isTerminating">true if the runtime is terminating; otherwise, false. </param>
		// Token: 0x06001470 RID: 5232 RVA: 0x00052220 File Offset: 0x00050420
		public UnhandledExceptionEventArgs(object exception, bool isTerminating)
		{
			this.exception = exception;
			this.m_isTerminating = isTerminating;
		}

		/// <summary>Gets the unhandled exception object.</summary>
		/// <returns>The unhandled exception object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00052238 File Offset: 0x00050438
		public object ExceptionObject
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.exception;
			}
		}

		/// <summary>Indicates whether the common language runtime is terminating.</summary>
		/// <returns>true if the runtime is terminating; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00052240 File Offset: 0x00050440
		public bool IsTerminating
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_isTerminating;
			}
		}

		// Token: 0x04000801 RID: 2049
		private object exception;

		// Token: 0x04000802 RID: 2050
		private bool m_isTerminating;
	}
}
