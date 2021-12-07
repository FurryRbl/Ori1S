using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to marshal an object across a context boundary fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000119 RID: 281
	[Obsolete("this type is obsoleted in 2.0 profile")]
	[ComVisible(true)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with default properties.</summary>
		// Token: 0x06000E95 RID: 3733 RVA: 0x0003CF38 File Offset: 0x0003B138
		public ContextMarshalException() : base(Locale.GetText("Attempt to marshal and object across a context failed."))
		{
			base.HResult = -2146233084;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000E96 RID: 3734 RVA: 0x0003CF58 File Offset: 0x0003B158
		public ContextMarshalException(string message) : base(message)
		{
			base.HResult = -2146233084;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000E97 RID: 3735 RVA: 0x0003CF6C File Offset: 0x0003B16C
		protected ContextMarshalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000E98 RID: 3736 RVA: 0x0003CF78 File Offset: 0x0003B178
		public ContextMarshalException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233084;
		}

		// Token: 0x0400047E RID: 1150
		private const int Result = -2146233084;
	}
}
