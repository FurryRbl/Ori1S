using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) whiteSpace facet.</summary>
	// Token: 0x0200024A RID: 586
	public class XmlSchemaWhiteSpaceFacet : XmlSchemaFacet
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000776B0 File Offset: 0x000758B0
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.whiteSpace;
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000776B4 File Offset: 0x000758B4
		internal static XmlSchemaWhiteSpaceFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaWhiteSpaceFacet xmlSchemaWhiteSpaceFacet = new XmlSchemaWhiteSpaceFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "whiteSpace")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaWhiteSpaceFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaWhiteSpaceFacet.LineNumber = reader.LineNumber;
			xmlSchemaWhiteSpaceFacet.LinePosition = reader.LinePosition;
			xmlSchemaWhiteSpaceFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaWhiteSpaceFacet.Id = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					Exception ex;
					xmlSchemaWhiteSpaceFacet.IsFixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for fixed attribute", ex);
					}
				}
				else if (reader.Name == "value")
				{
					xmlSchemaWhiteSpaceFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for whiteSpace", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaWhiteSpaceFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaWhiteSpaceFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "whiteSpace")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaWhiteSpaceFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaWhiteSpaceFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaWhiteSpaceFacet;
		}

		// Token: 0x040009A8 RID: 2472
		private const string xmlname = "whiteSpace";
	}
}
