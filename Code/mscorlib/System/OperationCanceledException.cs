using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown in a thread upon cancellation of an operation that the thread was executing.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000167 RID: 359
	[ComVisible(true)]
	[Serializable]
	public class OperationCanceledException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a system-supplied error message.</summary>
		// Token: 0x06001349 RID: 4937 RVA: 0x0004D538 File Offset: 0x0004B738
		public OperationCanceledException() : base(Locale.GetText("The operation was canceled."))
		{
			base.HResult = -2146233029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x0600134A RID: 4938 RVA: 0x0004D558 File Offset: 0x0004B758
		public OperationCanceledException(string message) : base(message)
		{
			base.HResult = -2146233029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600134B RID: 4939 RVA: 0x0004D56C File Offset: 0x0004B76C
		public OperationCanceledException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600134C RID: 4940 RVA: 0x0004D584 File Offset: 0x0004B784
		protected OperationCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000592 RID: 1426
		private const int Result = -2146233029;
	}
}
