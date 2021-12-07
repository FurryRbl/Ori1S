using System;
using System.Collections.Generic;
using System.Text;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> class is an abstract class for mapping XML Schema definition language (XSD) types to Common Language Runtime (CLR) types.</summary>
	// Token: 0x0200020C RID: 524
	public abstract class XmlSchemaDatatype
	{
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0005D7F8 File Offset: 0x0005B9F8
		internal virtual XsdWhitespaceFacet Whitespace
		{
			get
			{
				return this.WhitespaceValue;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0005D800 File Offset: 0x0005BA00
		public virtual XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</returns>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0005D804 File Offset: 0x0005BA04
		public virtual XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return XmlSchemaDatatypeVariety.Atomic;
			}
		}

		/// <summary>When overridden in a derived class, gets the type for the string as specified in the World Wide Web Consortium (W3C) XML 1.0 specification.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlTokenizedType" /> value for the string.</returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060014F6 RID: 5366
		public abstract XmlTokenizedType TokenizedType { get; }

		/// <summary>When overridden in a derived class, gets the Common Language Runtime (CLR) type of the item.</summary>
		/// <returns>The Common Language Runtime (CLR) type of the item.</returns>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060014F7 RID: 5367
		public abstract Type ValueType { get; }

		/// <summary>Converts the value specified, whose type is one of the valid Common Language Runtime (CLR) representations of the XML schema type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, to the CLR type specified.</summary>
		/// <returns>The converted input value.</returns>
		/// <param name="value">The input value to convert to the specified type.</param>
		/// <param name="targetType">The target type to convert the input value to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Object" /> or <see cref="T:System.Type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />   does not support a conversion from type of the value specified to the type specified.</exception>
		// Token: 0x060014F8 RID: 5368 RVA: 0x0005D808 File Offset: 0x0005BA08
		[MonoTODO]
		public virtual object ChangeType(object value, Type targetType)
		{
			return this.ChangeType(value, targetType, null);
		}

		/// <summary>Converts the value specified, whose type is one of the valid Common Language Runtime (CLR) representations of the XML schema type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, to the CLR type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> if the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> represents the xs:QName type or a type derived from it.</summary>
		/// <returns>The converted input value.</returns>
		/// <param name="value">The input value to convert to the specified type.</param>
		/// <param name="targetType">The target type to convert the input value to.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> used for resolving namespace prefixes. This is only of use if the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />  represents the xs:QName type or a type derived from it.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Object" /> or <see cref="T:System.Type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />   does not support a conversion from type of the value specified to the type specified.</exception>
		// Token: 0x060014F9 RID: 5369 RVA: 0x0005D814 File Offset: 0x0005BA14
		[MonoTODO]
		public virtual object ChangeType(object value, Type targetType, IXmlNamespaceResolver nsResolver)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Xml.Schema.XmlSchemaDatatype.IsDerivedFrom(System.Xml.Schema.XmlSchemaDatatype)" /> method always returns false.</summary>
		/// <returns>Always returns false.</returns>
		/// <param name="datatype">The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />.</param>
		// Token: 0x060014FA RID: 5370 RVA: 0x0005D81C File Offset: 0x0005BA1C
		public virtual bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			return this == datatype;
		}

		/// <summary>When overridden in a derived class, validates the string specified against a built-in or user-defined simple type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast safely to the type returned by the <see cref="P:System.Xml.Schema.XmlSchemaDatatype.ValueType" /> property.</returns>
		/// <param name="s">The string to validate against the simple type.</param>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> to use for atomization while parsing the string if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:NCName type. </param>
		/// <param name="nsmgr">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object to use while parsing the string if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:QName type.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The input value is not a valid instance of this W3C XML Schema type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value to parse cannot be null.</exception>
		// Token: 0x060014FB RID: 5371
		public abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		// Token: 0x060014FC RID: 5372 RVA: 0x0005D824 File Offset: 0x0005BA24
		internal virtual ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return null;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0005D828 File Offset: 0x0005BA28
		internal string Normalize(string s)
		{
			return this.Normalize(s, this.Whitespace);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0005D838 File Offset: 0x0005BA38
		internal string Normalize(string s, XsdWhitespaceFacet whitespaceFacet)
		{
			int num = s.IndexOfAny(XmlSchemaDatatype.wsChars);
			if (num < 0)
			{
				return s;
			}
			string text;
			if (whitespaceFacet == XsdWhitespaceFacet.Replace)
			{
				this.sb.Length = 0;
				this.sb.Append(s);
				for (int i = 0; i < this.sb.Length; i++)
				{
					switch (this.sb[i])
					{
					case '\t':
					case '\n':
					case '\r':
						this.sb[i] = ' ';
						break;
					}
				}
				text = this.sb.ToString();
				this.sb.Length = 0;
				return text;
			}
			if (whitespaceFacet != XsdWhitespaceFacet.Collapse)
			{
				return s;
			}
			foreach (string text2 in s.Trim().Split(XmlSchemaDatatype.wsChars))
			{
				if (text2 != string.Empty)
				{
					this.sb.Append(text2);
					this.sb.Append(" ");
				}
			}
			text = this.sb.ToString();
			this.sb.Length = 0;
			return text.Trim();
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0005D988 File Offset: 0x0005BB88
		internal static XmlSchemaDatatype FromName(XmlQualifiedName qname)
		{
			return XmlSchemaDatatype.FromName(qname.Name, qname.Namespace);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0005D99C File Offset: 0x0005BB9C
		internal static XmlSchemaDatatype FromName(string localName, string ns)
		{
			if (ns != null)
			{
				if (XmlSchemaDatatype.<>f__switch$map3F == null)
				{
					XmlSchemaDatatype.<>f__switch$map3F = new Dictionary<string, int>(2)
					{
						{
							"http://www.w3.org/2001/XMLSchema",
							0
						},
						{
							"http://www.w3.org/2003/11/xpath-datatypes",
							1
						}
					};
				}
				int num;
				if (XmlSchemaDatatype.<>f__switch$map3F.TryGetValue(ns, out num))
				{
					if (num == 0)
					{
						switch (localName)
						{
						case "anySimpleType":
							return XmlSchemaDatatype.datatypeAnySimpleType;
						case "string":
							return XmlSchemaDatatype.datatypeString;
						case "normalizedString":
							return XmlSchemaDatatype.datatypeNormalizedString;
						case "token":
							return XmlSchemaDatatype.datatypeToken;
						case "language":
							return XmlSchemaDatatype.datatypeLanguage;
						case "NMTOKEN":
							return XmlSchemaDatatype.datatypeNMToken;
						case "NMTOKENS":
							return XmlSchemaDatatype.datatypeNMTokens;
						case "Name":
							return XmlSchemaDatatype.datatypeName;
						case "NCName":
							return XmlSchemaDatatype.datatypeNCName;
						case "ID":
							return XmlSchemaDatatype.datatypeID;
						case "IDREF":
							return XmlSchemaDatatype.datatypeIDRef;
						case "IDREFS":
							return XmlSchemaDatatype.datatypeIDRefs;
						case "ENTITY":
							return XmlSchemaDatatype.datatypeEntity;
						case "ENTITIES":
							return XmlSchemaDatatype.datatypeEntities;
						case "NOTATION":
							return XmlSchemaDatatype.datatypeNotation;
						case "decimal":
							return XmlSchemaDatatype.datatypeDecimal;
						case "integer":
							return XmlSchemaDatatype.datatypeInteger;
						case "long":
							return XmlSchemaDatatype.datatypeLong;
						case "int":
							return XmlSchemaDatatype.datatypeInt;
						case "short":
							return XmlSchemaDatatype.datatypeShort;
						case "byte":
							return XmlSchemaDatatype.datatypeByte;
						case "nonPositiveInteger":
							return XmlSchemaDatatype.datatypeNonPositiveInteger;
						case "negativeInteger":
							return XmlSchemaDatatype.datatypeNegativeInteger;
						case "nonNegativeInteger":
							return XmlSchemaDatatype.datatypeNonNegativeInteger;
						case "unsignedLong":
							return XmlSchemaDatatype.datatypeUnsignedLong;
						case "unsignedInt":
							return XmlSchemaDatatype.datatypeUnsignedInt;
						case "unsignedShort":
							return XmlSchemaDatatype.datatypeUnsignedShort;
						case "unsignedByte":
							return XmlSchemaDatatype.datatypeUnsignedByte;
						case "positiveInteger":
							return XmlSchemaDatatype.datatypePositiveInteger;
						case "float":
							return XmlSchemaDatatype.datatypeFloat;
						case "double":
							return XmlSchemaDatatype.datatypeDouble;
						case "base64Binary":
							return XmlSchemaDatatype.datatypeBase64Binary;
						case "boolean":
							return XmlSchemaDatatype.datatypeBoolean;
						case "anyURI":
							return XmlSchemaDatatype.datatypeAnyURI;
						case "duration":
							return XmlSchemaDatatype.datatypeDuration;
						case "dateTime":
							return XmlSchemaDatatype.datatypeDateTime;
						case "date":
							return XmlSchemaDatatype.datatypeDate;
						case "time":
							return XmlSchemaDatatype.datatypeTime;
						case "hexBinary":
							return XmlSchemaDatatype.datatypeHexBinary;
						case "QName":
							return XmlSchemaDatatype.datatypeQName;
						case "gYearMonth":
							return XmlSchemaDatatype.datatypeGYearMonth;
						case "gMonthDay":
							return XmlSchemaDatatype.datatypeGMonthDay;
						case "gYear":
							return XmlSchemaDatatype.datatypeGYear;
						case "gMonth":
							return XmlSchemaDatatype.datatypeGMonth;
						case "gDay":
							return XmlSchemaDatatype.datatypeGDay;
						}
						return null;
					}
					if (num == 1)
					{
						switch (localName)
						{
						case "anyAtomicType":
							return XmlSchemaDatatype.datatypeAnyAtomicType;
						case "untypedAtomic":
							return XmlSchemaDatatype.datatypeUntypedAtomic;
						case "dayTimeDuration":
							return XmlSchemaDatatype.datatypeDayTimeDuration;
						case "yearMonthDuration":
							return XmlSchemaDatatype.datatypeYearMonthDuration;
						}
						return null;
					}
				}
			}
			return null;
		}

		// Token: 0x04000809 RID: 2057
		internal XsdWhitespaceFacet WhitespaceValue;

		// Token: 0x0400080A RID: 2058
		private static char[] wsChars = new char[]
		{
			' ',
			'\t',
			'\n',
			'\r'
		};

		// Token: 0x0400080B RID: 2059
		private StringBuilder sb = new StringBuilder();

		// Token: 0x0400080C RID: 2060
		private static readonly XsdAnySimpleType datatypeAnySimpleType = XsdAnySimpleType.Instance;

		// Token: 0x0400080D RID: 2061
		private static readonly XsdString datatypeString = new XsdString();

		// Token: 0x0400080E RID: 2062
		private static readonly XsdNormalizedString datatypeNormalizedString = new XsdNormalizedString();

		// Token: 0x0400080F RID: 2063
		private static readonly XsdToken datatypeToken = new XsdToken();

		// Token: 0x04000810 RID: 2064
		private static readonly XsdLanguage datatypeLanguage = new XsdLanguage();

		// Token: 0x04000811 RID: 2065
		private static readonly XsdNMToken datatypeNMToken = new XsdNMToken();

		// Token: 0x04000812 RID: 2066
		private static readonly XsdNMTokens datatypeNMTokens = new XsdNMTokens();

		// Token: 0x04000813 RID: 2067
		private static readonly XsdName datatypeName = new XsdName();

		// Token: 0x04000814 RID: 2068
		private static readonly XsdNCName datatypeNCName = new XsdNCName();

		// Token: 0x04000815 RID: 2069
		private static readonly XsdID datatypeID = new XsdID();

		// Token: 0x04000816 RID: 2070
		private static readonly XsdIDRef datatypeIDRef = new XsdIDRef();

		// Token: 0x04000817 RID: 2071
		private static readonly XsdIDRefs datatypeIDRefs = new XsdIDRefs();

		// Token: 0x04000818 RID: 2072
		private static readonly XsdEntity datatypeEntity = new XsdEntity();

		// Token: 0x04000819 RID: 2073
		private static readonly XsdEntities datatypeEntities = new XsdEntities();

		// Token: 0x0400081A RID: 2074
		private static readonly XsdNotation datatypeNotation = new XsdNotation();

		// Token: 0x0400081B RID: 2075
		private static readonly XsdDecimal datatypeDecimal = new XsdDecimal();

		// Token: 0x0400081C RID: 2076
		private static readonly XsdInteger datatypeInteger = new XsdInteger();

		// Token: 0x0400081D RID: 2077
		private static readonly XsdLong datatypeLong = new XsdLong();

		// Token: 0x0400081E RID: 2078
		private static readonly XsdInt datatypeInt = new XsdInt();

		// Token: 0x0400081F RID: 2079
		private static readonly XsdShort datatypeShort = new XsdShort();

		// Token: 0x04000820 RID: 2080
		private static readonly XsdByte datatypeByte = new XsdByte();

		// Token: 0x04000821 RID: 2081
		private static readonly XsdNonNegativeInteger datatypeNonNegativeInteger = new XsdNonNegativeInteger();

		// Token: 0x04000822 RID: 2082
		private static readonly XsdPositiveInteger datatypePositiveInteger = new XsdPositiveInteger();

		// Token: 0x04000823 RID: 2083
		private static readonly XsdUnsignedLong datatypeUnsignedLong = new XsdUnsignedLong();

		// Token: 0x04000824 RID: 2084
		private static readonly XsdUnsignedInt datatypeUnsignedInt = new XsdUnsignedInt();

		// Token: 0x04000825 RID: 2085
		private static readonly XsdUnsignedShort datatypeUnsignedShort = new XsdUnsignedShort();

		// Token: 0x04000826 RID: 2086
		private static readonly XsdUnsignedByte datatypeUnsignedByte = new XsdUnsignedByte();

		// Token: 0x04000827 RID: 2087
		private static readonly XsdNonPositiveInteger datatypeNonPositiveInteger = new XsdNonPositiveInteger();

		// Token: 0x04000828 RID: 2088
		private static readonly XsdNegativeInteger datatypeNegativeInteger = new XsdNegativeInteger();

		// Token: 0x04000829 RID: 2089
		private static readonly XsdFloat datatypeFloat = new XsdFloat();

		// Token: 0x0400082A RID: 2090
		private static readonly XsdDouble datatypeDouble = new XsdDouble();

		// Token: 0x0400082B RID: 2091
		private static readonly XsdBase64Binary datatypeBase64Binary = new XsdBase64Binary();

		// Token: 0x0400082C RID: 2092
		private static readonly XsdBoolean datatypeBoolean = new XsdBoolean();

		// Token: 0x0400082D RID: 2093
		private static readonly XsdAnyURI datatypeAnyURI = new XsdAnyURI();

		// Token: 0x0400082E RID: 2094
		private static readonly XsdDuration datatypeDuration = new XsdDuration();

		// Token: 0x0400082F RID: 2095
		private static readonly XsdDateTime datatypeDateTime = new XsdDateTime();

		// Token: 0x04000830 RID: 2096
		private static readonly XsdDate datatypeDate = new XsdDate();

		// Token: 0x04000831 RID: 2097
		private static readonly XsdTime datatypeTime = new XsdTime();

		// Token: 0x04000832 RID: 2098
		private static readonly XsdHexBinary datatypeHexBinary = new XsdHexBinary();

		// Token: 0x04000833 RID: 2099
		private static readonly XsdQName datatypeQName = new XsdQName();

		// Token: 0x04000834 RID: 2100
		private static readonly XsdGYearMonth datatypeGYearMonth = new XsdGYearMonth();

		// Token: 0x04000835 RID: 2101
		private static readonly XsdGMonthDay datatypeGMonthDay = new XsdGMonthDay();

		// Token: 0x04000836 RID: 2102
		private static readonly XsdGYear datatypeGYear = new XsdGYear();

		// Token: 0x04000837 RID: 2103
		private static readonly XsdGMonth datatypeGMonth = new XsdGMonth();

		// Token: 0x04000838 RID: 2104
		private static readonly XsdGDay datatypeGDay = new XsdGDay();

		// Token: 0x04000839 RID: 2105
		private static readonly XdtAnyAtomicType datatypeAnyAtomicType = new XdtAnyAtomicType();

		// Token: 0x0400083A RID: 2106
		private static readonly XdtUntypedAtomic datatypeUntypedAtomic = new XdtUntypedAtomic();

		// Token: 0x0400083B RID: 2107
		private static readonly XdtDayTimeDuration datatypeDayTimeDuration = new XdtDayTimeDuration();

		// Token: 0x0400083C RID: 2108
		private static readonly XdtYearMonthDuration datatypeYearMonthDuration = new XdtYearMonthDuration();
	}
}
