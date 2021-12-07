using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D0 RID: 464
	internal class XsdInt : XsdLong
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000521EC File Offset: 0x000503EC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Int;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x000521F0 File Offset: 0x000503F0
		public override Type ValueType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000521FC File Offset: 0x000503FC
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00052208 File Offset: 0x00050408
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToInt32(base.Normalize(s));
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0005221C File Offset: 0x0005041C
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is int) || !(y is int))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((int)x == (int)y)
			{
				return XsdOrdering.Equal;
			}
			if ((int)x < (int)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
