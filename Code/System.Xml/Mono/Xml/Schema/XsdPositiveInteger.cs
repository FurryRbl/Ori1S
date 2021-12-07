using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D8 RID: 472
	internal class XsdPositiveInteger : XsdNonNegativeInteger
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000525C4 File Offset: 0x000507C4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.PositiveInteger;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000525C8 File Offset: 0x000507C8
		public override Type ValueType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x000525D4 File Offset: 0x000507D4
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000525E0 File Offset: 0x000507E0
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToDecimal(base.Normalize(s));
		}
	}
}
