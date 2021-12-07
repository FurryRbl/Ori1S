using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for all facets that are used when simple types are derived by restriction.</summary>
	// Token: 0x02000214 RID: 532
	public abstract class XmlSchemaFacet : XmlSchemaAnnotated
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000608DC File Offset: 0x0005EADC
		internal virtual XmlSchemaFacet.Facet ThisFacet
		{
			get
			{
				return XmlSchemaFacet.Facet.None;
			}
		}

		/// <summary>Gets or sets the value attribute of the facet.</summary>
		/// <returns>The value attribute.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000608E0 File Offset: 0x0005EAE0
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x000608E8 File Offset: 0x0005EAE8
		[XmlAttribute("value")]
		public string Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		/// <summary>Gets or sets information that indicates that this facet is fixed.</summary>
		/// <returns>If true, value is fixed; otherwise, false. The default is false.Optional.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000608F4 File Offset: 0x0005EAF4
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x000608FC File Offset: 0x0005EAFC
		[DefaultValue(false)]
		[XmlAttribute("fixed")]
		public virtual bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				this.isFixed = value;
			}
		}

		// Token: 0x04000876 RID: 2166
		internal static readonly XmlSchemaFacet.Facet AllFacets = XmlSchemaFacet.Facet.length | XmlSchemaFacet.Facet.minLength | XmlSchemaFacet.Facet.maxLength | XmlSchemaFacet.Facet.pattern | XmlSchemaFacet.Facet.enumeration | XmlSchemaFacet.Facet.whiteSpace | XmlSchemaFacet.Facet.maxInclusive | XmlSchemaFacet.Facet.maxExclusive | XmlSchemaFacet.Facet.minExclusive | XmlSchemaFacet.Facet.minInclusive | XmlSchemaFacet.Facet.totalDigits | XmlSchemaFacet.Facet.fractionDigits;

		// Token: 0x04000877 RID: 2167
		private bool isFixed;

		// Token: 0x04000878 RID: 2168
		private string val;

		// Token: 0x02000215 RID: 533
		[Flags]
		protected internal enum Facet
		{
			// Token: 0x0400087A RID: 2170
			None = 0,
			// Token: 0x0400087B RID: 2171
			length = 1,
			// Token: 0x0400087C RID: 2172
			minLength = 2,
			// Token: 0x0400087D RID: 2173
			maxLength = 4,
			// Token: 0x0400087E RID: 2174
			pattern = 8,
			// Token: 0x0400087F RID: 2175
			enumeration = 16,
			// Token: 0x04000880 RID: 2176
			whiteSpace = 32,
			// Token: 0x04000881 RID: 2177
			maxInclusive = 64,
			// Token: 0x04000882 RID: 2178
			maxExclusive = 128,
			// Token: 0x04000883 RID: 2179
			minExclusive = 256,
			// Token: 0x04000884 RID: 2180
			minInclusive = 512,
			// Token: 0x04000885 RID: 2181
			totalDigits = 1024,
			// Token: 0x04000886 RID: 2182
			fractionDigits = 2048
		}
	}
}
