using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a program contains invalid Microsoft intermediate language (MSIL) or metadata. Generally this indicates a bug in the compiler that generated the program.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000147 RID: 327
	[ComVisible(true)]
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with default properties.</summary>
		// Token: 0x060011C2 RID: 4546 RVA: 0x0004719C File Offset: 0x0004539C
		public InvalidProgramException() : base(Locale.GetText("Metadata is invalid."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060011C3 RID: 4547 RVA: 0x000471B0 File Offset: 0x000453B0
		public InvalidProgramException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011C4 RID: 4548 RVA: 0x000471BC File Offset: 0x000453BC
		public InvalidProgramException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000471C8 File Offset: 0x000453C8
		internal InvalidProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
