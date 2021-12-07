using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the extension element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for complex types with complex content model derived by extension. It extends the complex type by adding attributes or elements.</summary>
	// Token: 0x02000205 RID: 517
	public class XmlSchemaComplexContentExtension : XmlSchemaContent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaComplexContentExtension" /> class.</summary>
		// Token: 0x060014A3 RID: 5283 RVA: 0x0005A2F8 File Offset: 0x000584F8
		public XmlSchemaComplexContentExtension()
		{
			this.attributes = new XmlSchemaObjectCollection();
			this.baseTypeName = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of the complex type from which this type is derived by extension.</summary>
		/// <returns>The name of the complex type from which this type is derived by extension.</returns>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0005A318 File Offset: 0x00058518
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x0005A320 File Offset: 0x00058520
		[XmlAttribute("base")]
		public XmlQualifiedName BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
			set
			{
				this.baseTypeName = value;
			}
		}

		/// <summary>Gets or sets one of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0005A32C File Offset: 0x0005852C
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x0005A334 File Offset: 0x00058534
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		public XmlSchemaParticle Particle
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

		/// <summary>Gets the collection of attributes for the complex content. Contains <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> elements.</summary>
		/// <returns>The collection of attributes for the complex content.</returns>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0005A340 File Offset: 0x00058540
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</returns>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0005A348 File Offset: 0x00058548
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x0005A350 File Offset: 0x00058550
		[XmlElement("anyAttribute")]
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0005A35C File Offset: 0x0005855C
		internal override bool IsExtension
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0005A360 File Offset: 0x00058560
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.Particle != null)
			{
				this.Particle.SetParent(this);
			}
			if (this.AnyAttribute != null)
			{
				this.AnyAttribute.SetParent(this);
			}
			foreach (XmlSchemaObject xmlSchemaObject in this.Attributes)
			{
				xmlSchemaObject.SetParent(this);
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0005A400 File Offset: 0x00058600
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
				if (this.AnyAttribute != null)
				{
					this.AnyAttribute.isRedefinedComponent = true;
				}
				foreach (XmlSchemaObject xmlSchemaObject in this.Attributes)
				{
					xmlSchemaObject.isRedefinedComponent = true;
				}
				if (this.Particle != null)
				{
					this.Particle.isRedefinedComponent = true;
				}
			}
			if (this.BaseTypeName == null || this.BaseTypeName.IsEmpty)
			{
				base.error(h, "base must be present, as a QName");
			}
			else if (!XmlSchemaUtil.CheckQName(this.BaseTypeName))
			{
				base.error(h, "BaseTypeName is not a valid XmlQualifiedName");
			}
			if (this.AnyAttribute != null)
			{
				this.errorCount += this.AnyAttribute.Compile(h, schema);
			}
			foreach (XmlSchemaObject xmlSchemaObject2 in this.Attributes)
			{
				if (xmlSchemaObject2 is XmlSchemaAttribute)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)xmlSchemaObject2;
					this.errorCount += xmlSchemaAttribute.Compile(h, schema);
				}
				else if (xmlSchemaObject2 is XmlSchemaAttributeGroupRef)
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)xmlSchemaObject2;
					this.errorCount += xmlSchemaAttributeGroupRef.Compile(h, schema);
				}
				else
				{
					base.error(h, xmlSchemaObject2.GetType() + " is not valid in this place::ComplexConetnetExtension");
				}
			}
			if (this.Particle != null)
			{
				if (this.Particle is XmlSchemaGroupRef)
				{
					this.errorCount += ((XmlSchemaGroupRef)this.Particle).Compile(h, schema);
				}
				else if (this.Particle is XmlSchemaAll)
				{
					this.errorCount += ((XmlSchemaAll)this.Particle).Compile(h, schema);
				}
				else if (this.Particle is XmlSchemaChoice)
				{
					this.errorCount += ((XmlSchemaChoice)this.Particle).Compile(h, schema);
				}
				else if (this.Particle is XmlSchemaSequence)
				{
					this.errorCount += ((XmlSchemaSequence)this.Particle).Compile(h, schema);
				}
				else
				{
					base.error(h, "Particle of a restriction is limited only to group, sequence, choice and all.");
				}
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0005A71C File Offset: 0x0005891C
		internal override XmlQualifiedName GetBaseTypeName()
		{
			return this.baseTypeName;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005A724 File Offset: 0x00058924
		internal override XmlSchemaParticle GetParticle()
		{
			return this.particle;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005A72C File Offset: 0x0005892C
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			if (this.AnyAttribute != null)
			{
				this.errorCount += this.AnyAttribute.Validate(h, schema);
			}
			foreach (XmlSchemaObject xmlSchemaObject in this.Attributes)
			{
				this.errorCount += xmlSchemaObject.Validate(h, schema);
			}
			if (this.Particle != null)
			{
				this.errorCount += this.Particle.Validate(h, schema);
			}
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005A818 File Offset: 0x00058A18
		internal static XmlSchemaComplexContentExtension Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = new XmlSchemaComplexContentExtension();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "extension")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaComplexContentExtension.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaComplexContentExtension.LineNumber = reader.LineNumber;
			xmlSchemaComplexContentExtension.LinePosition = reader.LinePosition;
			xmlSchemaComplexContentExtension.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "base")
				{
					Exception ex;
					xmlSchemaComplexContentExtension.baseTypeName = XmlSchemaUtil.ReadQNameAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for base attribute", ex);
					}
				}
				else if (reader.Name == "id")
				{
					xmlSchemaComplexContentExtension.Id = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for extension", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaComplexContentExtension);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaComplexContentExtension;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "extension")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaComplexContentExtension.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaComplexContentExtension.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					if (num <= 2)
					{
						if (reader.LocalName == "group")
						{
							num = 3;
							XmlSchemaGroupRef xmlSchemaGroupRef = XmlSchemaGroupRef.Read(reader, h);
							if (xmlSchemaGroupRef != null)
							{
								xmlSchemaComplexContentExtension.particle = xmlSchemaGroupRef;
							}
							continue;
						}
						if (reader.LocalName == "all")
						{
							num = 3;
							XmlSchemaAll xmlSchemaAll = XmlSchemaAll.Read(reader, h);
							if (xmlSchemaAll != null)
							{
								xmlSchemaComplexContentExtension.particle = xmlSchemaAll;
							}
							continue;
						}
						if (reader.LocalName == "choice")
						{
							num = 3;
							XmlSchemaChoice xmlSchemaChoice = XmlSchemaChoice.Read(reader, h);
							if (xmlSchemaChoice != null)
							{
								xmlSchemaComplexContentExtension.particle = xmlSchemaChoice;
							}
							continue;
						}
						if (reader.LocalName == "sequence")
						{
							num = 3;
							XmlSchemaSequence xmlSchemaSequence = XmlSchemaSequence.Read(reader, h);
							if (xmlSchemaSequence != null)
							{
								xmlSchemaComplexContentExtension.particle = xmlSchemaSequence;
							}
							continue;
						}
					}
					if (num <= 3)
					{
						if (reader.LocalName == "attribute")
						{
							num = 3;
							XmlSchemaAttribute xmlSchemaAttribute = XmlSchemaAttribute.Read(reader, h);
							if (xmlSchemaAttribute != null)
							{
								xmlSchemaComplexContentExtension.Attributes.Add(xmlSchemaAttribute);
							}
							continue;
						}
						if (reader.LocalName == "attributeGroup")
						{
							num = 3;
							XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = XmlSchemaAttributeGroupRef.Read(reader, h);
							if (xmlSchemaAttributeGroupRef != null)
							{
								xmlSchemaComplexContentExtension.attributes.Add(xmlSchemaAttributeGroupRef);
							}
							continue;
						}
					}
					if (num <= 4 && reader.LocalName == "anyAttribute")
					{
						num = 5;
						XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Read(reader, h);
						if (xmlSchemaAnyAttribute != null)
						{
							xmlSchemaComplexContentExtension.AnyAttribute = xmlSchemaAnyAttribute;
						}
					}
					else
					{
						reader.RaiseInvalidElementError();
					}
				}
			}
			return xmlSchemaComplexContentExtension;
		}

		// Token: 0x040007E2 RID: 2018
		private const string xmlname = "extension";

		// Token: 0x040007E3 RID: 2019
		private XmlSchemaAnyAttribute any;

		// Token: 0x040007E4 RID: 2020
		private XmlSchemaObjectCollection attributes;

		// Token: 0x040007E5 RID: 2021
		private XmlQualifiedName baseTypeName;

		// Token: 0x040007E6 RID: 2022
		private XmlSchemaParticle particle;
	}
}
