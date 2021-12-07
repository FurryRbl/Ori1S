using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to access an unloaded class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000190 RID: 400
	[ComVisible(true)]
	[Serializable]
	public class TypeUnloadedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class.</summary>
		// Token: 0x06001468 RID: 5224 RVA: 0x00052190 File Offset: 0x00050390
		public TypeUnloadedException() : base(Locale.GetText("Cannot access an unloaded class."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001469 RID: 5225 RVA: 0x000521A4 File Offset: 0x000503A4
		public TypeUnloadedException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600146A RID: 5226 RVA: 0x000521B0 File Offset: 0x000503B0
		protected TypeUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600146B RID: 5227 RVA: 0x000521BC File Offset: 0x000503BC
		public TypeUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
