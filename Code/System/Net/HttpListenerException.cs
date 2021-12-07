using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs processing an HTTP request.</summary>
	// Token: 0x02000317 RID: 791
	[Serializable]
	public class HttpListenerException : System.ComponentModel.Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class. </summary>
		// Token: 0x06001B95 RID: 7061 RVA: 0x0004EE20 File Offset: 0x0004D020
		public HttpListenerException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		// Token: 0x06001B96 RID: 7062 RVA: 0x0004EE28 File Offset: 0x0004D028
		public HttpListenerException(int errorCode) : base(errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code and message.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06001B97 RID: 7063 RVA: 0x0004EE34 File Offset: 0x0004D034
		public HttpListenerException(int errorCode, string message) : base(errorCode, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to deserialize the new <see cref="T:System.Net.HttpListenerException" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object. </param>
		// Token: 0x06001B98 RID: 7064 RVA: 0x0004EE40 File Offset: 0x0004D040
		protected HttpListenerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets a value that identifies the error that occurred.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0004EE4C File Offset: 0x0004D04C
		public override int ErrorCode
		{
			get
			{
				return base.ErrorCode;
			}
		}
	}
}
