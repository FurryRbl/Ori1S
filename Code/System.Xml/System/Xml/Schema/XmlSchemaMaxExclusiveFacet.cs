using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the maxExclusive element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the maximum value of a simpleType element. The element value must be less than the value of the maxExclusive element.</summary>
	// Token: 0x02000226 RID: 550
	public class XmlSchemaMaxExclusiveFacet : XmlSchemaFacet
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00064CF4 File Offset: 0x00062EF4
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.maxExclusive;
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00064CFC File Offset: 0x00062EFC
		internal static XmlSchemaMaxExclusiveFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaMaxExclusiveFacet xmlSchemaMaxExclusiveFacet = new XmlSchemaMaxExclusiveFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "maxExclusive")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaMaxExclusiveFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaMaxExclusiveFacet.LineNumber = reader.LineNumber;
			xmlSchemaMaxExclusiveFacet.LinePosition = reader.LinePosition;
			xmlSchemaMaxExclusiveFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaMaxExclusiveFacet.Id = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					Exception ex;
					xmlSchemaMaxExclusiveFacet.IsFixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for fixed attribute", ex);
					}
				}
				else if (reader.Name == "value")
				{
					xmlSchemaMaxExclusiveFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for maxExclusive", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaMaxExclusiveFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaMaxExclusiveFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "maxExclusive")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaMaxExclusiveFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaMaxExclusiveFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaMaxExclusiveFacet;
		}

		// Token: 0x040008D2 RID: 2258
		private const string xmlname = "maxExclusive";
	}
}
