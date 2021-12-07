using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001CC RID: 460
	internal class XsdNotation : XsdAnySimpleType
	{
		// Token: 0x06001267 RID: 4711 RVA: 0x00052018 File Offset: 0x00050218
		internal XsdNotation()
		{
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00052020 File Offset: 0x00050220
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.stringAllowedFacets;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00052028 File Offset: 0x00050228
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NOTATION;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0005202C File Offset: 0x0005022C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Notation;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00052030 File Offset: 0x00050230
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0005203C File Offset: 0x0005023C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return base.Normalize(s);
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00052048 File Offset: 0x00050248
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0005204C File Offset: 0x0005024C
		public override bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00052050 File Offset: 0x00050250
		public override bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00052054 File Offset: 0x00050254
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.False;
			}
		}
	}
}
