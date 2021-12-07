using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C5 RID: 453
	internal class XsdName : XsdToken
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x00051DA4 File Offset: 0x0004FFA4
		internal XsdName()
		{
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00051DAC File Offset: 0x0004FFAC
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00051DB0 File Offset: 0x0004FFB0
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Name;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00051DB4 File Offset: 0x0004FFB4
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00051DC0 File Offset: 0x0004FFC0
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (!XmlChar.IsName(s))
			{
				throw new ArgumentException("'" + s + "' is an invalid name.");
			}
			return s;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00051DF0 File Offset: 0x0004FFF0
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringValueType(this.ParseValue(s, nameTable, nsmgr) as string);
		}
	}
}
