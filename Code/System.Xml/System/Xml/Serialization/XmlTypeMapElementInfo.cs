using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002BB RID: 699
	internal class XmlTypeMapElementInfo
	{
		// Token: 0x06001D63 RID: 7523 RVA: 0x0009B6B8 File Offset: 0x000998B8
		public XmlTypeMapElementInfo(XmlTypeMapMember member, TypeData type)
		{
			this._member = member;
			this._type = type;
			if (type.IsValueType && type.IsNullable)
			{
				this._isNullable = true;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0009B708 File Offset: 0x00099908
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0009B710 File Offset: 0x00099910
		public TypeData TypeData
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0009B71C File Offset: 0x0009991C
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x0009B724 File Offset: 0x00099924
		public object ChoiceValue
		{
			get
			{
				return this._choiceValue;
			}
			set
			{
				this._choiceValue = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0009B730 File Offset: 0x00099930
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x0009B738 File Offset: 0x00099938
		public string ElementName
		{
			get
			{
				return this._elementName;
			}
			set
			{
				this._elementName = value;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0009B744 File Offset: 0x00099944
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x0009B74C File Offset: 0x0009994C
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0009B758 File Offset: 0x00099958
		public string DataTypeNamespace
		{
			get
			{
				if (this._mappedType == null)
				{
					return "http://www.w3.org/2001/XMLSchema";
				}
				return this._mappedType.XmlTypeNamespace;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0009B778 File Offset: 0x00099978
		public string DataTypeName
		{
			get
			{
				if (this._mappedType == null)
				{
					return this.TypeData.XmlType;
				}
				return this._mappedType.XmlType;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0009B7A8 File Offset: 0x000999A8
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x0009B7B0 File Offset: 0x000999B0
		public XmlSchemaForm Form
		{
			get
			{
				return this._form;
			}
			set
			{
				this._form = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0009B7BC File Offset: 0x000999BC
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0009B7C4 File Offset: 0x000999C4
		public XmlTypeMapping MappedType
		{
			get
			{
				return this._mappedType;
			}
			set
			{
				this._mappedType = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0009B7D0 File Offset: 0x000999D0
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x0009B7D8 File Offset: 0x000999D8
		public bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0009B7E4 File Offset: 0x000999E4
		internal bool IsPrimitive
		{
			get
			{
				return this._mappedType == null;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0009B7F0 File Offset: 0x000999F0
		// (set) Token: 0x06001D76 RID: 7542 RVA: 0x0009B7F8 File Offset: 0x000999F8
		public XmlTypeMapMember Member
		{
			get
			{
				return this._member;
			}
			set
			{
				this._member = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x0009B804 File Offset: 0x00099A04
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x0009B80C File Offset: 0x00099A0C
		public int NestingLevel
		{
			get
			{
				return this._nestingLevel;
			}
			set
			{
				this._nestingLevel = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0009B818 File Offset: 0x00099A18
		public bool MultiReferenceType
		{
			get
			{
				return this._mappedType != null && this._mappedType.MultiReferenceType;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0009B834 File Offset: 0x00099A34
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0009B83C File Offset: 0x00099A3C
		public bool WrappedElement
		{
			get
			{
				return this._wrappedElement;
			}
			set
			{
				this._wrappedElement = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0009B848 File Offset: 0x00099A48
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x0009B85C File Offset: 0x00099A5C
		public bool IsTextElement
		{
			get
			{
				return this.ElementName == "<text>";
			}
			set
			{
				if (!value)
				{
					throw new Exception("INTERNAL ERROR; someone wrote unexpected code in sys.xml");
				}
				this.ElementName = "<text>";
				this.Namespace = string.Empty;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0009B888 File Offset: 0x00099A88
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x0009B89C File Offset: 0x00099A9C
		public bool IsUnnamedAnyElement
		{
			get
			{
				return this.ElementName == string.Empty;
			}
			set
			{
				if (!value)
				{
					throw new Exception("INTERNAL ERROR; someone wrote unexpected code in sys.xml");
				}
				this.ElementName = string.Empty;
				this.Namespace = string.Empty;
			}
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0009B8C8 File Offset: 0x00099AC8
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)other;
			return !(this._elementName != xmlTypeMapElementInfo._elementName) && !(this._type.XmlType != xmlTypeMapElementInfo._type.XmlType) && !(this._namespace != xmlTypeMapElementInfo._namespace) && this._form == xmlTypeMapElementInfo._form && this._type == xmlTypeMapElementInfo._type && this._isNullable == xmlTypeMapElementInfo._isNullable && (this._choiceValue == null || this._choiceValue.Equals(xmlTypeMapElementInfo._choiceValue)) && this._choiceValue == xmlTypeMapElementInfo._choiceValue;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0009B9A8 File Offset: 0x00099BA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000BA8 RID: 2984
		private string _elementName;

		// Token: 0x04000BA9 RID: 2985
		private string _namespace = string.Empty;

		// Token: 0x04000BAA RID: 2986
		private XmlSchemaForm _form;

		// Token: 0x04000BAB RID: 2987
		private XmlTypeMapMember _member;

		// Token: 0x04000BAC RID: 2988
		private object _choiceValue;

		// Token: 0x04000BAD RID: 2989
		private bool _isNullable;

		// Token: 0x04000BAE RID: 2990
		private int _nestingLevel;

		// Token: 0x04000BAF RID: 2991
		private XmlTypeMapping _mappedType;

		// Token: 0x04000BB0 RID: 2992
		private TypeData _type;

		// Token: 0x04000BB1 RID: 2993
		private bool _wrappedElement = true;
	}
}
