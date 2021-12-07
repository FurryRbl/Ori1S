using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is not enough memory to continue the execution of a program.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000168 RID: 360
	[ComVisible(true)]
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class.</summary>
		// Token: 0x0600134D RID: 4941 RVA: 0x0004D590 File Offset: 0x0004B790
		public OutOfMemoryException() : base(Locale.GetText("Out of memory."))
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600134E RID: 4942 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
		public OutOfMemoryException(string message) : base(message)
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600134F RID: 4943 RVA: 0x0004D5C4 File Offset: 0x0004B7C4
		public OutOfMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001350 RID: 4944 RVA: 0x0004D5DC File Offset: 0x0004B7DC
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000593 RID: 1427
		private const int Result = -2147024882;
	}
}
