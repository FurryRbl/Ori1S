using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Extends the <see cref="T:System.ComponentModel.IBindingList" /> interface by providing advanced sorting and filtering capabilities.</summary>
	// Token: 0x02000150 RID: 336
	public interface IBindingListView : IList, ICollection, IEnumerable, IBindingList
	{
		/// <summary>Gets or sets the filter to be used to exclude items from the collection of items returned by the data source</summary>
		/// <returns>The string used to filter items out in the item collection returned by the data source. </returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C5D RID: 3165
		// (set) Token: 0x06000C5E RID: 3166
		string Filter { get; set; }

		/// <summary>Gets the collection of sort descriptions currently applied to the data source.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> currently applied to the data source.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C5F RID: 3167
		ListSortDescriptionCollection SortDescriptions { get; }

		/// <summary>Gets a value indicating whether the data source supports advanced sorting. </summary>
		/// <returns>true if the data source supports advanced sorting; otherwise, false. </returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C60 RID: 3168
		bool SupportsAdvancedSorting { get; }

		/// <summary>Gets a value indicating whether the data source supports filtering. </summary>
		/// <returns>true if the data source supports filtering; otherwise, false. </returns>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C61 RID: 3169
		bool SupportsFiltering { get; }

		/// <summary>Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</summary>
		/// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sorts to apply to the data source.</param>
		// Token: 0x06000C62 RID: 3170
		void ApplySort(ListSortDescriptionCollection sorts);

		/// <summary>Removes the current filter applied to the data source.</summary>
		// Token: 0x06000C63 RID: 3171
		void RemoveFilter();
	}
}
