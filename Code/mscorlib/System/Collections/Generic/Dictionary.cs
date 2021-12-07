using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of keys and values.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006BD RID: 1725
	[DebuggerDisplay("Count={Count}")]
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView<, >))]
	[Serializable]
	public class Dictionary<TKey, TValue> : IEnumerable, ISerializable, ICollection, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, IDictionary, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.</summary>
		// Token: 0x060041EE RID: 16878 RVA: 0x000E2458 File Offset: 0x000E0658
		public Dictionary()
		{
			this.Init(10, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		// Token: 0x060041EF RID: 16879 RVA: 0x000E246C File Offset: 0x000E066C
		public Dictionary(IEqualityComparer<TKey> comparer)
		{
			this.Init(10, comparer);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default equality comparer for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060041F0 RID: 16880 RVA: 0x000E2480 File Offset: 0x000E0680
		public Dictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x060041F1 RID: 16881 RVA: 0x000E248C File Offset: 0x000E068C
		public Dictionary(int capacity)
		{
			this.Init(capacity, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060041F2 RID: 16882 RVA: 0x000E249C File Offset: 0x000E069C
		public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			int capacity = dictionary.Count;
			this.Init(capacity, comparer);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x060041F3 RID: 16883 RVA: 0x000E2530 File Offset: 0x000E0730
		public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			this.Init(capacity, comparer);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class with serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		// Token: 0x060041F4 RID: 16884 RVA: 0x000E2540 File Offset: 0x000E0740
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			this.serialization_info = info;
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x000E2550 File Offset: 0x000E0750
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x000E2558 File Offset: 0x000E0758
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x000E2560 File Offset: 0x000E0760
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x000E2568 File Offset: 0x000E0768
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x000E2570 File Offset: 0x000E0770
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x000E2574 File Offset: 0x000E0774
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <returns>The value associated with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x17000C45 RID: 3141
		object IDictionary.this[object key]
		{
			get
			{
				if (key is TKey && this.ContainsKey((TKey)((object)key)))
				{
					return this[this.ToTKey(key)];
				}
				return null;
			}
			set
			{
				this[this.ToTKey(key)] = this.ToTValue(value);
			}
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The object to use as the key.</param>
		/// <param name="value">The object to use as the value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-<paramref name="value" /> is of a type that is not assignable to <paramref name="TValue" />, the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x060041FD RID: 16893 RVA: 0x000E25D0 File Offset: 0x000E07D0
		void IDictionary.Add(object key, object value)
		{
			this.Add(this.ToTKey(key), this.ToTValue(value));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060041FE RID: 16894 RVA: 0x000E25E8 File Offset: 0x000E07E8
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060041FF RID: 16895 RVA: 0x000E2620 File Offset: 0x000E0820
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key is TKey)
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x000E264C File Offset: 0x000E084C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. </returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x000E2650 File Offset: 0x000E0850
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x000E2654 File Offset: 0x000E0854
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000E2658 File Offset: 0x000E0858
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x000E2670 File Offset: 0x000E0870
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			return this.ContainsKeyValuePair(keyValuePair);
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000E267C File Offset: 0x000E087C
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000E2688 File Offset: 0x000E0888
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			return this.ContainsKeyValuePair(keyValuePair) && this.Remove(keyValuePair.Key);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.Generic.ICollection`1" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06004207 RID: 16903 RVA: 0x000E26A8 File Offset: 0x000E08A8
		void ICollection.CopyTo(Array array, int index)
		{
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			this.CopyToCheck(array, index);
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				this.Do_CopyTo<DictionaryEntry, DictionaryEntry>(array3, index, (TKey key, TValue value) => new DictionaryEntry(key, value));
				return;
			}
			this.Do_ICollectionCopyTo<KeyValuePair<TKey, TValue>>(array, index, new Dictionary<TKey, TValue>.Transform<KeyValuePair<TKey, TValue>>(Dictionary<TKey, TValue>.make_pair));
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06004208 RID: 16904 RVA: 0x000E271C File Offset: 0x000E091C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x000E272C File Offset: 0x000E092C
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x0600420A RID: 16906 RVA: 0x000E273C File Offset: 0x000E093C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.ShimEnumerator(this);
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x000E2744 File Offset: 0x000E0944
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000C4A RID: 3146
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = this.hcp.GetHashCode(key) | int.MinValue;
				for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
				{
					if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
					{
						return this.valueSlots[num2];
					}
				}
				throw new KeyNotFoundException();
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = this.hcp.GetHashCode(key) | int.MinValue;
				int num2 = (num & int.MaxValue) % this.table.Length;
				int num3 = this.table[num2] - 1;
				int num4 = -1;
				if (num3 != -1)
				{
					while (this.linkSlots[num3].HashCode != num || !this.hcp.Equals(this.keySlots[num3], key))
					{
						num4 = num3;
						num3 = this.linkSlots[num3].Next;
						if (num3 == -1)
						{
							break;
						}
					}
				}
				if (num3 == -1)
				{
					if (++this.count > this.threshold)
					{
						this.Resize();
						num2 = (num & int.MaxValue) % this.table.Length;
					}
					num3 = this.emptySlot;
					if (num3 == -1)
					{
						num3 = this.touchedSlots++;
					}
					else
					{
						this.emptySlot = this.linkSlots[num3].Next;
					}
					this.linkSlots[num3].Next = this.table[num2] - 1;
					this.table[num2] = num3 + 1;
					this.linkSlots[num3].HashCode = num;
					this.keySlots[num3] = key;
				}
				else if (num4 != -1)
				{
					this.linkSlots[num4].Next = this.linkSlots[num3].Next;
					this.linkSlots[num3].Next = this.table[num2] - 1;
					this.table[num2] = num3 + 1;
				}
				this.valueSlots[num3] = value;
				this.generation++;
			}
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x000E29E0 File Offset: 0x000E0BE0
		private void Init(int capacity, IEqualityComparer<TKey> hcp)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.hcp = ((hcp == null) ? EqualityComparer<TKey>.Default : hcp);
			if (capacity == 0)
			{
				capacity = 10;
			}
			capacity = (int)((float)capacity / 0.9f) + 1;
			this.InitArrays(capacity);
			this.generation = 0;
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x000E2A40 File Offset: 0x000E0C40
		private void InitArrays(int size)
		{
			this.table = new int[size];
			this.linkSlots = new Link[size];
			this.emptySlot = -1;
			this.keySlots = new TKey[size];
			this.valueSlots = new TValue[size];
			this.touchedSlots = 0;
			this.threshold = (int)((float)this.table.Length * 0.9f);
			if (this.threshold == 0 && this.table.Length > 0)
			{
				this.threshold = 1;
			}
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x000E2AC4 File Offset: 0x000E0CC4
		private void CopyToCheck(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index > array.Length)
			{
				throw new ArgumentException("index larger than largest valid index of array");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array cannot hold the requested elements!");
			}
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000E2B2C File Offset: 0x000E0D2C
		private void Do_CopyTo<TRet, TElem>(TElem[] array, int index, Dictionary<TKey, TValue>.Transform<TRet> transform) where TRet : TElem
		{
			for (int i = 0; i < this.touchedSlots; i++)
			{
				if ((this.linkSlots[i].HashCode & -2147483648) != 0)
				{
					array[index++] = (TElem)((object)transform(this.keySlots[i], this.valueSlots[i]));
				}
			}
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x000E2BA0 File Offset: 0x000E0DA0
		private static KeyValuePair<TKey, TValue> make_pair(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x000E2BAC File Offset: 0x000E0DAC
		private static TKey pick_key(TKey key, TValue value)
		{
			return key;
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000E2BB0 File Offset: 0x000E0DB0
		private static TValue pick_value(TKey key, TValue value)
		{
			return value;
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000E2BB4 File Offset: 0x000E0DB4
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyToCheck(array, index);
			this.Do_CopyTo<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>>(array, index, new Dictionary<TKey, TValue>.Transform<KeyValuePair<TKey, TValue>>(Dictionary<TKey, TValue>.make_pair));
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x000E2BE0 File Offset: 0x000E0DE0
		private void Do_ICollectionCopyTo<TRet>(Array array, int index, Dictionary<TKey, TValue>.Transform<TRet> transform)
		{
			Type typeFromHandle = typeof(TRet);
			Type elementType = array.GetType().GetElementType();
			try
			{
				if ((typeFromHandle.IsPrimitive || elementType.IsPrimitive) && !elementType.IsAssignableFrom(typeFromHandle))
				{
					throw new Exception();
				}
				this.Do_CopyTo<TRet, object>((object[])array, index, transform);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Cannot copy source collection elements to destination array", "array", innerException);
			}
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000E2C74 File Offset: 0x000E0E74
		private void Resize()
		{
			int num = Hashtable.ToPrime(this.table.Length << 1 | 1);
			int[] array = new int[num];
			Link[] array2 = new Link[num];
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num2 = this.table[i] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
				{
					int num3 = array2[num2].HashCode = (this.hcp.GetHashCode(this.keySlots[num2]) | int.MinValue);
					int num4 = (num3 & int.MaxValue) % num;
					array2[num2].Next = array[num4] - 1;
					array[num4] = num2 + 1;
				}
			}
			this.table = array;
			this.linkSlots = array2;
			TKey[] destinationArray = new TKey[num];
			TValue[] destinationArray2 = new TValue[num];
			Array.Copy(this.keySlots, 0, destinationArray, 0, this.touchedSlots);
			Array.Copy(this.valueSlots, 0, destinationArray2, 0, this.touchedSlots);
			this.keySlots = destinationArray;
			this.valueSlots = destinationArray2;
			this.threshold = (int)((float)num * 0.9f);
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x06004218 RID: 16920 RVA: 0x000E2DA8 File Offset: 0x000E0FA8
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & int.MaxValue) % this.table.Length;
			int num3;
			for (num3 = this.table[num2] - 1; num3 != -1; num3 = this.linkSlots[num3].Next)
			{
				if (this.linkSlots[num3].HashCode == num && this.hcp.Equals(this.keySlots[num3], key))
				{
					throw new ArgumentException("An element with the same key already exists in the dictionary.");
				}
			}
			if (++this.count > this.threshold)
			{
				this.Resize();
				num2 = (num & int.MaxValue) % this.table.Length;
			}
			num3 = this.emptySlot;
			if (num3 == -1)
			{
				num3 = this.touchedSlots++;
			}
			else
			{
				this.emptySlot = this.linkSlots[num3].Next;
			}
			this.linkSlots[num3].HashCode = num;
			this.linkSlots[num3].Next = this.table[num2] - 1;
			this.table[num2] = num3 + 1;
			this.keySlots[num3] = key;
			this.valueSlots[num3] = value;
			this.generation++;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> that is used to determine equality of keys for the dictionary. </summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface implementation that is used to determine equality of keys for the current <see cref="T:System.Collections.Generic.Dictionary`2" /> and to provide hash values for the keys.</returns>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x000E2F28 File Offset: 0x000E1128
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this.hcp;
			}
		}

		/// <summary>Removes all keys and values from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		// Token: 0x0600421A RID: 16922 RVA: 0x000E2F30 File Offset: 0x000E1130
		public void Clear()
		{
			this.count = 0;
			Array.Clear(this.table, 0, this.table.Length);
			Array.Clear(this.keySlots, 0, this.keySlots.Length);
			Array.Clear(this.valueSlots, 0, this.valueSlots.Length);
			Array.Clear(this.linkSlots, 0, this.linkSlots.Length);
			this.emptySlot = -1;
			this.touchedSlots = 0;
			this.generation++;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600421B RID: 16923 RVA: 0x000E2FB0 File Offset: 0x000E11B0
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
			{
				if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />. The value can be null for reference types.</param>
		// Token: 0x0600421C RID: 16924 RVA: 0x000E3058 File Offset: 0x000E1258
		public bool ContainsValue(TValue value)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num = this.table[i] - 1; num != -1; num = this.linkSlots[num].Next)
				{
					if (@default.Equals(this.valueSlots[num], value))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600421D RID: 16925 RVA: 0x000E30C8 File Offset: 0x000E12C8
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this.generation);
			info.AddValue("Comparer", this.hcp);
			KeyValuePair<TKey, TValue>[] array = null;
			if (this.count > 0)
			{
				array = new KeyValuePair<TKey, TValue>[this.count];
				this.CopyTo(array, 0);
			}
			info.AddValue("HashSize", this.table.Length);
			info.AddValue("KeyValuePairs", array);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.Dictionary`2" /> instance is invalid.</exception>
		// Token: 0x0600421E RID: 16926 RVA: 0x000E314C File Offset: 0x000E134C
		public virtual void OnDeserialization(object sender)
		{
			if (this.serialization_info == null)
			{
				return;
			}
			this.generation = this.serialization_info.GetInt32("Version");
			this.hcp = (IEqualityComparer<TKey>)this.serialization_info.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			int num = this.serialization_info.GetInt32("HashSize");
			KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])this.serialization_info.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
			if (num < 10)
			{
				num = 10;
			}
			this.InitArrays(num);
			this.count = 0;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i].Key, array[i].Value);
				}
			}
			this.generation++;
			this.serialization_info = null;
		}

		/// <summary>Removes the value with the specified key from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600421F RID: 16927 RVA: 0x000E3238 File Offset: 0x000E1438
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & int.MaxValue) % this.table.Length;
			int num3 = this.table[num2] - 1;
			if (num3 == -1)
			{
				return false;
			}
			int num4 = -1;
			while (this.linkSlots[num3].HashCode != num || !this.hcp.Equals(this.keySlots[num3], key))
			{
				num4 = num3;
				num3 = this.linkSlots[num3].Next;
				if (num3 == -1)
				{
					IL_A4:
					if (num3 == -1)
					{
						return false;
					}
					this.count--;
					if (num4 == -1)
					{
						this.table[num2] = this.linkSlots[num3].Next + 1;
					}
					else
					{
						this.linkSlots[num4].Next = this.linkSlots[num3].Next;
					}
					this.linkSlots[num3].Next = this.emptySlot;
					this.emptySlot = num3;
					this.linkSlots[num3].HashCode = 0;
					this.keySlots[num3] = default(TKey);
					this.valueSlots[num3] = default(TValue);
					this.generation++;
					return true;
				}
			}
			goto IL_A4;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004220 RID: 16928 RVA: 0x000E33B4 File Offset: 0x000E15B4
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
			{
				if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
				{
					value = this.valueSlots[num2];
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x000E347C File Offset: 0x000E167C
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				return new Dictionary<TKey, TValue>.KeyCollection(this);
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x000E3484 File Offset: 0x000E1684
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				return new Dictionary<TKey, TValue>.ValueCollection(this);
			}
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x000E348C File Offset: 0x000E168C
		private TKey ToTKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException("not of type: " + typeof(TKey).ToString(), "key");
			}
			return (TKey)((object)key);
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000E34E0 File Offset: 0x000E16E0
		private TValue ToTValue(object value)
		{
			if (value == null && !typeof(TValue).IsValueType)
			{
				return default(TValue);
			}
			if (!(value is TValue))
			{
				throw new ArgumentException("not of type: " + typeof(TValue).ToString(), "value");
			}
			return (TValue)((object)value);
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x000E3548 File Offset: 0x000E1748
		private bool ContainsKeyValuePair(KeyValuePair<TKey, TValue> pair)
		{
			TValue y;
			return this.TryGetValue(pair.Key, out y) && EqualityComparer<TValue>.Default.Equals(pair.Value, y);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x06004226 RID: 16934 RVA: 0x000E3580 File Offset: 0x000E1780
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x04001C2C RID: 7212
		private const int INITIAL_SIZE = 10;

		// Token: 0x04001C2D RID: 7213
		private const float DEFAULT_LOAD_FACTOR = 0.9f;

		// Token: 0x04001C2E RID: 7214
		private const int NO_SLOT = -1;

		// Token: 0x04001C2F RID: 7215
		private const int HASH_FLAG = -2147483648;

		// Token: 0x04001C30 RID: 7216
		private int[] table;

		// Token: 0x04001C31 RID: 7217
		private Link[] linkSlots;

		// Token: 0x04001C32 RID: 7218
		private TKey[] keySlots;

		// Token: 0x04001C33 RID: 7219
		private TValue[] valueSlots;

		// Token: 0x04001C34 RID: 7220
		private int touchedSlots;

		// Token: 0x04001C35 RID: 7221
		private int emptySlot;

		// Token: 0x04001C36 RID: 7222
		private int count;

		// Token: 0x04001C37 RID: 7223
		private int threshold;

		// Token: 0x04001C38 RID: 7224
		private IEqualityComparer<TKey> hcp;

		// Token: 0x04001C39 RID: 7225
		private SerializationInfo serialization_info;

		// Token: 0x04001C3A RID: 7226
		private int generation;

		// Token: 0x020006BE RID: 1726
		[Serializable]
		private class ShimEnumerator : IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06004228 RID: 16936 RVA: 0x000E359C File Offset: 0x000E179C
			public ShimEnumerator(Dictionary<TKey, TValue> host)
			{
				this.host_enumerator = host.GetEnumerator();
			}

			// Token: 0x06004229 RID: 16937 RVA: 0x000E35B0 File Offset: 0x000E17B0
			public void Dispose()
			{
				this.host_enumerator.Dispose();
			}

			// Token: 0x0600422A RID: 16938 RVA: 0x000E35C0 File Offset: 0x000E17C0
			public bool MoveNext()
			{
				return this.host_enumerator.MoveNext();
			}

			// Token: 0x17000C4E RID: 3150
			// (get) Token: 0x0600422B RID: 16939 RVA: 0x000E35D0 File Offset: 0x000E17D0
			public DictionaryEntry Entry
			{
				get
				{
					return ((IDictionaryEnumerator)this.host_enumerator).Entry;
				}
			}

			// Token: 0x17000C4F RID: 3151
			// (get) Token: 0x0600422C RID: 16940 RVA: 0x000E35E4 File Offset: 0x000E17E4
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.host_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17000C50 RID: 3152
			// (get) Token: 0x0600422D RID: 16941 RVA: 0x000E360C File Offset: 0x000E180C
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.host_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17000C51 RID: 3153
			// (get) Token: 0x0600422E RID: 16942 RVA: 0x000E3634 File Offset: 0x000E1834
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x0600422F RID: 16943 RVA: 0x000E3644 File Offset: 0x000E1844
			public void Reset()
			{
				this.host_enumerator.Reset();
			}

			// Token: 0x04001C3C RID: 7228
			private Dictionary<TKey, TValue>.Enumerator host_enumerator;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		// Token: 0x020006BF RID: 1727
		[Serializable]
		public struct Enumerator : IEnumerator, IDisposable, IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator
		{
			// Token: 0x06004230 RID: 16944 RVA: 0x000E3654 File Offset: 0x000E1854
			internal Enumerator(Dictionary<TKey, TValue> dictionary)
			{
				this.dictionary = dictionary;
				this.stamp = dictionary.generation;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000C52 RID: 3154
			// (get) Token: 0x06004231 RID: 16945 RVA: 0x000E366C File Offset: 0x000E186C
			object IEnumerator.Current
			{
				get
				{
					this.VerifyCurrent();
					return this.current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06004232 RID: 16946 RVA: 0x000E3680 File Offset: 0x000E1880
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the dictionary at the current position of the enumerator, as a <see cref="T:System.Collections.DictionaryEntry" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000C53 RID: 3155
			// (get) Token: 0x06004233 RID: 16947 RVA: 0x000E3688 File Offset: 0x000E1888
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					this.VerifyCurrent();
					return new DictionaryEntry(this.current.Key, this.current.Value);
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000C54 RID: 3156
			// (get) Token: 0x06004234 RID: 16948 RVA: 0x000E36C0 File Offset: 0x000E18C0
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this.CurrentKey;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000C55 RID: 3157
			// (get) Token: 0x06004235 RID: 16949 RVA: 0x000E36D0 File Offset: 0x000E18D0
			object IDictionaryEnumerator.Value
			{
				get
				{
					return this.CurrentValue;
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06004236 RID: 16950 RVA: 0x000E36E0 File Offset: 0x000E18E0
			public bool MoveNext()
			{
				this.VerifyState();
				if (this.next < 0)
				{
					return false;
				}
				while (this.next < this.dictionary.touchedSlots)
				{
					int num = this.next++;
					if ((this.dictionary.linkSlots[num].HashCode & -2147483648) != 0)
					{
						this.current = new KeyValuePair<TKey, TValue>(this.dictionary.keySlots[num], this.dictionary.valueSlots[num]);
						return true;
					}
				}
				this.next = -1;
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x17000C56 RID: 3158
			// (get) Token: 0x06004237 RID: 16951 RVA: 0x000E3788 File Offset: 0x000E1988
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000C57 RID: 3159
			// (get) Token: 0x06004238 RID: 16952 RVA: 0x000E3790 File Offset: 0x000E1990
			internal TKey CurrentKey
			{
				get
				{
					this.VerifyCurrent();
					return this.current.Key;
				}
			}

			// Token: 0x17000C58 RID: 3160
			// (get) Token: 0x06004239 RID: 16953 RVA: 0x000E37A4 File Offset: 0x000E19A4
			internal TValue CurrentValue
			{
				get
				{
					this.VerifyCurrent();
					return this.current.Value;
				}
			}

			// Token: 0x0600423A RID: 16954 RVA: 0x000E37B8 File Offset: 0x000E19B8
			internal void Reset()
			{
				this.VerifyState();
				this.next = 0;
			}

			// Token: 0x0600423B RID: 16955 RVA: 0x000E37C8 File Offset: 0x000E19C8
			private void VerifyState()
			{
				if (this.dictionary == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.dictionary.generation != this.stamp)
				{
					throw new InvalidOperationException("out of sync");
				}
			}

			// Token: 0x0600423C RID: 16956 RVA: 0x000E3800 File Offset: 0x000E1A00
			private void VerifyCurrent()
			{
				this.VerifyState();
				if (this.next <= 0)
				{
					throw new InvalidOperationException("Current is not valid");
				}
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" />.</summary>
			// Token: 0x0600423D RID: 16957 RVA: 0x000E3820 File Offset: 0x000E1A20
			public void Dispose()
			{
				this.dictionary = null;
			}

			// Token: 0x04001C3D RID: 7229
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x04001C3E RID: 7230
			private int next;

			// Token: 0x04001C3F RID: 7231
			private int stamp;

			// Token: 0x04001C40 RID: 7232
			internal KeyValuePair<TKey, TValue> current;
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited. </summary>
		// Token: 0x020006C0 RID: 1728
		[DebuggerTypeProxy(typeof(CollectionDebuggerView<, >))]
		[DebuggerDisplay("Count={Count}")]
		[Serializable]
		public sealed class KeyCollection : IEnumerable, ICollection, ICollection<TKey>, IEnumerable<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x0600423E RID: 16958 RVA: 0x000E382C File Offset: 0x000E1A2C
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			// Token: 0x0600423F RID: 16959 RVA: 0x000E384C File Offset: 0x000E1A4C
			void ICollection<!0>.Add(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x06004240 RID: 16960 RVA: 0x000E3858 File Offset: 0x000E1A58
			void ICollection<!0>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x06004241 RID: 16961 RVA: 0x000E3864 File Offset: 0x000E1A64
			bool ICollection<!0>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x06004242 RID: 16962 RVA: 0x000E3874 File Offset: 0x000E1A74
			bool ICollection<!0>.Remove(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x06004243 RID: 16963 RVA: 0x000E3880 File Offset: 0x000E1A80
			IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06004244 RID: 16964 RVA: 0x000E3890 File Offset: 0x000E1A90
			void ICollection.CopyTo(Array array, int index)
			{
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_ICollectionCopyTo<TKey>(array, index, new Dictionary<TKey, TValue>.Transform<TKey>(Dictionary<TKey, TValue>.pick_key));
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06004245 RID: 16965 RVA: 0x000E38DC File Offset: 0x000E1ADC
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000C59 RID: 3161
			// (get) Token: 0x06004246 RID: 16966 RVA: 0x000E38EC File Offset: 0x000E1AEC
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns false.</returns>
			// Token: 0x17000C5A RID: 3162
			// (get) Token: 0x06004247 RID: 16967 RVA: 0x000E38F0 File Offset: 0x000E1AF0
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000C5B RID: 3163
			// (get) Token: 0x06004248 RID: 16968 RVA: 0x000E38F4 File Offset: 0x000E1AF4
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06004249 RID: 16969 RVA: 0x000E3904 File Offset: 0x000E1B04
			public void CopyTo(TKey[] array, int index)
			{
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_CopyTo<TKey, TKey>(array, index, new Dictionary<TKey, TValue>.Transform<TKey>(Dictionary<TKey, TValue>.pick_key));
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</returns>
			// Token: 0x0600424A RID: 16970 RVA: 0x000E3938 File Offset: 0x000E1B38
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.Retrieving the value of this property is an O(1) operation.</returns>
			// Token: 0x17000C5C RID: 3164
			// (get) Token: 0x0600424B RID: 16971 RVA: 0x000E3948 File Offset: 0x000E1B48
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x04001C41 RID: 7233
			private Dictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			// Token: 0x020006C1 RID: 1729
			[Serializable]
			public struct Enumerator : IEnumerator, IDisposable, IEnumerator<TKey>
			{
				// Token: 0x0600424C RID: 16972 RVA: 0x000E3958 File Offset: 0x000E1B58
				internal Enumerator(Dictionary<TKey, TValue> host)
				{
					this.host_enumerator = host.GetEnumerator();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17000C5D RID: 3165
				// (get) Token: 0x0600424D RID: 16973 RVA: 0x000E3968 File Offset: 0x000E1B68
				object IEnumerator.Current
				{
					get
					{
						return this.host_enumerator.CurrentKey;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x0600424E RID: 16974 RVA: 0x000E397C File Offset: 0x000E1B7C
				void IEnumerator.Reset()
				{
					this.host_enumerator.Reset();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x0600424F RID: 16975 RVA: 0x000E398C File Offset: 0x000E1B8C
				public void Dispose()
				{
					this.host_enumerator.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06004250 RID: 16976 RVA: 0x000E399C File Offset: 0x000E1B9C
				public bool MoveNext()
				{
					return this.host_enumerator.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000C5E RID: 3166
				// (get) Token: 0x06004251 RID: 16977 RVA: 0x000E39AC File Offset: 0x000E1BAC
				public TKey Current
				{
					get
					{
						return this.host_enumerator.current.Key;
					}
				}

				// Token: 0x04001C42 RID: 7234
				private Dictionary<TKey, TValue>.Enumerator host_enumerator;
			}
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited. </summary>
		// Token: 0x020006C2 RID: 1730
		[DebuggerDisplay("Count={Count}")]
		[DebuggerTypeProxy(typeof(CollectionDebuggerView<, >))]
		[Serializable]
		public sealed class ValueCollection : IEnumerable, ICollection, ICollection<TValue>, IEnumerable<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x06004252 RID: 16978 RVA: 0x000E39C0 File Offset: 0x000E1BC0
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			// Token: 0x06004253 RID: 16979 RVA: 0x000E39E0 File Offset: 0x000E1BE0
			void ICollection<!1>.Add(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x06004254 RID: 16980 RVA: 0x000E39EC File Offset: 0x000E1BEC
			void ICollection<!1>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x06004255 RID: 16981 RVA: 0x000E39F8 File Offset: 0x000E1BF8
			bool ICollection<!1>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x06004256 RID: 16982 RVA: 0x000E3A08 File Offset: 0x000E1C08
			bool ICollection<!1>.Remove(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x06004257 RID: 16983 RVA: 0x000E3A14 File Offset: 0x000E1C14
			IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06004258 RID: 16984 RVA: 0x000E3A24 File Offset: 0x000E1C24
			void ICollection.CopyTo(Array array, int index)
			{
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_ICollectionCopyTo<TValue>(array, index, new Dictionary<TKey, TValue>.Transform<TValue>(Dictionary<TKey, TValue>.pick_value));
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06004259 RID: 16985 RVA: 0x000E3A70 File Offset: 0x000E1C70
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000C5F RID: 3167
			// (get) Token: 0x0600425A RID: 16986 RVA: 0x000E3A80 File Offset: 0x000E1C80
			bool ICollection<!1>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns false.</returns>
			// Token: 0x17000C60 RID: 3168
			// (get) Token: 0x0600425B RID: 16987 RVA: 0x000E3A84 File Offset: 0x000E1C84
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000C61 RID: 3169
			// (get) Token: 0x0600425C RID: 16988 RVA: 0x000E3A88 File Offset: 0x000E1C88
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x0600425D RID: 16989 RVA: 0x000E3A98 File Offset: 0x000E1C98
			public void CopyTo(TValue[] array, int index)
			{
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_CopyTo<TValue, TValue>(array, index, new Dictionary<TKey, TValue>.Transform<TValue>(Dictionary<TKey, TValue>.pick_value));
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x0600425E RID: 16990 RVA: 0x000E3ACC File Offset: 0x000E1CCC
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x17000C62 RID: 3170
			// (get) Token: 0x0600425F RID: 16991 RVA: 0x000E3ADC File Offset: 0x000E1CDC
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x04001C43 RID: 7235
			private Dictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			// Token: 0x020006C3 RID: 1731
			[Serializable]
			public struct Enumerator : IEnumerator, IDisposable, IEnumerator<TValue>
			{
				// Token: 0x06004260 RID: 16992 RVA: 0x000E3AEC File Offset: 0x000E1CEC
				internal Enumerator(Dictionary<TKey, TValue> host)
				{
					this.host_enumerator = host.GetEnumerator();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17000C63 RID: 3171
				// (get) Token: 0x06004261 RID: 16993 RVA: 0x000E3AFC File Offset: 0x000E1CFC
				object IEnumerator.Current
				{
					get
					{
						return this.host_enumerator.CurrentValue;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06004262 RID: 16994 RVA: 0x000E3B10 File Offset: 0x000E1D10
				void IEnumerator.Reset()
				{
					this.host_enumerator.Reset();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x06004263 RID: 16995 RVA: 0x000E3B20 File Offset: 0x000E1D20
				public void Dispose()
				{
					this.host_enumerator.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06004264 RID: 16996 RVA: 0x000E3B30 File Offset: 0x000E1D30
				public bool MoveNext()
				{
					return this.host_enumerator.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000C64 RID: 3172
				// (get) Token: 0x06004265 RID: 16997 RVA: 0x000E3B40 File Offset: 0x000E1D40
				public TValue Current
				{
					get
					{
						return this.host_enumerator.current.Value;
					}
				}

				// Token: 0x04001C44 RID: 7236
				private Dictionary<TKey, TValue>.Enumerator host_enumerator;
			}
		}

		// Token: 0x020006E5 RID: 1765
		// (Invoke) Token: 0x06004388 RID: 17288
		private delegate TRet Transform<TRet>(TKey key, TValue value);
	}
}
