using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the filter string and filter type to use for a toolbox item.</summary>
	// Token: 0x020001AC RID: 428
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		// Token: 0x06000EE9 RID: 3817 RVA: 0x00026A3C File Offset: 0x00024C3C
		public ToolboxItemFilterAttribute(string filterString)
		{
			this.Filter = filterString;
			this.ItemFilterType = ToolboxItemFilterType.Allow;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string and type.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		/// <param name="filterType">A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> indicating the type of the filter. </param>
		// Token: 0x06000EEA RID: 3818 RVA: 0x00026A54 File Offset: 0x00024C54
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			this.Filter = filterString;
			this.ItemFilterType = filterType;
		}

		/// <summary>Gets the filter string for the toolbox item.</summary>
		/// <returns>The filter string for the toolbox item.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00026A6C File Offset: 0x00024C6C
		public string FilterString
		{
			get
			{
				return this.Filter;
			}
		}

		/// <summary>Gets the type of the filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> that indicates the type of the filter.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00026A74 File Offset: 0x00024C74
		public ToolboxItemFilterType FilterType
		{
			get
			{
				return this.ItemFilterType;
			}
		}

		/// <summary>Gets the type ID for the attribute.</summary>
		/// <returns>The type ID for this attribute. All <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects with the same filter string return the same type ID.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00026A7C File Offset: 0x00024C7C
		public override object TypeId
		{
			get
			{
				return base.TypeId + this.Filter;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000EEE RID: 3822 RVA: 0x00026A90 File Offset: 0x00024C90
		public override bool Equals(object obj)
		{
			return obj is ToolboxItemFilterAttribute && (obj == this || (((ToolboxItemFilterAttribute)obj).FilterString == this.Filter && ((ToolboxItemFilterAttribute)obj).FilterType == this.ItemFilterType));
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00026AE4 File Offset: 0x00024CE4
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		/// <summary>Indicates whether the specified object has a matching filter string.</summary>
		/// <returns>true if the specified object has a matching filter string; otherwise, false.</returns>
		/// <param name="obj">The object to test for a matching filter string. </param>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x00026AF4 File Offset: 0x00024CF4
		public override bool Match(object obj)
		{
			return obj is ToolboxItemFilterAttribute && ((ToolboxItemFilterAttribute)obj).FilterString == this.Filter;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00026B1C File Offset: 0x00024D1C
		public override string ToString()
		{
			return string.Format("{0},{1}", this.Filter, this.ItemFilterType);
		}

		// Token: 0x0400043B RID: 1083
		private string Filter;

		// Token: 0x0400043C RID: 1084
		private ToolboxItemFilterType ItemFilterType;
	}
}
