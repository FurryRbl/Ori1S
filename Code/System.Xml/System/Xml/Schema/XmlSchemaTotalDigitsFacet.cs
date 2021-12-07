using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the totalDigits facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the number of digits that can be entered for the value of a simpleType element. That value of totalDigits must be a positive integer.</summary>
	// Token: 0x02000242 RID: 578
	public class XmlSchemaTotalDigitsFacet : XmlSchemaNumericFacet
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00073DB8 File Offset: 0x00071FB8
		internal override XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.totalDigits;
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00073DC0 File Offset: 0x00071FC0
		internal static XmlSchemaTotalDigitsFacet Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaTotalDigitsFacet xmlSchemaTotalDigitsFacet = new XmlSchemaTotalDigitsFacet();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "totalDigits")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaTotalDigitsFacet.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaTotalDigitsFacet.LineNumber = reader.LineNumber;
			xmlSchemaTotalDigitsFacet.LinePosition = reader.LinePosition;
			xmlSchemaTotalDigitsFacet.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaTotalDigitsFacet.Id = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					Exception ex;
					xmlSchemaTotalDigitsFacet.IsFixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for fixed attribute", ex);
					}
				}
				else if (reader.Name == "value")
				{
					xmlSchemaTotalDigitsFacet.Value = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for totalDigits", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaTotalDigitsFacet);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaTotalDigitsFacet;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "totalDigits")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaTotalDigitsFacet.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaTotalDigitsFacet.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaTotalDigitsFacet;
		}

		// Token: 0x04000970 RID: 2416
		private const string xmlname = "totalDigits";
	}
}
