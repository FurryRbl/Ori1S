using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when an attempt is made to open a system mutex or semaphore that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006BB RID: 1723
	[ComVisible(false)]
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with default values.</summary>
		// Token: 0x060041EA RID: 16874 RVA: 0x000E2420 File Offset: 0x000E0620
		public WaitHandleCannotBeOpenedException() : base(Locale.GetText("Named handle doesn't exists."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060041EB RID: 16875 RVA: 0x000E2434 File Offset: 0x000E0634
		public WaitHandleCannotBeOpenedException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060041EC RID: 16876 RVA: 0x000E2440 File Offset: 0x000E0640
		public WaitHandleCannotBeOpenedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x060041ED RID: 16877 RVA: 0x000E244C File Offset: 0x000E064C
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
