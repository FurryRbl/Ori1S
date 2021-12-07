using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D3 RID: 467
	internal class XsdNonNegativeInteger : XsdInteger
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0005237C File Offset: 0x0005057C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonNegativeInteger;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00052380 File Offset: 0x00050580
		public override Type ValueType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0005238C File Offset: 0x0005058C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00052398 File Offset: 0x00050598
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToDecimal(base.Normalize(s));
		}
	}
}
