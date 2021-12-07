using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E3 RID: 483
	internal class XsdDuration : XsdAnySimpleType
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x00052BFC File Offset: 0x00050DFC
		internal XsdDuration()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00052C0C File Offset: 0x00050E0C
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00052C14 File Offset: 0x00050E14
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00052C18 File Offset: 0x00050E18
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Duration;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00052C1C File Offset: 0x00050E1C
		public override Type ValueType
		{
			get
			{
				return typeof(TimeSpan);
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00052C28 File Offset: 0x00050E28
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00052C34 File Offset: 0x00050E34
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToTimeSpan(base.Normalize(s));
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00052C48 File Offset: 0x00050E48
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is TimeSpan) || !(y is TimeSpan))
			{
				return XsdOrdering.Indeterminate;
			}
			int num = TimeSpan.Compare((TimeSpan)x, (TimeSpan)y);
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00052C94 File Offset: 0x00050E94
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00052C98 File Offset: 0x00050E98
		public override bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00052C9C File Offset: 0x00050E9C
		public override bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00052CA0 File Offset: 0x00050EA0
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Partial;
			}
		}
	}
}
