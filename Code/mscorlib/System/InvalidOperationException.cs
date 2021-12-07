using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method call is invalid for the object's current state.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000146 RID: 326
	[ComVisible(true)]
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class.</summary>
		// Token: 0x060011BE RID: 4542 RVA: 0x00047144 File Offset: 0x00045344
		public InvalidOperationException() : base(Locale.GetText("Operation is not valid due to the current state of the object"))
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060011BF RID: 4543 RVA: 0x00047164 File Offset: 0x00045364
		public InvalidOperationException(string message) : base(message)
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011C0 RID: 4544 RVA: 0x00047178 File Offset: 0x00045378
		public InvalidOperationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060011C1 RID: 4545 RVA: 0x00047190 File Offset: 0x00045390
		protected InvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400051D RID: 1309
		private const int Result = -2146233079;
	}
}
