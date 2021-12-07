using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Represents the exception that is thrown when an attempt is made to invoke an invalid target.</summary>
	// Token: 0x020002BB RID: 699
	[ComVisible(true)]
	[Serializable]
	public class TargetException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with an empty message and the root cause of the exception.</summary>
		// Token: 0x06002346 RID: 9030 RVA: 0x0007E56C File Offset: 0x0007C76C
		public TargetException() : base(Locale.GetText("Unable to invoke an invalid target."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the given message and the root cause exception.</summary>
		/// <param name="message">A String describing the reason why the exception occurred. </param>
		// Token: 0x06002347 RID: 9031 RVA: 0x0007E580 File Offset: 0x0007C780
		public TargetException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002348 RID: 9032 RVA: 0x0007E58C File Offset: 0x0007C78C
		public TargetException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the object. </param>
		/// <param name="context">The source of and destination for the object. </param>
		// Token: 0x06002349 RID: 9033 RVA: 0x0007E598 File Offset: 0x0007C798
		protected TargetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
