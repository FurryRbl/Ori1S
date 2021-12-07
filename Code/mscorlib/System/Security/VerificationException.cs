using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when the security policy requires code to be type safe and the verification process is unable to verify that the code is type safe.</summary>
	// Token: 0x02000552 RID: 1362
	[ComVisible(true)]
	[Serializable]
	public class VerificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with default properties.</summary>
		// Token: 0x0600358D RID: 13709 RVA: 0x000B1280 File Offset: 0x000AF480
		public VerificationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with an explanatory message.</summary>
		/// <param name="message">A message indicating the reason the exception occurred. </param>
		// Token: 0x0600358E RID: 13710 RVA: 0x000B1288 File Offset: 0x000AF488
		public VerificationException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600358F RID: 13711 RVA: 0x000B1294 File Offset: 0x000AF494
		protected VerificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003590 RID: 13712 RVA: 0x000B12A0 File Offset: 0x000AF4A0
		public VerificationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
