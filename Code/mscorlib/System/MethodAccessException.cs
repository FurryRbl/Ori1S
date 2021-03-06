using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a method, such as accessing a private method from partially trusted code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014F RID: 335
	[ComVisible(true)]
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Attempt to access the method failed." This message takes into account the current system culture.</summary>
		// Token: 0x0600121C RID: 4636 RVA: 0x00047C50 File Offset: 0x00045E50
		public MethodAccessException() : base(Locale.GetText("Attempt to access a private/protected method failed."))
		{
			base.HResult = -2146233072;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x0600121D RID: 4637 RVA: 0x00047C70 File Offset: 0x00045E70
		public MethodAccessException(string message) : base(message)
		{
			base.HResult = -2146233072;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600121E RID: 4638 RVA: 0x00047C84 File Offset: 0x00045E84
		protected MethodAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600121F RID: 4639 RVA: 0x00047C90 File Offset: 0x00045E90
		public MethodAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233072;
		}

		// Token: 0x0400052E RID: 1326
		private const int Result = -2146233072;
	}
}
