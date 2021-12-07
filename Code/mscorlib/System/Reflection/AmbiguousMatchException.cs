using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when binding to a member results in more than one member matching the binding criteria. This class cannot be inherited.</summary>
	// Token: 0x0200026C RID: 620
	[ComVisible(true)]
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with an empty message string and the root cause exception set to null.</summary>
		// Token: 0x0600204A RID: 8266 RVA: 0x00076FB8 File Offset: 0x000751B8
		public AmbiguousMatchException() : base("Ambiguous matching in method resolution")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with its message string set to the given message and the root cause exception set to null.</summary>
		/// <param name="message">A string indicating the reason this exception was thrown. </param>
		// Token: 0x0600204B RID: 8267 RVA: 0x00076FC8 File Offset: 0x000751C8
		public AmbiguousMatchException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600204C RID: 8268 RVA: 0x00076FD4 File Offset: 0x000751D4
		public AmbiguousMatchException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00076FE0 File Offset: 0x000751E0
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
