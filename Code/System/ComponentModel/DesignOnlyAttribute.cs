using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property can only be set at design time.</summary>
	// Token: 0x02000125 RID: 293
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignOnlyAttribute" /> class.</summary>
		/// <param name="isDesignOnly">true if a property can be set only at design time; false if the property can be set at design time and at run time. </param>
		// Token: 0x06000B3F RID: 2879 RVA: 0x0001DB90 File Offset: 0x0001BD90
		public DesignOnlyAttribute(bool design_only)
		{
			this.design_only = design_only;
		}

		/// <summary>Gets a value indicating whether a property can be set only at design time.</summary>
		/// <returns>true if a property can be set only at design time; otherwise, false.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		public bool IsDesignOnly
		{
			get
			{
				return this.design_only;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000B42 RID: 2882 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public override bool Equals(object obj)
		{
			return obj is DesignOnlyAttribute && (obj == this || ((DesignOnlyAttribute)obj).IsDesignOnly == this.design_only);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public override int GetHashCode()
		{
			return this.design_only.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000B44 RID: 2884 RVA: 0x0001DC08 File Offset: 0x0001BE08
		public override bool IsDefaultAttribute()
		{
			return this.design_only == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		// Token: 0x040002EC RID: 748
		private bool design_only;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />, which is <see cref="F:System.ComponentModel.DesignOnlyAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x040002ED RID: 749
		public static readonly DesignOnlyAttribute Default = new DesignOnlyAttribute(false);

		/// <summary>Specifies that a property can be set at design time or at run time. This static field is read-only.</summary>
		// Token: 0x040002EE RID: 750
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		/// <summary>Specifies that a property can be set only at design time. This static field is read-only.</summary>
		// Token: 0x040002EF RID: 751
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);
	}
}
