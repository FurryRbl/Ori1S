using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001EA RID: 490
	internal class XsdGMonthDay : XsdAnySimpleType
	{
		// Token: 0x06001350 RID: 4944 RVA: 0x000530A8 File Offset: 0x000512A8
		internal XsdGMonthDay()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x000530B8 File Offset: 0x000512B8
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000530C0 File Offset: 0x000512C0
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonthDay;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x000530C4 File Offset: 0x000512C4
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000530D0 File Offset: 0x000512D0
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000530DC File Offset: 0x000512DC
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return DateTime.ParseExact(base.Normalize(s), "--MM-dd", null);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000530F8 File Offset: 0x000512F8
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is DateTime) || !(y is DateTime))
			{
				return XsdOrdering.Indeterminate;
			}
			int num = DateTime.Compare((DateTime)x, (DateTime)y);
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
	}
}
