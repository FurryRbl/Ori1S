using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) annotation element.</summary>
	// Token: 0x020001F9 RID: 505
	public class XmlSchemaAnnotation : XmlSchemaObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" /> class.</summary>
		// Token: 0x06001402 RID: 5122 RVA: 0x000568D0 File Offset: 0x00054AD0
		public XmlSchemaAnnotation()
		{
			this.items = new XmlSchemaObjectCollection();
		}

		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x000568E4 File Offset: 0x00054AE4
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x000568EC File Offset: 0x00054AEC
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets the Items collection that is used to store the appinfo and documentation child elements.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of appinfo and documentation child elements.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x000568F8 File Offset: 0x00054AF8
		[XmlElement("appinfo", typeof(XmlSchemaAppInfo))]
		[XmlElement("documentation", typeof(XmlSchemaDocumentation))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the schema's target namespace.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00056900 File Offset: 0x00054B00
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x00056948 File Offset: 0x00054B48
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				if (this.unhandledAttributeList != null)
				{
					this.unhandledAttributes = (XmlAttribute[])this.unhandledAttributeList.ToArray(typeof(XmlAttribute));
					this.unhandledAttributeList = null;
				}
				return this.unhandledAttributes;
			}
			set
			{
				this.unhandledAttributes = value;
				this.unhandledAttributeList = null;
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00056958 File Offset: 0x00054B58
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.CompilationId = schema.CompilationId;
			return 0;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00056980 File Offset: 0x00054B80
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			return 0;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00056984 File Offset: 0x00054B84
		internal static XmlSchemaAnnotation Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaAnnotation xmlSchemaAnnotation = new XmlSchemaAnnotation();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "annotation")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAnnotation.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAnnotation.LineNumber = reader.LineNumber;
			xmlSchemaAnnotation.LinePosition = reader.LinePosition;
			xmlSchemaAnnotation.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaAnnotation.Id = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for annotation", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaAnnotation);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaAnnotation;
			}
			bool flag = false;
			string text = null;
			while (!reader.EOF)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					reader.ReadNextElement();
				}
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					bool flag2 = true;
					string text2 = "annotation";
					if (text != null)
					{
						text2 = text;
						text = null;
						flag2 = false;
					}
					if (reader.LocalName != text2)
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaAnnotation.Read, name=" + reader.Name + ",expected=" + text2, null);
					}
					if (flag2)
					{
						break;
					}
				}
				else if (reader.LocalName == "appinfo")
				{
					XmlSchemaAppInfo xmlSchemaAppInfo = XmlSchemaAppInfo.Read(reader, h, out flag);
					if (xmlSchemaAppInfo != null)
					{
						xmlSchemaAnnotation.items.Add(xmlSchemaAppInfo);
					}
				}
				else if (reader.LocalName == "documentation")
				{
					XmlSchemaDocumentation xmlSchemaDocumentation = XmlSchemaDocumentation.Read(reader, h, out flag);
					if (xmlSchemaDocumentation != null)
					{
						xmlSchemaAnnotation.items.Add(xmlSchemaDocumentation);
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaAnnotation;
		}

		// Token: 0x040007AC RID: 1964
		private const string xmlname = "annotation";

		// Token: 0x040007AD RID: 1965
		private string id;

		// Token: 0x040007AE RID: 1966
		private XmlSchemaObjectCollection items;

		// Token: 0x040007AF RID: 1967
		private XmlAttribute[] unhandledAttributes;
	}
}
