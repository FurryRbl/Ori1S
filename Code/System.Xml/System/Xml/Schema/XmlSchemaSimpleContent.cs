using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the simpleContent element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for simple and complex types with simple content model.</summary>
	// Token: 0x0200023A RID: 570
	public class XmlSchemaSimpleContent : XmlSchemaContentModel
	{
		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentExtension" />.</summary>
		/// <returns>The content contained within the XmlSchemaSimpleContentRestriction or XmlSchemaSimpleContentExtension class.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0006EF4C File Offset: 0x0006D14C
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x0006EF54 File Offset: 0x0006D154
		[XmlElement("restriction", typeof(XmlSchemaSimpleContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaSimpleContentExtension))]
		public override XmlSchemaContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0006EF60 File Offset: 0x0006D160
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.Content != null)
			{
				this.Content.SetParent(this);
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0006EF8C File Offset: 0x0006D18C
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			if (this.Content == null)
			{
				base.error(h, "Content must be present in a simpleContent");
			}
			else if (this.Content is XmlSchemaSimpleContentRestriction)
			{
				XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)this.Content;
				this.errorCount += xmlSchemaSimpleContentRestriction.Compile(h, schema);
			}
			else if (this.Content is XmlSchemaSimpleContentExtension)
			{
				XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)this.Content;
				this.errorCount += xmlSchemaSimpleContentExtension.Compile(h, schema);
			}
			else
			{
				base.error(h, "simpleContent can't have any value other than restriction or extention");
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0006F06C File Offset: 0x0006D26C
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			this.errorCount += this.Content.Validate(h, schema);
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0006F0C0 File Offset: 0x0006D2C0
		internal static XmlSchemaSimpleContent Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "simpleContent")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaComplexContent.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaSimpleContent.LineNumber = reader.LineNumber;
			xmlSchemaSimpleContent.LinePosition = reader.LinePosition;
			xmlSchemaSimpleContent.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaSimpleContent.Id = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for simpleContent", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaSimpleContent);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaSimpleContent;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "simpleContent")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaSimpleContent.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaSimpleContent.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					if (num <= 2)
					{
						if (reader.LocalName == "restriction")
						{
							num = 3;
							XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = XmlSchemaSimpleContentRestriction.Read(reader, h);
							if (xmlSchemaSimpleContentRestriction != null)
							{
								xmlSchemaSimpleContent.content = xmlSchemaSimpleContentRestriction;
							}
							continue;
						}
						if (reader.LocalName == "extension")
						{
							num = 3;
							XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = XmlSchemaSimpleContentExtension.Read(reader, h);
							if (xmlSchemaSimpleContentExtension != null)
							{
								xmlSchemaSimpleContent.content = xmlSchemaSimpleContentExtension;
							}
							continue;
						}
					}
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaSimpleContent;
		}

		// Token: 0x0400090F RID: 2319
		private const string xmlname = "simpleContent";

		// Token: 0x04000910 RID: 2320
		private XmlSchemaContent content;
	}
}
