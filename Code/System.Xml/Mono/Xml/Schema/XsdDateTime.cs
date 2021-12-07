using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E6 RID: 486
	internal class XsdDateTime : XsdAnySimpleType
	{
		// Token: 0x0600132A RID: 4906 RVA: 0x00052D34 File Offset: 0x00050F34
		internal XsdDateTime()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00052D44 File Offset: 0x00050F44
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00052D4C File Offset: 0x00050F4C
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00052D50 File Offset: 0x00050F50
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DateTime;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00052D54 File Offset: 0x00050F54
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00052D60 File Offset: 0x00050F60
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00052D6C File Offset: 0x00050F6C
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToDateTime(base.Normalize(s));
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00052D80 File Offset: 0x00050F80
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

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00052DCC File Offset: 0x00050FCC
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00052DD0 File Offset: 0x00050FD0
		public override bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00052DD4 File Offset: 0x00050FD4
		public override bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00052DD8 File Offset: 0x00050FD8
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Partial;
			}
		}
	}
}
