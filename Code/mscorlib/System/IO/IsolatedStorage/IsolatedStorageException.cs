using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	/// <summary>The exception that is thrown when an operation in isolated storage fails.</summary>
	// Token: 0x02000266 RID: 614
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with default properties.</summary>
		// Token: 0x06001FFB RID: 8187 RVA: 0x00075D4C File Offset: 0x00073F4C
		public IsolatedStorageException() : base(Locale.GetText("An Isolated storage operation failed."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001FFC RID: 8188 RVA: 0x00075D60 File Offset: 0x00073F60
		public IsolatedStorageException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001FFD RID: 8189 RVA: 0x00075D6C File Offset: 0x00073F6C
		public IsolatedStorageException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001FFE RID: 8190 RVA: 0x00075D78 File Offset: 0x00073F78
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
