using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D9 RID: 473
	internal class XsdNonPositiveInteger : XsdInteger
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x000525FC File Offset: 0x000507FC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonPositiveInteger;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00052600 File Offset: 0x00050800
		public override Type ValueType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0005260C File Offset: 0x0005080C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00052618 File Offset: 0x00050818
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToDecimal(base.Normalize(s));
		}
	}
}
