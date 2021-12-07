using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the minInclusive element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum value of a simpleType element. The element value must be greater than or equal to the value of the minInclusive element.</summary>
	// Token: 0x0200022A RID: 554
	public class XmlSchemaMinInclusiveFacet : XmlSchemaFacet
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x000655EC File Offset: 0x000637EC
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.minInclusive;
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000655F4 File Offset: 0x000637F4
		internal static XmlSchemaMinInclusiveFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaMinInclusiveFacet xmlSchemaMinInclusiveFacet = new XmlSchemaMinInclusiveFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "minInclusive")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaMinInclusiveFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaMinInclusiveFacet.LineNumber = reader.LineNumber;
			xmlSchemaMinInclusiveFacet.LinePosition = reader.LinePosition;
			xmlSchemaMinInclusiveFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaMinInclusiveFacet.Id = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					Exception ex;
					xmlSchemaMinInclusiveFacet.IsFixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for fixed attribute", ex);
					}
				}
				else if (reader.Name == "value")
				{
					xmlSchemaMinInclusiveFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for minInclusive", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaMinInclusiveFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaMinInclusiveFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "minInclusive")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaMinInclusiveFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaMinInclusiveFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaMinInclusiveFacet;
		}

		// Token: 0x040008D6 RID: 2262
		private const string xmlname = "minInclusive";
	}
}
