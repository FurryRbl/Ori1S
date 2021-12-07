using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	/// <summary>The exception that is thrown when an unexpected operation occurs during a cryptographic operation.</summary>
	// Token: 0x0200059F RID: 1439
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with default properties.</summary>
		// Token: 0x06003778 RID: 14200 RVA: 0x000B3A7C File Offset: 0x000B1C7C
		public CryptographicUnexpectedOperationException() : base(Locale.GetText("Unexpected error occured during a cryptographic operation."))
		{
			base.HResult = -2146233295;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003779 RID: 14201 RVA: 0x000B3A9C File Offset: 0x000B1C9C
		public CryptographicUnexpectedOperationException(string message) : base(message)
		{
			base.HResult = -2146233295;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600377A RID: 14202 RVA: 0x000B3AB0 File Offset: 0x000B1CB0
		public CryptographicUnexpectedOperationException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233295;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message in the specified format.</summary>
		/// <param name="format">The format used to output the error message. </param>
		/// <param name="insert">The error message that explains the reason for the exception. </param>
		// Token: 0x0600377B RID: 14203 RVA: 0x000B3AC8 File Offset: 0x000B1CC8
		public CryptographicUnexpectedOperationException(string format, string insert) : base(string.Format(format, insert))
		{
			base.HResult = -2146233295;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600377C RID: 14204 RVA: 0x000B3AE4 File Offset: 0x000B1CE4
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
