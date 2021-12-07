using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a DLL specified in a DLL import cannot be found.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000129 RID: 297
	[ComVisible(true)]
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with default properties.</summary>
		// Token: 0x060010DA RID: 4314 RVA: 0x00045104 File Offset: 0x00043304
		public DllNotFoundException() : base(Locale.GetText("DLL not found."))
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060010DB RID: 4315 RVA: 0x00045124 File Offset: 0x00043324
		public DllNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060010DC RID: 4316 RVA: 0x00045138 File Offset: 0x00043338
		protected DllNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060010DD RID: 4317 RVA: 0x00045144 File Offset: 0x00043344
		public DllNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x040004CE RID: 1230
		private const int Result = -2146233052;
	}
}
