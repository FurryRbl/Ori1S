using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to divide an integral or decimal value by zero.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000128 RID: 296
	[ComVisible(true)]
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class.</summary>
		// Token: 0x060010D6 RID: 4310 RVA: 0x000450AC File Offset: 0x000432AC
		public DivideByZeroException() : base(Locale.GetText("Division by zero"))
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x060010D7 RID: 4311 RVA: 0x000450CC File Offset: 0x000432CC
		public DivideByZeroException(string message) : base(message)
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060010D8 RID: 4312 RVA: 0x000450E0 File Offset: 0x000432E0
		public DivideByZeroException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060010D9 RID: 4313 RVA: 0x000450F8 File Offset: 0x000432F8
		protected DivideByZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040004CD RID: 1229
		private const int Result = -2147352558;
	}
}
