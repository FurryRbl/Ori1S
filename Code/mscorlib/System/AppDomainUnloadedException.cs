using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an unloaded application domain. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FC RID: 252
	[ComVisible(true)]
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class.</summary>
		// Token: 0x06000D54 RID: 3412 RVA: 0x0003A810 File Offset: 0x00038A10
		public AppDomainUnloadedException() : base(Locale.GetText("Can't access an unloaded application domain."))
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000D55 RID: 3413 RVA: 0x0003A830 File Offset: 0x00038A30
		public AppDomainUnloadedException(string message) : base(message)
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000D56 RID: 3414 RVA: 0x0003A844 File Offset: 0x00038A44
		public AppDomainUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000D57 RID: 3415 RVA: 0x0003A85C File Offset: 0x00038A5C
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000395 RID: 917
		private const int Result = -2146234348;
	}
}
