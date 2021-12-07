using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to load a class fails due to the absence of an entry method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012C RID: 300
	[ComVisible(true)]
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class.</summary>
		// Token: 0x060010E3 RID: 4323 RVA: 0x000451D8 File Offset: 0x000433D8
		public EntryPointNotFoundException() : base(Locale.GetText("Cannot load class because of missing entry method."))
		{
			base.HResult = -2146233053;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060010E4 RID: 4324 RVA: 0x000451F8 File Offset: 0x000433F8
		public EntryPointNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233053;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060010E5 RID: 4325 RVA: 0x0004520C File Offset: 0x0004340C
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060010E6 RID: 4326 RVA: 0x00045218 File Offset: 0x00043418
		public EntryPointNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x040004D3 RID: 1235
		private const int Result = -2146233053;
	}
}
