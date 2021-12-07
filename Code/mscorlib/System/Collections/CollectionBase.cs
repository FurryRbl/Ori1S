using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed collection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B5 RID: 437
	[ComVisible(true)]
	[Serializable]
	public abstract class CollectionBase : IEnumerable, ICollection, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CollectionBase" /> class with the default initial capacity.</summary>
		// Token: 0x060016B8 RID: 5816 RVA: 0x00058804 File Offset: 0x00056A04
		protected CollectionBase()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CollectionBase" /> class with the specified capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store.</param>
		// Token: 0x060016B9 RID: 5817 RVA: 0x0005880C File Offset: 0x00056A0C
		protected CollectionBase(int capacity)
		{
			this.list = new ArrayList(capacity);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.CollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.CollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Collections.CollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.CollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060016BA RID: 5818 RVA: 0x00058820 File Offset: 0x00056A20
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.CollectionBase" />.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00058830 File Offset: 0x00056A30
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.CollectionBase" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.CollectionBase" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00058840 File Offset: 0x00056A40
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <returns>The <see cref="T:System.Collections.CollectionBase" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.-or-The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x060016BD RID: 5821 RVA: 0x00058850 File Offset: 0x00056A50
		int IList.Add(object value)
		{
			this.OnValidate(value);
			int count = this.InnerList.Count;
			this.OnInsert(count, value);
			this.InnerList.Add(value);
			try
			{
				this.OnInsertComplete(count, value);
			}
			catch
			{
				this.InnerList.RemoveAt(count);
				throw;
			}
			return count;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.CollectionBase" /> contains a specific element.</summary>
		/// <returns>true if the <see cref="T:System.Collections.CollectionBase" /> contains the specified <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.CollectionBase" />.</param>
		// Token: 0x060016BE RID: 5822 RVA: 0x000588C4 File Offset: 0x00056AC4
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.CollectionBase" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.CollectionBase" />.</param>
		// Token: 0x060016BF RID: 5823 RVA: 0x000588D4 File Offset: 0x00056AD4
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.CollectionBase" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.-or-The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x060016C0 RID: 5824 RVA: 0x000588E4 File Offset: 0x00056AE4
		void IList.Insert(int index, object value)
		{
			this.OnValidate(value);
			this.OnInsert(index, value);
			this.InnerList.Insert(index, value);
			try
			{
				this.OnInsertComplete(index, value);
			}
			catch
			{
				this.InnerList.RemoveAt(index);
				throw;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter was not found in the <see cref="T:System.Collections.CollectionBase" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.-or-The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x060016C1 RID: 5825 RVA: 0x0005894C File Offset: 0x00056B4C
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			int num = this.InnerList.IndexOf(value);
			if (num == -1)
			{
				throw new ArgumentException("The element cannot be found.", "value");
			}
			this.OnRemove(num, value);
			this.InnerList.Remove(value);
			this.OnRemoveComplete(num, value);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.CollectionBase" /> has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x000589A0 File Offset: 0x00056BA0
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.CollectionBase" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.CollectionBase" /> is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000589B0 File Offset: 0x00056BB0
		bool IList.IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		// Token: 0x1700036A RID: 874
		object IList.this[int index]
		{
			get
			{
				return this.InnerList[index];
			}
			set
			{
				if (index < 0 || index >= this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.OnValidate(value);
				object obj = this.InnerList[index];
				this.OnSet(index, obj, value);
				this.InnerList[index] = value;
				try
				{
					this.OnSetComplete(index, obj, value);
				}
				catch
				{
					this.InnerList[index] = obj;
					throw;
				}
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance. This property cannot be overridden.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance.Retrieving the value of this property is an O(1) operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00058A6C File Offset: 0x00056C6C
		public int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.CollectionBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016C7 RID: 5831 RVA: 0x00058A7C File Offset: 0x00056C7C
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.CollectionBase" /> instance. This method cannot be overridden.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016C8 RID: 5832 RVA: 0x00058A8C File Offset: 0x00056C8C
		public void Clear()
		{
			this.OnClear();
			this.InnerList.Clear();
			this.OnClearComplete();
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.CollectionBase" /> instance. This method is not overridable.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016C9 RID: 5833 RVA: 0x00058AB0 File Offset: 0x00056CB0
		public void RemoveAt(int index)
		{
			object value = this.InnerList[index];
			this.OnValidate(value);
			this.OnRemove(index, value);
			this.InnerList.RemoveAt(index);
			this.OnRemoveComplete(index, value);
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.CollectionBase" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.CollectionBase" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.CollectionBase.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00058AF0 File Offset: 0x00056CF0
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x00058B14 File Offset: 0x00056D14
		[ComVisible(false)]
		public int Capacity
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list.Capacity;
			}
			set
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				this.list.Capacity = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00058B44 File Offset: 0x00056D44
		protected ArrayList InnerList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.</returns>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00058B64 File Offset: 0x00056D64
		protected IList List
		{
			get
			{
				return this;
			}
		}

		/// <summary>Performs additional custom processes when clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		// Token: 0x060016CE RID: 5838 RVA: 0x00058B68 File Offset: 0x00056D68
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		// Token: 0x060016CF RID: 5839 RVA: 0x00058B6C File Offset: 0x00056D6C
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060016D0 RID: 5840 RVA: 0x00058B70 File Offset: 0x00056D70
		protected virtual void OnInsert(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060016D1 RID: 5841 RVA: 0x00058B74 File Offset: 0x00056D74
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x060016D2 RID: 5842 RVA: 0x00058B78 File Offset: 0x00056D78
		protected virtual void OnRemove(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x060016D3 RID: 5843 RVA: 0x00058B7C File Offset: 0x00056D7C
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060016D4 RID: 5844 RVA: 0x00058B80 File Offset: 0x00056D80
		protected virtual void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060016D5 RID: 5845 RVA: 0x00058B84 File Offset: 0x00056D84
		protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes when validating a value.</summary>
		/// <param name="value">The object to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060016D6 RID: 5846 RVA: 0x00058B88 File Offset: 0x00056D88
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("CollectionBase.OnValidate: Invalid parameter value passed to method: null");
			}
		}

		// Token: 0x04000873 RID: 2163
		private ArrayList list;
	}
}
