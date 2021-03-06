using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an element of an array with an index that is outside the bounds of the array. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000143 RID: 323
	[ComVisible(true)]
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class.</summary>
		// Token: 0x060011B2 RID: 4530 RVA: 0x0004707C File Offset: 0x0004527C
		public IndexOutOfRangeException() : base(Locale.GetText("Array index is out of range."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060011B3 RID: 4531 RVA: 0x00047090 File Offset: 0x00045290
		public IndexOutOfRangeException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011B4 RID: 4532 RVA: 0x0004709C File Offset: 0x0004529C
		public IndexOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000470A8 File Offset: 0x000452A8
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
