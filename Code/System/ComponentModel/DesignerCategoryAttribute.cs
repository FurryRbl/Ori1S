using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the designer for a class belongs to a certain category.</summary>
	// Token: 0x02000106 RID: 262
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with an empty string ("").</summary>
		// Token: 0x06000A8D RID: 2701 RVA: 0x0001D774 File Offset: 0x0001B974
		public DesignerCategoryAttribute()
		{
			this.category = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with the given category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x06000A8E RID: 2702 RVA: 0x0001D788 File Offset: 0x0001B988
		public DesignerCategoryAttribute(string category)
		{
			this.category = category;
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001D7E4 File Offset: 0x0001B9E4
		public override object TypeId
		{
			get
			{
				return base.GetType();
			}
		}

		/// <summary>Gets the name of the category.</summary>
		/// <returns>The name of the category.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000A92 RID: 2706 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		public override bool Equals(object obj)
		{
			return obj is DesignerCategoryAttribute && (obj == this || ((DesignerCategoryAttribute)obj).Category == this.category);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A93 RID: 2707 RVA: 0x0001D830 File Offset: 0x0001BA30
		public override int GetHashCode()
		{
			return this.category.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000A94 RID: 2708 RVA: 0x0001D840 File Offset: 0x0001BA40
		public override bool IsDefaultAttribute()
		{
			return this.category == DesignerCategoryAttribute.Default.Category;
		}

		// Token: 0x040002CB RID: 715
		private string category;

		/// <summary>Specifies that a component marked with this category use a component designer. This field is read-only.</summary>
		// Token: 0x040002CC RID: 716
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		/// <summary>Specifies that a component marked with this category use a form designer. This static field is read-only.</summary>
		// Token: 0x040002CD RID: 717
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		/// <summary>Specifies that a component marked with this category use a generic designer. This static field is read-only.</summary>
		// Token: 0x040002CE RID: 718
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");

		/// <summary>Specifies that a component marked with this category cannot use a visual designer. This static field is read-only.</summary>
		// Token: 0x040002CF RID: 719
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute(string.Empty);
	}
}
