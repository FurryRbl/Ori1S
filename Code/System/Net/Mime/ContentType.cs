using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	/// <summary>Represents a MIME protocol Content-Type header.</summary>
	// Token: 0x0200034F RID: 847
	public class ContentType
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Net.Mime.ContentType" /> class. </summary>
		// Token: 0x06001E21 RID: 7713 RVA: 0x0005C4A0 File Offset: 0x0005A6A0
		public ContentType()
		{
			this.mediaType = "application/octet-stream";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentType" /> class using the specified string. </summary>
		/// <param name="contentType">A <see cref="T:System.String" />, for example, "text/plain; charset=us-ascii", that contains the MIME media type, subtype, and optional parameters.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contentType" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is in a form that cannot be parsed.</exception>
		// Token: 0x06001E22 RID: 7714 RVA: 0x0005C4C0 File Offset: 0x0005A6C0
		public ContentType(string contentType)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (contentType.Length < 1)
			{
				throw new ArgumentException("contentType");
			}
			int num = contentType.IndexOf(';');
			if (num > 0)
			{
				string[] array = contentType.Split(new char[]
				{
					';'
				});
				this.MediaType = array[0].Trim();
				for (int i = 1; i < array.Length; i++)
				{
					this.Parse(array[i]);
				}
			}
			else
			{
				this.MediaType = contentType.Trim();
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0005C580 File Offset: 0x0005A780
		private void Parse(string pair)
		{
			if (pair == null || pair.Length < 1)
			{
				return;
			}
			string[] array = pair.Split(new char[]
			{
				'='
			});
			if (array.Length == 2)
			{
				this.parameters.Add(array[0].Trim(), array[1].Trim());
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0005C5D8 File Offset: 0x0005A7D8
		private static Encoding UTF8Unmarked
		{
			get
			{
				if (ContentType.utf8unmarked == null)
				{
					ContentType.utf8unmarked = new UTF8Encoding(false);
				}
				return ContentType.utf8unmarked;
			}
		}

		/// <summary>Gets or sets the value of the boundary parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the boundary parameter.</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x0005C5F4 File Offset: 0x0005A7F4
		// (set) Token: 0x06001E27 RID: 7719 RVA: 0x0005C608 File Offset: 0x0005A808
		public string Boundary
		{
			get
			{
				return this.parameters["boundary"];
			}
			set
			{
				this.parameters["boundary"] = value;
			}
		}

		/// <summary>Gets or sets the value of the charset parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the charset parameter.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x0005C61C File Offset: 0x0005A81C
		// (set) Token: 0x06001E29 RID: 7721 RVA: 0x0005C630 File Offset: 0x0005A830
		public string CharSet
		{
			get
			{
				return this.parameters["charset"];
			}
			set
			{
				this.parameters["charset"] = value;
			}
		}

		/// <summary>Gets or sets the media type value included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the media type and subtype value. This value does not include the semicolon (;) separator that follows the subtype.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">The value specified for a set operation is in a form that cannot be parsed.</exception>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x0005C644 File Offset: 0x0005A844
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x0005C64C File Offset: 0x0005A84C
		public string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.Length < 1)
				{
					throw new ArgumentException();
				}
				if (value.IndexOf('/') < 1)
				{
					throw new FormatException();
				}
				if (value.IndexOf(';') != -1)
				{
					throw new FormatException();
				}
				this.mediaType = value;
			}
		}

		/// <summary>Gets or sets the value of the name parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the name parameter. </returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		public string Name
		{
			get
			{
				return this.parameters["name"];
			}
			set
			{
				this.parameters["name"] = value;
			}
		}

		/// <summary>Gets the dictionary that contains the parameters included in the Content-Type header represented by this instance.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains name and value pairs.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0005C6D0 File Offset: 0x0005A8D0
		public System.Collections.Specialized.StringDictionary Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		/// <summary>Determines whether the content-type header of the specified <see cref="T:System.Net.Mime.ContentType" /> object is equal to the content-type header of this object.</summary>
		/// <returns>true if the content-type headers are the same; otherwise false.</returns>
		/// <param name="rparam">The <see cref="T:System.Net.Mime.ContentType" /> object to compare with this object.</param>
		// Token: 0x06001E2F RID: 7727 RVA: 0x0005C6D8 File Offset: 0x0005A8D8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ContentType);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0005C6E8 File Offset: 0x0005A8E8
		private bool Equals(ContentType other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		/// <summary>Determines the hash code of the specified <see cref="T:System.Net.Mime.ContentType" /> object</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06001E31 RID: 7729 RVA: 0x0005C704 File Offset: 0x0005A904
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Net.Mime.ContentType" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the current settings for this <see cref="T:System.Net.Mime.ContentType" />.</returns>
		// Token: 0x06001E32 RID: 7730 RVA: 0x0005C714 File Offset: 0x0005A914
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Encoding enc = (this.CharSet == null) ? Encoding.UTF8 : Encoding.GetEncoding(this.CharSet);
			stringBuilder.Append(this.MediaType);
			if (this.Parameters != null && this.Parameters.Count > 0)
			{
				foreach (object obj in this.parameters)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null && dictionaryEntry.Value.ToString().Length > 0)
					{
						stringBuilder.Append("; ");
						stringBuilder.Append(dictionaryEntry.Key);
						stringBuilder.Append("=");
						stringBuilder.Append(ContentType.WrapIfEspecialsExist(ContentType.EncodeSubjectRFC2047(dictionaryEntry.Value as string, enc)));
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0005C83C File Offset: 0x0005AA3C
		private static string WrapIfEspecialsExist(string s)
		{
			s = s.Replace("\"", "\\\"");
			if (s.IndexOfAny(ContentType.especials) >= 0)
			{
				return '"' + s + '"';
			}
			return s;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0005C878 File Offset: 0x0005AA78
		internal static Encoding GuessEncoding(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					return ContentType.UTF8Unmarked;
				}
			}
			return null;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0005C8B4 File Offset: 0x0005AAB4
		internal static TransferEncoding GuessTransferEncoding(Encoding enc)
		{
			if (Encoding.ASCII.Equals(enc))
			{
				return TransferEncoding.SevenBit;
			}
			if (Encoding.UTF8.CodePage == enc.CodePage || Encoding.Unicode.CodePage == enc.CodePage || Encoding.UTF32.CodePage == enc.CodePage)
			{
				return TransferEncoding.Base64;
			}
			return TransferEncoding.QuotedPrintable;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0005C918 File Offset: 0x0005AB18
		internal static string To2047(byte[] bytes)
		{
			StringWriter stringWriter = new StringWriter();
			foreach (byte b in bytes)
			{
				if (b > 127 || b == 9)
				{
					stringWriter.Write("=");
					stringWriter.Write(Convert.ToString(b, 16).ToUpper());
				}
				else
				{
					stringWriter.Write(Convert.ToChar(b));
				}
			}
			return stringWriter.GetStringBuilder().ToString();
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0005C990 File Offset: 0x0005AB90
		internal static string EncodeSubjectRFC2047(string s, Encoding enc)
		{
			if (s == null || Encoding.ASCII.Equals(enc))
			{
				return s;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					string text = ContentType.To2047(enc.GetBytes(s));
					return string.Concat(new string[]
					{
						"=?",
						enc.HeaderName,
						"?Q?",
						text,
						"?="
					});
				}
			}
			return s;
		}

		// Token: 0x040012B1 RID: 4785
		private static Encoding utf8unmarked;

		// Token: 0x040012B2 RID: 4786
		private string mediaType;

		// Token: 0x040012B3 RID: 4787
		private System.Collections.Specialized.StringDictionary parameters = new System.Collections.Specialized.StringDictionary();

		// Token: 0x040012B4 RID: 4788
		private static readonly char[] especials = new char[]
		{
			'(',
			')',
			'<',
			'>',
			'@',
			',',
			';',
			':',
			'<',
			'>',
			'/',
			'[',
			']',
			'?',
			'.',
			'='
		};
	}
}
