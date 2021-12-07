using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted by key based on the associated <see cref="T:System.Collections.Generic.IComparer`1" /> implementation.</summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TValue">The type of values in the collection.</typeparam>
	// Token: 0x020000A3 RID: 163
	[ComVisible(false)]
	[Serializable]
	public class SortedList<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable, IDictionary<TKey, TValue>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		// Token: 0x060006D0 RID: 1744 RVA: 0x00015160 File Offset: 0x00013360
		public SortedList() : this(SortedList<TKey, TValue>.INITIAL_SIZE, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060006D1 RID: 1745 RVA: 0x00015170 File Offset: 0x00013370
		public SortedList(int capacity) : this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060006D2 RID: 1746 RVA: 0x0001517C File Offset: 0x0001337C
		public SortedList(int capacity, IComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity");
			}
			if (capacity == 0)
			{
				this.defaultCapacity = 0;
			}
			else
			{
				this.defaultCapacity = SortedList<TKey, TValue>.INITIAL_SIZE;
			}
			this.Init(comparer, capacity, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x060006D3 RID: 1747 RVA: 0x000151C8 File Offset: 0x000133C8
		public SortedList(IComparer<TKey> comparer) : this(SortedList<TKey, TValue>.INITIAL_SIZE, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060006D4 RID: 1748 RVA: 0x000151D8 File Offset: 0x000133D8
		public SortedList(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060006D5 RID: 1749 RVA: 0x000151E4 File Offset: 0x000133E4
		public SortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.Init(comparer, dictionary.Count, true);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00015280 File Offset: 0x00013480
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns the current instance.</returns>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00015284 File Offset: 0x00013484
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00015288 File Offset: 0x00013488
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001528C File Offset: 0x0001348C
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x17000162 RID: 354
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is TKey))
				{
					return null;
				}
				return this[(TKey)((object)key)];
			}
			set
			{
				this[this.ToKey(key)] = this.ToValue(value);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x000152C8 File Offset: 0x000134C8
		ICollection IDictionary.Keys
		{
			get
			{
				return new SortedList<TKey, TValue>.ListKeys(this);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x000152D0 File Offset: 0x000134D0
		ICollection IDictionary.Values
		{
			get
			{
				return new SortedList<TKey, TValue>.ListValues(this);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x000152D8 File Offset: 0x000134D8
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x000152E0 File Offset: 0x000134E0
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x000152E8 File Offset: 0x000134E8
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000152EC File Offset: 0x000134EC
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			this.defaultCapacity = SortedList<TKey, TValue>.INITIAL_SIZE;
			this.table = new KeyValuePair<TKey, TValue>[this.defaultCapacity];
			this.inUse = 0;
			this.modificationCount++;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00015320 File Offset: 0x00013520
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this.Count == 0)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentNullException("arrayIndex is greater than or equal to array.Length");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentNullException("Not enough space in array from arrayIndex to end of array");
			}
			int num = arrayIndex;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[num++] = keyValuePair;
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000153E0 File Offset: 0x000135E0
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000153F8 File Offset: 0x000135F8
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.Find(keyValuePair.Key);
			return num >= 0 && Comparer<KeyValuePair<TKey, TValue>>.Default.Compare(this.table[num], keyValuePair) == 0;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001543C File Offset: 0x0001363C
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.Find(keyValuePair.Key);
			if (num >= 0 && Comparer<KeyValuePair<TKey, TValue>>.Default.Compare(this.table[num], keyValuePair) == 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001548C File Offset: 0x0001368C
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			for (int i = 0; i < this.inUse; i++)
			{
				KeyValuePair<TKey, TValue> current = this.table[i];
				yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
			}
			yield break;
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060006E8 RID: 1768 RVA: 0x000154A8 File Offset: 0x000136A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.-or-<paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.-or-An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x060006E9 RID: 1769 RVA: 0x000154B0 File Offset: 0x000136B0
		void IDictionary.Add(object key, object value)
		{
			this.PutImpl(this.ToKey(key), this.ToValue(value), false);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060006EA RID: 1770 RVA: 0x000154D4 File Offset: 0x000136D4
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
			return key is TKey && this.Find((TKey)((object)key)) >= 0;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x060006EB RID: 1771 RVA: 0x00015504 File Offset: 0x00013704
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, SortedList<TKey, TValue>.EnumeratorMode.ENTRY_MODE);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060006EC RID: 1772 RVA: 0x00015510 File Offset: 0x00013710
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				return;
			}
			int num = this.IndexOfKey((TKey)((object)key));
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060006ED RID: 1773 RVA: 0x00015558 File Offset: 0x00013758
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (this.Count == 0)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("array is multi-dimensional");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentNullException("arrayIndex is greater than or equal to array.Length");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentNullException("Not enough space in array from arrayIndex to end of array");
			}
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator = this.GetEnumerator();
			int num = arrayIndex;
			while (enumerator.MoveNext())
			{
				KeyValuePair<!0, !1> keyValuePair = enumerator.Current;
				array.SetValue(keyValuePair, num++);
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00015608 File Offset: 0x00013808
		public int Count
		{
			get
			{
				return this.inUse;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" /> and a set operation creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000169 RID: 361
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = this.Find(key);
				if (num >= 0)
				{
					return this.table[num].Value;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.PutImpl(key, value, true);
			}
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.Generic.SortedList`2.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00015680 File Offset: 0x00013880
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0001568C File Offset: 0x0001388C
		public int Capacity
		{
			get
			{
				return this.table.Length;
			}
			set
			{
				int num = this.table.Length;
				if (this.inUse > value)
				{
					throw new ArgumentOutOfRangeException("capacity too small");
				}
				if (value == 0)
				{
					KeyValuePair<TKey, TValue>[] destinationArray = new KeyValuePair<TKey, TValue>[this.defaultCapacity];
					Array.Copy(this.table, destinationArray, this.inUse);
					this.table = destinationArray;
				}
				else if (value > this.inUse)
				{
					KeyValuePair<TKey, TValue>[] destinationArray2 = new KeyValuePair<TKey, TValue>[value];
					Array.Copy(this.table, destinationArray2, this.inUse);
					this.table = destinationArray2;
				}
				else if (value > num)
				{
					KeyValuePair<TKey, TValue>[] destinationArray3 = new KeyValuePair<TKey, TValue>[value];
					Array.Copy(this.table, destinationArray3, num);
					this.table = destinationArray3;
				}
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001573C File Offset: 0x0001393C
		public IList<TKey> Keys
		{
			get
			{
				return new SortedList<TKey, TValue>.ListKeys(this);
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00015744 File Offset: 0x00013944
		public IList<TValue> Values
		{
			get
			{
				return new SortedList<TKey, TValue>.ListValues(this);
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> for the sorted list. </summary>
		/// <returns>The <see cref="T:System.IComparable`1" /> for the current <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001574C File Offset: 0x0001394C
		public IComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x060006F6 RID: 1782 RVA: 0x00015754 File Offset: 0x00013954
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.PutImpl(key, value, false);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060006F7 RID: 1783 RVA: 0x00015778 File Offset: 0x00013978
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.Find(key) >= 0;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> of type <see cref="T:System.Collections.Generic.KeyValuePair`2" /> for the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x060006F8 RID: 1784 RVA: 0x000157A0 File Offset: 0x000139A0
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			for (int i = 0; i < this.inUse; i++)
			{
				KeyValuePair<TKey, TValue> current = this.table[i];
				yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
			}
			yield break;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060006F9 RID: 1785 RVA: 0x000157BC File Offset: 0x000139BC
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		// Token: 0x060006FA RID: 1786 RVA: 0x000157F8 File Offset: 0x000139F8
		public void Clear()
		{
			this.defaultCapacity = SortedList<TKey, TValue>.INITIAL_SIZE;
			this.table = new KeyValuePair<TKey, TValue>[this.defaultCapacity];
			this.inUse = 0;
			this.modificationCount++;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		// Token: 0x060006FB RID: 1787 RVA: 0x0001582C File Offset: 0x00013A2C
		public void RemoveAt(int index)
		{
			KeyValuePair<TKey, TValue>[] array = this.table;
			int count = this.Count;
			if (index >= 0 && index < count)
			{
				if (index != count - 1)
				{
					Array.Copy(array, index + 1, array, index, count - 1 - index);
				}
				else
				{
					array[index] = default(KeyValuePair<TKey, TValue>);
				}
				this.inUse--;
				this.modificationCount++;
				return;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		/// <summary>Searches for the specified key and returns the zero-based index within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of <paramref name="key" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060006FC RID: 1788 RVA: 0x000158B4 File Offset: 0x00013AB4
		public int IndexOfKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = 0;
			try
			{
				num = this.Find(key);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			return num | num >> 31;
		}

		/// <summary>Searches for the specified value and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.  The value can be null for reference types.</param>
		// Token: 0x060006FD RID: 1789 RVA: 0x00015914 File Offset: 0x00013B14
		public int IndexOfValue(TValue value)
		{
			if (this.inUse == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.inUse; i++)
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.table[i];
				if (object.Equals(value, keyValuePair.Value))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />. The value can be null for reference types.</param>
		// Token: 0x060006FE RID: 1790 RVA: 0x00015978 File Offset: 0x00013B78
		public bool ContainsValue(TValue value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.SortedList`2" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x060006FF RID: 1791 RVA: 0x00015988 File Offset: 0x00013B88
		public void TrimExcess()
		{
			if ((double)this.inUse < (double)this.table.Length * 0.9)
			{
				this.Capacity = this.inUse;
			}
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06000700 RID: 1792 RVA: 0x000159B8 File Offset: 0x00013BB8
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.Find(key);
			if (num >= 0)
			{
				value = this.table[num].Value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00015A14 File Offset: 0x00013C14
		private void EnsureCapacity(int n, int free)
		{
			KeyValuePair<TKey, TValue>[] array = this.table;
			KeyValuePair<TKey, TValue>[] array2 = null;
			int capacity = this.Capacity;
			bool flag = free >= 0 && free < this.Count;
			if (n > capacity)
			{
				array2 = new KeyValuePair<TKey, TValue>[n << 1];
			}
			if (array2 != null)
			{
				if (flag)
				{
					if (free > 0)
					{
						Array.Copy(array, 0, array2, 0, free);
					}
					int num = this.Count - free;
					if (num > 0)
					{
						Array.Copy(array, free, array2, free + 1, num);
					}
				}
				else
				{
					Array.Copy(array, array2, this.Count);
				}
				this.table = array2;
			}
			else if (flag)
			{
				Array.Copy(array, free, array, free + 1, this.Count - free);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00015AD0 File Offset: 0x00013CD0
		private void PutImpl(TKey key, TValue value, bool overwrite)
		{
			if (key == null)
			{
				throw new ArgumentNullException("null key");
			}
			KeyValuePair<TKey, TValue>[] array = this.table;
			int num = -1;
			try
			{
				num = this.Find(key);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			if (num >= 0)
			{
				if (!overwrite)
				{
					throw new ArgumentException("element already exists");
				}
				array[num] = new KeyValuePair<TKey, TValue>(key, value);
				this.modificationCount++;
				return;
			}
			else
			{
				num = ~num;
				if (num > this.Capacity + 1)
				{
					throw new Exception(string.Concat(new object[]
					{
						"SortedList::internal error (",
						key,
						", ",
						value,
						") at [",
						num,
						"]"
					}));
				}
				this.EnsureCapacity(this.Count + 1, num);
				array = this.table;
				array[num] = new KeyValuePair<TKey, TValue>(key, value);
				this.inUse++;
				this.modificationCount++;
				return;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00015C10 File Offset: 0x00013E10
		private void Init(IComparer<TKey> comparer, int capacity, bool forceSize)
		{
			if (comparer == null)
			{
				comparer = Comparer<TKey>.Default;
			}
			this.comparer = comparer;
			if (!forceSize && capacity < this.defaultCapacity)
			{
				capacity = this.defaultCapacity;
			}
			this.table = new KeyValuePair<TKey, TValue>[capacity];
			this.inUse = 0;
			this.modificationCount = 0;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00015C68 File Offset: 0x00013E68
		private void CopyToArray(Array arr, int i, SortedList<TKey, TValue>.EnumeratorMode mode)
		{
			if (arr == null)
			{
				throw new ArgumentNullException("arr");
			}
			if (i < 0 || i + this.Count > arr.Length)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			IEnumerator enumerator = new SortedList<TKey, TValue>.Enumerator(this, mode);
			while (enumerator.MoveNext())
			{
				object value = enumerator.Current;
				arr.SetValue(value, i++);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00015CD8 File Offset: 0x00013ED8
		private int Find(TKey key)
		{
			KeyValuePair<TKey, TValue>[] array = this.table;
			int count = this.Count;
			if (count == 0)
			{
				return -1;
			}
			int i = 0;
			int num = count - 1;
			while (i <= num)
			{
				int num2 = i + num >> 1;
				int num3 = this.comparer.Compare(array[num2].Key, key);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00015D54 File Offset: 0x00013F54
		private TKey ToKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"The value \"",
					key,
					"\" isn't of type \"",
					typeof(TKey),
					"\" and can't be used in this generic collection."
				}), "key");
			}
			return (TKey)((object)key);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00015DC4 File Offset: 0x00013FC4
		private TValue ToValue(object value)
		{
			if (!(value is TValue))
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"The value \"",
					value,
					"\" isn't of type \"",
					typeof(TValue),
					"\" and can't be used in this generic collection."
				}), "value");
			}
			return (TValue)((object)value);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00015E24 File Offset: 0x00014024
		internal TKey KeyAt(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				return this.table[index].Key;
			}
			throw new ArgumentOutOfRangeException("Index out of range");
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00015E58 File Offset: 0x00014058
		internal TValue ValueAt(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				return this.table[index].Value;
			}
			throw new ArgumentOutOfRangeException("Index out of range");
		}

		// Token: 0x040001D8 RID: 472
		private static readonly int INITIAL_SIZE = 16;

		// Token: 0x040001D9 RID: 473
		private int inUse;

		// Token: 0x040001DA RID: 474
		private int modificationCount;

		// Token: 0x040001DB RID: 475
		private KeyValuePair<TKey, TValue>[] table;

		// Token: 0x040001DC RID: 476
		private IComparer<TKey> comparer;

		// Token: 0x040001DD RID: 477
		private int defaultCapacity;

		// Token: 0x020000A4 RID: 164
		private enum EnumeratorMode
		{
			// Token: 0x040001DF RID: 479
			KEY_MODE,
			// Token: 0x040001E0 RID: 480
			VALUE_MODE,
			// Token: 0x040001E1 RID: 481
			ENTRY_MODE
		}

		// Token: 0x020000A5 RID: 165
		private sealed class Enumerator : IEnumerator, IDictionaryEnumerator, ICloneable
		{
			// Token: 0x0600070A RID: 1802 RVA: 0x00015E8C File Offset: 0x0001408C
			public Enumerator(SortedList<TKey, TValue> host, SortedList<TKey, TValue>.EnumeratorMode mode)
			{
				this.host = host;
				this.stamp = host.modificationCount;
				this.size = host.Count;
				this.mode = mode;
				this.Reset();
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x00015ECC File Offset: 0x000140CC
			public Enumerator(SortedList<TKey, TValue> host) : this(host, SortedList<TKey, TValue>.EnumeratorMode.ENTRY_MODE)
			{
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x00015EE4 File Offset: 0x000140E4
			public void Reset()
			{
				if (this.host.modificationCount != this.stamp || this.invalid)
				{
					throw new InvalidOperationException(SortedList<TKey, TValue>.Enumerator.xstr);
				}
				this.pos = -1;
				this.currentKey = null;
				this.currentValue = null;
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x00015F34 File Offset: 0x00014134
			public bool MoveNext()
			{
				if (this.host.modificationCount != this.stamp || this.invalid)
				{
					throw new InvalidOperationException(SortedList<TKey, TValue>.Enumerator.xstr);
				}
				KeyValuePair<TKey, TValue>[] table = this.host.table;
				if (++this.pos < this.size)
				{
					KeyValuePair<TKey, TValue> keyValuePair = table[this.pos];
					this.currentKey = keyValuePair.Key;
					this.currentValue = keyValuePair.Value;
					return true;
				}
				this.currentKey = null;
				this.currentValue = null;
				return false;
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x0600070F RID: 1807 RVA: 0x00015FDC File Offset: 0x000141DC
			public DictionaryEntry Entry
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList<TKey, TValue>.Enumerator.xstr);
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000710 RID: 1808 RVA: 0x00016030 File Offset: 0x00014230
			public object Key
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList<TKey, TValue>.Enumerator.xstr);
					}
					return this.currentKey;
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001606C File Offset: 0x0001426C
			public object Value
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList<TKey, TValue>.Enumerator.xstr);
					}
					return this.currentValue;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000712 RID: 1810 RVA: 0x000160A8 File Offset: 0x000142A8
			public object Current
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList<TKey, TValue>.Enumerator.xstr);
					}
					switch (this.mode)
					{
					case SortedList<TKey, TValue>.EnumeratorMode.KEY_MODE:
						return this.currentKey;
					case SortedList<TKey, TValue>.EnumeratorMode.VALUE_MODE:
						return this.currentValue;
					case SortedList<TKey, TValue>.EnumeratorMode.ENTRY_MODE:
						return this.Entry;
					default:
						throw new NotSupportedException(this.mode + " is not a supported mode.");
					}
				}
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x0001613C File Offset: 0x0001433C
			public object Clone()
			{
				return new SortedList<TKey, TValue>.Enumerator(this.host, this.mode)
				{
					stamp = this.stamp,
					pos = this.pos,
					size = this.size,
					currentKey = this.currentKey,
					currentValue = this.currentValue,
					invalid = this.invalid
				};
			}

			// Token: 0x040001E2 RID: 482
			private SortedList<TKey, TValue> host;

			// Token: 0x040001E3 RID: 483
			private int stamp;

			// Token: 0x040001E4 RID: 484
			private int pos;

			// Token: 0x040001E5 RID: 485
			private int size;

			// Token: 0x040001E6 RID: 486
			private SortedList<TKey, TValue>.EnumeratorMode mode;

			// Token: 0x040001E7 RID: 487
			private object currentKey;

			// Token: 0x040001E8 RID: 488
			private object currentValue;

			// Token: 0x040001E9 RID: 489
			private bool invalid;

			// Token: 0x040001EA RID: 490
			private static readonly string xstr = "SortedList.Enumerator: snapshot out of sync.";
		}

		// Token: 0x020000A6 RID: 166
		[Serializable]
		public struct KeyEnumerator : IEnumerator, IDisposable, IEnumerator<TKey>
		{
			// Token: 0x06000714 RID: 1812 RVA: 0x000161A4 File Offset: 0x000143A4
			internal KeyEnumerator(SortedList<TKey, TValue> l)
			{
				this.l = l;
				this.idx = -2;
				this.ver = l.modificationCount;
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x000161C4 File Offset: 0x000143C4
			void IEnumerator.Reset()
			{
				if (this.ver != this.l.modificationCount)
				{
					throw new InvalidOperationException("Collection was modified after the enumerator was instantiated.");
				}
				this.idx = -2;
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000716 RID: 1814 RVA: 0x000161F0 File Offset: 0x000143F0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x00016200 File Offset: 0x00014400
			public void Dispose()
			{
				this.idx = -2;
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x0001620C File Offset: 0x0001440C
			public bool MoveNext()
			{
				if (this.ver != this.l.modificationCount)
				{
					throw new InvalidOperationException("Collection was modified after the enumerator was instantiated.");
				}
				if (this.idx == -2)
				{
					this.idx = this.l.Count;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x06000719 RID: 1817 RVA: 0x00016280 File Offset: 0x00014480
			public TKey Current
			{
				get
				{
					if (this.idx < 0)
					{
						throw new InvalidOperationException();
					}
					return this.l.KeyAt(this.l.Count - 1 - this.idx);
				}
			}

			// Token: 0x040001EB RID: 491
			private const int NOT_STARTED = -2;

			// Token: 0x040001EC RID: 492
			private const int FINISHED = -1;

			// Token: 0x040001ED RID: 493
			private SortedList<TKey, TValue> l;

			// Token: 0x040001EE RID: 494
			private int idx;

			// Token: 0x040001EF RID: 495
			private int ver;
		}

		// Token: 0x020000A7 RID: 167
		[Serializable]
		public struct ValueEnumerator : IEnumerator, IDisposable, IEnumerator<TValue>
		{
			// Token: 0x0600071A RID: 1818 RVA: 0x000162B4 File Offset: 0x000144B4
			internal ValueEnumerator(SortedList<TKey, TValue> l)
			{
				this.l = l;
				this.idx = -2;
				this.ver = l.modificationCount;
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x000162D4 File Offset: 0x000144D4
			void IEnumerator.Reset()
			{
				if (this.ver != this.l.modificationCount)
				{
					throw new InvalidOperationException("Collection was modified after the enumerator was instantiated.");
				}
				this.idx = -2;
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x00016300 File Offset: 0x00014500
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x00016310 File Offset: 0x00014510
			public void Dispose()
			{
				this.idx = -2;
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x0001631C File Offset: 0x0001451C
			public bool MoveNext()
			{
				if (this.ver != this.l.modificationCount)
				{
					throw new InvalidOperationException("Collection was modified after the enumerator was instantiated.");
				}
				if (this.idx == -2)
				{
					this.idx = this.l.Count;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x00016390 File Offset: 0x00014590
			public TValue Current
			{
				get
				{
					if (this.idx < 0)
					{
						throw new InvalidOperationException();
					}
					return this.l.ValueAt(this.l.Count - 1 - this.idx);
				}
			}

			// Token: 0x040001F0 RID: 496
			private const int NOT_STARTED = -2;

			// Token: 0x040001F1 RID: 497
			private const int FINISHED = -1;

			// Token: 0x040001F2 RID: 498
			private SortedList<TKey, TValue> l;

			// Token: 0x040001F3 RID: 499
			private int idx;

			// Token: 0x040001F4 RID: 500
			private int ver;
		}

		// Token: 0x020000A8 RID: 168
		private class ListKeys : ICollection, IEnumerable, IList<TKey>, ICollection<TKey>, IEnumerable<TKey>
		{
			// Token: 0x06000720 RID: 1824 RVA: 0x000163C4 File Offset: 0x000145C4
			public ListKeys(SortedList<TKey, TValue> host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x000163E0 File Offset: 0x000145E0
			IEnumerator IEnumerable.GetEnumerator()
			{
				for (int i = 0; i < this.host.Count; i++)
				{
					yield return this.host.KeyAt(i);
				}
				yield break;
			}

			// Token: 0x06000722 RID: 1826 RVA: 0x000163FC File Offset: 0x000145FC
			public virtual void Add(TKey item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000723 RID: 1827 RVA: 0x00016404 File Offset: 0x00014604
			public virtual bool Remove(TKey key)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x0001640C File Offset: 0x0001460C
			public virtual void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000725 RID: 1829 RVA: 0x00016414 File Offset: 0x00014614
			public virtual void CopyTo(TKey[] array, int arrayIndex)
			{
				if (this.host.Count == 0)
				{
					return;
				}
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (arrayIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex is greater than or equal to array.Length");
				}
				if (this.Count > array.Length - arrayIndex)
				{
					throw new ArgumentOutOfRangeException("Not enough space in array from arrayIndex to end of array");
				}
				int num = arrayIndex;
				for (int i = 0; i < this.Count; i++)
				{
					array[num++] = this.host.KeyAt(i);
				}
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x000164B0 File Offset: 0x000146B0
			public virtual bool Contains(TKey item)
			{
				return this.host.IndexOfKey(item) > -1;
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x000164C4 File Offset: 0x000146C4
			public virtual int IndexOf(TKey item)
			{
				return this.host.IndexOfKey(item);
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x000164D4 File Offset: 0x000146D4
			public virtual void Insert(int index, TKey item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x000164DC File Offset: 0x000146DC
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000176 RID: 374
			public virtual TKey this[int index]
			{
				get
				{
					return this.host.KeyAt(index);
				}
				set
				{
					throw new NotSupportedException("attempt to modify a key");
				}
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x00016500 File Offset: 0x00014700
			public virtual IEnumerator<TKey> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.KeyEnumerator(this.host);
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x00016514 File Offset: 0x00014714
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x0600072E RID: 1838 RVA: 0x00016524 File Offset: 0x00014724
			public virtual bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.host).IsSynchronized;
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x0600072F RID: 1839 RVA: 0x00016534 File Offset: 0x00014734
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x00016538 File Offset: 0x00014738
			public virtual object SyncRoot
			{
				get
				{
					return ((ICollection)this.host).SyncRoot;
				}
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x00016548 File Offset: 0x00014748
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				this.host.CopyToArray(array, arrayIndex, SortedList<TKey, TValue>.EnumeratorMode.KEY_MODE);
			}

			// Token: 0x040001F5 RID: 501
			private SortedList<TKey, TValue> host;
		}

		// Token: 0x020000A9 RID: 169
		private class ListValues : ICollection, IEnumerable, IList<TValue>, ICollection<TValue>, IEnumerable<TValue>
		{
			// Token: 0x06000732 RID: 1842 RVA: 0x00016558 File Offset: 0x00014758
			public ListValues(SortedList<TKey, TValue> host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x00016574 File Offset: 0x00014774
			IEnumerator IEnumerable.GetEnumerator()
			{
				for (int i = 0; i < this.host.Count; i++)
				{
					yield return this.host.ValueAt(i);
				}
				yield break;
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x00016590 File Offset: 0x00014790
			public virtual void Add(TValue item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x00016598 File Offset: 0x00014798
			public virtual bool Remove(TValue value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x000165A0 File Offset: 0x000147A0
			public virtual void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x000165A8 File Offset: 0x000147A8
			public virtual void CopyTo(TValue[] array, int arrayIndex)
			{
				if (this.host.Count == 0)
				{
					return;
				}
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (arrayIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex is greater than or equal to array.Length");
				}
				if (this.Count > array.Length - arrayIndex)
				{
					throw new ArgumentOutOfRangeException("Not enough space in array from arrayIndex to end of array");
				}
				int num = arrayIndex;
				for (int i = 0; i < this.Count; i++)
				{
					array[num++] = this.host.ValueAt(i);
				}
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x00016644 File Offset: 0x00014844
			public virtual bool Contains(TValue item)
			{
				return this.host.IndexOfValue(item) > -1;
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x00016658 File Offset: 0x00014858
			public virtual int IndexOf(TValue item)
			{
				return this.host.IndexOfValue(item);
			}

			// Token: 0x0600073A RID: 1850 RVA: 0x00016668 File Offset: 0x00014868
			public virtual void Insert(int index, TValue item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x00016670 File Offset: 0x00014870
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700017B RID: 379
			public virtual TValue this[int index]
			{
				get
				{
					return this.host.ValueAt(index);
				}
				set
				{
					throw new NotSupportedException("attempt to modify a key");
				}
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x00016694 File Offset: 0x00014894
			public virtual IEnumerator<TValue> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.ValueEnumerator(this.host);
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x0600073F RID: 1855 RVA: 0x000166A8 File Offset: 0x000148A8
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000740 RID: 1856 RVA: 0x000166B8 File Offset: 0x000148B8
			public virtual bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.host).IsSynchronized;
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000741 RID: 1857 RVA: 0x000166C8 File Offset: 0x000148C8
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000742 RID: 1858 RVA: 0x000166CC File Offset: 0x000148CC
			public virtual object SyncRoot
			{
				get
				{
					return ((ICollection)this.host).SyncRoot;
				}
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x000166DC File Offset: 0x000148DC
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				this.host.CopyToArray(array, arrayIndex, SortedList<TKey, TValue>.EnumeratorMode.VALUE_MODE);
			}

			// Token: 0x040001F6 RID: 502
			private SortedList<TKey, TValue> host;
		}
	}
}
