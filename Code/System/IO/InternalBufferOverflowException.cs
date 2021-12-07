using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception thrown when the internal buffer overflows.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000289 RID: 649
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class.</summary>
		// Token: 0x060016C9 RID: 5833 RVA: 0x0003E9EC File Offset: 0x0003CBEC
		public InternalBufferOverflowException() : base("Internal buffer overflow occurred.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the error message to be displayed specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		// Token: 0x060016CA RID: 5834 RVA: 0x0003E9FC File Offset: 0x0003CBFC
		public InternalBufferOverflowException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The information required to serialize the T:System.IO.InternalBufferOverflowException object.</param>
		/// <param name="context">The source and destination of the serialized stream associated with the T:System.IO.InternalBufferOverflowException object.</param>
		// Token: 0x060016CB RID: 5835 RVA: 0x0003EA08 File Offset: 0x0003CC08
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the message to be displayed and the generated inner exception specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		/// <param name="inner">The inner exception. </param>
		// Token: 0x060016CC RID: 5836 RVA: 0x0003EA14 File Offset: 0x0003CC14
		public InternalBufferOverflowException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
