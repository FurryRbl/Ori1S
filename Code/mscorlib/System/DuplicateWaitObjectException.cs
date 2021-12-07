using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an object appears more than once in an array of synchronization objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012B RID: 299
	[ComVisible(true)]
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class.</summary>
		// Token: 0x060010DE RID: 4318 RVA: 0x0004515C File Offset: 0x0004335C
		public DuplicateWaitObjectException() : base(Locale.GetText("Duplicate objects in argument."))
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception. </param>
		// Token: 0x060010DF RID: 4319 RVA: 0x0004517C File Offset: 0x0004337C
		public DuplicateWaitObjectException(string parameterName) : base(Locale.GetText("Duplicate objects in argument."), parameterName)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060010E0 RID: 4320 RVA: 0x0004519C File Offset: 0x0004339C
		public DuplicateWaitObjectException(string parameterName, string message) : base(message, parameterName)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060010E1 RID: 4321 RVA: 0x000451B4 File Offset: 0x000433B4
		public DuplicateWaitObjectException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060010E2 RID: 4322 RVA: 0x000451CC File Offset: 0x000433CC
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040004D2 RID: 1234
		private const int Result = -2146233047;
	}
}
