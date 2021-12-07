using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001DC RID: 476
	internal class XsdDouble : XsdAnySimpleType
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x00052708 File Offset: 0x00050908
		internal XsdDouble()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00052718 File Offset: 0x00050918
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00052720 File Offset: 0x00050920
		public override bool Bounded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00052724 File Offset: 0x00050924
		public override bool Finite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00052728 File Offset: 0x00050928
		public override bool Numeric
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0005272C File Offset: 0x0005092C
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Total;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00052730 File Offset: 0x00050930
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Double;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00052734 File Offset: 0x00050934
		public override Type ValueType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00052740 File Offset: 0x00050940
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0005274C File Offset: 0x0005094C
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToDouble(base.Normalize(s));
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00052760 File Offset: 0x00050960
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is double) || !(y is double))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((double)x == (double)y)
			{
				return XsdOrdering.Equal;
			}
			if ((double)x < (double)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
