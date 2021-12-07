using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attributeGroup element with the ref attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). AttributesGroupRef is the reference for an attributeGroup, name property contains the attribute group being referenced. </summary>
	// Token: 0x020001FF RID: 511
	public class XmlSchemaAttributeGroupRef : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> class.</summary>
		// Token: 0x06001467 RID: 5223 RVA: 0x00058C68 File Offset: 0x00056E68
		public XmlSchemaAttributeGroupRef()
		{
			this.refName = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of the referenced attributeGroup element.</summary>
		/// <returns>The name of the referenced attribute group. The value must be a QName.</returns>
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00058C7C File Offset: 0x00056E7C
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x00058C84 File Offset: 0x00056E84
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = value;
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00058C90 File Offset: 0x00056E90
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.errorCount = 0;
			if (this.RefName == null || this.RefName.IsEmpty)
			{
				base.error(h, "ref must be present");
			}
			else if (!XmlSchemaUtil.CheckQName(this.RefName))
			{
				base.error(h, "ref must be a valid qname");
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00058D30 File Offset: 0x00056F30
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			return this.errorCount;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00058D38 File Offset: 0x00056F38
		internal static XmlSchemaAttributeGroupRef Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = new XmlSchemaAttributeGroupRef();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "attributeGroup")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAttributeGroupRef.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAttributeGroupRef.LineNumber = reader.LineNumber;
			xmlSchemaAttributeGroupRef.LinePosition = reader.LinePosition;
			xmlSchemaAttributeGroupRef.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaAttributeGroupRef.Id = reader.Value;
				}
				else if (reader.Name == "ref")
				{
					Exception ex;
					xmlSchemaAttributeGroupRef.refName = XmlSchemaUtil.ReadQNameAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for ref attribute", ex);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for attributeGroup in this context", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaAttributeGroupRef);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaAttributeGroupRef;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "attributeGroup")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaAttributeGroupRef.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaAttributeGroupRef.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaAttributeGroupRef;
		}

		// Token: 0x040007D6 RID: 2006
		private const string xmlname = "attributeGroup";

		// Token: 0x040007D7 RID: 2007
		private XmlQualifiedName refName;
	}
}
