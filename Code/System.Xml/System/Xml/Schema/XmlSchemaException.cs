using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Returns detailed information about the schema exception.</summary>
	// Token: 0x02000212 RID: 530
	[Serializable]
	public class XmlSchemaException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class.</summary>
		// Token: 0x06001540 RID: 5440 RVA: 0x000604E4 File Offset: 0x0005E6E4
		public XmlSchemaException() : this("A schema error occured.", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		// Token: 0x06001541 RID: 5441 RVA: 0x000604F4 File Offset: 0x0005E6F4
		public XmlSchemaException(string message) : this(message, null)
		{
		}

		/// <summary>Constructs a new XmlSchemaException object with the given SerializationInfo and StreamingContext information that contains all the properties of the XmlSchemaException.</summary>
		/// <param name="info">SerializationInfo.</param>
		/// <param name="context">StreamingContext.</param>
		// Token: 0x06001542 RID: 5442 RVA: 0x00060500 File Offset: 0x0005E700
		protected XmlSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.hasLineInfo = info.GetBoolean("hasLineInfo");
			this.lineNumber = info.GetInt32("lineNumber");
			this.linePosition = info.GetInt32("linePosition");
			this.sourceUri = info.GetString("sourceUri");
			this.sourceObj = (info.GetValue("sourceObj", typeof(XmlSchemaObject)) as XmlSchemaObject);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified, and the original <see cref="T:System.Exception" /> object, line number, and line position of the XML that cause this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		/// <param name="lineNumber">The line number of the XML that caused this exception.</param>
		/// <param name="linePosition">The line position of the XML that caused this exception.</param>
		// Token: 0x06001543 RID: 5443 RVA: 0x0006057C File Offset: 0x0005E77C
		public XmlSchemaException(string message, Exception innerException, int lineNumber, int linePosition) : this(message, lineNumber, linePosition, null, null, innerException)
		{
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0006058C File Offset: 0x0005E78C
		internal XmlSchemaException(string message, int lineNumber, int linePosition, XmlSchemaObject sourceObject, string sourceUri, Exception innerException) : base(XmlSchemaException.GetMessage(message, sourceUri, lineNumber, linePosition, sourceObject), innerException)
		{
			this.hasLineInfo = true;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
			this.sourceObj = sourceObject;
			this.sourceUri = sourceUri;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000605C8 File Offset: 0x0005E7C8
		internal XmlSchemaException(string message, object sender, string sourceUri, XmlSchemaObject sourceObject, Exception innerException) : base(XmlSchemaException.GetMessage(message, sourceUri, sender, sourceObject), innerException)
		{
			IXmlLineInfo xmlLineInfo = sender as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				this.hasLineInfo = true;
				this.lineNumber = xmlLineInfo.LineNumber;
				this.linePosition = xmlLineInfo.LinePosition;
			}
			this.sourceObj = sourceObject;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00060628 File Offset: 0x0005E828
		internal XmlSchemaException(string message, XmlSchemaObject sourceObject, Exception innerException) : base(XmlSchemaException.GetMessage(message, null, 0, 0, sourceObject), innerException)
		{
			this.hasLineInfo = true;
			this.lineNumber = sourceObject.LineNumber;
			this.linePosition = sourceObject.LinePosition;
			this.sourceObj = sourceObject;
			this.sourceUri = sourceObject.SourceUri;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message and original <see cref="T:System.Exception" /> object that caused this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		// Token: 0x06001547 RID: 5447 RVA: 0x00060678 File Offset: 0x0005E878
		public XmlSchemaException(string message, Exception innerException) : base(XmlSchemaException.GetMessage(message, null, 0, 0, null), innerException)
		{
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0006068C File Offset: 0x0005E88C
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00060694 File Offset: 0x0005E894
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>The XmlSchemaObject that produced the XmlSchemaException.</summary>
		/// <returns>A valid object instance represents a structural validation error in the XML Schema Object Model (SOM).</returns>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0006069C File Offset: 0x0005E89C
		public XmlSchemaObject SourceSchemaObject
		{
			get
			{
				return this.sourceObj;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) location of the schema that caused the exception.</summary>
		/// <returns>The URI location of the schema that caused the exception.</returns>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000606A4 File Offset: 0x0005E8A4
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000606AC File Offset: 0x0005E8AC
		private static string GetMessage(string message, string sourceUri, object sender, XmlSchemaObject sourceObj)
		{
			IXmlLineInfo xmlLineInfo = sender as IXmlLineInfo;
			if (xmlLineInfo == null)
			{
				return XmlSchemaException.GetMessage(message, sourceUri, 0, 0, sourceObj);
			}
			return XmlSchemaException.GetMessage(message, sourceUri, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition, sourceObj);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000606E8 File Offset: 0x0005E8E8
		private static string GetMessage(string message, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject sourceObj)
		{
			string text = "XmlSchema error: " + message;
			if (lineNumber > 0)
			{
				text += string.Format(CultureInfo.InvariantCulture, " XML {0} Line {1}, Position {2}.", new object[]
				{
					(sourceUri == null || !(sourceUri != string.Empty)) ? string.Empty : ("URI: " + sourceUri + " ."),
					lineNumber,
					linePosition
				});
			}
			if (sourceObj != null)
			{
				text += string.Format(CultureInfo.InvariantCulture, " Related schema item SourceUri: {0}, Line {1}, Position {2}.", new object[]
				{
					sourceObj.SourceUri,
					sourceObj.LineNumber,
					sourceObj.LinePosition
				});
			}
			return text;
		}

		/// <summary>Gets the description of the error condition of this exception.</summary>
		/// <returns>The description of the error condition of this exception.</returns>
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000607B8 File Offset: 0x0005E9B8
		public override string Message
		{
			get
			{
				return base.Message;
			}
		}

		/// <summary>Streams all the XmlSchemaException properties into the SerializationInfo class for the given StreamingContext.</summary>
		/// <param name="info">The SerializationInfo. </param>
		/// <param name="context">The StreamingContext information. </param>
		// Token: 0x0600154F RID: 5455 RVA: 0x000607C0 File Offset: 0x0005E9C0
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hasLineInfo", this.hasLineInfo);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("sourceObj", this.sourceObj);
		}

		// Token: 0x0400086D RID: 2157
		private bool hasLineInfo;

		// Token: 0x0400086E RID: 2158
		private int lineNumber;

		// Token: 0x0400086F RID: 2159
		private int linePosition;

		// Token: 0x04000870 RID: 2160
		private XmlSchemaObject sourceObj;

		// Token: 0x04000871 RID: 2161
		private string sourceUri;
	}
}
