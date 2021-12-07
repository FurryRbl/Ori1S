using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E4 RID: 484
	internal class XdtDayTimeDuration : XsdDuration
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x00052CA4 File Offset: 0x00050EA4
		internal XdtDayTimeDuration()
		{
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00052CAC File Offset: 0x00050EAC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DayTimeDuration;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00052CB0 File Offset: 0x00050EB0
		public override Type ValueType
		{
			get
			{
				return typeof(TimeSpan);
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00052CBC File Offset: 0x00050EBC
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00052CC8 File Offset: 0x00050EC8
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToTimeSpan(base.Normalize(s));
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00052CDC File Offset: 0x00050EDC
		public override bool Bounded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00052CE0 File Offset: 0x00050EE0
		public override bool Finite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00052CE4 File Offset: 0x00050EE4
		public override bool Numeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00052CE8 File Offset: 0x00050EE8
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Partial;
			}
		}
	}
}
