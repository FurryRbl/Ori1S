using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001EC RID: 492
	internal class XsdGMonth : XsdAnySimpleType
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x000531E0 File Offset: 0x000513E0
		internal XsdGMonth()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x000531F0 File Offset: 0x000513F0
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x000531F8 File Offset: 0x000513F8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonth;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x000531FC File Offset: 0x000513FC
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00053208 File Offset: 0x00051408
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00053214 File Offset: 0x00051414
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return DateTime.ParseExact(base.Normalize(s), "--MM--", null);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00053230 File Offset: 0x00051430
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
