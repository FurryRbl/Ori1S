using System;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
	/// <summary>Thrown when a thread on which an operation should execute no longer exists or has no message loop. </summary>
	// Token: 0x02000166 RID: 358
	[Serializable]
	public class InvalidAsynchronousStateException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class. </summary>
		// Token: 0x06000CB0 RID: 3248 RVA: 0x000203A4 File Offset: 0x0001E5A4
		public InvalidAsynchronousStateException() : this("Invalid asynchrinous state is occured")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description.</summary>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x06000CB1 RID: 3249 RVA: 0x000203B4 File Offset: 0x0001E5B4
		public InvalidAsynchronousStateException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06000CB2 RID: 3250 RVA: 0x000203C0 File Offset: 0x0001E5C0
		public InvalidAsynchronousStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06000CB3 RID: 3251 RVA: 0x000203CC File Offset: 0x0001E5CC
		protected InvalidAsynchronousStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
