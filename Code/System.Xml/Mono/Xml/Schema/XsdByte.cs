using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D2 RID: 466
	internal class XsdByte : XsdShort
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000522F4 File Offset: 0x000504F4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Byte;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x000522F8 File Offset: 0x000504F8
		public override Type ValueType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00052304 File Offset: 0x00050504
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00052310 File Offset: 0x00050510
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToSByte(base.Normalize(s));
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00052324 File Offset: 0x00050524
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is sbyte) || !(y is sbyte))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((int)((sbyte)x) == (int)((sbyte)y))
			{
				return XsdOrdering.Equal;
			}
			if ((int)((sbyte)x) < (int)((sbyte)y))
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
