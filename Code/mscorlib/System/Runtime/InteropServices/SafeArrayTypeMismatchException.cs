using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the type of the incoming SAFEARRAY does not match the type specified in the managed signature.</summary>
	// Token: 0x020003BA RID: 954
	[ComVisible(true)]
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class with default values.</summary>
		// Token: 0x06002B6D RID: 11117 RVA: 0x0009394C File Offset: 0x00091B4C
		public SafeArrayTypeMismatchException() : base(Locale.GetText("The incoming SAVEARRAY does not match the expected managed signature"))
		{
			base.HResult = -2146233037;
		}

		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06002B6E RID: 11118 RVA: 0x0009396C File Offset: 0x00091B6C
		public SafeArrayTypeMismatchException(string message) : base(message)
		{
			base.HResult = -2146233037;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeArrayTypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002B6F RID: 11119 RVA: 0x00093980 File Offset: 0x00091B80
		public SafeArrayTypeMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233037;
		}

		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002B70 RID: 11120 RVA: 0x00093998 File Offset: 0x00091B98
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001194 RID: 4500
		private const int ErrorCode = -2146233037;
	}
}
