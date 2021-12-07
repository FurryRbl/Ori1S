using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000101 RID: 257
	[ComVisible(true)]
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class.</summary>
		// Token: 0x06000D74 RID: 3444 RVA: 0x0003AD0C File Offset: 0x00038F0C
		public ArgumentNullException() : base(Locale.GetText("Argument cannot be null."))
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		// Token: 0x06000D75 RID: 3445 RVA: 0x0003AD2C File Offset: 0x00038F2C
		public ArgumentNullException(string paramName) : base(Locale.GetText("Argument cannot be null."), paramName)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="message">A message that describes the error. </param>
		// Token: 0x06000D76 RID: 3446 RVA: 0x0003AD4C File Offset: 0x00038F4C
		public ArgumentNullException(string paramName, string message) : base(message, paramName)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for this exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000D77 RID: 3447 RVA: 0x0003AD64 File Offset: 0x00038F64
		public ArgumentNullException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		// Token: 0x06000D78 RID: 3448 RVA: 0x0003AD7C File Offset: 0x00038F7C
		protected ArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040003A0 RID: 928
		private const int Result = -2147467261;
	}
}
