using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the list element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to define a simpleType element as a list of values of a specified data type.</summary>
	// Token: 0x0200023F RID: 575
	public class XmlSchemaSimpleTypeList : XmlSchemaSimpleTypeContent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeList" /> class.</summary>
		// Token: 0x06001714 RID: 5908 RVA: 0x00071418 File Offset: 0x0006F618
		public XmlSchemaSimpleTypeList()
		{
			this.ItemTypeName = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of a built-in data type or simpleType element defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The type name of the simple type list.</returns>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0007142C File Offset: 0x0006F62C
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00071434 File Offset: 0x0006F634
		[XmlAttribute("itemType")]
		public XmlQualifiedName ItemTypeName
		{
			get
			{
				return this.itemTypeName;
			}
			set
			{
				this.itemTypeName = value;
			}
		}

		/// <summary>Gets or sets the simpleType element that is derived from the type specified by the base value.</summary>
		/// <returns>The item type for the simple type element.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x00071440 File Offset: 0x0006F640
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x00071448 File Offset: 0x0006F648
		[XmlElement("simpleType", Type = typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the simpleType element based on the <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemType" /> and <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemTypeName" /> values of the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the simpleType element.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00071454 File Offset: 0x0006F654
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x0007145C File Offset: 0x0006F65C
		[XmlIgnore]
		public XmlSchemaSimpleType BaseItemType
		{
			get
			{
				return this.validatedListItemSchemaType;
			}
			set
			{
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x00071460 File Offset: 0x0006F660
		internal object ValidatedListItemType
		{
			get
			{
				return this.validatedListItemType;
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00071468 File Offset: 0x0006F668
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.ItemType != null)
			{
				this.ItemType.SetParent(this);
			}
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00071494 File Offset: 0x0006F694
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			this.errorCount = 0;
			if (this.ItemType != null && !this.ItemTypeName.IsEmpty)
			{
				base.error(h, "both itemType and simpletype can't be present");
			}
			if (this.ItemType == null && this.ItemTypeName.IsEmpty)
			{
				base.error(h, "one of itemType or simpletype must be present");
			}
			if (this.ItemType != null)
			{
				this.errorCount += this.ItemType.Compile(h, schema);
			}
			if (!XmlSchemaUtil.CheckQName(this.ItemTypeName))
			{
				base.error(h, "BaseTypeName must be a XmlQualifiedName");
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00071574 File Offset: 0x0006F774
		internal override int Validate(ValidationEventHandler h, XmlSchema schema)
		{
			if (base.IsValidated(schema.ValidationId))
			{
				return this.errorCount;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType = this.itemType;
			if (xmlSchemaSimpleType == null)
			{
				xmlSchemaSimpleType = (schema.FindSchemaType(this.itemTypeName) as XmlSchemaSimpleType);
			}
			if (xmlSchemaSimpleType != null)
			{
				this.errorCount += xmlSchemaSimpleType.Validate(h, schema);
				this.validatedListItemType = xmlSchemaSimpleType;
			}
			else if (this.itemTypeName == XmlSchemaComplexType.AnyTypeName)
			{
				this.validatedListItemType = XmlSchemaSimpleType.AnySimpleType;
			}
			else if (XmlSchemaUtil.IsBuiltInDatatypeName(this.itemTypeName))
			{
				this.validatedListItemType = XmlSchemaDatatype.FromName(this.itemTypeName);
				if (this.validatedListItemType == null)
				{
					base.error(h, "Invalid schema type name was specified: " + this.itemTypeName);
				}
			}
			else if (!schema.IsNamespaceAbsent(this.itemTypeName.Namespace))
			{
				base.error(h, "Referenced base list item schema type " + this.itemTypeName + " was not found.");
			}
			XmlSchemaSimpleType xmlSchemaSimpleType2 = this.validatedListItemType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType2 == null && this.validatedListItemType != null)
			{
				xmlSchemaSimpleType2 = XmlSchemaType.GetBuiltInSimpleType(((XmlSchemaDatatype)this.validatedListItemType).TypeCode);
			}
			this.validatedListItemSchemaType = xmlSchemaSimpleType2;
			this.ValidationId = schema.ValidationId;
			return this.errorCount;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000716CC File Offset: 0x0006F8CC
		internal static XmlSchemaSimpleTypeList Read(XmlSchemaReader reader, ValidationEventHandler h)
		{
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != "list")
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaSimpleTypeList.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaSimpleTypeList.LineNumber = reader.LineNumber;
			xmlSchemaSimpleTypeList.LinePosition = reader.LinePosition;
			xmlSchemaSimpleTypeList.SourceUri = reader.BaseURI;
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaSimpleTypeList.Id = reader.Value;
				}
				else if (reader.Name == "itemType")
				{
					Exception ex;
					xmlSchemaSimpleTypeList.ItemTypeName = XmlSchemaUtil.ReadQNameAttribute(reader, out ex);
					if (ex != null)
					{
						XmlSchemaObject.error(h, reader.Value + " is not a valid value for itemType attribute", ex);
					}
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for list", null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaSimpleTypeList);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaSimpleTypeList;
			}
			int num = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != "list")
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaSimpleTypeList.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num <= 1 && reader.LocalName == "annotation")
				{
					num = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaSimpleTypeList.Annotation = xmlSchemaAnnotation;
					}
				}
				else if (num <= 2 && reader.LocalName == "simpleType")
				{
					num = 3;
					XmlSchemaSimpleType xmlSchemaSimpleType = XmlSchemaSimpleType.Read(reader, h);
					if (xmlSchemaSimpleType != null)
					{
						xmlSchemaSimpleTypeList.itemType = xmlSchemaSimpleType;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaSimpleTypeList;
		}

		// Token: 0x04000953 RID: 2387
		private const string xmlname = "list";

		// Token: 0x04000954 RID: 2388
		private XmlSchemaSimpleType itemType;

		// Token: 0x04000955 RID: 2389
		private XmlQualifiedName itemTypeName;

		// Token: 0x04000956 RID: 2390
		private object validatedListItemType;

		// Token: 0x04000957 RID: 2391
		private XmlSchemaSimpleType validatedListItemSchemaType;
	}
}
