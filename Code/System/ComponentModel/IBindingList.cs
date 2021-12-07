using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides the features required to support both complex and simple scenarios when binding to a data source.</summary>
	// Token: 0x0200014F RID: 335
	public interface IBindingList : IList, ICollection, IEnumerable
	{
		/// <summary>Occurs when the list changes or an item in the list changes.</summary>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000C4C RID: 3148
		// (remove) Token: 0x06000C4D RID: 3149
		event ListChangedEventHandler ListChanged;

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
		// Token: 0x06000C4E RID: 3150
		void AddIndex(PropertyDescriptor property);

		/// <summary>Adds a new item to the list.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is false. </exception>
		// Token: 0x06000C4F RID: 3151
		object AddNew();

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x06000C50 RID: 3152
		void ApplySort(PropertyDescriptor property, ListSortDirection direction);

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on. </param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" /> is false. </exception>
		// Token: 0x06000C51 RID: 3153
		int Find(PropertyDescriptor property, object key);

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching. </param>
		// Token: 0x06000C52 RID: 3154
		void RemoveIndex(PropertyDescriptor property);

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x06000C53 RID: 3155
		void RemoveSort();

		/// <summary>Gets whether you can update items in the list.</summary>
		/// <returns>true if you can update the items in the list; otherwise, false.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C54 RID: 3156
		bool AllowEdit { get; }

		/// <summary>Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />; otherwise, false.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C55 RID: 3157
		bool AllowNew { get; }

		/// <summary>Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <returns>true if you can remove items from the list; otherwise, false.</returns>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C56 RID: 3158
		bool AllowRemove { get; }

		/// <summary>Gets whether the items in the list are sorted.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C57 RID: 3159
		bool IsSorted { get; }

		/// <summary>Gets the direction of the sort.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C58 RID: 3160
		ListSortDirection SortDirection { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C59 RID: 3161
		PropertyDescriptor SortProperty { get; }

		/// <summary>Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or an item in the list changes.</summary>
		/// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, false.</returns>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C5A RID: 3162
		bool SupportsChangeNotification { get; }

		/// <summary>Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
		/// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, false.</returns>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C5B RID: 3163
		bool SupportsSearching { get; }

		/// <summary>Gets whether the list supports sorting.</summary>
		/// <returns>true if the list supports sorting; otherwise, false.</returns>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C5C RID: 3164
		bool SupportsSorting { get; }
	}
}
