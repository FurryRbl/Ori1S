using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attribute element from the XML Schema as specified by the World Wide Web Consortium (W3C). Attributes provide additional information for other document elements. The attribute tag is nested between the tags of a document's element for the schema. The XML document displays attributes as named items in the opening tag of an element.</summary>
	// Token: 0x020001FD RID: 509
	public class XmlSchemaAttribute : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> class.</summary>
		// Token: 0x0600143A RID: 5178 RVA: 0x00057908 File Offset: 0x00055B08
		public XmlSchemaAttribute()
		{
			this.form = XmlSchemaForm.None;
			this.use = XmlSchemaUse.None;
			this.schemaTypeName = XmlQualifiedName.Empty;
			this.qualifiedName = XmlQualifiedName.Empty;
			this.refName = XmlQualifiedName.Empty;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00057940 File Offset: 0x00055B40
		internal bool ParentIsSchema
		{
			get
			{
				return base.Parent is XmlSchema;
			}
		}

		/// <summary>Gets or sets the default value for the attribute.</summary>
		/// <returns>The default value for the attribute. The default is a null reference.Optional.</returns>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00057950 File Offset: 0x00055B50
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x00057958 File Offset: 0x00055B58
		[DefaultValue(null)]
		[XmlAttribute("default")]
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.fixedValue = null;
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets the fixed value for the attribute.</summary>
		/// <returns>The fixed value for the attribute. The default is null.Optional.</returns>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00057968 File Offset: 0x00055B68
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00057970 File Offset: 0x00055B70
		[XmlAttribute("fixed")]
		[DefaultValue(null)]
		public string FixedValue
		{
			get
			{
				return this.fixedValue;
			}
			set
			{
				this.defaultValue = null;
				this.fixedValue = value;
			}
		}

		/// <summary>Gets or sets the form for the attribute.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is the value of the <see cref="P:System.Xml.Schema.XmlSchema.AttributeFormDefault" /> of the schema element containing the attribute.Optional.</returns>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00057980 File Offset: 0x00055B80
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00057988 File Offset: 0x00055B88
		[DefaultValue(XmlSchemaForm.None)]
		[XmlAttribute("form")]
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00057994 File Offset: 0x00055B94
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x0005799C File Offset: 0x00055B9C
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

		/// <summary>Gets or sets the name of an attribute declared in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of the attribute declared.</returns>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000579A8 File Offset: 0x00055BA8
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x000579B0 File Offset: 0x00055BB0
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

		/// <summary>Gets or sets the name of the simple type defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of the simple type.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000579BC File Offset: 0x00055BBC
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x000579C4 File Offset: 0x00055BC4
		[XmlAttribute("type")]
		public XmlQualifiedName SchemaTypeName
		{
			get
			{
				return this.schemaTypeName;
			}
			set
			{
				this.schemaTypeName = value;
			}
		}

		/// <summary>Gets or sets the attribute type to a simple type.</summary>
		/// <returns>The simple type defined in this schema.</returns>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000579D0 File Offset: 0x00055BD0
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x000579D8 File Offset: 0x00055BD8
		[XmlElement("simpleType")]
		public XmlSchemaSimpleType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		/// <summary>Gets or sets information about how the attribute is used.</summary>
		/// <returns>One of the following values: None, Prohibited, Optional, or Required. The default is Optional.Optional.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x000579E4 File Offset: 0x00055BE4
		// (set) Token: 0x0600144B RID: 5195 RVA: 0x000579EC File Offset: 0x00055BEC
		[XmlAttribute("use")]
		[DefaultValue(XmlSchemaUse.None)]
		public XmlSchemaUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		/// <summary>Gets the qualified name for the attribute.</summary>
		/// <returns>The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x000579F8 File Offset: 0x00055BF8
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		/// <summary>Gets the common language runtime (CLR) object based on the <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaTypeName" /> of the attribute that holds the post-compilation value of the AttributeType property.</summary>
		/// <returns>The common runtime library (CLR) object that holds the post-compilation value of the AttributeType property.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00057A00 File Offset: 0x00055C00
		[XmlIgnore]
		[Obsolete]
		public object AttributeType
		{
			get
			{
				if (this.referencedAttribute != null)
				{
					return this.referencedAttribute.AttributeType;
				}
				return this.attributeType;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object representing the type of the attribute based on the <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaTypeName" /> of the attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object.</returns>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00057A20 File Offset: 0x00055C20
		[XmlIgnore]
		public XmlSchemaSimpleType AttributeSchemaType
		{
			get
			{
				if (this.referencedAttribute != null)
				{
					return this.referencedAttribute.AttributeSchemaType;
				}
				return this.attributeSchemaType;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00057A40 File Offset: 0x00055C40
		internal string ValidatedDefaultValue
		{
			get
			{
				return this.validatedDefaultValue;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00057A48 File Offset: 0x00055C48
		internal string ValidatedFixedValue
		{
			get
			{
				return this.validatedFixedValue;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00057A50 File Offset: 0x00055C50
		internal object ValidatedFixedTypedValue
		{
			get
			{
				return this.validatedFixedTypedValue;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00057A58 File Offset: 0x00055C58
		internal XmlSchemaUse ValidatedUse
		{
			get
			{
				return this.validatedUse;
			}
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00057A60 File Offset: 0x00055C60
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.schemaType != null)
			{
				this.schemaType.SetParent(this);
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00057A80 File Offset: 0x00055C80
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.errorCount = 0;
			if (this.ParentIsSchema || this.isRedefineChild)
			{
				if (this.RefName != null && !this.RefName.IsEmpty)
				{
					base.error(h, "ref must be absent in the top level <attribute>");
				}
				if (this.Form != XmlSchemaForm.None)
				{
					base.error(h, "form must be absent in the top level <attribute>");
				}
				if (this.Use != XmlSchemaUse.None)
				{
					base.error(h, "use must be absent in the top level <attribute>");
				}
				this.targetNamespace = base.AncestorSchema.TargetNamespace;
				this.CompileCommon(h, schema, true);
			}
			else if (this.RefName == null || this.RefName.IsEmpty)
			{
				if (this.form == XmlSchemaForm.Qualified || (this.form == XmlSchemaForm.None && schema.AttributeFormDefault == XmlSchemaForm.Qualified))
				{
					this.targetNamespace = base.AncestorSchema.TargetNamespace;
				}
				else
				{
					this.targetNamespace = string.Empty;
				}
				this.CompileCommon(h, schema, true);
			}
			else
			{
				if (this.name != null)
				{
					base.error(h, "name must be absent if ref is present");
				}
				if (this.form != XmlSchemaForm.None)
				{
					base.error(h, "form must be absent if ref is present");
				}
				if (this.schemaType != null)
				{
					base.error(h, "simpletype must be absent if ref is present");
				}
				if (this.schemaTypeName != null && !this.schemaTypeName.IsEmpty)
				{
					base.error(h, "type must be absent if ref is present");
				}
				this.CompileCommon(h, schema, false);
			}
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00057C3C File Offset: 0x00055E3C
		private void CompileCommon(ValidationEventHandler h, XmlSchema schema, bool refIsNotPresent)
		{
			if (refIsNotPresent)
			{
				if (this.Name == null)
				{
					base.error(h, "Required attribute name must be present");
				}
				else if (!XmlSchemaUtil.CheckNCName(this.Name))
				{
					base.error(h, "attribute name must be NCName");
				}
				else if (this.Name == "xmlns")
				{
					base.error(h, "attribute name must not be xmlns");
				}
				else
				{
					this.qualifiedName = new XmlQualifiedName(this.Name, this.targetNamespace);
				}
				if (this.SchemaType != null)
				{
					if (this.SchemaTypeName != null && !this.SchemaTypeName.IsEmpty)
					{
						base.error(h, "attribute can't have both a type and <simpleType> content");
					}
					this.errorCount += this.SchemaType.Compile(h, schema);
				}
				if (this.SchemaTypeName != null && !XmlSchemaUtil.CheckQName(this.SchemaTypeName))
				{
					base.error(h, this.SchemaTypeName + " is not a valid QName");
				}
			}
			else
			{
				if (this.RefName == null || this.RefName.IsEmpty)
				{
					throw new InvalidOperationException("Error: Should Never Happen. refname must be present");
				}
				this.qualifiedName = this.RefName;
			}
			if (base.AncestorSchema.TargetNamespace == "http://www.w3.org/2001/XMLSchema-instance" && this.Name != "nil" && this.Name != "type" && this.Name != "schemaLocation" && this.Name != "noNamespaceSchemaLocation")
			{
				base.error(h, "targetNamespace can't be http://www.w3.org/2001/XMLSchema-instance");
			}
			if (this.DefaultValue != null && this.FixedValue != null)
			{
				base.error(h, "default and fixed must not both be present in an Attribute");
			}
			if (this.DefaultValue != null && this.Use != XmlSchemaUse.None && this.Use != XmlSchemaUse.Optional)
			{
				base.error(h, "if default is present, use must be optional");
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00057E70 File Offset: 0x00056070
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			if (this.SchemaType != null)
			{
				this.SchemaType.Validate(h, schema);
				this.attributeType = this.SchemaType;
			}
			else if (this.SchemaTypeName != null && this.SchemaTypeName != XmlQualifiedName.Empty)
			{
				XmlSchemaType xmlSchemaType = schema.FindSchemaType(this.SchemaTypeName);
				if (xmlSchemaType is XmlSchemaComplexType)
				{
					base.error(h, "An attribute can't have complexType Content");
				}
				else if (xmlSchemaType != null)
				{
					this.errorCount += xmlSchemaType.Validate(h, schema);
					this.attributeType = xmlSchemaType;
				}
				else if (this.SchemaTypeName == XmlSchemaComplexType.AnyTypeName)
				{
					this.attributeType = XmlSchemaComplexType.AnyType;
				}
				else if (XmlSchemaUtil.IsBuiltInDatatypeName(this.SchemaTypeName))
				{
					this.attributeType = XmlSchemaDatatype.FromName(this.SchemaTypeName);
					if (this.attributeType == null)
					{
						base.error(h, "Invalid xml schema namespace datatype was specified.");
					}
				}
				else if (!schema.IsNamespaceAbsent(this.SchemaTypeName.Namespace))
				{
					base.error(h, "Referenced schema type " + this.SchemaTypeName + " was not found in the corresponding schema.");
				}
			}
			if (this.RefName != null && this.RefName != XmlQualifiedName.Empty)
			{
				this.referencedAttribute = schema.FindAttribute(this.RefName);
				if (this.referencedAttribute != null)
				{
					this.errorCount += this.referencedAttribute.Validate(h, schema);
				}
				else if (!schema.IsNamespaceAbsent(this.RefName.Namespace))
				{
					base.error(h, "Referenced attribute " + this.RefName + " was not found in the corresponding schema.");
				}
			}
			if (this.attributeType == null)
			{
				this.attributeType = XmlSchemaSimpleType.AnySimpleType;
			}
			if (this.defaultValue != null || this.fixedValue != null)
			{
				XmlSchemaDatatype xmlSchemaDatatype = this.attributeType as XmlSchemaDatatype;
				if (xmlSchemaDatatype == null)
				{
					xmlSchemaDatatype = ((XmlSchemaSimpleType)this.attributeType).Datatype;
				}
				if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.QName)
				{
					base.error(h, "By the defection of the W3C XML Schema specification, it is impossible to supply QName default or fixed values.");
				}
				else
				{
					try
					{
						if (this.defaultValue != null)
						{
							this.validatedDefaultValue = xmlSchemaDatatype.Normalize(this.defaultValue);
							xmlSchemaDatatype.ParseValue(this.validatedDefaultValue, null, null);
						}
					}
					catch (Exception innerException)
					{
						XmlSchemaObject.error(h, "The Attribute's default value is invalid with its type definition.", innerException);
					}
					try
					{
						if (this.fixedValue != null)
						{
							this.validatedFixedValue = xmlSchemaDatatype.Normalize(this.fixedValue);
							this.validatedFixedTypedValue = xmlSchemaDatatype.ParseValue(this.validatedFixedValue, null, null);
						}
					}
					catch (Exception innerException2)
					{
						XmlSchemaObject.error(h, "The Attribute's fixed value is invalid with its type definition.", innerException2);
					}
				}
			}
			if (this.Use == XmlSchemaUse.None)
			{
				this.validatedUse = XmlSchemaUse.Optional;
			}
			else
			{
				this.validatedUse = this.Use;
			}
			if (this.attributeType != null)
			{
				this.attributeSchemaType = (this.attributeType as XmlSchemaSimpleType);
				if (this.attributeType == XmlSchemaSimpleType.AnySimpleType)
				{
					this.attributeSchemaType = XmlSchemaSimpleType.XsAnySimpleType;
				}
				if (this.attributeSchemaType == null)
				{
					this.attributeSchemaType = XmlSchemaType.GetBuiltInSimpleType(this.SchemaTypeName);
				}
			}
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00058214 File Offset: 0x00056414
		internal bool AttributeEquals(XmlSchemaAttribute other)
		{
			return !(base.Id != other.Id) && !(this.QualifiedName != other.QualifiedName) && this.AttributeType == other.AttributeType && this.ValidatedUse == other.ValidatedUse && !(this.ValidatedDefaultValue != other.ValidatedDefaultValue) && !(this.ValidatedFixedValue != other.ValidatedFixedValue);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x000582A0 File Offset: 0x000564A0
		internal static XmlSchemaAttribute Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "attribute")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaAttribute.Read, name=" + reader.Name, null);
				reader.SkipToEnd();
				return null;
			}
			xmlSchemaAttribute.LineNumber = reader.LineNumber;
			xmlSchemaAttribute.LinePosition = reader.LinePosition;
			xmlSchemaAttribute.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "default")
				{
					xmlSchemaAttribute.defaultValue = reader.Value;
				}
				else if (reader.Name == "fixed")
				{
					xmlSchemaAttribute.fixedValue = reader.Value;
				}
				else if (reader.Name == "form")
				{
					Exception ex;
					xmlSchemaAttribute.form = XmlSchemaUtil.ReadFormAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for form attribute", ex);
					}
				}
				else if (reader.Name == "id")
				{
					xmlSchemaAttribute.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					xmlSchemaAttribute.name = reader.Value;
				}
				else if (reader.Name == "ref")
				{
					Exception ex2;
					xmlSchemaAttribute.refName = XmlSchemaUtil.ReadQNameAttribute(reader, out ex2);
					if (ex2 != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for ref attribute", ex2);
					}
				}
				else if (reader.Name == "type")
				{
					Exception ex3;
					xmlSchemaAttribute.schemaTypeName = XmlSchemaUtil.ReadQNameAttribute(reader, out ex3);
					if (ex3 != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for type attribute", ex3);
					}
				}
				else if (reader.Name == "use")
				{
					Exception ex4;
					xmlSchemaAttribute.use = XmlSchemaUtil.ReadUseAttribute(reader, out ex4);
					if (ex4 != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for use attribute", ex4);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for attribute", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaAttribute);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaAttribute;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "attribute")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaAttribute.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaAttribute.Annotation = xmlSchemaAnnotation;
					}
				}
				else if (num <= 2 && reader.LocalName == "simpleType")
				{
					num = 3;
					XmlSchemaSimpleType xmlSchemaSimpleType = XmlSchemaSimpleType.Read(reader, h);
					if (xmlSchemaSimpleType != null)
					{
						xmlSchemaAttribute.schemaType = xmlSchemaSimpleType;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x040007BB RID: 1979
		private const string xmlname = "attribute";

		// Token: 0x040007BC RID: 1980
		private object attributeType;

		// Token: 0x040007BD RID: 1981
		private XmlSchemaSimpleType attributeSchemaType;

		// Token: 0x040007BE RID: 1982
		private string defaultValue;

		// Token: 0x040007BF RID: 1983
		private string fixedValue;

		// Token: 0x040007C0 RID: 1984
		private string validatedDefaultValue;

		// Token: 0x040007C1 RID: 1985
		private string validatedFixedValue;

		// Token: 0x040007C2 RID: 1986
		private object validatedFixedTypedValue;

		// Token: 0x040007C3 RID: 1987
		private XmlSchemaForm form;

		// Token: 0x040007C4 RID: 1988
		private string name;

		// Token: 0x040007C5 RID: 1989
		private string targetNamespace;

		// Token: 0x040007C6 RID: 1990
		private XmlQualifiedName qualifiedName;

		// Token: 0x040007C7 RID: 1991
		private XmlQualifiedName refName;

		// Token: 0x040007C8 RID: 1992
		private XmlSchemaSimpleType schemaType;

		// Token: 0x040007C9 RID: 1993
		private XmlQualifiedName schemaTypeName;

		// Token: 0x040007CA RID: 1994
		private XmlSchemaUse use;

		// Token: 0x040007CB RID: 1995
		private XmlSchemaUse validatedUse;

		// Token: 0x040007CC RID: 1996
		private XmlSchemaAttribute referencedAttribute;
	}
}
