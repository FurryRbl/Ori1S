using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D4 RID: 468
	internal class XsdUnsignedLong : XsdNonNegativeInteger
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000523B4 File Offset: 0x000505B4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedLong;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x000523B8 File Offset: 0x000505B8
		public override Type ValueType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000523C4 File Offset: 0x000505C4
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000523D0 File Offset: 0x000505D0
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToUInt64(base.Normalize(s));
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000523E4 File Offset: 0x000505E4
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is ulong) || !(y is ulong))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((ulong)x == (ulong)y)
			{
				return XsdOrdering.Equal;
			}
			if ((ulong)x < (ulong)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
