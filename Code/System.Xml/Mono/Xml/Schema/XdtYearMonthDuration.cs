using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E5 RID: 485
	internal class XdtYearMonthDuration : XsdDuration
	{
		// Token: 0x06001321 RID: 4897 RVA: 0x00052CEC File Offset: 0x00050EEC
		internal XdtYearMonthDuration()
		{
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00052CF4 File Offset: 0x00050EF4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.YearMonthDuration;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00052CF8 File Offset: 0x00050EF8
		public override Type ValueType
		{
			get
			{
				return typeof(TimeSpan);
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00052D04 File Offset: 0x00050F04
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00052D10 File Offset: 0x00050F10
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToTimeSpan(base.Normalize(s));
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00052D24 File Offset: 0x00050F24
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00052D28 File Offset: 0x00050F28
		public override bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00052D2C File Offset: 0x00050F2C
		public override bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00052D30 File Offset: 0x00050F30
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Partial;
			}
		}
	}
}
