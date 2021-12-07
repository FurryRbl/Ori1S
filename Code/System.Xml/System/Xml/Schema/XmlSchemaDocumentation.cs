using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the documentation element from XML Schema as specified by the World Wide Web Consortium (W3C). This class specifies information to be read or used by humans within an annotation.</summary>
	// Token: 0x0200020F RID: 527
	public class XmlSchemaDocumentation : XmlSchemaObject
	{
		/// <summary>Gets or sets an array of XmlNodes that represents the documentation child nodes.</summary>
		/// <returns>The array that represents the documentation child nodes.</returns>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0005DEEC File Offset: 0x0005C0EC
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0005DEF4 File Offset: 0x0005C0F4
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

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) source of the information.</summary>
		/// <returns>A URI reference. The default is String.Empty.Optional.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0005DF00 File Offset: 0x0005C100
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0005DF08 File Offset: 0x0005C108
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

		/// <summary>Gets or sets the xml:lang attribute. This serves as an indicator of the language used in the contents.</summary>
		/// <returns>The xml:lang attribute.Optional.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0005DF14 File Offset: 0x0005C114
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0005DF1C File Offset: 0x0005C11C
		[XmlAttribute("xml:lang")]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0005DF28 File Offset: 0x0005C128
		internal static XmlSchemaDocumentation Read(XmlSchemaReader reader, ValidationEventHandler h, out bool skip)
		{
			skip = false;
			XmlSchemaDocumentation xmlSchemaDocumentation = new XmlSchemaDocumentation();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "documentation")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaDocumentation.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaDocumentation.LineNumber = reader.LineNumber;
			xmlSchemaDocumentation.LinePosition = reader.LinePosition;
			xmlSchemaDocumentation.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "source")
				{
					xmlSchemaDocumentation.source = reader.Value;
				}
				else if (reader.Name == "xml:lang")
				{
					xmlSchemaDocumentation.language = reader.Value;
				}
				else
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for documentation", null);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				xmlSchemaDocumentation.Markup = new XmlNode[0];
				return xmlSchemaDocumentation;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.ReadNode(reader));
			XmlNode firstChild = xmlDocument.FirstChild;
			if (firstChild != null && firstChild.ChildNodes != null)
			{
				xmlSchemaDocumentation.Markup = new XmlNode[firstChild.ChildNodes.Count];
				for (int i = 0; i < firstChild.ChildNodes.Count; i++)
				{
					xmlSchemaDocumentation.Markup[i] = firstChild.ChildNodes[i];
				}
			}
			if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
			{
				skip = true;
			}
			return xmlSchemaDocumentation;
		}

		// Token: 0x0400084D RID: 2125
		private string language;

		// Token: 0x0400084E RID: 2126
		private XmlNode[] markup;

		// Token: 0x0400084F RID: 2127
		private string source;
	}
}
