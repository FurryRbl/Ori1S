using System;
using System.Runtime.Serialization;

namespace System.Net.Mail
{
	/// <summary>The exception that is thrown when e-mail is sent using an <see cref="T:System.Net.Mail.SmtpClient" /> and cannot be delivered to all recipients.</summary>
	// Token: 0x0200034A RID: 842
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class.</summary>
		// Token: 0x06001DF0 RID: 7664 RVA: 0x0005BA1C File Offset: 0x00059C1C
		public SmtpFailedRecipientsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" />.</summary>
		/// <param name="message">The exception message.</param>
		// Token: 0x06001DF1 RID: 7665 RVA: 0x0005BA24 File Offset: 0x00059C24
		public SmtpFailedRecipientsException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and inner <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		// Token: 0x06001DF2 RID: 7666 RVA: 0x0005BA30 File Offset: 0x00059C30
		public SmtpFailedRecipientsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerExceptions">The array of recipients with delivery errors.</param>
		// Token: 0x06001DF3 RID: 7667 RVA: 0x0005BA3C File Offset: 0x00059C3C
		public SmtpFailedRecipientsException(string message, SmtpFailedRecipientException[] innerExceptions) : base(message)
		{
			this.innerExceptions = innerExceptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance. </param>
		// Token: 0x06001DF4 RID: 7668 RVA: 0x0005BA4C File Offset: 0x00059C4C
		protected SmtpFailedRecipientsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.innerExceptions = (SmtpFailedRecipientException[])info.GetValue("innerExceptions", typeof(SmtpFailedRecipientException[]));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />. </param>
		// Token: 0x06001DF5 RID: 7669 RVA: 0x0005BA88 File Offset: 0x00059C88
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Gets one or more <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />s that indicate the e-mail recipients with SMTP delivery errors.</summary>
		/// <returns>An array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> that lists the recipients with delivery errors.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0005BA94 File Offset: 0x00059C94
		public SmtpFailedRecipientException[] InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x06001DF7 RID: 7671 RVA: 0x0005BA9C File Offset: 0x00059C9C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("innerExceptions", this.innerExceptions);
		}

		// Token: 0x0400128E RID: 4750
		private SmtpFailedRecipientException[] innerExceptions;
	}
}
