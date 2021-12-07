using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The base exception type for all COM interop exceptions and structured exception handling (SEH) exceptions.</summary>
	// Token: 0x0200038E RID: 910
	[ComVisible(true)]
	[Serializable]
	public class ExternalException : SystemException
	{
		/// <summary>Initializes a new instance of the ExternalException class with default properties.</summary>
		// Token: 0x06002A54 RID: 10836 RVA: 0x0009268C File Offset: 0x0009088C
		public ExternalException() : base(Locale.GetText("External exception"))
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the ExternalException class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		// Token: 0x06002A55 RID: 10837 RVA: 0x000926AC File Offset: 0x000908AC
		public ExternalException(string message) : base(message)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the ExternalException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002A56 RID: 10838 RVA: 0x000926C0 File Offset: 0x000908C0
		protected ExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ExternalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002A57 RID: 10839 RVA: 0x000926CC File Offset: 0x000908CC
		public ExternalException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the ExternalException class with a specified error message and the HRESULT of the error.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		/// <param name="errorCode">The HRESULT of the error. </param>
		// Token: 0x06002A58 RID: 10840 RVA: 0x000926E4 File Offset: 0x000908E4
		public ExternalException(string message, int errorCode) : base(message)
		{
			base.HResult = errorCode;
		}

		/// <summary>Gets the HRESULT of the error.</summary>
		/// <returns>The HRESULT of the error.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x000926F4 File Offset: 0x000908F4
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}
	}
}
