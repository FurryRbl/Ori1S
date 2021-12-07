using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001CF RID: 463
	internal class XsdLong : XsdInteger
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00052168 File Offset: 0x00050368
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Long;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0005216C File Offset: 0x0005036C
		public override Type ValueType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00052178 File Offset: 0x00050378
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00052184 File Offset: 0x00050384
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToInt64(base.Normalize(s));
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00052198 File Offset: 0x00050398
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is long) || !(y is long))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((long)x == (long)y)
			{
				return XsdOrdering.Equal;
			}
			if ((long)x < (long)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
