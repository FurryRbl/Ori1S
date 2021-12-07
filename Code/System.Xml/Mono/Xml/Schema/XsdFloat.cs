using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001DB RID: 475
	internal class XsdFloat : XsdAnySimpleType
	{
		// Token: 0x060012C6 RID: 4806 RVA: 0x00052664 File Offset: 0x00050864
		internal XsdFloat()
		{
			this.WhitespaceValue = XsdWhitespaceFacet.Collapse;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00052674 File Offset: 0x00050874
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Float;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00052678 File Offset: 0x00050878
		internal override XmlSchemaFacet.Facet AllowedFacets
		{
			get
			{
				return XsdAnySimpleType.durationAllowedFacets;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00052680 File Offset: 0x00050880
		public override bool Bounded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00052684 File Offset: 0x00050884
		public override bool Finite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00052688 File Offset: 0x00050888
		public override bool Numeric
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0005268C File Offset: 0x0005088C
		public override XsdOrderedFacet Ordered
		{
			get
			{
				return XsdOrderedFacet.Total;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00052690 File Offset: 0x00050890
		public override Type ValueType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0005269C File Offset: 0x0005089C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000526A8 File Offset: 0x000508A8
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToSingle(base.Normalize(s));
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000526BC File Offset: 0x000508BC
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is float) || !(y is float))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((float)x == (float)y)
			{
				return XsdOrdering.Equal;
			}
			if ((float)x < (float)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
