using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C7 RID: 455
	internal class XsdID : XsdName
	{
		// Token: 0x0600124B RID: 4683 RVA: 0x00051E74 File Offset: 0x00050074
		internal XsdID()
		{
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00051E7C File Offset: 0x0005007C
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ID;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00051E80 File Offset: 0x00050080
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Id;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00051E84 File Offset: 0x00050084
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00051E90 File Offset: 0x00050090
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
