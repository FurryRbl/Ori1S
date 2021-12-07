using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Provides the abstract base class for a collection whose keys are embedded in the values.</summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TItem">The type of items in the collection.</typeparam>
	// Token: 0x020006D1 RID: 1745
	[ComVisible(false)]
	[Serializable]
	public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the default equality comparer.</summary>
		// Token: 0x0600430E RID: 17166 RVA: 0x000E54B8 File Offset: 0x000E36B8
		protected KeyedCollection() : this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		// Token: 0x0600430F RID: 17167 RVA: 0x000E54C4 File Offset: 0x000E36C4
		protected KeyedCollection(IEqualityComparer<TKey> comparer) : this(comparer, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer and creates a lookup dictionary when the specified threshold is exceeded.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		/// <param name="dictionaryCreationThreshold">The number of elements the collection can hold without creating a lookup dictionary (0 creates the lookup dictionary when the first item is added), or –1 to specify that a lookup dictionary is never created.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dictionaryCreationThreshold" /> is less than –1.</exception>
		// Token: 0x06004310 RID: 17168 RVA: 0x000E54D0 File Offset: 0x000E36D0
		protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
			else
			{
				this.comparer = EqualityComparer<TKey>.Default;
			}
			this.dictionaryCreationThreshold = dictionaryCreationThreshold;
			if (dictionaryCreationThreshold == 0)
			{
				this.dictionary = new Dictionary<TKey, TItem>(this.comparer);
			}
		}

		/// <summary>Determines whether the collection contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004311 RID: 17169 RVA: 0x000E5520 File Offset: 0x000E3720
		public bool Contains(TKey key)
		{
			if (this.dictionary != null)
			{
				return this.dictionary.ContainsKey(key);
			}
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000E5548 File Offset: 0x000E3748
		private int IndexOfKey(TKey key)
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				TKey keyForItem = this.GetKeyForItem(this[i]);
				if (this.comparer.Equals(key, keyForItem))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004313 RID: 17171 RVA: 0x000E5594 File Offset: 0x000E3794
		public bool Remove(TKey key)
		{
			if (this.dictionary != null)
			{
				TItem item;
				return this.dictionary.TryGetValue(key, out item) && base.Remove(item);
			}
			int num = this.IndexOfKey(key);
			if (num == -1)
			{
				return false;
			}
			this.RemoveAt(num);
			return true;
		}

		/// <summary>Gets the generic equality comparer that is used to determine equality of keys in the collection.</summary>
		/// <returns>The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface that is used to determine equality of keys in the collection.</returns>
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x000E55E4 File Offset: 0x000E37E4
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets the element with the specified key. </summary>
		/// <returns>The element with the specified key. If an element with the specified key is not found, an exception is thrown.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">An element with the specified key does not exist in the collection.</exception>
		// Token: 0x17000C82 RID: 3202
		public TItem this[TKey key]
		{
			get
			{
				if (this.dictionary != null)
				{
					return this.dictionary[key];
				}
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return base[num];
				}
				throw new KeyNotFoundException();
			}
		}

		/// <summary>Changes the key associated with the specified element in the lookup dictionary.</summary>
		/// <param name="item">The element to change the key of.</param>
		/// <param name="newKey">The new key for <paramref name="item" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.-or-<paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is not found.-or-<paramref name="key" /> already exists in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</exception>
		// Token: 0x06004316 RID: 17174 RVA: 0x000E5630 File Offset: 0x000E3830
		protected void ChangeItemKey(TItem item, TKey newKey)
		{
			if (!this.Contains(item))
			{
				throw new ArgumentException();
			}
			TKey keyForItem = this.GetKeyForItem(item);
			if (this.comparer.Equals(keyForItem, newKey))
			{
				return;
			}
			if (this.Contains(newKey))
			{
				throw new ArgumentException();
			}
			if (this.dictionary != null)
			{
				if (!this.dictionary.Remove(keyForItem))
				{
					throw new ArgumentException();
				}
				this.dictionary.Add(newKey, item);
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		// Token: 0x06004317 RID: 17175 RVA: 0x000E56AC File Offset: 0x000E38AC
		protected override void ClearItems()
		{
			if (this.dictionary != null)
			{
				this.dictionary.Clear();
			}
			base.ClearItems();
		}

		/// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
		/// <returns>The key for the specified element.</returns>
		/// <param name="item">The element from which to extract the key.</param>
		// Token: 0x06004318 RID: 17176
		protected abstract TKey GetKeyForItem(TItem item);

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06004319 RID: 17177 RVA: 0x000E56CC File Offset: 0x000E38CC
		protected override void InsertItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			if (keyForItem == null)
			{
				throw new ArgumentNullException("GetKeyForItem(item)");
			}
			if (this.dictionary != null && this.dictionary.ContainsKey(keyForItem))
			{
				throw new ArgumentException("An element with the same key already exists in the dictionary.");
			}
			if (this.dictionary == null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.comparer.Equals(keyForItem, this.GetKeyForItem(this[i])))
					{
						throw new ArgumentException("An element with the same key already exists in the dictionary.");
					}
				}
			}
			base.InsertItem(index, item);
			if (this.dictionary != null)
			{
				this.dictionary.Add(keyForItem, item);
			}
			else if (this.dictionaryCreationThreshold != -1 && this.Count > this.dictionaryCreationThreshold)
			{
				this.dictionary = new Dictionary<TKey, TItem>(this.comparer);
				for (int j = 0; j < this.Count; j++)
				{
					TItem titem = this[j];
					this.dictionary.Add(this.GetKeyForItem(titem), titem);
				}
			}
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <param name="index">The index of the element to remove.</param>
		// Token: 0x0600431A RID: 17178 RVA: 0x000E57F0 File Offset: 0x000E39F0
		protected override void RemoveItem(int index)
		{
			if (this.dictionary != null)
			{
				TKey keyForItem = this.GetKeyForItem(this[index]);
				this.dictionary.Remove(keyForItem);
			}
			base.RemoveItem(index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to be replaced.</param>
		/// <param name="item">The new item.</param>
		// Token: 0x0600431B RID: 17179 RVA: 0x000E582C File Offset: 0x000E3A2C
		protected override void SetItem(int index, TItem item)
		{
			if (this.dictionary != null)
			{
				this.dictionary.Remove(this.GetKeyForItem(this[index]));
				this.dictionary.Add(this.GetKeyForItem(item), item);
			}
			base.SetItem(index, item);
		}

		/// <summary>Gets the lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <returns>The lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />, if it exists; otherwise, null.</returns>
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000E5878 File Offset: 0x000E3A78
		protected IDictionary<TKey, TItem> Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x04001C55 RID: 7253
		private Dictionary<TKey, TItem> dictionary;

		// Token: 0x04001C56 RID: 7254
		private IEqualityComparer<TKey> comparer;

		// Token: 0x04001C57 RID: 7255
		private int dictionaryCreationThreshold;
	}
}
