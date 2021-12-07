using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml
{
	/// <summary>Returns detailed information about the last exception.</summary>
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class XmlException : SystemException
	{
		/// <summary>Initializes a new instance of the XmlException class.</summary>
		// Token: 0x060009B3 RID: 2483 RVA: 0x00033BD0 File Offset: 0x00031DD0
		public XmlException()
		{
			this.res = "Xml_DefaultException";
			this.messages = new string[1];
		}

		/// <summary>Initializes a new instance of the XmlException class.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the XmlException, if any. This value can be null. </param>
		// Token: 0x060009B4 RID: 2484 RVA: 0x00033BF0 File Offset: 0x00031DF0
		public XmlException(string message, Exception innerException) : base(message, innerException)
		{
			this.res = "Xml_UserException";
			this.messages = new string[]
			{
				message
			};
		}

		/// <summary>Initializes a new instance of the XmlException class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The SerializationInfo object containing all the properties of an XmlException. </param>
		/// <param name="context">The StreamingContext object containing the context information. </param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x00033C18 File Offset: 0x00031E18
		protected XmlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lineNumber = info.GetInt32("lineNumber");
			this.linePosition = info.GetInt32("linePosition");
			this.res = info.GetString("res");
			this.messages = (string[])info.GetValue("args", typeof(string[]));
			this.sourceUri = info.GetString("sourceUri");
		}

		/// <summary>Initializes a new instance of the XmlException class with a specified error message.</summary>
		/// <param name="message">The error description. </param>
		// Token: 0x060009B6 RID: 2486 RVA: 0x00033C94 File Offset: 0x00031E94
		public XmlException(string message) : base(message)
		{
			this.res = "Xml_UserException";
			this.messages = new string[]
			{
				message
			};
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00033CC4 File Offset: 0x00031EC4
		internal XmlException(IXmlLineInfo li, string sourceUri, string message) : this(li, null, sourceUri, message)
		{
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00033CD0 File Offset: 0x00031ED0
		internal XmlException(IXmlLineInfo li, Exception innerException, string sourceUri, string message) : this(message, innerException)
		{
			if (li != null)
			{
				this.lineNumber = li.LineNumber;
				this.linePosition = li.LinePosition;
			}
			this.sourceUri = sourceUri;
		}

		/// <summary>Initializes a new instance of the XmlException class with the specified message, inner exception, line number, and line position.</summary>
		/// <param name="message">The error description. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. This value can be null. </param>
		/// <param name="lineNumber">The line number indicating where the error occurred. </param>
		/// <param name="linePosition">The line position indicating where the error occurred. </param>
		// Token: 0x060009B9 RID: 2489 RVA: 0x00033D0C File Offset: 0x00031F0C
		public XmlException(string message, Exception innerException, int lineNumber, int linePosition) : this(message, innerException)
		{
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00033D28 File Offset: 0x00031F28
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00033D30 File Offset: 0x00031F30
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the location of the XML file.</summary>
		/// <returns>The source URI for the XML data. If there is no source URI, this property returns null.</returns>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00033D38 File Offset: 0x00031F38
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets a message describing the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception.</returns>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00033D40 File Offset: 0x00031F40
		public override string Message
		{
			get
			{
				if (this.lineNumber == 0)
				{
					return base.Message;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0} {3} Line {1}, position {2}.", new object[]
				{
					base.Message,
					this.lineNumber,
					this.linePosition,
					this.sourceUri
				});
			}
		}

		/// <summary>Streams all the XmlException properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The SerializationInfo object. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060009BE RID: 2494 RVA: 0x00033DA4 File Offset: 0x00031FA4
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("res", this.res);
			info.AddValue("args", this.messages);
			info.AddValue("sourceUri", this.sourceUri);
		}

		// Token: 0x040004EA RID: 1258
		private const string Xml_DefaultException = "Xml_DefaultException";

		// Token: 0x040004EB RID: 1259
		private const string Xml_UserException = "Xml_UserException";

		// Token: 0x040004EC RID: 1260
		private int lineNumber;

		// Token: 0x040004ED RID: 1261
		private int linePosition;

		// Token: 0x040004EE RID: 1262
		private string sourceUri;

		// Token: 0x040004EF RID: 1263
		private string res;

		// Token: 0x040004F0 RID: 1264
		private string[] messages;
	}
}
