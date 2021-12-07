using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the import element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to import schema components from other schemas.</summary>
	// Token: 0x0200021C RID: 540
	public class XmlSchemaImport : XmlSchemaExternal
	{
		/// <summary>Gets or sets the target namespace for the imported schema as a Uniform Resource Identifier (URI) reference.</summary>
		/// <returns>The target namespace for the imported schema as a URI reference.Optional.</returns>
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00061F58 File Offset: 0x00060158
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x00061F60 File Offset: 0x00060160
		[XmlAttribute("namespace", DataType = "anyURI")]
		public string Namespace
		{
			get
			{
				return this.nameSpace;
			}
			set
			{
				this.nameSpace = value;
			}
		}

		/// <summary>Gets or sets the annotation property.</summary>
		/// <returns>The annotation.</returns>
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00061F6C File Offset: 0x0006016C
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x00061F74 File Offset: 0x00060174
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

		// Token: 0x06001594 RID: 5524 RVA: 0x00061F80 File Offset: 0x00060180
		internal static XmlSchemaImport Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "import")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaImport.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaImport.LineNumber = reader.LineNumber;
			xmlSchemaImport.LinePosition = reader.LinePosition;
			xmlSchemaImport.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaImport.Id = reader.Value;
				}
				else if (reader.Name == "namespace")
				{
					xmlSchemaImport.nameSpace = reader.Value;
				}
				else if (reader.Name == "schemaLocation")
				{
					xmlSchemaImport.SchemaLocation = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for import", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaImport);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaImport;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "import")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaImport.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaImport.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaImport;
		}

		// Token: 0x0400089C RID: 2204
		private const string xmlname = "import";

		// Token: 0x0400089D RID: 2205
		private XmlSchemaAnnotation annotation;

		// Token: 0x0400089E RID: 2206
		private string nameSpace;
	}
}
