using System;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an e-mail message that can be sent using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x0200033E RID: 830
	public class MailMessage : IDisposable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.MailMessage" /> class.</summary>
		// Token: 0x06001D6B RID: 7531 RVA: 0x0005929C File Offset: 0x0005749C
		public MailMessage()
		{
			this.to = new MailAddressCollection();
			this.alternateViews = new AlternateViewCollection();
			this.attachments = new AttachmentCollection();
			this.bcc = new MailAddressCollection();
			this.cc = new MailAddressCollection();
			this.replyTo = new MailAddressCollection();
			this.headers = new System.Collections.Specialized.NameValueCollection();
			this.headers.Add("MIME-Version", "1.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.Net.Mail.MailAddress" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the recipient of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06001D6C RID: 7532 RVA: 0x0005931C File Offset: 0x0005751C
		public MailMessage(MailAddress from, MailAddress to) : this()
		{
			if (from == null || to == null)
			{
				throw new ArgumentNullException();
			}
			this.From = from;
			this.to.Add(to);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.String" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the addresses of the recipients of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06001D6D RID: 7533 RVA: 0x0005934C File Offset: 0x0005754C
		public MailMessage(string from, string to) : this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[]
			{
				','
			}))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the address of the recipient of the e-mail message.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject text.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06001D6E RID: 7534 RVA: 0x000593F0 File Offset: 0x000575F0
		public MailMessage(string from, string to, string subject, string body) : this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[]
			{
				','
			}))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
			this.Body = body;
			this.Subject = subject;
		}

		/// <summary>Gets the attachment collection used to store alternate forms of the message body.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AlternateViewCollection" />.</returns>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x000594A0 File Offset: 0x000576A0
		public AlternateViewCollection AlternateViews
		{
			get
			{
				return this.alternateViews;
			}
		}

		/// <summary>Gets the attachment collection used to store data attached to this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AttachmentCollection" />.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000594A8 File Offset: 0x000576A8
		public AttachmentCollection Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		/// <summary>Gets the address collection that contains the blind carbon copy (BCC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x000594B0 File Offset: 0x000576B0
		public MailAddressCollection Bcc
		{
			get
			{
				return this.bcc;
			}
		}

		/// <summary>Gets or sets the message body.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the body text.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000594B8 File Offset: 0x000576B8
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x000594C0 File Offset: 0x000576C0
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				if (value != null && this.bodyEncoding == null)
				{
					this.bodyEncoding = (this.GuessEncoding(value) ?? Encoding.ASCII);
				}
				this.body = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x000594F4 File Offset: 0x000576F4
		internal System.Net.Mime.ContentType BodyContentType
		{
			get
			{
				return new System.Net.Mime.ContentType((!this.isHtml) ? "text/plain" : "text/html")
				{
					CharSet = (this.BodyEncoding ?? Encoding.ASCII).HeaderName
				};
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x00059540 File Offset: 0x00057740
		internal System.Net.Mime.TransferEncoding ContentTransferEncoding
		{
			get
			{
				return System.Net.Mime.ContentType.GuessTransferEncoding(this.BodyEncoding);
			}
		}

		/// <summary>Gets or sets the encoding used to encode the message body.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00059550 File Offset: 0x00057750
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x00059558 File Offset: 0x00057758
		public Encoding BodyEncoding
		{
			get
			{
				return this.bodyEncoding;
			}
			set
			{
				this.bodyEncoding = value;
			}
		}

		/// <summary>Gets the address collection that contains the carbon copy (CC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00059564 File Offset: 0x00057764
		public MailAddressCollection CC
		{
			get
			{
				return this.cc;
			}
		}

		/// <summary>Gets or sets the delivery notifications for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.DeliveryNotificationOptions" /> value that contains the delivery notifications for this message.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0005956C File Offset: 0x0005776C
		// (set) Token: 0x06001D7A RID: 7546 RVA: 0x00059574 File Offset: 0x00057774
		public DeliveryNotificationOptions DeliveryNotificationOptions
		{
			get
			{
				return this.deliveryNotificationOptions;
			}
			set
			{
				this.deliveryNotificationOptions = value;
			}
		}

		/// <summary>Gets or sets the from address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the from address information.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00059580 File Offset: 0x00057780
		// (set) Token: 0x06001D7C RID: 7548 RVA: 0x00059588 File Offset: 0x00057788
		public MailAddress From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		/// <summary>Gets the e-mail headers that are transmitted with this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the e-mail headers.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x00059594 File Offset: 0x00057794
		public System.Collections.Specialized.NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets or sets a value indicating whether the mail message body is in Html.</summary>
		/// <returns>true if the message body is in Html; else false. The default is false.</returns>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0005959C File Offset: 0x0005779C
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x000595A4 File Offset: 0x000577A4
		public bool IsBodyHtml
		{
			get
			{
				return this.isHtml;
			}
			set
			{
				this.isHtml = value;
			}
		}

		/// <summary>Gets or sets the priority of this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailPriority" /> that contains the priority of this message.</returns>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000595B0 File Offset: 0x000577B0
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x000595B8 File Offset: 0x000577B8
		public MailPriority Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x000595C4 File Offset: 0x000577C4
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x000595CC File Offset: 0x000577CC
		internal Encoding HeadersEncoding
		{
			get
			{
				return this.headersEncoding;
			}
			set
			{
				this.headersEncoding = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x000595D8 File Offset: 0x000577D8
		internal MailAddressCollection ReplyToList
		{
			get
			{
				return this.replyTo;
			}
		}

		/// <summary>Gets or sets the ReplyTo address for the mail message.</summary>
		/// <returns>A MailAddress that indicates the value of the <see cref="P:System.Net.Mail.MailMessage.ReplyTo" /> field.</returns>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x000595E0 File Offset: 0x000577E0
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x00059600 File Offset: 0x00057800
		public MailAddress ReplyTo
		{
			get
			{
				if (this.replyTo.Count == 0)
				{
					return null;
				}
				return this.replyTo[0];
			}
			set
			{
				this.replyTo.Clear();
				this.replyTo.Add(value);
			}
		}

		/// <summary>Gets or sets the sender's address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the sender's address information.</returns>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0005961C File Offset: 0x0005781C
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x00059624 File Offset: 0x00057824
		public MailAddress Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		/// <summary>Gets or sets the subject line for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the subject content.</returns>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00059630 File Offset: 0x00057830
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x00059638 File Offset: 0x00057838
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				if (value != null && this.subjectEncoding == null)
				{
					this.subjectEncoding = this.GuessEncoding(value);
				}
				this.subject = value;
			}
		}

		/// <summary>Gets or sets the encoding used for the subject content for this e-mail message.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that was used to encode the <see cref="P:System.Net.Mail.MailMessage.Subject" /> property.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00059660 File Offset: 0x00057860
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x00059668 File Offset: 0x00057868
		public Encoding SubjectEncoding
		{
			get
			{
				return this.subjectEncoding;
			}
			set
			{
				this.subjectEncoding = value;
			}
		}

		/// <summary>Gets the address collection that contains the recipients of this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00059674 File Offset: 0x00057874
		public MailAddressCollection To
		{
			get
			{
				return this.to;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.MailMessage" />. </summary>
		// Token: 0x06001D8E RID: 7566 RVA: 0x0005967C File Offset: 0x0005787C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.MailMessage" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001D8F RID: 7567 RVA: 0x0005968C File Offset: 0x0005788C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00059690 File Offset: 0x00057890
		private Encoding GuessEncoding(string s)
		{
			return System.Net.Mime.ContentType.GuessEncoding(s);
		}

		// Token: 0x0400123E RID: 4670
		private AlternateViewCollection alternateViews;

		// Token: 0x0400123F RID: 4671
		private AttachmentCollection attachments;

		// Token: 0x04001240 RID: 4672
		private MailAddressCollection bcc;

		// Token: 0x04001241 RID: 4673
		private MailAddressCollection replyTo;

		// Token: 0x04001242 RID: 4674
		private string body;

		// Token: 0x04001243 RID: 4675
		private MailPriority priority;

		// Token: 0x04001244 RID: 4676
		private MailAddress sender;

		// Token: 0x04001245 RID: 4677
		private DeliveryNotificationOptions deliveryNotificationOptions;

		// Token: 0x04001246 RID: 4678
		private MailAddressCollection cc;

		// Token: 0x04001247 RID: 4679
		private MailAddress from;

		// Token: 0x04001248 RID: 4680
		private System.Collections.Specialized.NameValueCollection headers;

		// Token: 0x04001249 RID: 4681
		private MailAddressCollection to;

		// Token: 0x0400124A RID: 4682
		private string subject;

		// Token: 0x0400124B RID: 4683
		private Encoding subjectEncoding;

		// Token: 0x0400124C RID: 4684
		private Encoding bodyEncoding;

		// Token: 0x0400124D RID: 4685
		private Encoding headersEncoding = Encoding.UTF8;

		// Token: 0x0400124E RID: 4686
		private bool isHtml;
	}
}
