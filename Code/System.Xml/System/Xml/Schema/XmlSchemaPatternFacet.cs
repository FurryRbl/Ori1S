using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the pattern element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the value entered for a simpleType element.</summary>
	// Token: 0x02000235 RID: 565
	public class XmlSchemaPatternFacet : XmlSchemaFacet
	{
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0006680C File Offset: 0x00064A0C
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.pattern;
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00066810 File Offset: 0x00064A10
		internal static XmlSchemaPatternFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaPatternFacet xmlSchemaPatternFacet = new XmlSchemaPatternFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "pattern")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaPatternFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaPatternFacet.LineNumber = reader.LineNumber;
			xmlSchemaPatternFacet.LinePosition = reader.LinePosition;
			xmlSchemaPatternFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaPatternFacet.Id = reader.Value;
				}
				else if (reader.Name == "value")
				{
					xmlSchemaPatternFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for pattern", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaPatternFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaPatternFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "pattern")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaPatternFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaPatternFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaPatternFacet;
		}

		// Token: 0x040008F9 RID: 2297
		private const string xmlname = "pattern";
	}
}
