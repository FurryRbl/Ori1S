using System;

namespace System
{
	/// <summary>The exception that is thrown when a check for sufficient available memory fails. This class cannot be inherited.</summary>
	// Token: 0x02000144 RID: 324
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060011B6 RID: 4534 RVA: 0x000470B4 File Offset: 0x000452B4
		public InsufficientMemoryException() : base("Insufficient memory")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060011B7 RID: 4535 RVA: 0x000470C4 File Offset: 0x000452C4
		public InsufficientMemoryException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011B8 RID: 4536 RVA: 0x000470D0 File Offset: 0x000452D0
		public InsufficientMemoryException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
