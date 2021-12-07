using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is interrupted while it is in a waiting state.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006B0 RID: 1712
	[ComVisible(true)]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with default properties.</summary>
		// Token: 0x06004190 RID: 16784 RVA: 0x000E1378 File Offset: 0x000DF578
		public ThreadInterruptedException() : base("Thread interrupted")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06004191 RID: 16785 RVA: 0x000E1388 File Offset: 0x000DF588
		public ThreadInterruptedException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06004192 RID: 16786 RVA: 0x000E1394 File Offset: 0x000DF594
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06004193 RID: 16787 RVA: 0x000E13A0 File Offset: 0x000DF5A0
		public ThreadInterruptedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
