using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Provides the base class for a generic collection.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020006D0 RID: 1744
	[ComVisible(false)]
	[Serializable]
	public class Collection<T> : IEnumerable, ICollection, IList, ICollection<T>, IList<T>, IEnumerable<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.Collection`1" /> class that is empty.</summary>
		// Token: 0x060042E8 RID: 17128 RVA: 0x000E5134 File Offset: 0x000E3334
		public Collection()
		{
			List<T> list = new List<T>();
			IList list2 = list;
			this.syncRoot = list2.SyncRoot;
			this.list = list;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.Collection`1" /> class as a wrapper for the specified list.</summary>
		/// <param name="list">The list that is wrapped by the new collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		// Token: 0x060042E9 RID: 17129 RVA: 0x000E5164 File Offset: 0x000E3364
		public Collection(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.list = list;
			ICollection collection = list as ICollection;
			this.syncRoot = ((collection == null) ? new object() : collection.SyncRoot);
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x000E51B4 File Offset: 0x000E33B4
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060042EB RID: 17131 RVA: 0x000E51C4 File Offset: 0x000E33C4
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.list).CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060042EC RID: 17132 RVA: 0x000E51D8 File Offset: 0x000E33D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x060042ED RID: 17133 RVA: 0x000E51E8 File Offset: 0x000E33E8
		int IList.Add(object value)
		{
			int count = this.list.Count;
			this.InsertItem(count, Collection<T>.ConvertItem(value));
			return count;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x060042EE RID: 17134 RVA: 0x000E5210 File Offset: 0x000E3410
		bool IList.Contains(object value)
		{
			return Collection<T>.IsValidItem(value) && this.list.Contains((T)((object)value));
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x060042EF RID: 17135 RVA: 0x000E5230 File Offset: 0x000E3430
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsValidItem(value))
			{
				return this.list.IndexOf((T)((object)value));
			}
			return -1;
		}

		/// <summary>Inserts an item into the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x060042F0 RID: 17136 RVA: 0x000E5250 File Offset: 0x000E3450
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, Collection<T>.ConvertItem(value));
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x060042F1 RID: 17137 RVA: 0x000E5260 File Offset: 0x000E3460
		void IList.Remove(object value)
		{
			Collection<T>.CheckWritable(this.list);
			int index = this.IndexOf(Collection<T>.ConvertItem(value));
			this.RemoveItem(index);
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns false.</returns>
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x000E528C File Offset: 0x000E348C
		bool ICollection.IsSynchronized
		{
			get
			{
				return Collection<T>.IsSynchronized(this.list);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x000E529C File Offset: 0x000E349C
		object ICollection.SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns false.</returns>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x000E52A4 File Offset: 0x000E34A4
		bool IList.IsFixedSize
		{
			get
			{
				return Collection<T>.IsFixedSize(this.list);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns false.</returns>
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x000E52B4 File Offset: 0x000E34B4
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is set, and <paramref name="index" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x17000C7D RID: 3197
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.SetItem(index, Collection<T>.ConvertItem(value));
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1" />. </summary>
		/// <param name="item">The object to be added to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1" />. The value can be null for reference types.</param>
		// Token: 0x060042F8 RID: 17144 RVA: 0x000E52E8 File Offset: 0x000E34E8
		public void Add(T item)
		{
			int count = this.list.Count;
			this.InsertItem(count, item);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		// Token: 0x060042F9 RID: 17145 RVA: 0x000E530C File Offset: 0x000E350C
		public void Clear()
		{
			this.ClearItems();
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		// Token: 0x060042FA RID: 17146 RVA: 0x000E5314 File Offset: 0x000E3514
		protected virtual void ClearItems()
		{
			this.list.Clear();
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.ObjectModel.Collection`1" />; otherwise, false.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.ObjectModel.Collection`1" />. The value can be null for reference types.</param>
		// Token: 0x060042FB RID: 17147 RVA: 0x000E5324 File Offset: 0x000E3524
		public bool Contains(T item)
		{
			return this.list.Contains(item);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ObjectModel.Collection`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ObjectModel.Collection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.ObjectModel.Collection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060042FC RID: 17148 RVA: 0x000E5334 File Offset: 0x000E3534
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> for the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		// Token: 0x060042FD RID: 17149 RVA: 0x000E5344 File Offset: 0x000E3544
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item" /> within the entire <see cref="T:System.Collections.ObjectModel.Collection`1" />, if found; otherwise, -1.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be null for reference types.</param>
		// Token: 0x060042FE RID: 17150 RVA: 0x000E5354 File Offset: 0x000E3554
		public int IndexOf(T item)
		{
			return this.list.IndexOf(item);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x060042FF RID: 17151 RVA: 0x000E5364 File Offset: 0x000E3564
		public void Insert(int index, T item)
		{
			this.InsertItem(index, item);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06004300 RID: 17152 RVA: 0x000E5370 File Offset: 0x000E3570
		protected virtual void InsertItem(int index, T item)
		{
			this.list.Insert(index, item);
		}

		/// <summary>Gets a <see cref="T:System.Collections.Generic.IList`1" /> wrapper around the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> wrapper around the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x000E5380 File Offset: 0x000E3580
		protected IList<T> Items
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is successfully removed; otherwise, false.  This method also returns false if <paramref name="item" /> was not found in the original <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.ObjectModel.Collection`1" />. The value can be null for reference types.</param>
		// Token: 0x06004302 RID: 17154 RVA: 0x000E5388 File Offset: 0x000E3588
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num == -1)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06004303 RID: 17155 RVA: 0x000E53B0 File Offset: 0x000E35B0
		public void RemoveAt(int index)
		{
			this.RemoveItem(index);
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06004304 RID: 17156 RVA: 0x000E53BC File Offset: 0x000E35BC
		protected virtual void RemoveItem(int index)
		{
			this.list.RemoveAt(index);
		}

		/// <summary>Gets the number of elements actually contained in the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>The number of elements actually contained in the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x000E53CC File Offset: 0x000E35CC
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />. </exception>
		// Token: 0x17000C80 RID: 3200
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06004308 RID: 17160 RVA: 0x000E53F8 File Offset: 0x000E35F8
		protected virtual void SetItem(int index, T item)
		{
			this.list[index] = item;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000E5408 File Offset: 0x000E3608
		internal static bool IsValidItem(object item)
		{
			return item is T || (item == null && !typeof(T).IsValueType);
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000E5434 File Offset: 0x000E3634
		internal static T ConvertItem(object item)
		{
			if (Collection<T>.IsValidItem(item))
			{
				return (T)((object)item);
			}
			throw new ArgumentException("item");
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000E5454 File Offset: 0x000E3654
		internal static void CheckWritable(IList<T> list)
		{
			if (list.IsReadOnly)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000E5468 File Offset: 0x000E3668
		internal static bool IsSynchronized(IList<T> list)
		{
			ICollection collection = list as ICollection;
			return collection != null && collection.IsSynchronized;
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000E5490 File Offset: 0x000E3690
		internal static bool IsFixedSize(IList<T> list)
		{
			IList list2 = list as IList;
			return list2 != null && list2.IsFixedSize;
		}

		// Token: 0x04001C53 RID: 7251
		private IList<T> list;

		// Token: 0x04001C54 RID: 7252
		private object syncRoot;
	}
}
