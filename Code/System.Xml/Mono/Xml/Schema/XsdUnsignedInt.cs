using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D5 RID: 469
	internal class XsdUnsignedInt : XsdUnsignedLong
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00052438 File Offset: 0x00050638
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedInt;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0005243C File Offset: 0x0005063C
		public override Type ValueType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00052448 File Offset: 0x00050648
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00052454 File Offset: 0x00050654
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToUInt32(base.Normalize(s));
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00052468 File Offset: 0x00050668
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is uint) || !(y is uint))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((uint)x == (uint)y)
			{
				return XsdOrdering.Equal;
			}
			if ((uint)x < (uint)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
