using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attributeGroup element from the XML Schema as specified by the World Wide Web Consortium (W3C). AttributesGroups provides a mechanism to group a set of attribute declarations so that they can be incorporated as a group into complex type definitions.</summary>
	// Token: 0x020001FE RID: 510
	public class XmlSchemaAttributeGroup : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroup" /> class.</summary>
		// Token: 0x06001459 RID: 5209 RVA: 0x00058630 File Offset: 0x00056830
		public XmlSchemaAttributeGroup()
		{
			this.attributes = new XmlSchemaObjectCollection();
			this.qualifiedName = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of the attribute group.</summary>
		/// <returns>The name of the attribute group.</returns>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00058650 File Offset: 0x00056850
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x00058658 File Offset: 0x00056858
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

		/// <summary>Gets the collection of attributes for the attribute group. Contains XmlSchemaAttribute and XmlSchemaAttributeGroupRef elements.</summary>
		/// <returns>The collection of attributes for the attribute group.</returns>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00058664 File Offset: 0x00056864
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0005866C File Offset: 0x0005686C
		internal XmlSchemaObjectTable AttributeUses
		{
			get
			{
				return this.attributeUses;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00058674 File Offset: 0x00056874
		internal XmlSchemaAnyAttribute AnyAttributeUse
		{
			get
			{
				return this.anyAttributeUse;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the attribute group.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" />.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0005867C File Offset: 0x0005687C
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x00058684 File Offset: 0x00056884
		[XmlElement("anyAttribute")]
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		/// <summary>Gets the redefined attribute group property from the XML Schema.</summary>
		/// <returns>The redefined attribute group property.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00058690 File Offset: 0x00056890
		[XmlIgnore]
		public XmlSchemaAttributeGroup RedefinedAttributeGroup
		{
			get
			{
				return this.redefined;
			}
		}

		/// <summary>Gets the qualified name of the attribute group.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> object representing the qualified name of the attribute group.</returns>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00058698 File Offset: 0x00056898
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x000586A0 File Offset: 0x000568A0
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.AnyAttribute != null)
			{
				this.AnyAttribute.SetParent(this);
			}
			foreach (XmlSchemaObject xmlSchemaObject in this.Attributes)
			{
				xmlSchemaObject.SetParent(this);
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00058728 File Offset: 0x00056928
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return this.errorCount;
			}
			this.errorCount = 0;
			if (this.redefinedObject != null)
			{
				this.errorCount += this.redefined.Compile(h, schema);
				if (this.errorCount == 0)
				{
					this.redefined = (XmlSchemaAttributeGroup)this.redefinedObject;
				}
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			if (this.Name == null || this.Name == string.Empty)
			{
				base.error(h, "Name is required in top level simpletype");
			}
			else if (!XmlSchemaUtil.CheckNCName(this.Name))
			{
				base.error(h, "name attribute of a simpleType must be NCName");
			}
			else
			{
				this.qualifiedName = new XmlQualifiedName(this.Name, base.AncestorSchema.TargetNamespace);
			}
			if (this.AnyAttribute != null)
			{
				this.errorCount += this.AnyAttribute.Compile(h, schema);
			}
			foreach (XmlSchemaObject xmlSchemaObject in this.Attributes)
			{
				if (xmlSchemaObject is XmlSchemaAttribute)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)xmlSchemaObject;
					this.errorCount += xmlSchemaAttribute.Compile(h, schema);
				}
				else if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)xmlSchemaObject;
					this.errorCount += xmlSchemaAttributeGroupRef.Compile(h, schema);
				}
				else
				{
					base.error(h, "invalid type of object in Attributes property");
				}
			}
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00058910 File Offset: 0x00056B10
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.CompilationId))
			{
				return this.errorCount;
			}
			if (this.redefined == null && this.redefinedObject != null)
			{
				this.redefinedObject.Compile(h, schema);
				this.redefined = (XmlSchemaAttributeGroup)this.redefinedObject;
				this.redefined.Validate(h, schema);
			}
			XmlSchemaObjectCollection xmlSchemaObjectCollection = this.Attributes;
			this.attributeUses = new XmlSchemaObjectTable();
			this.errorCount += XmlSchemaUtil.ValidateAttributesResolved(this.attributeUses, h, schema, xmlSchemaObjectCollection, this.AnyAttribute, ref this.anyAttributeUse, this.redefined, false);
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x000589CC File Offset: 0x00056BCC
		internal static XmlSchemaAttributeGroup Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = new XmlSchemaAttributeGroup();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "attributeGroup")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAttributeGroup.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAttributeGroup.LineNumber = reader.LineNumber;
			xmlSchemaAttributeGroup.LinePosition = reader.LinePosition;
			xmlSchemaAttributeGroup.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaAttributeGroup.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					xmlSchemaAttributeGroup.name = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for attributeGroup in this context", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaAttributeGroup);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaAttributeGroup;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "attributeGroup")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaAttributeGroup.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaAttributeGroup.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					if (num <= 2)
					{
						if (reader.LocalName == "attribute")
						{
							num = 2;
							XmlSchemaAttribute xmlSchemaAttribute = XmlSchemaAttribute.Read(reader, h);
							if (xmlSchemaAttribute != null)
							{
								xmlSchemaAttributeGroup.Attributes.Add(xmlSchemaAttribute);
							}
							continue;
						}
						if (reader.LocalName == "attributeGroup")
						{
							num = 2;
							XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = XmlSchemaAttributeGroupRef.Read(reader, h);
							if (xmlSchemaAttributeGroupRef != null)
							{
								xmlSchemaAttributeGroup.attributes.Add(xmlSchemaAttributeGroupRef);
							}
							continue;
						}
					}
					if (num <= 3 && reader.LocalName == "anyAttribute")
					{
						num = 4;
						XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Read(reader, h);
						if (xmlSchemaAnyAttribute != null)
						{
							xmlSchemaAttributeGroup.AnyAttribute = xmlSchemaAnyAttribute;
						}
					}
					else
					{
						reader.RaiseInvalidElementError();
					}
				}
			}
			return xmlSchemaAttributeGroup;
		}

		// Token: 0x040007CD RID: 1997
		private const string xmlname = "attributeGroup";

		// Token: 0x040007CE RID: 1998
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x040007CF RID: 1999
		private XmlSchemaObjectCollection attributes;

		// Token: 0x040007D0 RID: 2000
		private string name;

		// Token: 0x040007D1 RID: 2001
		private XmlSchemaAttributeGroup redefined;

		// Token: 0x040007D2 RID: 2002
		private XmlQualifiedName qualifiedName;

		// Token: 0x040007D3 RID: 2003
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x040007D4 RID: 2004
		private XmlSchemaAnyAttribute anyAttributeUse;

		// Token: 0x040007D5 RID: 2005
		internal bool AttributeGroupRecursionCheck;
	}
}
