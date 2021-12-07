using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Defines the base class for predefined exceptions in the <see cref="N:System" /> namespace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000178 RID: 376
	[ComVisible(true)]
	[Serializable]
	public class SystemException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class.</summary>
		// Token: 0x060013A2 RID: 5026 RVA: 0x0004E18C File Offset: 0x0004C38C
		public SystemException() : base(Locale.GetText("A system exception has occurred."))
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060013A3 RID: 5027 RVA: 0x0004E1AC File Offset: 0x0004C3AC
		public SystemException(string message) : base(message)
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060013A4 RID: 5028 RVA: 0x0004E1C0 File Offset: 0x0004C3C0
		protected SystemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060013A5 RID: 5029 RVA: 0x0004E1CC File Offset: 0x0004C3CC
		public SystemException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x040005BA RID: 1466
		private const int Result = -2146233087;
	}
}
