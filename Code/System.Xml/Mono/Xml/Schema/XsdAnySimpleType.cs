using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001BC RID: 444
	internal class XsdAnySimpleType : XmlSchemaDatatype
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x00051B14 File Offset: 0x0004FD14
		protected XsdAnySimpleType()
		{
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00051B1C File Offset: 0x0004FD1C
		static XsdAnySimpleType()
		{
			XsdAnySimpleType.instance = new XsdAnySimpleType();
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00051B68 File Offset: 0x0004FD68
		public static XsdAnySimpleType Instance
		{
			get
			{
				return XsdAnySimpleType.instance;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00051B70 File Offset: 0x0004FD70
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00051B74 File Offset: 0x0004FD74
		public virtual bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00051B78 File Offset: 0x0004FD78
		public virtual bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00051B7C File Offset: 0x0004FD7C
		public virtual bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00051B80 File Offset: 0x0004FD80
		public virtual XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.False;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00051B84 File Offset: 0x0004FD84
		public override Type ValueType
		{
			get
			{
				if (XmlSchemaUtil.StrictMsCompliant)
				{
					return typeof(string);
				}
				return typeof(object);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00051BA8 File Offset: 0x0004FDA8
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00051BAC File Offset: 0x0004FDAC
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return base.Normalize(s);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00051BB8 File Offset: 0x0004FDB8
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringValueType(base.Normalize(s));
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00051BCC File Offset: 0x0004FDCC
		internal string[] ParseListValue(string s, XmlNameTable nameTable)
		{
			return base.Normalize(s, XsdWhitespaceFacet.Collapse).Split(XsdAnySimpleType.whitespaceArray);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00051BE0 File Offset: 0x0004FDE0
		internal bool AllowsFacet(XmlSchemaFacet xsf)
		{
			return (this.AllowedFacets & xsf.ThisFacet) != XmlSchemaFacet.Facet.None;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00051BF8 File Offset: 0x0004FDF8
		internal virtual XsdOrdering Compare(object x, object y)
		{
			return XsdOrdering.Indeterminate;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00051BFC File Offset: 0x0004FDFC
		internal virtual int Length(string s)
		{
			return s.Length;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00051C04 File Offset: 0x0004FE04
		internal virtual XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XmlSchemaFacet.AllFacets;
			}
		}

		// Token: 0x04000770 RID: 1904
		private static XsdAnySimpleType instance;

		// Token: 0x04000771 RID: 1905
		private static readonly char[] whitespaceArray = new char[]
		{
			' '
		};

		// Token: 0x04000772 RID: 1906
		internal static readonly XmlSchemaFacet.Facet booleanAllowedFacets = XmlSchemaFacet.Facet.pattern | XmlSchemaFacet.Facet.whiteSpace;

		// Token: 0x04000773 RID: 1907
		internal static readonly XmlSchemaFacet.Facet decimalAllowedFacets = XmlSchemaFacet.Facet.pattern | XmlSchemaFacet.Facet.enumeration | XmlSchemaFacet.Facet.whiteSpace | XmlSchemaFacet.Facet.maxInclusive | XmlSchemaFacet.Facet.maxExclusive | XmlSchemaFacet.Facet.minExclusive | XmlSchemaFacet.Facet.minInclusive | XmlSchemaFacet.Facet.totalDigits | XmlSchemaFacet.Facet.fractionDigits;

		// Token: 0x04000774 RID: 1908
		internal static readonly XmlSchemaFacet.Facet durationAllowedFacets = XmlSchemaFacet.Facet.pattern | XmlSchemaFacet.Facet.enumeration | XmlSchemaFacet.Facet.whiteSpace | XmlSchemaFacet.Facet.maxInclusive | XmlSchemaFacet.Facet.maxExclusive | XmlSchemaFacet.Facet.minExclusive | XmlSchemaFacet.Facet.minInclusive;

		// Token: 0x04000775 RID: 1909
		internal static readonly XmlSchemaFacet.Facet stringAllowedFacets = XmlSchemaFacet.Facet.length | XmlSchemaFacet.Facet.minLength | XmlSchemaFacet.Facet.maxLength | XmlSchemaFacet.Facet.pattern | XmlSchemaFacet.Facet.enumeration | XmlSchemaFacet.Facet.whiteSpace;
	}
}
