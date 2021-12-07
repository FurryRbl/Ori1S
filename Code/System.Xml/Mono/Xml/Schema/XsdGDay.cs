using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001ED RID: 493
	internal class XsdGDay : XsdAnySimpleType
	{
		// Token: 0x06001365 RID: 4965 RVA: 0x0005327C File Offset: 0x0005147C
		internal XsdGDay()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0005328C File Offset: 0x0005148C
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00053294 File Offset: 0x00051494
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GDay;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x00053298 File Offset: 0x00051498
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000532A4 File Offset: 0x000514A4
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000532B0 File Offset: 0x000514B0
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return DateTime.ParseExact(base.Normalize(s), "---dd", null);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000532CC File Offset: 0x000514CC
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
