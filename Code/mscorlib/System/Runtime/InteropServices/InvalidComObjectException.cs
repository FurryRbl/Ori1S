using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when an invalid COM object is used.</summary>
	// Token: 0x020003A7 RID: 935
	[ComVisible(true)]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		/// <summary>Initializes an instance of the InvalidComObjectException with default properties.</summary>
		// Token: 0x06002A96 RID: 10902 RVA: 0x0009298C File Offset: 0x00090B8C
		public InvalidComObjectException() : base(Locale.GetText("Invalid COM object is used"))
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes an instance of the InvalidComObjectException with a message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06002A97 RID: 10903 RVA: 0x000929AC File Offset: 0x00090BAC
		public InvalidComObjectException(string message) : base(message)
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InvalidComObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002A98 RID: 10904 RVA: 0x000929C0 File Offset: 0x00090BC0
		public InvalidComObjectException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes a new instance of the COMException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002A99 RID: 10905 RVA: 0x000929D8 File Offset: 0x00090BD8
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001152 RID: 4434
		private const int ErrorCode = -2146233049;
	}
}
