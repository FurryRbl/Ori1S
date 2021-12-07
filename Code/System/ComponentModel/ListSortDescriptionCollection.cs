using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
	// Token: 0x0200017F RID: 383
	public class ListSortDescriptionCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class. </summary>
		// Token: 0x06000D1D RID: 3357 RVA: 0x00020D08 File Offset: 0x0001EF08
		public ListSortDescriptionCollection()
		{
			this.list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class with the specified array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
		/// <param name="sorts">The array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects to be contained in the collection.</param>
		// Token: 0x06000D1E RID: 3358 RVA: 0x00020D1C File Offset: 0x0001EF1C
		public ListSortDescriptionCollection(ListSortDescription[] sorts)
		{
			this.list = new ArrayList();
			foreach (ListSortDescription value in sorts)
			{
				this.list.Add(value);
			}
		}

		/// <summary>Gets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get in the collection </param>
		// Token: 0x170002F9 RID: 761
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new InvalidOperationException("ListSortDescriptorCollection is read only.");
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00020D7C File Offset: 0x0001EF7C
		bool IList.IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is thread safe.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00020D8C File Offset: 0x0001EF8C
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets the current instance that can be used to synchronize access to the collection.</summary>
		/// <returns>The current instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00020D9C File Offset: 0x0001EF9C
		object ICollection.SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00020DAC File Offset: 0x0001EFAC
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000D25 RID: 3365 RVA: 0x00020DBC File Offset: 0x0001EFBC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06000D26 RID: 3366 RVA: 0x00020DCC File Offset: 0x0001EFCC
		int IList.Add(object value)
		{
			return this.list.Add(value);
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06000D27 RID: 3367 RVA: 0x00020DDC File Offset: 0x0001EFDC
		void IList.Clear()
		{
			this.list.Clear();
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get or set in the collection</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06000D28 RID: 3368 RVA: 0x00020DEC File Offset: 0x0001EFEC
		void IList.Insert(int index, object value)
		{
			this.list.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of an item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06000D29 RID: 3369 RVA: 0x00020DFC File Offset: 0x0001EFFC
		void IList.Remove(object value)
		{
			this.list.Remove(value);
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to remove from the collection</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06000D2A RID: 3370 RVA: 0x00020E0C File Offset: 0x0001F00C
		void IList.RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00020E1C File Offset: 0x0001F01C
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get or set in the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">An item is set in the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />, which is read-only.</exception>
		// Token: 0x170002FF RID: 767
		public ListSortDescription this[int index]
		{
			get
			{
				return this.list[index] as ListSortDescription;
			}
			set
			{
				throw new InvalidOperationException("ListSortDescriptorCollection is read only.");
			}
		}

		/// <summary>Determines if the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00020E4C File Offset: 0x0001F04C
		public bool Contains(object value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Copies the contents of the collection to the specified array, starting at the specified destination array index.</summary>
		/// <param name="array">The destination array for the items copied from the collection.</param>
		/// <param name="index">The index of the destination array at which copying begins.</param>
		// Token: 0x06000D2F RID: 3375 RVA: 0x00020E5C File Offset: 0x0001F05C
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns the index of the specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06000D30 RID: 3376 RVA: 0x00020E6C File Offset: 0x0001F06C
		public int IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x0400039D RID: 925
		private ArrayList list;
	}
}
