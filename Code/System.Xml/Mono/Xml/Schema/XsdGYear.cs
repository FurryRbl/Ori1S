using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001EB RID: 491
	internal class XsdGYear : XsdAnySimpleType
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x00053144 File Offset: 0x00051344
		internal XsdGYear()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x00053154 File Offset: 0x00051354
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0005315C File Offset: 0x0005135C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYear;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x00053160 File Offset: 0x00051360
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0005316C File Offset: 0x0005136C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00053178 File Offset: 0x00051378
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return DateTime.ParseExact(base.Normalize(s), "yyyy", null);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00053194 File Offset: 0x00051394
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
