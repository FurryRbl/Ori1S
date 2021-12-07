using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an attachment to an e-mail.</summary>
	// Token: 0x02000338 RID: 824
	public class Attachment : AttachmentBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string. </summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is empty.</exception>
		// Token: 0x06001D39 RID: 7481 RVA: 0x00058BAC File Offset: 0x00056DAC
		public Attachment(string fileName) : base(fileName)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and MIME type information. </summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x06001D3A RID: 7482 RVA: 0x00058BC8 File Offset: 0x00056DC8
		public Attachment(string fileName, string mediaType) : base(fileName, mediaType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="string" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x06001D3B RID: 7483 RVA: 0x00058BE4 File Offset: 0x00056DE4
		public Attachment(string fileName, System.Net.Mime.ContentType contentType) : base(fileName, contentType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and content type. </summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="stream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is null.-or-<paramref name="contentStream" /> is null.</exception>
		// Token: 0x06001D3C RID: 7484 RVA: 0x00058C00 File Offset: 0x00056E00
		public Attachment(Stream contentStream, System.Net.Mime.ContentType contentType) : base(contentStream, contentType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and name.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		// Token: 0x06001D3D RID: 7485 RVA: 0x00058C18 File Offset: 0x00056E18
		public Attachment(Stream contentStream, string name) : base(contentStream)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream, name, and MIME type information. </summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be null.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x06001D3E RID: 7486 RVA: 0x00058C34 File Offset: 0x00056E34
		public Attachment(Stream contentStream, string name, string mediaType) : base(contentStream, mediaType)
		{
			this.Name = name;
		}

		/// <summary>Gets the MIME content disposition for this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mime.ContentDisposition" /> that provides the presentation information for this attachment. </returns>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00058C50 File Offset: 0x00056E50
		public System.Net.Mime.ContentDisposition ContentDisposition
		{
			get
			{
				return this.contentDisposition;
			}
		}

		/// <summary>Gets or sets the MIME content type name value in the content type associated with this attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value for the content type <paramref name="name" /> represented by the <see cref="P:System.Net.Mime.ContentType.Name" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x00058C58 File Offset: 0x00056E58
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x00058C68 File Offset: 0x00056E68
		public string Name
		{
			get
			{
				return base.ContentType.Name;
			}
			set
			{
				base.ContentType.Name = value;
			}
		}

		/// <summary>Specifies the encoding for the <see cref="T:System.Net.Mail.Attachment" /><see cref="P:System.Net.Mail.Attachment.Name" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> value that specifies the type of name encoding. The default value is determined from the name of the attachment.</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x00058C78 File Offset: 0x00056E78
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x00058C80 File Offset: 0x00056E80
		public Encoding NameEncoding
		{
			get
			{
				return this.nameEncoding;
			}
			set
			{
				this.nameEncoding = value;
			}
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> object that represents the Multipurpose Internet Mail Exchange (MIME) protocol Content-Type header to be used.</param>
		// Token: 0x06001D44 RID: 7492 RVA: 0x00058C8C File Offset: 0x00056E8C
		public static Attachment CreateAttachmentFromString(string content, System.Net.Mime.ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, contentType)
			{
				TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified MIME content type name.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		// Token: 0x06001D45 RID: 7493 RVA: 0x00058CDC File Offset: 0x00056EDC
		public static Attachment CreateAttachmentFromString(string content, string name)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, new System.Net.Mime.ContentType("text/plain"))
			{
				TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable,
				Name = name
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, the specified MIME content type name, character encoding, and MIME header information for the attachment.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be null.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		// Token: 0x06001D46 RID: 7494 RVA: 0x00058D3C File Offset: 0x00056F3C
		public static Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, contentEncoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, name, mediaType)
			{
				TransferEncoding = System.Net.Mime.ContentType.GuessTransferEncoding(contentEncoding),
				ContentType = 
				{
					CharSet = streamWriter.Encoding.BodyName
				}
			};
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00058DAC File Offset: 0x00056FAC
		private void InitName(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.Name = Path.GetFileName(fileName);
		}

		// Token: 0x04001233 RID: 4659
		private System.Net.Mime.ContentDisposition contentDisposition = new System.Net.Mime.ContentDisposition();

		// Token: 0x04001234 RID: 4660
		private Encoding nameEncoding;
	}
}
