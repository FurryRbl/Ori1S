using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the notation element from XML Schema as specified by the World Wide Web Consortium (W3C). An XML Schema notation declaration is a reconstruction of XML 1.0 NOTATION declarations. The purpose of notations is to describe the format of non-XML data within an XML document.</summary>
	// Token: 0x0200022C RID: 556
	public class XmlSchemaNotation : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the notation.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00065A68 File Offset: 0x00063C68
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x00065A70 File Offset: 0x00063C70
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the public identifier.</summary>
		/// <returns>The public identifier. The value must be a valid Uniform Resource Identifier (URI).</returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00065A7C File Offset: 0x00063C7C
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x00065A84 File Offset: 0x00063C84
		[XmlAttribute("public")]
		public string Public
		{
			get
			{
				return this.pub;
			}
			set
			{
				this.pub = value;
			}
		}

		/// <summary>Gets or sets the system identifier.</summary>
		/// <returns>The system identifier. The value must be a valid URI.</returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00065A90 File Offset: 0x00063C90
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x00065A98 File Offset: 0x00063C98
		[XmlAttribute("system")]
		public string System
		{
			get
			{
				return this.system;
			}
			set
			{
				this.system = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00065AA4 File Offset: 0x00063CA4
		[XmlIgnore]
		internal XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00065AAC File Offset: 0x00063CAC
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			if (this.Name == null)
			{
				base.error(h, "Required attribute name must be present");
			}
			else if (!XmlSchemaUtil.CheckNCName(this.name))
			{
				base.error(h, "attribute name must be NCName");
			}
			else
			{
				this.qualifiedName = new XmlQualifiedName(this.Name, base.AncestorSchema.TargetNamespace);
			}
			if (this.Public == null)
			{
				base.error(h, "public must be present");
			}
			else if (!XmlSchemaUtil.CheckAnyUri(this.Public))
			{
				base.error(h, "public must be anyURI");
			}
			if (this.system != null && !XmlSchemaUtil.CheckAnyUri(this.system))
			{
				base.error(h, "system must be present and of Type anyURI");
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			return this.errorCount;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00065BA4 File Offset: 0x00063DA4
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			return this.errorCount;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00065BAC File Offset: 0x00063DAC
		internal static XmlSchemaNotation Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaNotation xmlSchemaNotation = new XmlSchemaNotation();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "notation")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaInclude.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaNotation.LineNumber = reader.LineNumber;
			xmlSchemaNotation.LinePosition = reader.LinePosition;
			xmlSchemaNotation.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaNotation.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					xmlSchemaNotation.name = reader.Value;
				}
				else if (reader.Name == "public")
				{
					xmlSchemaNotation.pub = reader.Value;
				}
				else if (reader.Name == "system")
				{
					xmlSchemaNotation.system = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for notation", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaNotation);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaNotation;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "notation")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaNotation.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaNotation.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaNotation;
		}

		// Token: 0x040008D8 RID: 2264
		private const string xmlname = "notation";

		// Token: 0x040008D9 RID: 2265
		private string name;

		// Token: 0x040008DA RID: 2266
		private string pub;

		// Token: 0x040008DB RID: 2267
		private string system;

		// Token: 0x040008DC RID: 2268
		private XmlQualifiedName qualifiedName;
	}
}
