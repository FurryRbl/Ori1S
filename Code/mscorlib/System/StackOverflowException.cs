using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the execution stack overflows because it contains too many nested method calls. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000171 RID: 369
	[ComVisible(true)]
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "The requested operation caused a stack overflow." This message takes into account the current system culture.</summary>
		// Token: 0x06001386 RID: 4998 RVA: 0x0004DE74 File Offset: 0x0004C074
		public StackOverflowException() : base(Locale.GetText("The requested operation caused a stack overflow."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of message is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06001387 RID: 4999 RVA: 0x0004DE88 File Offset: 0x0004C088
		public StackOverflowException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001388 RID: 5000 RVA: 0x0004DE94 File Offset: 0x0004C094
		public StackOverflowException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004DEA0 File Offset: 0x0004C0A0
		internal StackOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
