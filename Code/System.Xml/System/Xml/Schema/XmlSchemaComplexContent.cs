using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the complexContent element from XML Schema as specified by the World Wide Web Consortium (W3C). This class represents the complex content model for complex types. It contains extensions or restrictions on a complex type that has either only elements or mixed content.</summary>
	// Token: 0x02000204 RID: 516
	public class XmlSchemaComplexContent : XmlSchemaContentModel
	{
		/// <summary>Gets or sets information that determines if the type has a mixed content model.</summary>
		/// <returns>If this property is true, character data is allowed to appear between the child elements of the complex type (mixed content model). The default is false.Optional.</returns>
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00059EC0 File Offset: 0x000580C0
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x00059EC8 File Offset: 0x000580C8
		[XmlAttribute("mixed")]
		public bool IsMixed
		{
			get
			{
				return this.isMixed;
			}
			set
			{
				this.isMixed = value;
			}
		}

		/// <summary>Gets or sets the content.</summary>
		/// <returns>One of either the <see cref="T:System.Xml.Schema.XmlSchemaComplexContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaComplexContentExtension" /> classes.</returns>
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00059ED4 File Offset: 0x000580D4
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x00059EDC File Offset: 0x000580DC
		[XmlElement("restriction", typeof(XmlSchemaComplexContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaComplexContentExtension))]
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

		// Token: 0x0600149F RID: 5279 RVA: 0x00059EE8 File Offset: 0x000580E8
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.Content != null)
			{
				this.Content.SetParent(this);
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00059F14 File Offset: 0x00058114
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			if (this.isRedefinedComponent)
			{
				if (base.Annotation != null)
				{
					base.Annotation.isRedefinedComponent = true;
				}
				if (this.Content != null)
				{
					this.Content.isRedefinedComponent = true;
				}
			}
			if (this.Content == null)
			{
				base.error(h, "Content must be present in a complexContent");
			}
			else if (this.Content is XmlSchemaComplexContentRestriction)
			{
				XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = (XmlSchemaComplexContentRestriction)this.Content;
				this.errorCount += xmlSchemaComplexContentRestriction.Compile(h, schema);
			}
			else if (this.Content is XmlSchemaComplexContentExtension)
			{
				XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)this.Content;
				this.errorCount += xmlSchemaComplexContentExtension.Compile(h, schema);
			}
			else
			{
				base.error(h, "complexContent can't have any value other than restriction or extention");
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0005A02C File Offset: 0x0005822C
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

		// Token: 0x060014A2 RID: 5282 RVA: 0x0005A080 File Offset: 0x00058280
		internal static XmlSchemaComplexContent Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "complexContent")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaComplexContent.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaComplexContent.LineNumber = reader.LineNumber;
			xmlSchemaComplexContent.LinePosition = reader.LinePosition;
			xmlSchemaComplexContent.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaComplexContent.Id = reader.Value;
				}
				else if (reader.Name == "mixed")
				{
					Exception ex;
					xmlSchemaComplexContent.isMixed = XmlSchemaUtil.ReadBoolAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is an invalid value for mixed", ex);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for complexContent", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaComplexContent);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaComplexContent;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "complexContent")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaComplexContent.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaComplexContent.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					if (num <= 2)
					{
						if (reader.LocalName == "restriction")
						{
							num = 3;
							XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = XmlSchemaComplexContentRestriction.Read(reader, h);
							if (xmlSchemaComplexContentRestriction != null)
							{
								xmlSchemaComplexContent.content = xmlSchemaComplexContentRestriction;
							}
							continue;
						}
						if (reader.LocalName == "extension")
						{
							num = 3;
							XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = XmlSchemaComplexContentExtension.Read(reader, h);
							if (xmlSchemaComplexContentExtension != null)
							{
								xmlSchemaComplexContent.content = xmlSchemaComplexContentExtension;
							}
							continue;
						}
					}
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaComplexContent;
		}

		// Token: 0x040007DF RID: 2015
		private const string xmlname = "complexContent";

		// Token: 0x040007E0 RID: 2016
		private XmlSchemaContent content;

		// Token: 0x040007E1 RID: 2017
		private bool isMixed;
	}
}
