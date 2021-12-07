using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001DE RID: 478
	internal class XsdHexBinary : XsdAnySimpleType
	{
		// Token: 0x060012E5 RID: 4837 RVA: 0x00052938 File Offset: 0x00050B38
		internal XsdHexBinary()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00052948 File Offset: 0x00050B48
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.stringAllowedFacets;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00052950 File Offset: 0x00050B50
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.HexBinary;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00052954 File Offset: 0x00050B54
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00052958 File Offset: 0x00050B58
		public override Type ValueType
		{
			get
			{
				return typeof(byte[]);
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00052964 File Offset: 0x00050B64
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.FromBinHexString(base.Normalize(s));
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00052974 File Offset: 0x00050B74
		internal override int Length(string s)
		{
			return s.Length / 2 + s.Length % 2;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00052988 File Offset: 0x00050B88
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringValueType(this.ParseValue(s, nameTable, nsmgr) as string);
		}
	}
}
