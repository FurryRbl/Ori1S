using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the restriction element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to derive simple types by restriction. Such derivations can be used to restrict the range of values for the element to a subset of the values specified in the inherited simple type.</summary>
	// Token: 0x0200023C RID: 572
	public class XmlSchemaSimpleContentRestriction : XmlSchemaContent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentRestriction" /> class.</summary>
		// Token: 0x060016F0 RID: 5872 RVA: 0x0006F9D4 File Offset: 0x0006DBD4
		public XmlSchemaSimpleContentRestriction()
		{
			this.baseTypeName = XmlQualifiedName.Empty;
			this.attributes = new XmlSchemaObjectCollection();
			this.facets = new XmlSchemaObjectCollection();
		}

		/// <summary>Gets or sets the name of the built-in data type or simple type from which this type is derived.</summary>
		/// <returns>The name of the base type.</returns>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0006FA00 File Offset: 0x0006DC00
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0006FA08 File Offset: 0x0006DC08
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

		/// <summary>Gets or sets the simple type base value.</summary>
		/// <returns>The simple type base value.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0006FA14 File Offset: 0x0006DC14
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x0006FA1C File Offset: 0x0006DC1C
		[XmlElement("simpleType", Type = typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType BaseType
		{
			get
			{
				return this.baseType;
			}
			set
			{
				this.baseType = value;
			}
		}

		/// <summary>Gets or sets an Xml Schema facet. </summary>
		/// <returns>One of the following facet classes:<see cref="T:System.Xml.Schema.XmlSchemaLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaPatternFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaEnumerationFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaFractionDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaTotalDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaWhiteSpaceFacet" />.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0006FA28 File Offset: 0x0006DC28
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> collection of attributes for the simple type.</summary>
		/// <returns>The collection of attributes for a simple type.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0006FA30 File Offset: 0x0006DC30
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> to be used for the attribute value.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> for the attribute value.Optional.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0006FA38 File Offset: 0x0006DC38
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x0006FA40 File Offset: 0x0006DC40
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

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0006FA4C File Offset: 0x0006DC4C
		internal override bool IsExtension
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0006FA50 File Offset: 0x0006DC50
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.BaseType != null)
			{
				this.BaseType.SetParent(this);
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

		// Token: 0x060016FB RID: 5883 RVA: 0x0006FAF0 File Offset: 0x0006DCF0
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
			}
			if (this.BaseTypeName == null || this.BaseTypeName.IsEmpty)
			{
				base.error(h, "base must be present, as a QName");
			}
			else if (!XmlSchemaUtil.CheckQName(this.BaseTypeName))
			{
				base.error(h, "BaseTypeName must be a QName");
			}
			if (this.BaseType != null)
			{
				this.errorCount += this.BaseType.Compile(h, schema);
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
					base.error(h, xmlSchemaObject2.GetType() + " is not valid in this place::SimpleContentRestriction");
				}
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0006FD34 File Offset: 0x0006DF34
		internal override XmlQualifiedName GetBaseTypeName()
		{
			return this.baseTypeName;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0006FD3C File Offset: 0x0006DF3C
		internal override XmlSchemaParticle GetParticle()
		{
			return null;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0006FD40 File Offset: 0x0006DF40
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			if (this.baseType != null)
			{
				this.baseType.Validate(h, schema);
				this.actualBaseSchemaType = this.baseType;
			}
			else if (this.baseTypeName != XmlQualifiedName.Empty)
			{
				XmlSchemaType xmlSchemaType = schema.FindSchemaType(this.baseTypeName);
				if (xmlSchemaType != null)
				{
					xmlSchemaType.Validate(h, schema);
					this.actualBaseSchemaType = xmlSchemaType;
				}
				else if (this.baseTypeName == XmlSchemaComplexType.AnyTypeName)
				{
					this.actualBaseSchemaType = XmlSchemaComplexType.AnyType;
				}
				else if (XmlSchemaUtil.IsBuiltInDatatypeName(this.baseTypeName))
				{
					this.actualBaseSchemaType = XmlSchemaDatatype.FromName(this.baseTypeName);
					if (this.actualBaseSchemaType == null)
					{
						base.error(h, "Invalid schema datatype name is specified.");
					}
				}
				else if (!schema.IsNamespaceAbsent(this.baseTypeName.Namespace))
				{
					base.error(h, "Referenced base schema type " + this.baseTypeName + " was not found in the corresponding schema.");
				}
			}
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0006FE74 File Offset: 0x0006E074
		internal static XmlSchemaSimpleContentRestriction Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = new XmlSchemaSimpleContentRestriction();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "restriction")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaComplexContentRestriction.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaSimpleContentRestriction.LineNumber = reader.LineNumber;
			xmlSchemaSimpleContentRestriction.LinePosition = reader.LinePosition;
			xmlSchemaSimpleContentRestriction.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "base")
				{
					Exception ex;
					xmlSchemaSimpleContentRestriction.baseTypeName = XmlSchemaUtil.ReadQNameAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for base attribute", ex);
					}
				}
				else if (reader.Name == "id")
				{
					xmlSchemaSimpleContentRestriction.Id = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for restriction", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaSimpleContentRestriction);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaSimpleContentRestriction;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "restriction")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaSimpleContentRestriction.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaSimpleContentRestriction.Annotation = xmlSchemaAnnotation;
					}
				}
				else if (num <= 2 && reader.LocalName == "simpleType")
				{
					num = 3;
					XmlSchemaSimpleType xmlSchemaSimpleType = XmlSchemaSimpleType.Read(reader, h);
					if (xmlSchemaSimpleType != null)
					{
						xmlSchemaSimpleContentRestriction.baseType = xmlSchemaSimpleType;
					}
				}
				else
				{
					if (num <= 3)
					{
						if (reader.LocalName == "minExclusive")
						{
							num = 3;
							XmlSchemaMinExclusiveFacet xmlSchemaMinExclusiveFacet = XmlSchemaMinExclusiveFacet.Read(reader, h);
							if (xmlSchemaMinExclusiveFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaMinExclusiveFacet);
							}
							continue;
						}
						if (reader.LocalName == "minInclusive")
						{
							num = 3;
							XmlSchemaMinInclusiveFacet xmlSchemaMinInclusiveFacet = XmlSchemaMinInclusiveFacet.Read(reader, h);
							if (xmlSchemaMinInclusiveFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaMinInclusiveFacet);
							}
							continue;
						}
						if (reader.LocalName == "maxExclusive")
						{
							num = 3;
							XmlSchemaMaxExclusiveFacet xmlSchemaMaxExclusiveFacet = XmlSchemaMaxExclusiveFacet.Read(reader, h);
							if (xmlSchemaMaxExclusiveFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaMaxExclusiveFacet);
							}
							continue;
						}
						if (reader.LocalName == "maxInclusive")
						{
							num = 3;
							XmlSchemaMaxInclusiveFacet xmlSchemaMaxInclusiveFacet = XmlSchemaMaxInclusiveFacet.Read(reader, h);
							if (xmlSchemaMaxInclusiveFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaMaxInclusiveFacet);
							}
							continue;
						}
						if (reader.LocalName == "totalDigits")
						{
							num = 3;
							XmlSchemaTotalDigitsFacet xmlSchemaTotalDigitsFacet = XmlSchemaTotalDigitsFacet.Read(reader, h);
							if (xmlSchemaTotalDigitsFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaTotalDigitsFacet);
							}
							continue;
						}
						if (reader.LocalName == "fractionDigits")
						{
							num = 3;
							XmlSchemaFractionDigitsFacet xmlSchemaFractionDigitsFacet = XmlSchemaFractionDigitsFacet.Read(reader, h);
							if (xmlSchemaFractionDigitsFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaFractionDigitsFacet);
							}
							continue;
						}
						if (reader.LocalName == "length")
						{
							num = 3;
							XmlSchemaLengthFacet xmlSchemaLengthFacet = XmlSchemaLengthFacet.Read(reader, h);
							if (xmlSchemaLengthFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaLengthFacet);
							}
							continue;
						}
						if (reader.LocalName == "minLength")
						{
							num = 3;
							XmlSchemaMinLengthFacet xmlSchemaMinLengthFacet = XmlSchemaMinLengthFacet.Read(reader, h);
							if (xmlSchemaMinLengthFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaMinLengthFacet);
							}
							continue;
						}
						if (reader.LocalName == "maxLength")
						{
							num = 3;
							XmlSchemaMaxLengthFacet xmlSchemaMaxLengthFacet = XmlSchemaMaxLengthFacet.Read(reader, h);
							if (xmlSchemaMaxLengthFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaMaxLengthFacet);
							}
							continue;
						}
						if (reader.LocalName == "enumeration")
						{
							num = 3;
							XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = XmlSchemaEnumerationFacet.Read(reader, h);
							if (xmlSchemaEnumerationFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaEnumerationFacet);
							}
							continue;
						}
						if (reader.LocalName == "whiteSpace")
						{
							num = 3;
							XmlSchemaWhiteSpaceFacet xmlSchemaWhiteSpaceFacet = XmlSchemaWhiteSpaceFacet.Read(reader, h);
							if (xmlSchemaWhiteSpaceFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaWhiteSpaceFacet);
							}
							continue;
						}
						if (reader.LocalName == "pattern")
						{
							num = 3;
							XmlSchemaPatternFacet xmlSchemaPatternFacet = XmlSchemaPatternFacet.Read(reader, h);
							if (xmlSchemaPatternFacet != null)
							{
								xmlSchemaSimpleContentRestriction.facets.Add(xmlSchemaPatternFacet);
							}
							continue;
						}
					}
					if (num <= 4)
					{
						if (reader.LocalName == "attribute")
						{
							num = 4;
							XmlSchemaAttribute xmlSchemaAttribute = XmlSchemaAttribute.Read(reader, h);
							if (xmlSchemaAttribute != null)
							{
								xmlSchemaSimpleContentRestriction.Attributes.Add(xmlSchemaAttribute);
							}
							continue;
						}
						if (reader.LocalName == "attributeGroup")
						{
							num = 4;
							XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = XmlSchemaAttributeGroupRef.Read(reader, h);
							if (xmlSchemaAttributeGroupRef != null)
							{
								xmlSchemaSimpleContentRestriction.attributes.Add(xmlSchemaAttributeGroupRef);
							}
							continue;
						}
					}
					if (num <= 5 && reader.LocalName == "anyAttribute")
					{
						num = 6;
						XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Read(reader, h);
						if (xmlSchemaAnyAttribute != null)
						{
							xmlSchemaSimpleContentRestriction.AnyAttribute = xmlSchemaAnyAttribute;
						}
					}
					else
					{
						reader.RaiseInvalidElementError();
					}
				}
			}
			return xmlSchemaSimpleContentRestriction;
		}

		// Token: 0x04000915 RID: 2325
		private const string xmlname = "restriction";

		// Token: 0x04000916 RID: 2326
		private XmlSchemaAnyAttribute any;

		// Token: 0x04000917 RID: 2327
		private XmlSchemaObjectCollection attributes;

		// Token: 0x04000918 RID: 2328
		private XmlSchemaSimpleType baseType;

		// Token: 0x04000919 RID: 2329
		private XmlQualifiedName baseTypeName;

		// Token: 0x0400091A RID: 2330
		private XmlSchemaObjectCollection facets;
	}
}
