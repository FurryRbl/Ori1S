using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a feature does not run on a particular platform.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016B RID: 363
	[ComVisible(true)]
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with default properties.</summary>
		// Token: 0x06001355 RID: 4949 RVA: 0x0004D640 File Offset: 0x0004B840
		public PlatformNotSupportedException() : base(Locale.GetText("This platform is not supported."))
		{
			base.HResult = -2146233031;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The text message that explains the reason for the exception. </param>
		// Token: 0x06001356 RID: 4950 RVA: 0x0004D660 File Offset: 0x0004B860
		public PlatformNotSupportedException(string message) : base(message)
		{
			base.HResult = -2146233031;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06001357 RID: 4951 RVA: 0x0004D674 File Offset: 0x0004B874
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001358 RID: 4952 RVA: 0x0004D680 File Offset: 0x0004B880
		public PlatformNotSupportedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233031;
		}

		// Token: 0x0400059D RID: 1437
		private const int Result = -2146233031;
	}
}
