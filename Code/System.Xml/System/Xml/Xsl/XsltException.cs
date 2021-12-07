using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>The exception that is thrown when an error occurs while processing an XSLT transformation.</summary>
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public class XsltException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltException" /> class.</summary>
		// Token: 0x060011EE RID: 4590 RVA: 0x000517B4 File Offset: 0x0004F9B4
		public XsltException() : this(string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltException" /> class with a specified error message. </summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060011EF RID: 4591 RVA: 0x000517C4 File Offset: 0x0004F9C4
		public XsltException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the XsltException class.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> which threw the XsltException, if any. This value can be null. </param>
		// Token: 0x060011F0 RID: 4592 RVA: 0x000517D0 File Offset: 0x0004F9D0
		public XsltException(string message, Exception innerException) : this("{0}", message, innerException, 0, 0, null)
		{
		}

		/// <summary>Initializes a new instance of the XsltException class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The SerializationInfo object containing all the properties of an XsltException. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060011F1 RID: 4593 RVA: 0x000517E4 File Offset: 0x0004F9E4
		protected XsltException(SerializationInfo info, StreamingContext context)
		{
			this.lineNumber = info.GetInt32("lineNumber");
			this.linePosition = info.GetInt32("linePosition");
			this.sourceUri = info.GetString("sourceUri");
			this.templateFrames = info.GetString("templateFrames");
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0005183C File Offset: 0x0004FA3C
		internal XsltException(string msgFormat, string message, Exception innerException, int lineNumber, int linePosition, string sourceUri) : base(XsltException.CreateMessage(msgFormat, message, lineNumber, linePosition, sourceUri), innerException)
		{
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
			this.sourceUri = sourceUri;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00051878 File Offset: 0x0004FA78
		internal XsltException(string message, Exception innerException, XPathNavigator nav) : base(XsltException.CreateMessage(message, nav), innerException)
		{
			IXmlLineInfo xmlLineInfo = nav as IXmlLineInfo;
			this.lineNumber = ((xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber);
			this.linePosition = ((xmlLineInfo == null) ? 0 : xmlLineInfo.LinePosition);
			this.sourceUri = ((nav == null) ? string.Empty : nav.BaseURI);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000518E8 File Offset: 0x0004FAE8
		private static string CreateMessage(string message, XPathNavigator nav)
		{
			IXmlLineInfo xmlLineInfo = nav as IXmlLineInfo;
			int num = (xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber;
			int num2 = (xmlLineInfo == null) ? 0 : xmlLineInfo.LinePosition;
			string text = (nav == null) ? string.Empty : nav.BaseURI;
			if (num != 0)
			{
				return XsltException.CreateMessage("{0} at {1}({2},{3}).", message, num, num2, text);
			}
			return XsltException.CreateMessage("{0}.", message, num, num2, text);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0005195C File Offset: 0x0004FB5C
		private static string CreateMessage(string msgFormat, string message, int lineNumber, int linePosition, string sourceUri)
		{
			return string.Format(CultureInfo.InvariantCulture, msgFormat, new object[]
			{
				message,
				sourceUri,
				lineNumber.ToString(CultureInfo.InvariantCulture),
				linePosition.ToString(CultureInfo.InvariantCulture)
			});
		}

		/// <summary>Gets the line number indicating where the error occurred in the style sheet.</summary>
		/// <returns>The line number indicating where the error occurred in the style sheet.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x000519A4 File Offset: 0x0004FBA4
		public virtual int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred in the style sheet.</summary>
		/// <returns>The line position indicating where the error occurred in the style sheet.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x000519AC File Offset: 0x0004FBAC
		public virtual int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the formatted error message describing the current exception.</summary>
		/// <returns>The formatted error message describing the current exception.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000519B4 File Offset: 0x0004FBB4
		public override string Message
		{
			get
			{
				return (this.templateFrames == null) ? base.Message : (base.Message + this.templateFrames);
			}
		}

		/// <summary>Gets the location path of the style sheet.</summary>
		/// <returns>The location path of the style sheet.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x000519E0 File Offset: 0x0004FBE0
		public virtual string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Streams all the XsltException properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The SerializationInfo object. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060011FA RID: 4602 RVA: 0x000519E8 File Offset: 0x0004FBE8
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("templateFrames", this.templateFrames);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00051A44 File Offset: 0x0004FC44
		internal void AddTemplateFrame(string frame)
		{
			this.templateFrames += frame;
		}

		// Token: 0x0400075A RID: 1882
		private int lineNumber;

		// Token: 0x0400075B RID: 1883
		private int linePosition;

		// Token: 0x0400075C RID: 1884
		private string sourceUri;

		// Token: 0x0400075D RID: 1885
		private string templateFrames;
	}
}
