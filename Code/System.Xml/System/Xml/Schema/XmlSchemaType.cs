using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>The base class for all simple types and complex types.</summary>
	// Token: 0x02000243 RID: 579
	public class XmlSchemaType : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaType" /> class.</summary>
		// Token: 0x06001748 RID: 5960 RVA: 0x00073FF0 File Offset: 0x000721F0
		public XmlSchemaType()
		{
			this.final = XmlSchemaDerivationMethod.None;
			this.QNameInternal = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of the type.</summary>
		/// <returns>The name of the type.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00074010 File Offset: 0x00072210
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x00074018 File Offset: 0x00072218
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

		/// <summary>Gets or sets the final attribute of the type derivation that indicates if further derivations are allowed.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values. The default is <see cref="F:System.Xml.Schema.XmlSchemaDerivationMethod.None" />.</returns>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00074024 File Offset: 0x00072224
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x0007402C File Offset: 0x0007222C
		[XmlAttribute("final")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod Final
		{
			get
			{
				return this.final;
			}
			set
			{
				this.final = value;
			}
		}

		/// <summary>Gets the qualified name for the type built from the Name attribute of this type. This is a post-schema-compilation property.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlQualifiedName" /> for the type built from the Name attribute of this type.</returns>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00074038 File Offset: 0x00072238
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.QNameInternal;
			}
		}

		/// <summary>Gets the post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property.</summary>
		/// <returns>The post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property. The default is the finalDefault attribute value of the schema element.</returns>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00074040 File Offset: 0x00072240
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		/// <summary>Gets the post-compilation object type or the built-in XML Schema Definition Language (XSD) data type, simpleType element, or complexType element. This is a post-schema-compilation infoset property.</summary>
		/// <returns>The built-in XSD data type, simpleType element, or complexType element.</returns>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00074048 File Offset: 0x00072248
		[Obsolete("This property is going away. Use BaseXmlSchemaType instead")]
		[XmlIgnore]
		public object BaseSchemaType
		{
			get
			{
				if (this.BaseXmlSchemaType != null)
				{
					return this.BaseXmlSchemaType;
				}
				if (this == XmlSchemaComplexType.AnyType)
				{
					return null;
				}
				return this.Datatype;
			}
		}

		/// <summary>Gets the post-compilation value for the base type of this schema type.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object representing the base type of this schema type.</returns>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0007407C File Offset: 0x0007227C
		[MonoTODO]
		[XmlIgnore]
		public XmlSchemaType BaseXmlSchemaType
		{
			get
			{
				return this.BaseXmlSchemaTypeInternal;
			}
		}

		/// <summary>Gets the post-compilation information on how this element was derived from its base type.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00074084 File Offset: 0x00072284
		[XmlIgnore]
		public XmlSchemaDerivationMethod DerivedBy
		{
			get
			{
				return this.resolvedDerivedBy;
			}
		}

		/// <summary>Gets the post-compilation value for the data type of the complex type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> post-schema-compilation value.</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x0007408C File Offset: 0x0007228C
		[XmlIgnore]
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.DatatypeInternal;
			}
		}

		/// <summary>Gets or sets a value indicating if this type has a mixed content model. This property is only valid in a complex type.</summary>
		/// <returns>true if the type has a mixed content model; otherwise, false. The default is false.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00074094 File Offset: 0x00072294
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x0007409C File Offset: 0x0007229C
		[XmlIgnore]
		public virtual bool IsMixed
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

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> of the type.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x000740A8 File Offset: 0x000722A8
		[XmlIgnore]
		public XmlTypeCode TypeCode
		{
			get
			{
				if (this == XmlSchemaComplexType.AnyType)
				{
					return XmlTypeCode.Item;
				}
				if (this.DatatypeInternal == XmlSchemaSimpleType.AnySimpleType)
				{
					return XmlTypeCode.AnyAtomicType;
				}
				if (this == XmlSchemaSimpleType.XsIDRefs)
				{
					return XmlTypeCode.Idref;
				}
				if (this == XmlSchemaSimpleType.XsEntities)
				{
					return XmlTypeCode.Entity;
				}
				if (this == XmlSchemaSimpleType.XsNMTokens)
				{
					return XmlTypeCode.NmToken;
				}
				if (this.DatatypeInternal != null)
				{
					return this.DatatypeInternal.TypeCode;
				}
				return this.BaseXmlSchemaType.TypeCode;
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00074124 File Offset: 0x00072324
		internal static XmlSchemaType GetBuiltInType(XmlQualifiedName qualifiedName)
		{
			XmlSchemaType xmlSchemaType = XmlSchemaType.GetBuiltInSimpleType(qualifiedName);
			if (xmlSchemaType == null)
			{
				xmlSchemaType = XmlSchemaType.GetBuiltInComplexType(qualifiedName);
			}
			return xmlSchemaType;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00074148 File Offset: 0x00072348
		internal static XmlSchemaType GetBuiltInType(XmlTypeCode typecode)
		{
			if (typecode == XmlTypeCode.Item)
			{
				return XmlSchemaComplexType.AnyType;
			}
			return XmlSchemaType.GetBuiltInSimpleType(typecode);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type of the complex type specified by qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type.</returns>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the complex type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is null.</exception>
		// Token: 0x06001758 RID: 5976 RVA: 0x00074160 File Offset: 0x00072360
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName.Name == "anyType" && qualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return XmlSchemaComplexType.AnyType;
			}
			return null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type of the complex type specified.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type.</returns>
		/// <param name="typeCode">One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values representing the complex type.</param>
		// Token: 0x06001759 RID: 5977 RVA: 0x000741A0 File Offset: 0x000723A0
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlTypeCode type)
		{
			if (type != XmlTypeCode.Item)
			{
				return null;
			}
			return XmlSchemaComplexType.AnyType;
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the simple type specified by qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the simple type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is null.</exception>
		// Token: 0x0600175A RID: 5978 RVA: 0x000741C4 File Offset: 0x000723C4
		[MonoTODO]
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlQualifiedName qualifiedName)
		{
			string text;
			if (qualifiedName.Namespace == "http://www.w3.org/2003/11/xpath-datatypes")
			{
				text = qualifiedName.Name;
				switch (text)
				{
				case "untypedAtomic":
					return XmlSchemaSimpleType.XdtUntypedAtomic;
				case "anyAtomicType":
					return XmlSchemaSimpleType.XdtAnyAtomicType;
				case "yearMonthDuration":
					return XmlSchemaSimpleType.XdtYearMonthDuration;
				case "dayTimeDuration":
					return XmlSchemaSimpleType.XdtDayTimeDuration;
				}
				return null;
			}
			if (qualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
			{
				return null;
			}
			text = qualifiedName.Name;
			switch (text)
			{
			case "anySimpleType":
				return XmlSchemaSimpleType.XsAnySimpleType;
			case "string":
				return XmlSchemaSimpleType.XsString;
			case "boolean":
				return XmlSchemaSimpleType.XsBoolean;
			case "decimal":
				return XmlSchemaSimpleType.XsDecimal;
			case "float":
				return XmlSchemaSimpleType.XsFloat;
			case "double":
				return XmlSchemaSimpleType.XsDouble;
			case "duration":
				return XmlSchemaSimpleType.XsDuration;
			case "dateTime":
				return XmlSchemaSimpleType.XsDateTime;
			case "time":
				return XmlSchemaSimpleType.XsTime;
			case "date":
				return XmlSchemaSimpleType.XsDate;
			case "gYearMonth":
				return XmlSchemaSimpleType.XsGYearMonth;
			case "gYear":
				return XmlSchemaSimpleType.XsGYear;
			case "gMonthDay":
				return XmlSchemaSimpleType.XsGMonthDay;
			case "gDay":
				return XmlSchemaSimpleType.XsGDay;
			case "gMonth":
				return XmlSchemaSimpleType.XsGMonth;
			case "hexBinary":
				return XmlSchemaSimpleType.XsHexBinary;
			case "base64Binary":
				return XmlSchemaSimpleType.XsBase64Binary;
			case "anyURI":
				return XmlSchemaSimpleType.XsAnyUri;
			case "QName":
				return XmlSchemaSimpleType.XsQName;
			case "NOTATION":
				return XmlSchemaSimpleType.XsNotation;
			case "normalizedString":
				return XmlSchemaSimpleType.XsNormalizedString;
			case "token":
				return XmlSchemaSimpleType.XsToken;
			case "language":
				return XmlSchemaSimpleType.XsLanguage;
			case "NMTOKEN":
				return XmlSchemaSimpleType.XsNMToken;
			case "NMTOKENS":
				return XmlSchemaSimpleType.XsNMTokens;
			case "Name":
				return XmlSchemaSimpleType.XsName;
			case "NCName":
				return XmlSchemaSimpleType.XsNCName;
			case "ID":
				return XmlSchemaSimpleType.XsID;
			case "IDREF":
				return XmlSchemaSimpleType.XsIDRef;
			case "IDREFS":
				return XmlSchemaSimpleType.XsIDRefs;
			case "ENTITY":
				return XmlSchemaSimpleType.XsEntity;
			case "ENTITIES":
				return XmlSchemaSimpleType.XsEntities;
			case "integer":
				return XmlSchemaSimpleType.XsInteger;
			case "nonPositiveInteger":
				return XmlSchemaSimpleType.XsNonPositiveInteger;
			case "negativeInteger":
				return XmlSchemaSimpleType.XsNegativeInteger;
			case "long":
				return XmlSchemaSimpleType.XsLong;
			case "int":
				return XmlSchemaSimpleType.XsInt;
			case "short":
				return XmlSchemaSimpleType.XsShort;
			case "byte":
				return XmlSchemaSimpleType.XsByte;
			case "nonNegativeInteger":
				return XmlSchemaSimpleType.XsNonNegativeInteger;
			case "positiveInteger":
				return XmlSchemaSimpleType.XsPositiveInteger;
			case "unsignedLong":
				return XmlSchemaSimpleType.XsUnsignedLong;
			case "unsignedInt":
				return XmlSchemaSimpleType.XsUnsignedInt;
			case "unsignedShort":
				return XmlSchemaSimpleType.XsUnsignedShort;
			case "unsignedByte":
				return XmlSchemaSimpleType.XsUnsignedByte;
			}
			return null;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000746E0 File Offset: 0x000728E0
		internal static XmlSchemaSimpleType GetBuiltInSimpleType(XmlSchemaDatatype type)
		{
			if (type is XsdEntities)
			{
				return XmlSchemaSimpleType.XsEntities;
			}
			if (type is XsdNMTokens)
			{
				return XmlSchemaSimpleType.XsNMTokens;
			}
			if (type is XsdIDRefs)
			{
				return XmlSchemaSimpleType.XsIDRefs;
			}
			return XmlSchemaType.GetBuiltInSimpleType(type.TypeCode);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the simple type specified.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		/// <param name="typeCode">One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values representing the simple type.</param>
		// Token: 0x0600175C RID: 5980 RVA: 0x0007472C File Offset: 0x0007292C
		[MonoTODO]
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlTypeCode type)
		{
			switch (type)
			{
			case XmlTypeCode.None:
			case XmlTypeCode.Item:
			case XmlTypeCode.Node:
			case XmlTypeCode.Document:
			case XmlTypeCode.Element:
			case XmlTypeCode.Attribute:
			case XmlTypeCode.Namespace:
			case XmlTypeCode.ProcessingInstruction:
			case XmlTypeCode.Comment:
			case XmlTypeCode.Text:
				return null;
			case XmlTypeCode.AnyAtomicType:
				return XmlSchemaSimpleType.XdtAnyAtomicType;
			case XmlTypeCode.UntypedAtomic:
				return XmlSchemaSimpleType.XdtUntypedAtomic;
			case XmlTypeCode.String:
				return XmlSchemaSimpleType.XsString;
			case XmlTypeCode.Boolean:
				return XmlSchemaSimpleType.XsBoolean;
			case XmlTypeCode.Decimal:
				return XmlSchemaSimpleType.XsDecimal;
			case XmlTypeCode.Float:
				return XmlSchemaSimpleType.XsFloat;
			case XmlTypeCode.Double:
				return XmlSchemaSimpleType.XsDouble;
			case XmlTypeCode.Duration:
				return XmlSchemaSimpleType.XsDuration;
			case XmlTypeCode.DateTime:
				return XmlSchemaSimpleType.XsDateTime;
			case XmlTypeCode.Time:
				return XmlSchemaSimpleType.XsTime;
			case XmlTypeCode.Date:
				return XmlSchemaSimpleType.XsDate;
			case XmlTypeCode.GYearMonth:
				return XmlSchemaSimpleType.XsGYearMonth;
			case XmlTypeCode.GYear:
				return XmlSchemaSimpleType.XsGYear;
			case XmlTypeCode.GMonthDay:
				return XmlSchemaSimpleType.XsGMonthDay;
			case XmlTypeCode.GDay:
				return XmlSchemaSimpleType.XsGDay;
			case XmlTypeCode.GMonth:
				return XmlSchemaSimpleType.XsGMonth;
			case XmlTypeCode.HexBinary:
				return XmlSchemaSimpleType.XsHexBinary;
			case XmlTypeCode.Base64Binary:
				return XmlSchemaSimpleType.XsBase64Binary;
			case XmlTypeCode.AnyUri:
				return XmlSchemaSimpleType.XsAnyUri;
			case XmlTypeCode.QName:
				return XmlSchemaSimpleType.XsQName;
			case XmlTypeCode.Notation:
				return XmlSchemaSimpleType.XsNotation;
			case XmlTypeCode.NormalizedString:
				return XmlSchemaSimpleType.XsNormalizedString;
			case XmlTypeCode.Token:
				return XmlSchemaSimpleType.XsToken;
			case XmlTypeCode.Language:
				return XmlSchemaSimpleType.XsLanguage;
			case XmlTypeCode.NmToken:
				return XmlSchemaSimpleType.XsNMToken;
			case XmlTypeCode.Name:
				return XmlSchemaSimpleType.XsName;
			case XmlTypeCode.NCName:
				return XmlSchemaSimpleType.XsNCName;
			case XmlTypeCode.Id:
				return XmlSchemaSimpleType.XsID;
			case XmlTypeCode.Idref:
				return XmlSchemaSimpleType.XsIDRef;
			case XmlTypeCode.Entity:
				return XmlSchemaSimpleType.XsEntity;
			case XmlTypeCode.Integer:
				return XmlSchemaSimpleType.XsInteger;
			case XmlTypeCode.NonPositiveInteger:
				return XmlSchemaSimpleType.XsNonPositiveInteger;
			case XmlTypeCode.NegativeInteger:
				return XmlSchemaSimpleType.XsNegativeInteger;
			case XmlTypeCode.Long:
				return XmlSchemaSimpleType.XsLong;
			case XmlTypeCode.Int:
				return XmlSchemaSimpleType.XsInt;
			case XmlTypeCode.Short:
				return XmlSchemaSimpleType.XsShort;
			case XmlTypeCode.Byte:
				return XmlSchemaSimpleType.XsByte;
			case XmlTypeCode.NonNegativeInteger:
				return XmlSchemaSimpleType.XsNonNegativeInteger;
			case XmlTypeCode.UnsignedLong:
				return XmlSchemaSimpleType.XsUnsignedLong;
			case XmlTypeCode.UnsignedInt:
				return XmlSchemaSimpleType.XsUnsignedInt;
			case XmlTypeCode.UnsignedShort:
				return XmlSchemaSimpleType.XsUnsignedShort;
			case XmlTypeCode.UnsignedByte:
				return XmlSchemaSimpleType.XsUnsignedByte;
			case XmlTypeCode.PositiveInteger:
				return XmlSchemaSimpleType.XsPositiveInteger;
			case XmlTypeCode.YearMonthDuration:
				return XmlSchemaSimpleType.XdtYearMonthDuration;
			case XmlTypeCode.DayTimeDuration:
				return XmlSchemaSimpleType.XdtDayTimeDuration;
			default:
				return null;
			}
		}

		/// <summary>Returns a value indicating if the derived schema type specified is derived from the base schema type specified</summary>
		/// <returns>true if the derived type is derived from the base type; otherwise, false.</returns>
		/// <param name="derivedType">The derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test.</param>
		/// <param name="baseType">The base <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test the derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> against.</param>
		/// <param name="except">One of the <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values representing a type derivation method to exclude from testing.</param>
		// Token: 0x0600175D RID: 5981 RVA: 0x00074934 File Offset: 0x00072B34
		public static bool IsDerivedFrom(XmlSchemaType derivedType, XmlSchemaType baseType, XmlSchemaDerivationMethod except)
		{
			return derivedType.BaseXmlSchemaType != null && (derivedType.DerivedBy & except) == XmlSchemaDerivationMethod.Empty && (derivedType.BaseXmlSchemaType == baseType || XmlSchemaType.IsDerivedFrom(derivedType.BaseXmlSchemaType, baseType, except));
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00074978 File Offset: 0x00072B78
		internal bool ValidateRecursionCheck()
		{
			if (this.recursed)
			{
				return this != XmlSchemaComplexType.AnyType;
			}
			this.recursed = true;
			XmlSchemaType baseXmlSchemaType = this.BaseXmlSchemaType;
			bool result = false;
			if (baseXmlSchemaType != null)
			{
				result = baseXmlSchemaType.ValidateRecursionCheck();
			}
			this.recursed = false;
			return result;
		}

		// Token: 0x04000971 RID: 2417
		private XmlSchemaDerivationMethod final;

		// Token: 0x04000972 RID: 2418
		private bool isMixed;

		// Token: 0x04000973 RID: 2419
		private string name;

		// Token: 0x04000974 RID: 2420
		private bool recursed;

		// Token: 0x04000975 RID: 2421
		internal XmlQualifiedName BaseSchemaTypeName;

		// Token: 0x04000976 RID: 2422
		internal XmlSchemaType BaseXmlSchemaTypeInternal;

		// Token: 0x04000977 RID: 2423
		internal XmlSchemaDatatype DatatypeInternal;

		// Token: 0x04000978 RID: 2424
		internal XmlSchemaDerivationMethod resolvedDerivedBy;

		// Token: 0x04000979 RID: 2425
		internal XmlSchemaDerivationMethod finalResolved;

		// Token: 0x0400097A RID: 2426
		internal XmlQualifiedName QNameInternal;
	}
}
