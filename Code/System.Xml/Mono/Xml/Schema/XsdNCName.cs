using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C6 RID: 454
	internal class XsdNCName : XsdName
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x00051E0C File Offset: 0x0005000C
		internal XsdNCName()
		{
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00051E14 File Offset: 0x00050014
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NCName;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00051E18 File Offset: 0x00050018
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NCName;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00051E1C File Offset: 0x0005001C
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00051E28 File Offset: 0x00050028
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (!XmlChar.IsNCName(s))
			{
				throw new ArgumentException("'" + s + "' is an invalid NCName.");
			}
			return s;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00051E58 File Offset: 0x00050058
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringValueType(this.ParseValue(s, nameTable, nsmgr) as string);
		}
	}
}
