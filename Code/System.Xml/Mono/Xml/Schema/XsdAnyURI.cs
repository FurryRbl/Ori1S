using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E1 RID: 481
	internal class XsdAnyURI : XsdString
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00052AA0 File Offset: 0x00050CA0
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00052AA4 File Offset: 0x00050CA4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyUri;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00052AA8 File Offset: 0x00050CA8
		public override Type ValueType
		{
			get
			{
				return typeof(Uri);
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00052AB4 File Offset: 0x00050CB4
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new XmlSchemaUri(base.Normalize(s));
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00052AC4 File Offset: 0x00050CC4
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new UriValueType((XmlSchemaUri)this.ParseValue(s, nameTable, nsmgr));
		}
	}
}
