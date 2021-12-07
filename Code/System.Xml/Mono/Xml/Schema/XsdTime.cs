using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E8 RID: 488
	internal class XsdTime : XsdAnySimpleType
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x00052E80 File Offset: 0x00051080
		internal XsdTime()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00052F78 File Offset: 0x00051178
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00052F80 File Offset: 0x00051180
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00052F84 File Offset: 0x00051184
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Time;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00052F88 File Offset: 0x00051188
		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00052F94 File Offset: 0x00051194
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00052FA0 File Offset: 0x000511A0
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return DateTime.ParseExact(base.Normalize(s), XsdTime.timeFormats, null, DateTimeStyles.None);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00052FBC File Offset: 0x000511BC
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is DateTime) || !(y is DateTime))
			{
				return XsdOrdering.Indeterminate;
			}
			int num = DateTime.Compare((DateTime)x, (DateTime)y);
			if (num < 0)
			{
				return XsdOrdering.LessThan;
			}
			if (num > 0)
			{
				return XsdOrdering.GreaterThan;
			}
			return XsdOrdering.Equal;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00053008 File Offset: 0x00051208
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Partial;
			}
		}

		// Token: 0x04000779 RID: 1913
		private static string[] timeFormats = new string[]
		{
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:sszzz",
			"HH:mm:ss.fzzz",
			"HH:mm:ss.ffzzz",
			"HH:mm:ss.fffzzz",
			"HH:mm:ss.ffffzzz",
			"HH:mm:ss.fffffzzz",
			"HH:mm:ss.ffffffzzz",
			"HH:mm:ss.fffffffzzz",
			"HH:mm:ssZ",
			"HH:mm:ss.fZ",
			"HH:mm:ss.ffZ",
			"HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ",
			"HH:mm:ss.fffffZ",
			"HH:mm:ss.ffffffZ",
			"HH:mm:ss.fffffffZ"
		};
	}
}
