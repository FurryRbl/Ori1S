using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001CD RID: 461
	internal class XsdDecimal : XsdAnySimpleType
	{
		// Token: 0x06001271 RID: 4721 RVA: 0x00052058 File Offset: 0x00050258
		internal XsdDecimal()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00052068 File Offset: 0x00050268
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.decimalAllowedFacets;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00052070 File Offset: 0x00050270
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00052074 File Offset: 0x00050274
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Decimal;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00052078 File Offset: 0x00050278
		public override Type ValueType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00052084 File Offset: 0x00050284
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00052090 File Offset: 0x00050290
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToDecimal(base.Normalize(s));
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000520A4 File Offset: 0x000502A4
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is decimal) || !(y is decimal))
			{
				return XsdOrdering.Indeterminate;
			}
			int num = decimal.Compare((decimal)x, (decimal)y);
			if (num < 0)
			{
				return XsdOrdering.LessThan;
			}
			if (num > 0)
			{
				return XsdOrdering.GreaterThan;
			}
			return XsdOrdering.Equal;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x000520F0 File Offset: 0x000502F0
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x000520F4 File Offset: 0x000502F4
		public override bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x000520F8 File Offset: 0x000502F8
		public override bool Numeric
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000520FC File Offset: 0x000502FC
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Total;
			}
		}
	}
}
