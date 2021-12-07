using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a requested method or operation is not implemented.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015E RID: 350
	[ComVisible(true)]
	[Serializable]
	public class NotImplementedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with default properties.</summary>
		// Token: 0x0600129B RID: 4763 RVA: 0x000495C0 File Offset: 0x000477C0
		public NotImplementedException() : base(Locale.GetText("The requested feature is not implemented."))
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x0600129C RID: 4764 RVA: 0x000495E0 File Offset: 0x000477E0
		public NotImplementedException(string message) : base(message)
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600129D RID: 4765 RVA: 0x000495F4 File Offset: 0x000477F4
		public NotImplementedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600129E RID: 4766 RVA: 0x0004960C File Offset: 0x0004780C
		protected NotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400054B RID: 1355
		private const int Result = -2147467263;
	}
}
