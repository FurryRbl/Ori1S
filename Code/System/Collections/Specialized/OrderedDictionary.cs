using System;
using System.Runtime.Serialization;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of key/value pairs that are accessible by the key or index.</summary>
	// Token: 0x020000BB RID: 187
	[Serializable]
	public class OrderedDictionary : IDictionary, ICollection, IEnumerable, IDeserializationCallback, IOrderedDictionary, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class.</summary>
		// Token: 0x0600080E RID: 2062 RVA: 0x000189B0 File Offset: 0x00016BB0
		public OrderedDictionary()
		{
			this.list = new ArrayList();
			this.hash = new Hashtable();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		// Token: 0x0600080F RID: 2063 RVA: 0x000189D0 File Offset: 0x00016BD0
		public OrderedDictionary(int capacity)
		{
			this.initialCapacity = ((capacity >= 0) ? capacity : 0);
			this.list = new ArrayList(this.initialCapacity);
			this.hash = new Hashtable(this.initialCapacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x06000810 RID: 2064 RVA: 0x00018A1C File Offset: 0x00016C1C
		public OrderedDictionary(IEqualityComparer equalityComparer)
		{
			this.list = new ArrayList();
			this.hash = new Hashtable(equalityComparer);
			this.comparer = equalityComparer;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity and comparer.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x06000811 RID: 2065 RVA: 0x00018A50 File Offset: 0x00016C50
		public OrderedDictionary(int capacity, IEqualityComparer equalityComparer)
		{
			this.initialCapacity = ((capacity >= 0) ? capacity : 0);
			this.list = new ArrayList(this.initialCapacity);
			this.hash = new Hashtable(this.initialCapacity, equalityComparer);
			this.comparer = equalityComparer;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		// Token: 0x06000812 RID: 2066 RVA: 0x00018AA4 File Offset: 0x00016CA4
		protected OrderedDictionary(SerializationInfo info, StreamingContext context)
		{
			this.serializationInfo = info;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x06000813 RID: 2067 RVA: 0x00018AB4 File Offset: 0x00016CB4
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (this.serializationInfo == null)
			{
				return;
			}
			this.comparer = (IEqualityComparer)this.serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
			this.readOnly = this.serializationInfo.GetBoolean("ReadOnly");
			this.initialCapacity = this.serializationInfo.GetInt32("InitialCapacity");
			if (this.list == null)
			{
				this.list = new ArrayList();
			}
			else
			{
				this.list.Clear();
			}
			this.hash = new Hashtable(this.comparer);
			object[] array = (object[])this.serializationInfo.GetValue("ArrayList", typeof(object[]));
			foreach (DictionaryEntry dictionaryEntry in array)
			{
				this.hash.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				this.list.Add(dictionaryEntry);
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x06000814 RID: 2068 RVA: 0x00018BC0 File Offset: 0x00016DC0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object is synchronized (thread-safe).</summary>
		/// <returns>This method always returns false.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00018BD0 File Offset: 0x00016DD0
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00018BE0 File Offset: 0x00016DE0
		object ICollection.SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00018BF0 File Offset: 0x00016DF0
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is invalid.</exception>
		// Token: 0x06000818 RID: 2072 RVA: 0x00018BF4 File Offset: 0x00016DF4
		protected virtual void OnDeserialization(object sender)
		{
			((IDeserializationCallback)this).OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000819 RID: 2073 RVA: 0x00018C00 File Offset: 0x00016E00
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("KeyComparer", this.comparer, typeof(IEqualityComparer));
			info.AddValue("ReadOnly", this.readOnly);
			info.AddValue("InitialCapacity", this.initialCapacity);
			object[] array = new object[this.hash.Count];
			this.hash.CopyTo(array, 0);
			info.AddValue("ArrayList", array);
		}

		/// <summary>Gets the number of key/values pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00018C88 File Offset: 0x00016E88
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> elements to a one-dimensional <see cref="T:System.Array" /> object at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600081B RID: 2075 RVA: 0x00018C98 File Offset: 0x00016E98
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00018CA8 File Offset: 0x00016EA8
		public bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x170001BA RID: 442
		public object this[object key]
		{
			get
			{
				return this.hash[key];
			}
			set
			{
				this.WriteCheck();
				if (this.hash.Contains(key))
				{
					int index = this.FindListEntry(key);
					this.list[index] = new DictionaryEntry(key, value);
				}
				else
				{
					this.list.Add(new DictionaryEntry(key, value));
				}
				this.hash[key] = value;
			}
		}

		/// <summary>Gets or sets the value at the specified index.</summary>
		/// <returns>The value of the item at the specified index. </returns>
		/// <param name="index">The zero-based index of the value to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x170001BB RID: 443
		public object this[int index]
		{
			get
			{
				return ((DictionaryEntry)this.list[index]).Value;
			}
			set
			{
				this.WriteCheck();
				DictionaryEntry dictionaryEntry = (DictionaryEntry)this.list[index];
				dictionaryEntry.Value = value;
				this.list[index] = dictionaryEntry;
				this.hash[dictionaryEntry.Key] = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00018DAC File Offset: 0x00016FAC
		public ICollection Keys
		{
			get
			{
				return new OrderedDictionary.OrderedCollection(this.list, true);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00018DBC File Offset: 0x00016FBC
		public ICollection Values
		{
			get
			{
				return new OrderedDictionary.OrderedCollection(this.list, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the lowest available index.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. This value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x06000823 RID: 2083 RVA: 0x00018DCC File Offset: 0x00016FCC
		public void Add(object key, object value)
		{
			this.WriteCheck();
			this.hash.Add(key, value);
			this.list.Add(new DictionaryEntry(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x06000824 RID: 2084 RVA: 0x00018DFC File Offset: 0x00016FFC
		public void Clear()
		{
			this.WriteCheck();
			this.hash.Clear();
			this.list.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		// Token: 0x06000825 RID: 2085 RVA: 0x00018E1C File Offset: 0x0001701C
		public bool Contains(object key)
		{
			return this.hash.Contains(key);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x06000826 RID: 2086 RVA: 0x00018E2C File Offset: 0x0001702C
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new OrderedDictionary.OrderedEntryCollectionEnumerator(this.list.GetEnumerator());
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06000827 RID: 2087 RVA: 0x00018E40 File Offset: 0x00017040
		public void Remove(object key)
		{
			this.WriteCheck();
			if (this.hash.Contains(key))
			{
				this.hash.Remove(key);
				int index = this.FindListEntry(key);
				this.list.RemoveAt(index);
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00018E84 File Offset: 0x00017084
		private int FindListEntry(object key)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)this.list[i];
				if ((this.comparer == null) ? dictionaryEntry.Key.Equals(key) : this.comparer.Equals(dictionaryEntry.Key, key))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00018EF8 File Offset: 0x000170F8
		private void WriteCheck()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException("Collection is read only");
			}
		}

		/// <summary>Returns a read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>A read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x0600082A RID: 2090 RVA: 0x00018F10 File Offset: 0x00017110
		public OrderedDictionary AsReadOnly()
		{
			return new OrderedDictionary
			{
				list = this.list,
				hash = this.hash,
				comparer = this.comparer,
				readOnly = true
			};
		}

		/// <summary>Inserts a new entry into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the specified key and value at the specified index.</summary>
		/// <param name="index">The zero-based index at which the element should be inserted.</param>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be null.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is out of range.</exception>
		/// <exception cref="T:System.NotSupportedException">This collection is read-only.</exception>
		// Token: 0x0600082B RID: 2091 RVA: 0x00018F50 File Offset: 0x00017150
		public void Insert(int index, object key, object value)
		{
			this.WriteCheck();
			this.hash.Add(key, value);
			this.list.Insert(index, new DictionaryEntry(key, value));
		}

		/// <summary>Removes the entry at the specified index from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="index">The zero-based index of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x0600082C RID: 2092 RVA: 0x00018F88 File Offset: 0x00017188
		public void RemoveAt(int index)
		{
			this.WriteCheck();
			DictionaryEntry dictionaryEntry = (DictionaryEntry)this.list[index];
			this.list.RemoveAt(index);
			this.hash.Remove(dictionaryEntry.Key);
		}

		// Token: 0x04000227 RID: 551
		private ArrayList list;

		// Token: 0x04000228 RID: 552
		private Hashtable hash;

		// Token: 0x04000229 RID: 553
		private bool readOnly;

		// Token: 0x0400022A RID: 554
		private int initialCapacity;

		// Token: 0x0400022B RID: 555
		private SerializationInfo serializationInfo;

		// Token: 0x0400022C RID: 556
		private IEqualityComparer comparer;

		// Token: 0x020000BC RID: 188
		private class OrderedEntryCollectionEnumerator : IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x0600082D RID: 2093 RVA: 0x00018FCC File Offset: 0x000171CC
			public OrderedEntryCollectionEnumerator(IEnumerator listEnumerator)
			{
				this.listEnumerator = listEnumerator;
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x00018FDC File Offset: 0x000171DC
			public bool MoveNext()
			{
				return this.listEnumerator.MoveNext();
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x00018FEC File Offset: 0x000171EC
			public void Reset()
			{
				this.listEnumerator.Reset();
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000830 RID: 2096 RVA: 0x00018FFC File Offset: 0x000171FC
			public object Current
			{
				get
				{
					return this.listEnumerator.Current;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001900C File Offset: 0x0001720C
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.listEnumerator.Current;
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000832 RID: 2098 RVA: 0x00019020 File Offset: 0x00017220
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001903C File Offset: 0x0001723C
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x0400022D RID: 557
			private IEnumerator listEnumerator;
		}

		// Token: 0x020000BD RID: 189
		private class OrderedCollection : ICollection, IEnumerable
		{
			// Token: 0x06000834 RID: 2100 RVA: 0x00019058 File Offset: 0x00017258
			public OrderedCollection(ArrayList list, bool isKeyList)
			{
				this.list = list;
				this.isKeyList = isKeyList;
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000835 RID: 2101 RVA: 0x00019070 File Offset: 0x00017270
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000836 RID: 2102 RVA: 0x00019080 File Offset: 0x00017280
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000837 RID: 2103 RVA: 0x00019084 File Offset: 0x00017284
			public object SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06000838 RID: 2104 RVA: 0x00019094 File Offset: 0x00017294
			public void CopyTo(Array array, int index)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.list[i];
					if (this.isKeyList)
					{
						array.SetValue(dictionaryEntry.Key, index + i);
					}
					else
					{
						array.SetValue(dictionaryEntry.Value, index + i);
					}
				}
			}

			// Token: 0x06000839 RID: 2105 RVA: 0x00019100 File Offset: 0x00017300
			public IEnumerator GetEnumerator()
			{
				return new OrderedDictionary.OrderedCollection.OrderedCollectionEnumerator(this.list.GetEnumerator(), this.isKeyList);
			}

			// Token: 0x0400022E RID: 558
			private ArrayList list;

			// Token: 0x0400022F RID: 559
			private bool isKeyList;

			// Token: 0x020000BE RID: 190
			private class OrderedCollectionEnumerator : IEnumerator
			{
				// Token: 0x0600083A RID: 2106 RVA: 0x00019118 File Offset: 0x00017318
				public OrderedCollectionEnumerator(IEnumerator listEnumerator, bool isKeyList)
				{
					this.listEnumerator = listEnumerator;
					this.isKeyList = isKeyList;
				}

				// Token: 0x170001C5 RID: 453
				// (get) Token: 0x0600083B RID: 2107 RVA: 0x00019130 File Offset: 0x00017330
				public object Current
				{
					get
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this.listEnumerator.Current;
						return (!this.isKeyList) ? dictionaryEntry.Value : dictionaryEntry.Key;
					}
				}

				// Token: 0x0600083C RID: 2108 RVA: 0x0001916C File Offset: 0x0001736C
				public bool MoveNext()
				{
					return this.listEnumerator.MoveNext();
				}

				// Token: 0x0600083D RID: 2109 RVA: 0x0001917C File Offset: 0x0001737C
				public void Reset()
				{
					this.listEnumerator.Reset();
				}

				// Token: 0x04000230 RID: 560
				private bool isKeyList;

				// Token: 0x04000231 RID: 561
				private IEnumerator listEnumerator;
			}
		}
	}
}
