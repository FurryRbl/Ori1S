using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an arithmetic, casting, or conversion operation in a checked context results in an overflow.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000169 RID: 361
	[ComVisible(true)]
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class.</summary>
		// Token: 0x06001351 RID: 4945 RVA: 0x0004D5E8 File Offset: 0x0004B7E8
		public OverflowException() : base(Locale.GetText("Number overflow."))
		{
			base.HResult = -2146233066;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001352 RID: 4946 RVA: 0x0004D608 File Offset: 0x0004B808
		public OverflowException(string message) : base(message)
		{
			base.HResult = -2146233066;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001353 RID: 4947 RVA: 0x0004D61C File Offset: 0x0004B81C
		public OverflowException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233066;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001354 RID: 4948 RVA: 0x0004D634 File Offset: 0x0004B834
		protected OverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000594 RID: 1428
		private const int Result = -2146233066;
	}
}
