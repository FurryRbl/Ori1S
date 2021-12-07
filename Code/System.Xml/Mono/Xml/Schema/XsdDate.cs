using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E7 RID: 487
	internal class XsdDate : XsdAnySimpleType
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x00052DDC File Offset: 0x00050FDC
		internal XsdDate()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x00052DEC File Offset: 0x00050FEC
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00052DF4 File Offset: 0x00050FF4
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00052DF8 File Offset: 0x00050FF8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Date;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00052DFC File Offset: 0x00050FFC
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00052E08 File Offset: 0x00051008
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00052E14 File Offset: 0x00051014
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return DateTime.ParseExact(base.Normalize(s), "yyyy-MM-dd", null);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00052E30 File Offset: 0x00051030
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00052E7C File Offset: 0x0005107C
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Partial;
			}
		}
	}
}
