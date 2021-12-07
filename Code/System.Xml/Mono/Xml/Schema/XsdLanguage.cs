using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C2 RID: 450
	internal class XsdLanguage : XsdToken
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x00051CA0 File Offset: 0x0004FEA0
		internal XsdLanguage()
		{
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00051CAC File Offset: 0x0004FEAC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Language;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00051CB0 File Offset: 0x0004FEB0
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}
	}
}
