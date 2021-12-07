using System;

namespace System.Xml.Schema
{
	/// <summary>Specifies a restriction on the number of digits that can be entered for the fraction value of a simpleType element. The value of fractionDigits must be a positive integer. Represents the World Wide Web Consortium (W3C) fractionDigits facet.</summary>
	// Token: 0x02000217 RID: 535
	public class XmlSchemaFractionDigitsFacet : XmlSchemaNumericFacet
	{
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00060910 File Offset: 0x0005EB10
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.fractionDigits;
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00060918 File Offset: 0x0005EB18
		internal static XmlSchemaFractionDigitsFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaFractionDigitsFacet xmlSchemaFractionDigitsFacet = new XmlSchemaFractionDigitsFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "fractionDigits")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaFractionDigitsFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaFractionDigitsFacet.LineNumber = reader.LineNumber;
			xmlSchemaFractionDigitsFacet.LinePosition = reader.LinePosition;
			xmlSchemaFractionDigitsFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaFractionDigitsFacet.Id = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					Exception ex;
					xmlSchemaFractionDigitsFacet.IsFixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for fixed attribute", ex);
					}
				}
				else if (reader.Name == "value")
				{
					xmlSchemaFractionDigitsFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for fractionDigits", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaFractionDigitsFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaFractionDigitsFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "fractionDigits")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaFractionDigitsFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaFractionDigitsFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaFractionDigitsFacet;
		}

		// Token: 0x0400088B RID: 2187
		private const string xmlname = "fractionDigits";
	}
}
