using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E9 RID: 489
	internal class XsdGYearMonth : XsdAnySimpleType
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x0005300C File Offset: 0x0005120C
		internal XsdGYearMonth()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0005301C File Offset: 0x0005121C
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00053024 File Offset: 0x00051224
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYearMonth;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00053028 File Offset: 0x00051228
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00053034 File Offset: 0x00051234
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00053040 File Offset: 0x00051240
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return DateTime.ParseExact(base.Normalize(s), "yyyy-MM", null);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0005305C File Offset: 0x0005125C
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
