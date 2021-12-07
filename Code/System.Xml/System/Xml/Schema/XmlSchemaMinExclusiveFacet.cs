using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the minExclusive element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum value of a simpleType element. The element value must be greater than the value of the minExclusive element.</summary>
	// Token: 0x02000229 RID: 553
	public class XmlSchemaMinExclusiveFacet : XmlSchemaFacet
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000653AC File Offset: 0x000635AC
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.minExclusive;
			}
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000653B4 File Offset: 0x000635B4
		internal static XmlSchemaMinExclusiveFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaMinExclusiveFacet xmlSchemaMinExclusiveFacet = new XmlSchemaMinExclusiveFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "minExclusive")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaMinExclusiveFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaMinExclusiveFacet.LineNumber = reader.LineNumber;
			xmlSchemaMinExclusiveFacet.LinePosition = reader.LinePosition;
			xmlSchemaMinExclusiveFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaMinExclusiveFacet.Id = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					Exception ex;
					xmlSchemaMinExclusiveFacet.IsFixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for fixed attribute", ex);
					}
				}
				else if (reader.Name == "value")
				{
					xmlSchemaMinExclusiveFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for minExclusive", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaMinExclusiveFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaMinExclusiveFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "minExclusive")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaMinExclusiveFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaMinExclusiveFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaMinExclusiveFacet;
		}

		// Token: 0x040008D5 RID: 2261
		private const string xmlname = "minExclusive";
	}
}
