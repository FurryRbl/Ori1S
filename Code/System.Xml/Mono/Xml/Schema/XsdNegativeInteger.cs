using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001DA RID: 474
	internal class XsdNegativeInteger : XsdNonPositiveInteger
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00052634 File Offset: 0x00050834
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NegativeInteger;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00052638 File Offset: 0x00050838
		public override Type ValueType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00052644 File Offset: 0x00050844
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00052650 File Offset: 0x00050850
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToDecimal(base.Normalize(s));
		}
	}
}
