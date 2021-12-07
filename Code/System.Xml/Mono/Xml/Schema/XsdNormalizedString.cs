using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C0 RID: 448
	internal class XsdNormalizedString : XsdString
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x00051C58 File Offset: 0x0004FE58
		internal XsdNormalizedString()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Replace;
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00051C68 File Offset: 0x0004FE68
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00051C6C File Offset: 0x0004FE6C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00051C70 File Offset: 0x0004FE70
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}
	}
}
