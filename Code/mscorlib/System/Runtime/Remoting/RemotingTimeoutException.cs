using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown when the server or the client cannot be reached for a previously specified period of time.</summary>
	// Token: 0x02000429 RID: 1065
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with default properties.</summary>
		// Token: 0x06002D42 RID: 11586 RVA: 0x000966A8 File Offset: 0x000948A8
		public RemotingTimeoutException()
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason why the exception occurred. </param>
		// Token: 0x06002D43 RID: 11587 RVA: 0x000966B0 File Offset: 0x000948B0
		public RemotingTimeoutException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002D44 RID: 11588 RVA: 0x000966BC File Offset: 0x000948BC
		public RemotingTimeoutException(string message, Exception InnerException) : base(message, InnerException)
		{
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000966C8 File Offset: 0x000948C8
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
