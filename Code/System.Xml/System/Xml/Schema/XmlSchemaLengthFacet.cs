using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the length facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the length of a simpleType element on the data type.</summary>
	// Token: 0x02000225 RID: 549
	public class XmlSchemaLengthFacet : XmlSchemaNumericFacet
	{
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00064AB8 File Offset: 0x00062CB8
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.length;
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00064ABC File Offset: 0x00062CBC
		internal static XmlSchemaLengthFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaLengthFacet xmlSchemaLengthFacet = new XmlSchemaLengthFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "length")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaLengthFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaLengthFacet.LineNumber = reader.LineNumber;
			xmlSchemaLengthFacet.LinePosition = reader.LinePosition;
			xmlSchemaLengthFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaLengthFacet.Id = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					Exception ex;
					xmlSchemaLengthFacet.IsFixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for fixed attribute", ex);
					}
				}
				else if (reader.Name == "value")
				{
					xmlSchemaLengthFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for group", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaLengthFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaLengthFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "length")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaLengthFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaLengthFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaLengthFacet;
		}

		// Token: 0x040008D1 RID: 2257
		private const string xmlname = "length";
	}
}
