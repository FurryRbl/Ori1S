using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to unload an application domain fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010B RID: 267
	[ComVisible(true)]
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class.</summary>
		// Token: 0x06000DBF RID: 3519 RVA: 0x0003B940 File Offset: 0x00039B40
		public CannotUnloadAppDomainException() : base(Locale.GetText("Attempt to unload application domain failed."))
		{
			base.HResult = -2146234347;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003B960 File Offset: 0x00039B60
		public CannotUnloadAppDomainException(string message) : base(message)
		{
			base.HResult = -2146234347;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003B974 File Offset: 0x00039B74
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003B980 File Offset: 0x00039B80
		public CannotUnloadAppDomainException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234347;
		}

		// Token: 0x040003BF RID: 959
		private const int Result = -2146234347;
	}
}
