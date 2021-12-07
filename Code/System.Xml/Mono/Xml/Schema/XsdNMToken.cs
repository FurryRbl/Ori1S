using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C3 RID: 451
	internal class XsdNMToken : XsdToken
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x00051CBC File Offset: 0x0004FEBC
		internal XsdNMToken()
		{
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00051CC4 File Offset: 0x0004FEC4
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NMTOKEN;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00051CC8 File Offset: 0x0004FEC8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NmToken;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00051CCC File Offset: 0x0004FECC
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00051CD8 File Offset: 0x0004FED8
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (!XmlChar.IsNmToken(s))
			{
				throw new ArgumentException("'" + s + "' is an invalid NMTOKEN.");
			}
			return s;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00051D08 File Offset: 0x0004FF08
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringValueType(this.ParseValue(s, nameTable, nsmgr) as string);
		}
	}
}
