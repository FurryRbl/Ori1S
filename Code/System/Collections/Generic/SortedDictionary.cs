using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted on the key.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200009B RID: 155
	[Serializable]
	public class SortedDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		// Token: 0x0600066B RID: 1643 RVA: 0x000143E0 File Offset: 0x000125E0
		public SortedDictionary() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x0600066C RID: 1644 RVA: 0x000143EC File Offset: 0x000125EC
		public SortedDictionary(IComparer<TKey> comparer)
		{
			this.hlp = SortedDictionary<TKey, TValue>.NodeHelper.GetHelper(comparer);
			this.tree = new RBTree(this.hlp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x0600066D RID: 1645 RVA: 0x00014414 File Offset: 0x00012614
		public SortedDictionary(IDictionary<TKey, TValue> dic) : this(dic, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x0600066E RID: 1646 RVA: 0x00014420 File Offset: 0x00012620
		public SortedDictionary(IDictionary<TKey, TValue> dic, IComparer<TKey> comparer) : this(comparer)
		{
			if (dic == null)
			{
				throw new ArgumentNullException();
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dic)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000144A0 File Offset: 0x000126A0
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection(this);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x000144A8 File Offset: 0x000126A8
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection(this);
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000144B0 File Offset: 0x000126B0
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000144C8 File Offset: 0x000126C8
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			TValue y;
			return this.TryGetValue(item.Key, out y) && EqualityComparer<TValue>.Default.Equals(item.Value, y);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00014500 File Offset: 0x00012700
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00014504 File Offset: 0x00012704
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			TValue y;
			return this.TryGetValue(item.Key, out y) && EqualityComparer<TValue>.Default.Equals(item.Value, y) && this.Remove(item.Key);
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.-or-<paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.-or-An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x06000675 RID: 1653 RVA: 0x0001454C File Offset: 0x0001274C
		void IDictionary.Add(object key, object value)
		{
			this.Add(this.ToKey(key), this.ToValue(value));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06000676 RID: 1654 RVA: 0x00014564 File Offset: 0x00012764
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey(this.ToKey(key));
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06000677 RID: 1655 RVA: 0x00014574 File Offset: 0x00012774
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00014584 File Offset: 0x00012784
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00014588 File Offset: 0x00012788
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001458C File Offset: 0x0001278C
		ICollection IDictionary.Keys
		{
			get
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection(this);
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600067B RID: 1659 RVA: 0x00014594 File Offset: 0x00012794
		void IDictionary.Remove(object key)
		{
			this.Remove(this.ToKey(key));
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x000145A4 File Offset: 0x000127A4
		ICollection IDictionary.Values
		{
			get
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection(this);
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x17000144 RID: 324
		object IDictionary.this[object key]
		{
			get
			{
				return this[this.ToKey(key)];
			}
			set
			{
				this[this.ToKey(key)] = this.ToValue(value);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.Generic.ICollection`1" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600067F RID: 1663 RVA: 0x000145D8 File Offset: 0x000127D8
		void ICollection.CopyTo(Array array, int index)
		{
			if (this.Count == 0)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (index < 0 || array.Length <= index)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException();
			}
			foreach (RBTree.Node node in this.tree)
			{
				SortedDictionary<TKey, TValue>.Node node2 = (SortedDictionary<TKey, TValue>.Node)node;
				array.SetValue(node2.AsDE(), index++);
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000146A0 File Offset: 0x000128A0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. </returns>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000146A4 File Offset: 0x000128A4
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000682 RID: 1666 RVA: 0x000146A8 File Offset: 0x000128A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000146B8 File Offset: 0x000128B8
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this);
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /></returns>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000146C8 File Offset: 0x000128C8
		public IComparer<TKey> Comparer
		{
			get
			{
				return this.hlp.cmp;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000146D8 File Offset: 0x000128D8
		public int Count
		{
			get
			{
				return this.tree.Count;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000149 RID: 329
		public TValue this[TKey key]
		{
			get
			{
				SortedDictionary<TKey, TValue>.Node node = (SortedDictionary<TKey, TValue>.Node)this.tree.Lookup<TKey>(key);
				if (node == null)
				{
					throw new KeyNotFoundException();
				}
				return node.value;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				SortedDictionary<TKey, TValue>.Node node = (SortedDictionary<TKey, TValue>.Node)this.tree.Intern<TKey>(key, null);
				node.value = value;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001475C File Offset: 0x0001295C
		public SortedDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection(this);
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00014764 File Offset: 0x00012964
		public SortedDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection(this);
			}
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x0600068A RID: 1674 RVA: 0x0001476C File Offset: 0x0001296C
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			RBTree.Node node = new SortedDictionary<TKey, TValue>.Node(key, value);
			if (this.tree.Intern<TKey>(key, node) != node)
			{
				throw new ArgumentException("key already present in dictionary", "key");
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		// Token: 0x0600068B RID: 1675 RVA: 0x000147BC File Offset: 0x000129BC
		public void Clear()
		{
			this.tree.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600068C RID: 1676 RVA: 0x000147CC File Offset: 0x000129CC
		public bool ContainsKey(TKey key)
		{
			return this.tree.Lookup<TKey>(key) != null;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />. The value can be null for reference types.</param>
		// Token: 0x0600068D RID: 1677 RVA: 0x000147E0 File Offset: 0x000129E0
		public bool ContainsValue(TValue value)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			foreach (RBTree.Node node in this.tree)
			{
				SortedDictionary<TKey, TValue>.Node node2 = (SortedDictionary<TKey, TValue>.Node)node;
				if (@default.Equals(value, node2.value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> to the specified array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures that is the destination of the elements copied from the current <see cref="T:System.Collections.Generic.SortedDictionary`2" /> The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600068E RID: 1678 RVA: 0x00014868 File Offset: 0x00012A68
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this.Count == 0)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0 || array.Length <= arrayIndex)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException();
			}
			foreach (RBTree.Node node in this.tree)
			{
				SortedDictionary<TKey, TValue>.Node node2 = (SortedDictionary<TKey, TValue>.Node)node;
				array[arrayIndex++] = node2.AsKV();
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" /> for the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x0600068F RID: 1679 RVA: 0x00014928 File Offset: 0x00012B28
		public SortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06000690 RID: 1680 RVA: 0x00014930 File Offset: 0x00012B30
		public bool Remove(TKey key)
		{
			return this.tree.Remove<TKey>(key) != null;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06000691 RID: 1681 RVA: 0x00014944 File Offset: 0x00012B44
		public bool TryGetValue(TKey key, out TValue value)
		{
			SortedDictionary<TKey, TValue>.Node node = (SortedDictionary<TKey, TValue>.Node)this.tree.Lookup<TKey>(key);
			value = ((node != null) ? node.value : default(TValue));
			return node != null;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001498C File Offset: 0x00012B8C
		private TKey ToKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(string.Format("Key \"{0}\" cannot be converted to the key type {1}.", key, typeof(TKey)));
			}
			return (TKey)((object)key);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000149CC File Offset: 0x00012BCC
		private TValue ToValue(object value)
		{
			if (!(value is TValue) && (value != null || typeof(TValue).IsValueType))
			{
				throw new ArgumentException(string.Format("Value \"{0}\" cannot be converted to the value type {1}.", value, typeof(TValue)));
			}
			return (TValue)((object)value);
		}

		// Token: 0x040001CA RID: 458
		private RBTree tree;

		// Token: 0x040001CB RID: 459
		private SortedDictionary<TKey, TValue>.NodeHelper hlp;

		// Token: 0x0200009C RID: 156
		private class Node : RBTree.Node
		{
			// Token: 0x06000694 RID: 1684 RVA: 0x00014A20 File Offset: 0x00012C20
			public Node(TKey key)
			{
				this.key = key;
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x00014A30 File Offset: 0x00012C30
			public Node(TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x00014A48 File Offset: 0x00012C48
			public override void SwapValue(RBTree.Node other)
			{
				SortedDictionary<TKey, TValue>.Node node = (SortedDictionary<TKey, TValue>.Node)other;
				TKey tkey = this.key;
				this.key = node.key;
				node.key = tkey;
				TValue tvalue = this.value;
				this.value = node.value;
				node.value = tvalue;
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x00014A90 File Offset: 0x00012C90
			public KeyValuePair<TKey, TValue> AsKV()
			{
				return new KeyValuePair<TKey, TValue>(this.key, this.value);
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x00014AA4 File Offset: 0x00012CA4
			public DictionaryEntry AsDE()
			{
				return new DictionaryEntry(this.key, this.value);
			}

			// Token: 0x040001CC RID: 460
			public TKey key;

			// Token: 0x040001CD RID: 461
			public TValue value;
		}

		// Token: 0x0200009D RID: 157
		private class NodeHelper : RBTree.INodeHelper<TKey>
		{
			// Token: 0x06000699 RID: 1689 RVA: 0x00014AC4 File Offset: 0x00012CC4
			private NodeHelper(IComparer<TKey> cmp)
			{
				this.cmp = cmp;
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x00014AE8 File Offset: 0x00012CE8
			public int Compare(TKey key, RBTree.Node node)
			{
				return this.cmp.Compare(key, ((SortedDictionary<TKey, TValue>.Node)node).key);
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x00014B04 File Offset: 0x00012D04
			public RBTree.Node CreateNode(TKey key)
			{
				return new SortedDictionary<TKey, TValue>.Node(key);
			}

			// Token: 0x0600069D RID: 1693 RVA: 0x00014B0C File Offset: 0x00012D0C
			public static SortedDictionary<TKey, TValue>.NodeHelper GetHelper(IComparer<TKey> cmp)
			{
				if (cmp == null || cmp == Comparer<TKey>.Default)
				{
					return SortedDictionary<TKey, TValue>.NodeHelper.Default;
				}
				return new SortedDictionary<TKey, TValue>.NodeHelper(cmp);
			}

			// Token: 0x040001CE RID: 462
			public IComparer<TKey> cmp;

			// Token: 0x040001CF RID: 463
			private static SortedDictionary<TKey, TValue>.NodeHelper Default = new SortedDictionary<TKey, TValue>.NodeHelper(Comparer<TKey>.Default);
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited.</summary>
		// Token: 0x0200009E RID: 158
		[Serializable]
		public sealed class ValueCollection : ICollection, IEnumerable, ICollection<TValue>, IEnumerable<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x0600069E RID: 1694 RVA: 0x00014B2C File Offset: 0x00012D2C
			public ValueCollection(SortedDictionary<TKey, TValue> dic)
			{
				this._dic = dic;
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x00014B3C File Offset: 0x00012D3C
			void ICollection<!1>.Add(TValue item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x00014B44 File Offset: 0x00012D44
			void ICollection<!1>.Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x00014B4C File Offset: 0x00012D4C
			bool ICollection<!1>.Contains(TValue item)
			{
				return this._dic.ContainsValue(item);
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00014B5C File Offset: 0x00012D5C
			bool ICollection<!1>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x00014B60 File Offset: 0x00012D60
			bool ICollection<!1>.Remove(TValue item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x00014B68 File Offset: 0x00012D68
			IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x060006A5 RID: 1701 RVA: 0x00014B78 File Offset: 0x00012D78
			void ICollection.CopyTo(Array array, int index)
			{
				if (this.Count == 0)
				{
					return;
				}
				if (array == null)
				{
					throw new ArgumentNullException();
				}
				if (index < 0 || array.Length <= index)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (array.Length - index < this.Count)
				{
					throw new ArgumentException();
				}
				foreach (RBTree.Node node in this._dic.tree)
				{
					SortedDictionary<TKey, TValue>.Node node2 = (SortedDictionary<TKey, TValue>.Node)node;
					array.SetValue(node2.value, index++);
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns false.</returns>
			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00014C44 File Offset: 0x00012E44
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00014C48 File Offset: 0x00012E48
			object ICollection.SyncRoot
			{
				get
				{
					return this._dic;
				}
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x060006A8 RID: 1704 RVA: 0x00014C50 File Offset: 0x00012E50
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dic);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x060006A9 RID: 1705 RVA: 0x00014C64 File Offset: 0x00012E64
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				if (this.Count == 0)
				{
					return;
				}
				if (array == null)
				{
					throw new ArgumentNullException();
				}
				if (arrayIndex < 0 || array.Length <= arrayIndex)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (array.Length - arrayIndex < this.Count)
				{
					throw new ArgumentException();
				}
				foreach (RBTree.Node node in this._dic.tree)
				{
					SortedDictionary<TKey, TValue>.Node node2 = (SortedDictionary<TKey, TValue>.Node)node;
					array[arrayIndex++] = node2.value;
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060006AA RID: 1706 RVA: 0x00014D24 File Offset: 0x00012F24
			public int Count
			{
				get
				{
					return this._dic.Count;
				}
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x060006AB RID: 1707 RVA: 0x00014D34 File Offset: 0x00012F34
			public SortedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dic);
			}

			// Token: 0x040001D0 RID: 464
			private SortedDictionary<TKey, TValue> _dic;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			// Token: 0x0200009F RID: 159
			public struct Enumerator : IEnumerator, IDisposable, IEnumerator<TValue>
			{
				// Token: 0x060006AC RID: 1708 RVA: 0x00014D44 File Offset: 0x00012F44
				internal Enumerator(SortedDictionary<TKey, TValue> dic)
				{
					this.host = dic.tree.GetEnumerator();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17000150 RID: 336
				// (get) Token: 0x060006AD RID: 1709 RVA: 0x00014D58 File Offset: 0x00012F58
				object IEnumerator.Current
				{
					get
					{
						this.host.check_current();
						return this.current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x060006AE RID: 1710 RVA: 0x00014D70 File Offset: 0x00012F70
				void IEnumerator.Reset()
				{
					this.host.Reset();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000151 RID: 337
				// (get) Token: 0x060006AF RID: 1711 RVA: 0x00014D80 File Offset: 0x00012F80
				public TValue Current
				{
					get
					{
						return this.current;
					}
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x060006B0 RID: 1712 RVA: 0x00014D88 File Offset: 0x00012F88
				public bool MoveNext()
				{
					if (!this.host.MoveNext())
					{
						return false;
					}
					this.current = ((SortedDictionary<TKey, TValue>.Node)this.host.Current).value;
					return true;
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x060006B1 RID: 1713 RVA: 0x00014DC4 File Offset: 0x00012FC4
				public void Dispose()
				{
					this.host.Dispose();
				}

				// Token: 0x040001D1 RID: 465
				private RBTree.NodeEnumerator host;

				// Token: 0x040001D2 RID: 466
				private TValue current;
			}
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited. </summary>
		// Token: 0x020000A0 RID: 160
		[Serializable]
		public sealed class KeyCollection : ICollection, IEnumerable, ICollection<TKey>, IEnumerable<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x060006B2 RID: 1714 RVA: 0x00014DD4 File Offset: 0x00012FD4
			public KeyCollection(SortedDictionary<TKey, TValue> dic)
			{
				this._dic = dic;
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x00014DE4 File Offset: 0x00012FE4
			void ICollection<!0>.Add(TKey item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x00014DEC File Offset: 0x00012FEC
			void ICollection<!0>.Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00014DF4 File Offset: 0x00012FF4
			bool ICollection<!0>.Contains(TKey item)
			{
				return this._dic.ContainsKey(item);
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x00014E04 File Offset: 0x00013004
			IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00014E14 File Offset: 0x00013014
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x00014E18 File Offset: 0x00013018
			bool ICollection<!0>.Remove(TKey item)
			{
				throw new NotSupportedException();
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x060006B9 RID: 1721 RVA: 0x00014E20 File Offset: 0x00013020
			void ICollection.CopyTo(Array array, int index)
			{
				if (this.Count == 0)
				{
					return;
				}
				if (array == null)
				{
					throw new ArgumentNullException();
				}
				if (index < 0 || array.Length <= index)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (array.Length - index < this.Count)
				{
					throw new ArgumentException();
				}
				foreach (RBTree.Node node in this._dic.tree)
				{
					SortedDictionary<TKey, TValue>.Node node2 = (SortedDictionary<TKey, TValue>.Node)node;
					array.SetValue(node2.key, index++);
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns false.</returns>
			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060006BA RID: 1722 RVA: 0x00014EEC File Offset: 0x000130EC
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060006BB RID: 1723 RVA: 0x00014EF0 File Offset: 0x000130F0
			object ICollection.SyncRoot
			{
				get
				{
					return this._dic;
				}
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x060006BC RID: 1724 RVA: 0x00014EF8 File Offset: 0x000130F8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dic);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x060006BD RID: 1725 RVA: 0x00014F0C File Offset: 0x0001310C
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				if (this.Count == 0)
				{
					return;
				}
				if (array == null)
				{
					throw new ArgumentNullException();
				}
				if (arrayIndex < 0 || array.Length <= arrayIndex)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (array.Length - arrayIndex < this.Count)
				{
					throw new ArgumentException();
				}
				foreach (RBTree.Node node in this._dic.tree)
				{
					SortedDictionary<TKey, TValue>.Node node2 = (SortedDictionary<TKey, TValue>.Node)node;
					array[arrayIndex++] = node2.key;
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060006BE RID: 1726 RVA: 0x00014FCC File Offset: 0x000131CC
			public int Count
			{
				get
				{
					return this._dic.Count;
				}
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x060006BF RID: 1727 RVA: 0x00014FDC File Offset: 0x000131DC
			public SortedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dic);
			}

			// Token: 0x040001D3 RID: 467
			private SortedDictionary<TKey, TValue> _dic;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			// Token: 0x020000A1 RID: 161
			public struct Enumerator : IEnumerator, IDisposable, IEnumerator<TKey>
			{
				// Token: 0x060006C0 RID: 1728 RVA: 0x00014FEC File Offset: 0x000131EC
				internal Enumerator(SortedDictionary<TKey, TValue> dic)
				{
					this.host = dic.tree.GetEnumerator();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17000156 RID: 342
				// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00015000 File Offset: 0x00013200
				object IEnumerator.Current
				{
					get
					{
						this.host.check_current();
						return this.current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x060006C2 RID: 1730 RVA: 0x00015018 File Offset: 0x00013218
				void IEnumerator.Reset()
				{
					this.host.Reset();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000157 RID: 343
				// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00015028 File Offset: 0x00013228
				public TKey Current
				{
					get
					{
						return this.current;
					}
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x060006C4 RID: 1732 RVA: 0x00015030 File Offset: 0x00013230
				public bool MoveNext()
				{
					if (!this.host.MoveNext())
					{
						return false;
					}
					this.current = ((SortedDictionary<TKey, TValue>.Node)this.host.Current).key;
					return true;
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x060006C5 RID: 1733 RVA: 0x0001506C File Offset: 0x0001326C
				public void Dispose()
				{
					this.host.Dispose();
				}

				// Token: 0x040001D4 RID: 468
				private RBTree.NodeEnumerator host;

				// Token: 0x040001D5 RID: 469
				private TKey current;
			}
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		// Token: 0x020000A2 RID: 162
		public struct Enumerator : IEnumerator, IDisposable, IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator
		{
			// Token: 0x060006C6 RID: 1734 RVA: 0x0001507C File Offset: 0x0001327C
			internal Enumerator(SortedDictionary<TKey, TValue> dic)
			{
				this.host = dic.tree.GetEnumerator();
			}

			/// <summary>Gets the element at the current position of the enumerator as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</summary>
			/// <returns>The element in the collection at the current position of the dictionary, as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00015090 File Offset: 0x00013290
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					return this.CurrentNode.AsDE();
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060006C8 RID: 1736 RVA: 0x000150A0 File Offset: 0x000132A0
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this.CurrentNode.key;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060006C9 RID: 1737 RVA: 0x000150B4 File Offset: 0x000132B4
			object IDictionaryEnumerator.Value
			{
				get
				{
					return this.CurrentNode.value;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060006CA RID: 1738 RVA: 0x000150C8 File Offset: 0x000132C8
			object IEnumerator.Current
			{
				get
				{
					return this.CurrentNode.AsDE();
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060006CB RID: 1739 RVA: 0x000150DC File Offset: 0x000132DC
			void IEnumerator.Reset()
			{
				this.host.Reset();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060006CC RID: 1740 RVA: 0x000150EC File Offset: 0x000132EC
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this.current;
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060006CD RID: 1741 RVA: 0x000150F4 File Offset: 0x000132F4
			public bool MoveNext()
			{
				if (!this.host.MoveNext())
				{
					return false;
				}
				this.current = ((SortedDictionary<TKey, TValue>.Node)this.host.Current).AsKV();
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" />.</summary>
			// Token: 0x060006CE RID: 1742 RVA: 0x00015130 File Offset: 0x00013330
			public void Dispose()
			{
				this.host.Dispose();
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060006CF RID: 1743 RVA: 0x00015140 File Offset: 0x00013340
			private SortedDictionary<TKey, TValue>.Node CurrentNode
			{
				get
				{
					this.host.check_current();
					return (SortedDictionary<TKey, TValue>.Node)this.host.Current;
				}
			}

			// Token: 0x040001D6 RID: 470
			private RBTree.NodeEnumerator host;

			// Token: 0x040001D7 RID: 471
			private KeyValuePair<TKey, TValue> current;
		}
	}
}
