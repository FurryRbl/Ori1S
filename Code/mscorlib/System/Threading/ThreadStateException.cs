using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is in an invalid <see cref="P:System.Threading.Thread.ThreadState" /> for the method call.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006B5 RID: 1717
	[ComVisible(true)]
	[Serializable]
	public class ThreadStateException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with default properties.</summary>
		// Token: 0x060041AB RID: 16811 RVA: 0x000E1564 File Offset: 0x000DF764
		public ThreadStateException() : base("Thread State Error")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060041AC RID: 16812 RVA: 0x000E1574 File Offset: 0x000DF774
		public ThreadStateException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060041AD RID: 16813 RVA: 0x000E1580 File Offset: 0x000DF780
		protected ThreadStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060041AE RID: 16814 RVA: 0x000E158C File Offset: 0x000DF78C
		public ThreadStateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
