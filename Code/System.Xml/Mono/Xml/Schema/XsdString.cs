using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001BF RID: 447
	internal class XsdString : XsdAnySimpleType
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00051C24 File Offset: 0x0004FE24
		internal XsdString()
		{
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00051C2C File Offset: 0x0004FE2C
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.stringAllowedFacets;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00051C34 File Offset: 0x0004FE34
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00051C38 File Offset: 0x0004FE38
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.String;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00051C3C File Offset: 0x0004FE3C
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00051C48 File Offset: 0x0004FE48
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00051C4C File Offset: 0x0004FE4C
		public override bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00051C50 File Offset: 0x0004FE50
		public override bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00051C54 File Offset: 0x0004FE54
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.False;
			}
		}
	}
}
