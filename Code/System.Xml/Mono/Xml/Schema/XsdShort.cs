using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D1 RID: 465
	internal class XsdShort : XsdInt
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00052270 File Offset: 0x00050470
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Short;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00052274 File Offset: 0x00050474
		public override Type ValueType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00052280 File Offset: 0x00050480
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0005228C File Offset: 0x0005048C
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToInt16(base.Normalize(s));
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000522A0 File Offset: 0x000504A0
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is short) || !(y is short))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((short)x == (short)y)
			{
				return XsdOrdering.Equal;
			}
			if ((short)x < (short)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
