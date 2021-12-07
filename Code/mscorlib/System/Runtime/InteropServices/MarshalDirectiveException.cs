using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown by the marshaler when it encounters a <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> it does not support.</summary>
	// Token: 0x020003AD RID: 941
	[ComVisible(true)]
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		/// <summary>Initializes a new instance of the MarshalDirectiveException class with default properties.</summary>
		// Token: 0x06002B4A RID: 11082 RVA: 0x0009370C File Offset: 0x0009190C
		public MarshalDirectiveException() : base(Locale.GetText("Unsupported MarshalAsAttribute found"))
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the MarshalDirectiveException class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		// Token: 0x06002B4B RID: 11083 RVA: 0x0009372C File Offset: 0x0009192C
		public MarshalDirectiveException(string message) : base(message)
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalDirectiveException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002B4C RID: 11084 RVA: 0x00093740 File Offset: 0x00091940
		public MarshalDirectiveException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the MarshalDirectiveException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002B4D RID: 11085 RVA: 0x00093758 File Offset: 0x00091958
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001160 RID: 4448
		private const int ErrorCode = -2146233035;
	}
}
