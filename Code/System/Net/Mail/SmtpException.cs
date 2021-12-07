using System;
using System.Runtime.Serialization;

namespace System.Net.Mail
{
	/// <summary>Represents the exception that is thrown when the <see cref="T:System.Net.Mail.SmtpClient" /> is not able to complete a <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> or <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> operation.</summary>
	// Token: 0x02000348 RID: 840
	[Serializable]
	public class SmtpException : Exception, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class. </summary>
		// Token: 0x06001DDC RID: 7644 RVA: 0x0005B81C File Offset: 0x00059A1C
		public SmtpException() : this(SmtpStatusCode.GeneralFailure)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		// Token: 0x06001DDD RID: 7645 RVA: 0x0005B828 File Offset: 0x00059A28
		public SmtpException(SmtpStatusCode statusCode) : this(statusCode, "Syntax error, command unrecognized.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06001DDE RID: 7646 RVA: 0x0005B838 File Offset: 0x00059A38
		public SmtpException(string message) : this(SmtpStatusCode.GeneralFailure, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes. </summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the new instance. </param>
		// Token: 0x06001DDF RID: 7647 RVA: 0x0005B844 File Offset: 0x00059A44
		protected SmtpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			try
			{
				this.statusCode = (SmtpStatusCode)((int)info.GetValue("Status", typeof(int)));
			}
			catch (SerializationException)
			{
				this.statusCode = (SmtpStatusCode)((int)info.GetValue("statusCode", typeof(SmtpStatusCode)));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code and error message.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06001DE0 RID: 7648 RVA: 0x0005B8C0 File Offset: 0x00059AC0
		public SmtpException(SmtpStatusCode statusCode, string message) : base(message)
		{
			this.statusCode = statusCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. </param>
		// Token: 0x06001DE1 RID: 7649 RVA: 0x0005B8D0 File Offset: 0x00059AD0
		public SmtpException(string message, Exception innerException) : base(message, innerException)
		{
			this.statusCode = SmtpStatusCode.GeneralFailure;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />, which holds the serialized data for the <see cref="T:System.Net.Mail.SmtpException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream associated with the new <see cref="T:System.Net.Mail.SmtpException" />. </param>
		// Token: 0x06001DE2 RID: 7650 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Gets the status code returned by an SMTP server when an e-mail message is transmitted.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value that indicates the error that occurred.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0005B8F0 File Offset: 0x00059AF0
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0005B8F8 File Offset: 0x00059AF8
		public SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x0005B904 File Offset: 0x00059B04
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Status", this.statusCode, typeof(int));
		}

		// Token: 0x0400128C RID: 4748
		private SmtpStatusCode statusCode;
	}
}
