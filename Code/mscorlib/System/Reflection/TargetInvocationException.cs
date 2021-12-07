using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by methods invoked through reflection. This class cannot be inherited.</summary>
	// Token: 0x020002BC RID: 700
	[ComVisible(true)]
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600234A RID: 9034 RVA: 0x0007E5A4 File Offset: 0x0007C7A4
		public TargetInvocationException(Exception inner) : base("Exception has been thrown by the target of an invocation.", inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600234B RID: 9035 RVA: 0x0007E5B4 File Offset: 0x0007C7B4
		public TargetInvocationException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0007E5C0 File Offset: 0x0007C7C0
		internal TargetInvocationException(SerializationInfo info, StreamingContext sc) : base(info, sc)
		{
		}
	}
}
