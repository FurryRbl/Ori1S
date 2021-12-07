using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the group element from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines groups at the schema level that are referenced from the complex types. It groups a set of element declarations so that they can be incorporated as a group into complex type definitions.</summary>
	// Token: 0x02000218 RID: 536
	public class XmlSchemaGroup : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaGroup" /> class.</summary>
		// Token: 0x06001563 RID: 5475 RVA: 0x00060B48 File Offset: 0x0005ED48
		public XmlSchemaGroup()
		{
			this.qualifiedName = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of the schema group.</summary>
		/// <returns>The name of the schema group.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00060B5C File Offset: 0x0005ED5C
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00060B64 File Offset: 0x0005ED64
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

		/// <summary>Gets or sets one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00060B70 File Offset: 0x0005ED70
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x00060B78 File Offset: 0x0005ED78
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		/// <summary>Gets the qualified name of the schema group.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> object representing the qualified name of the schema group.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x00060B84 File Offset: 0x0005ED84
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00060B8C File Offset: 0x0005ED8C
		internal bool IsCircularDefinition
		{
			get
			{
				return this.isCircularDefinition;
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00060B94 File Offset: 0x0005ED94
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.Particle != null)
			{
				this.Particle.SetParent(this);
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00060BC0 File Offset: 0x0005EDC0
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
			if (this.Particle == null)
			{
				base.error(h, "Particle is required");
			}
			else
			{
				if (this.Particle.MaxOccursString != null)
				{
					this.Particle.error(h, "MaxOccurs must not be present when the Particle is a child of Group");
				}
				if (this.Particle.MinOccursString != null)
				{
					this.Particle.error(h, "MinOccurs must not be present when the Particle is a child of Group");
				}
				this.Particle.Compile(h, schema);
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00060CD0 File Offset: 0x0005EED0
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			if (this.Particle != null)
			{
				this.Particle.parentIsGroupDefinition = true;
				try
				{
					this.Particle.CheckRecursion(0, h, schema);
				}
				catch (XmlSchemaException ex)
				{
					XmlSchemaObject.error(h, ex.Message, ex);
					this.isCircularDefinition = true;
					return this.errorCount;
				}
				this.errorCount += this.Particle.Validate(h, schema);
				this.Particle.ValidateUniqueParticleAttribution(new XmlSchemaObjectTable(), new ArrayList(), h, schema);
				this.Particle.ValidateUniqueTypeAttribution(new XmlSchemaObjectTable(), h, schema);
			}
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00060DB8 File Offset: 0x0005EFB8
		internal static XmlSchemaGroup Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaGroup xmlSchemaGroup = new XmlSchemaGroup();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "group")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaGroup.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaGroup.LineNumber = reader.LineNumber;
			xmlSchemaGroup.LinePosition = reader.LinePosition;
			xmlSchemaGroup.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaGroup.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					xmlSchemaGroup.name = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for group", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaGroup);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaGroup;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "group")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaGroup.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaGroup.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					if (num <= 2)
					{
						if (reader.LocalName == "all")
						{
							num = 3;
							XmlSchemaAll xmlSchemaAll = XmlSchemaAll.Read(reader, h);
							if (xmlSchemaAll != null)
							{
								xmlSchemaGroup.Particle = xmlSchemaAll;
							}
							continue;
						}
						if (reader.LocalName == "choice")
						{
							num = 3;
							XmlSchemaChoice xmlSchemaChoice = XmlSchemaChoice.Read(reader, h);
							if (xmlSchemaChoice != null)
							{
								xmlSchemaGroup.Particle = xmlSchemaChoice;
							}
							continue;
						}
						if (reader.LocalName == "sequence")
						{
							num = 3;
							XmlSchemaSequence xmlSchemaSequence = XmlSchemaSequence.Read(reader, h);
							if (xmlSchemaSequence != null)
							{
								xmlSchemaGroup.Particle = xmlSchemaSequence;
							}
							continue;
						}
					}
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaGroup;
		}

		// Token: 0x0400088C RID: 2188
		private const string xmlname = "group";

		// Token: 0x0400088D RID: 2189
		private string name;

		// Token: 0x0400088E RID: 2190
		private XmlSchemaGroupBase particle;

		// Token: 0x0400088F RID: 2191
		private XmlQualifiedName qualifiedName;

		// Token: 0x04000890 RID: 2192
		private bool isCircularDefinition;
	}
}
