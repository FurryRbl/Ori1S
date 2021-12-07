using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) appinfo element.</summary>
	// Token: 0x020001FC RID: 508
	public class XmlSchemaAppInfo : XmlSchemaObject
	{
		/// <summary>Gets or sets the source of the application information.</summary>
		/// <returns>A Uniform Resource Identifier (URI) reference. The default is String.Empty.Optional.</returns>
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00057758 File Offset: 0x00055958
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x00057760 File Offset: 0x00055960
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets or sets an array of <see cref="T:System.Xml.XmlNode" /> objects that represents the appinfo child nodes.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlNode" /> objects that represents the appinfo child nodes.</returns>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0005776C File Offset: 0x0005596C
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00057774 File Offset: 0x00055974
		[XmlAnyElement]
		[XmlText]
		public XmlNode[] Markup
		{
			get
			{
				return this.markup;
			}
			set
			{
				this.markup = value;
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00057780 File Offset: 0x00055980
		internal static XmlSchemaAppInfo Read(XmlSchemaReader reader, ValidationEventHandler h, out bool skip)
		{
			skip = false;
			XmlSchemaAppInfo xmlSchemaAppInfo = new XmlSchemaAppInfo();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "appinfo")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAppInfo.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAppInfo.LineNumber = reader.LineNumber;
			xmlSchemaAppInfo.LinePosition = reader.LinePosition;
			xmlSchemaAppInfo.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "source")
				{
					xmlSchemaAppInfo.source = reader.Value;
				}
				else
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for appinfo", null);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				xmlSchemaAppInfo.Markup = new XmlNode[0];
				return xmlSchemaAppInfo;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.ReadNode(reader));
			XmlNode firstChild = xmlDocument.FirstChild;
			if (firstChild != null && firstChild.ChildNodes != null)
			{
				xmlSchemaAppInfo.Markup = new XmlNode[firstChild.ChildNodes.Count];
				for (int i = 0; i < firstChild.ChildNodes.Count; i++)
				{
					xmlSchemaAppInfo.Markup[i] = firstChild.ChildNodes[i];
				}
			}
			if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
			{
				skip = true;
			}
			return xmlSchemaAppInfo;
		}

		// Token: 0x040007B9 RID: 1977
		private XmlNode[] markup;

		// Token: 0x040007BA RID: 1978
		private string source;
	}
}
