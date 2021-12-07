using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to store an element of the wrong type within an array. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000104 RID: 260
	[ComVisible(true)]
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class.</summary>
		// Token: 0x06000D86 RID: 3462 RVA: 0x0003AEE0 File Offset: 0x000390E0
		public ArrayTypeMismatchException() : base(Locale.GetText("Source array type cannot be assigned to destination array type."))
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000D87 RID: 3463 RVA: 0x0003AF00 File Offset: 0x00039100
		public ArrayTypeMismatchException(string message) : base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000D88 RID: 3464 RVA: 0x0003AF14 File Offset: 0x00039114
		public ArrayTypeMismatchException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000D89 RID: 3465 RVA: 0x0003AF2C File Offset: 0x0003912C
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040003A4 RID: 932
		private const int Result = -2146233085;
	}
}
