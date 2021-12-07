using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000191 RID: 401
	[ComVisible(true)]
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class.</summary>
		// Token: 0x0600146C RID: 5228 RVA: 0x000521C8 File Offset: 0x000503C8
		public UnauthorizedAccessException() : base(Locale.GetText("Access to the requested resource is not authorized."))
		{
			base.HResult = -2146233088;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600146D RID: 5229 RVA: 0x000521E8 File Offset: 0x000503E8
		public UnauthorizedAccessException(string message) : base(message)
		{
			base.HResult = -2146233088;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600146E RID: 5230 RVA: 0x000521FC File Offset: 0x000503FC
		public UnauthorizedAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233088;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600146F RID: 5231 RVA: 0x00052214 File Offset: 0x00050414
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000800 RID: 2048
		private const int Result = -2146233088;
	}
}
