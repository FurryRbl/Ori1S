using System;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs while accessing the network through a pluggable protocol.</summary>
	// Token: 0x02000417 RID: 1047
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class.</summary>
		// Token: 0x060025AB RID: 9643 RVA: 0x00074F24 File Offset: 0x00073124
		public WebException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message.</summary>
		/// <param name="message">The text of the error message. </param>
		// Token: 0x060025AC RID: 9644 RVA: 0x00074F34 File Offset: 0x00073134
		public WebException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebException" />. </param>
		// Token: 0x060025AD RID: 9645 RVA: 0x00074F48 File Offset: 0x00073148
		protected WebException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and nested exception.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		// Token: 0x060025AE RID: 9646 RVA: 0x00074F5C File Offset: 0x0007315C
		public WebException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and status.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		// Token: 0x060025AF RID: 9647 RVA: 0x00074F70 File Offset: 0x00073170
		public WebException(string message, WebExceptionStatus status) : base(message)
		{
			this.status = status;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00074F88 File Offset: 0x00073188
		internal WebException(string message, Exception innerException, WebExceptionStatus status) : base(message, innerException)
		{
			this.status = status;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message, nested exception, status, and response.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> instance that contains the response from the remote host. </param>
		// Token: 0x060025B1 RID: 9649 RVA: 0x00074FA4 File Offset: 0x000731A4
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response) : base(message, innerException)
		{
			this.status = status;
			this.response = response;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebException" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x060025B2 RID: 9650 RVA: 0x00074FC8 File Offset: 0x000731C8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Gets the response that the remote host returned.</summary>
		/// <returns>If a response is available from the Internet resource, a <see cref="T:System.Net.WebResponse" /> instance that contains the error response from an Internet resource; otherwise, null.</returns>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00074FD4 File Offset: 0x000731D4
		public WebResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.WebExceptionStatus" /> values.</returns>
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x00074FDC File Offset: 0x000731DC
		public WebExceptionStatus Status
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060025B5 RID: 9653 RVA: 0x00074FE4 File Offset: 0x000731E4
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x0400174E RID: 5966
		private WebResponse response;

		// Token: 0x0400174F RID: 5967
		private WebExceptionStatus status = WebExceptionStatus.UnknownError;
	}
}
