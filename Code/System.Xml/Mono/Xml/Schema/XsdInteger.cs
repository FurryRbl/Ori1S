using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001CE RID: 462
	internal class XsdInteger : XsdDecimal
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00052108 File Offset: 0x00050308
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Integer;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0005210C File Offset: 0x0005030C
		public override Type ValueType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00052118 File Offset: 0x00050318
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00052124 File Offset: 0x00050324
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			decimal num = XmlConvert.ToDecimal(base.Normalize(s));
			if (decimal.Floor(num) != num)
			{
				throw new FormatException("Integer contains point number.");
			}
			return num;
		}
	}
}
