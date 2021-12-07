using System;

namespace System.ComponentModel
{
	/// <summary>Provides a description of the sort operation applied to a data source.</summary>
	// Token: 0x02000180 RID: 384
	public class ListSortDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescription" /> class with the specified property description and direction.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which the data source is sorted.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDescription" />  values.</param>
		// Token: 0x06000D31 RID: 3377 RVA: 0x00020E7C File Offset: 0x0001F07C
		public ListSortDescription(PropertyDescriptor propertyDescriptor, ListSortDirection sortDirection)
		{
			this.propertyDescriptor = propertyDescriptor;
			this.sortDirection = sortDirection;
		}

		/// <summary>Gets or sets the abstract description of a class property associated with this <see cref="T:System.ComponentModel.ListSortDescription" /></summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.ComponentModel.ListSortDescription" />. </returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00020E94 File Offset: 0x0001F094
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00020E9C File Offset: 0x0001F09C
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propertyDescriptor;
			}
			set
			{
				this.propertyDescriptor = value;
			}
		}

		/// <summary>Gets or sets the direction of the sort operation associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00020EB0 File Offset: 0x0001F0B0
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				this.sortDirection = value;
			}
		}

		// Token: 0x0400039E RID: 926
		private PropertyDescriptor propertyDescriptor;

		// Token: 0x0400039F RID: 927
		private ListSortDirection sortDirection;
	}
}
