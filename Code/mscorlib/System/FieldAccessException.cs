using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a private or protected field inside a class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000137 RID: 311
	[ComVisible(true)]
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class.</summary>
		// Token: 0x0600112F RID: 4399 RVA: 0x0004614C File Offset: 0x0004434C
		public FieldAccessException() : base(Locale.GetText("Attempt to access a private/protected field failed."))
		{
			base.HResult = -2146233081;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001130 RID: 4400 RVA: 0x0004616C File Offset: 0x0004436C
		public FieldAccessException(string message) : base(message)
		{
			base.HResult = -2146233081;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001131 RID: 4401 RVA: 0x00046180 File Offset: 0x00044380
		protected FieldAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001132 RID: 4402 RVA: 0x0004618C File Offset: 0x0004438C
		public FieldAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233081;
		}

		// Token: 0x040004FE RID: 1278
		private const int Result = -2146233081;
	}
}
