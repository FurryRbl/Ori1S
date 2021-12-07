using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the unique element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to identify a unique constraint among a set of elements.</summary>
	// Token: 0x02000244 RID: 580
	public class XmlSchemaUnique : XmlSchemaIdentityConstraint
	{
		// Token: 0x06001760 RID: 5984 RVA: 0x000749CC File Offset: 0x00072BCC
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			return base.Compile(h, schema);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000749D8 File Offset: 0x00072BD8
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			return this.errorCount;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000749E0 File Offset: 0x00072BE0
		internal static XmlSchemaUnique Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaUnique xmlSchemaUnique = new XmlSchemaUnique();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "unique")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaUnique.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaUnique.LineNumber = reader.LineNumber;
			xmlSchemaUnique.LinePosition = reader.LinePosition;
			xmlSchemaUnique.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaUnique.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					xmlSchemaUnique.Name = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for unique", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaUnique);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaUnique;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "unique")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaUnion.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaUnique.Annotation = xmlSchemaAnnotation;
					}
				}
				else if (num <= 2 && reader.LocalName == "selector")
				{
					num = 3;
					XmlSchemaXPath xmlSchemaXPath = XmlSchemaXPath.Read(reader, h, "selector");
					if (xmlSchemaXPath != null)
					{
						xmlSchemaUnique.Selector = xmlSchemaXPath;
					}
				}
				else if (num <= 3 && reader.LocalName == "field")
				{
					num = 3;
					if (xmlSchemaUnique.Selector == null)
					{
						XmlSchemaObject.error(h, "selector must be defined before field declarations", null);
					}
					XmlSchemaXPath xmlSchemaXPath2 = XmlSchemaXPath.Read(reader, h, "field");
					if (xmlSchemaXPath2 != null)
					{
						xmlSchemaUnique.Fields.Add(xmlSchemaXPath2);
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaUnique;
		}

		// Token: 0x0400097D RID: 2429
		private const string xmlname = "unique";
	}
}
