using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E0 RID: 480
	internal class XsdBoolean : XsdAnySimpleType
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x00052A30 File Offset: 0x00050C30
		internal XsdBoolean()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00052A40 File Offset: 0x00050C40
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.booleanAllowedFacets;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00052A48 File Offset: 0x00050C48
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				if (XmlSchemaUtil.StrictMsCompliant)
				{
					return XmlTokenizedType.None;
				}
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00052A58 File Offset: 0x00050C58
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Boolean;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00052A5C File Offset: 0x00050C5C
		public override Type ValueType
		{
			get
			{
				return typeof(bool);
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00052A68 File Offset: 0x00050C68
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00052A74 File Offset: 0x00050C74
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToBoolean(base.Normalize(s));
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00052A88 File Offset: 0x00050C88
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00052A8C File Offset: 0x00050C8C
		public override bool Finite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00052A90 File Offset: 0x00050C90
		public override bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00052A94 File Offset: 0x00050C94
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Total;
			}
		}
	}
}
