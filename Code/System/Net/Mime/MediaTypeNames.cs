using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the media type information for an e-mail message attachment.</summary>
	// Token: 0x02000351 RID: 849
	public static class MediaTypeNames
	{
		/// <summary>Specifies the kind of application data in an e-mail message attachment.</summary>
		// Token: 0x02000352 RID: 850
		public static class Application
		{
			// Token: 0x040012B7 RID: 4791
			private const string prefix = "application/";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is not interpreted.</summary>
			// Token: 0x040012B8 RID: 4792
			public const string Octet = "application/octet-stream";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Portable Document Format (PDF).</summary>
			// Token: 0x040012B9 RID: 4793
			public const string Pdf = "application/pdf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x040012BA RID: 4794
			public const string Rtf = "application/rtf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is a SOAP document.</summary>
			// Token: 0x040012BB RID: 4795
			public const string Soap = "application/soap+xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is compressed.</summary>
			// Token: 0x040012BC RID: 4796
			public const string Zip = "application/zip";
		}

		/// <summary>Specifies the type of image data in an e-mail message attachment.</summary>
		// Token: 0x02000353 RID: 851
		public static class Image
		{
			// Token: 0x040012BD RID: 4797
			private const string prefix = "image/";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Graphics Interchange Format (GIF).</summary>
			// Token: 0x040012BE RID: 4798
			public const string Gif = "image/gif";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Joint Photographic Experts Group (JPEG) format.</summary>
			// Token: 0x040012BF RID: 4799
			public const string Jpeg = "image/jpeg";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Tagged Image File Format (TIFF).</summary>
			// Token: 0x040012C0 RID: 4800
			public const string Tiff = "image/tiff";
		}

		/// <summary>Specifies the type of text data in an e-mail message attachment.</summary>
		// Token: 0x02000354 RID: 852
		public static class Text
		{
			// Token: 0x040012C1 RID: 4801
			private const string prefix = "text/";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in HTML format.</summary>
			// Token: 0x040012C2 RID: 4802
			public const string Html = "text/html";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in plain text format.</summary>
			// Token: 0x040012C3 RID: 4803
			public const string Plain = "text/plain";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x040012C4 RID: 4804
			public const string RichText = "text/richtext";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in XML format.</summary>
			// Token: 0x040012C5 RID: 4805
			public const string Xml = "text/xml";
		}
	}
}
