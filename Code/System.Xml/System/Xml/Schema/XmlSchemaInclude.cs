using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the include element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to include declarations and definitions from an external schema. The included declarations and definitions are then available for processing in the containing schema.</summary>
	// Token: 0x0200021D RID: 541
	public class XmlSchemaInclude : XmlSchemaExternal
	{
		/// <summary>Gets or sets the annotation property.</summary>
		/// <returns>The annotation.</returns>
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00062198 File Offset: 0x00060398
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x000621A0 File Offset: 0x000603A0
		[XmlElement("annotation", Type = typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000621AC File Offset: 0x000603AC
		internal static XmlSchemaInclude Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaInclude xmlSchemaInclude = new XmlSchemaInclude();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "include")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaInclude.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaInclude.LineNumber = reader.LineNumber;
			xmlSchemaInclude.LinePosition = reader.LinePosition;
			xmlSchemaInclude.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaInclude.Id = reader.Value;
				}
				else if (reader.Name == "schemaLocation")
				{
					xmlSchemaInclude.SchemaLocation = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for include", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaInclude);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaInclude;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "include")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaInclude.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaInclude.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaInclude;
		}

		// Token: 0x0400089F RID: 2207
		private const string xmlname = "include";

		// Token: 0x040008A0 RID: 2208
		private XmlSchemaAnnotation annotation;
	}
}
