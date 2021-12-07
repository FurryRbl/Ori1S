using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C1 RID: 449
	internal class XsdToken : XsdNormalizedString
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x00051C7C File Offset: 0x0004FE7C
		internal XsdToken()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00051C8C File Offset: 0x0004FE8C
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00051C90 File Offset: 0x0004FE90
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00051C94 File Offset: 0x0004FE94
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}
	}
}
