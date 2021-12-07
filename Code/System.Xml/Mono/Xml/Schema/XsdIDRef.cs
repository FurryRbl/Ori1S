using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C8 RID: 456
	internal class XsdIDRef : XsdName
	{
		// Token: 0x06001250 RID: 4688 RVA: 0x00051EC0 File Offset: 0x000500C0
		internal XsdIDRef()
		{
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00051EC8 File Offset: 0x000500C8
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.IDREF;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00051ECC File Offset: 0x000500CC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Idref;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00051ED0 File Offset: 0x000500D0
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00051EDC File Offset: 0x000500DC
		public override object ParseValue(string s, XmlNameTable nt, IXmlNamespaceResolver nsmgr)
		{
			if (!XmlChar.IsNCName(s))
			{
				throw new ArgumentException("'" + s + "' is an invalid NCName.");
			}
			return s;
		}
	}
}
