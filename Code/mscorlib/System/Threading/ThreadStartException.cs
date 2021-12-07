using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a failure occurs in a managed thread after the underlying operating system thread has been started, but before the thread is ready to execute user code.</summary>
	// Token: 0x020006B3 RID: 1715
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x060041A7 RID: 16807 RVA: 0x000E1530 File Offset: 0x000DF730
		internal ThreadStartException() : base("Thread Start Error")
		{
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000E1540 File Offset: 0x000DF740
		internal ThreadStartException(string message) : base(message)
		{
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000E154C File Offset: 0x000DF74C
		internal ThreadStartException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000E1558 File Offset: 0x000DF758
		internal ThreadStartException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
