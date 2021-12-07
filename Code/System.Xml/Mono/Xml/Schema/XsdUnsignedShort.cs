using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D6 RID: 470
	internal class XsdUnsignedShort : XsdUnsignedInt
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x000524BC File Offset: 0x000506BC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedShort;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x000524C0 File Offset: 0x000506C0
		public override Type ValueType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000524CC File Offset: 0x000506CC
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000524D8 File Offset: 0x000506D8
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToUInt16(base.Normalize(s));
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000524EC File Offset: 0x000506EC
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is ushort) || !(y is ushort))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((ushort)x == (ushort)y)
			{
				return XsdOrdering.Equal;
			}
			if ((ushort)x < (ushort)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
