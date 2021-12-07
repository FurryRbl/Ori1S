using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the rank of an incoming SAFEARRAY does not match the rank specified in the managed signature.</summary>
	// Token: 0x020003B9 RID: 953
	[ComVisible(true)]
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class with default values.</summary>
		// Token: 0x06002B69 RID: 11113 RVA: 0x000938F4 File Offset: 0x00091AF4
		public SafeArrayRankMismatchException() : base(Locale.GetText("The incoming SAVEARRAY does not match the rank of the expected managed signature"))
		{
			base.HResult = -2146233032;
		}

		/// <summary>Initializes a new instance of the SafeArrayRankMismatchException class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06002B6A RID: 11114 RVA: 0x00093914 File Offset: 0x00091B14
		public SafeArrayRankMismatchException(string message) : base(message)
		{
			base.HResult = -2146233032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeArrayRankMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002B6B RID: 11115 RVA: 0x00093928 File Offset: 0x00091B28
		public SafeArrayRankMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233032;
		}

		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002B6C RID: 11116 RVA: 0x00093940 File Offset: 0x00091B40
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001193 RID: 4499
		private const int ErrorCode = -2146233032;
	}
}
