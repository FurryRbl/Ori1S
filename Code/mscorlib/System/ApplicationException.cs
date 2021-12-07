using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a non-fatal application error occurs.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FD RID: 253
	[ComVisible(true)]
	[Serializable]
	public class ApplicationException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class.</summary>
		// Token: 0x06000D58 RID: 3416 RVA: 0x0003A868 File Offset: 0x00038A68
		public ApplicationException() : base(Locale.GetText("An application exception has occurred."))
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error. </param>
		// Token: 0x06000D59 RID: 3417 RVA: 0x0003A888 File Offset: 0x00038A88
		public ApplicationException(string message) : base(message)
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000D5A RID: 3418 RVA: 0x0003A89C File Offset: 0x00038A9C
		public ApplicationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000D5B RID: 3419 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000396 RID: 918
		private const int Result = -2146232832;
	}
}
