using System;

namespace System.Collections.Specialized
{
	/// <summary>Implements IDictionary by using a <see cref="T:System.Collections.Specialized.ListDictionary" /> while the collection is small, and then switching to a <see cref="T:System.Collections.Hashtable" /> when the collection gets large.</summary>
	// Token: 0x020000AF RID: 175
	[Serializable]
	public class HybridDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty case-sensitive <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		// Token: 0x0600077B RID: 1915 RVA: 0x0001703C File Offset: 0x0001523C
		public HybridDictionary() : this(0, false)
		{
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified case sensitivity.</summary>
		/// <param name="caseInsensitive">A Boolean that denotes whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is case-insensitive. </param>
		// Token: 0x0600077C RID: 1916 RVA: 0x00017048 File Offset: 0x00015248
		public HybridDictionary(bool caseInsensitive) : this(0, caseInsensitive)
		{
		}

		/// <summary>Creates a case-sensitive <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified initial size.</summary>
		/// <param name="initialSize">The approximate number of entries that the <see cref="T:System.Collections.Specialized.HybridDictionary" /> can initially contain. </param>
		// Token: 0x0600077D RID: 1917 RVA: 0x00017054 File Offset: 0x00015254
		public HybridDictionary(int initialSize) : this(initialSize, false)
		{
		}

		/// <summary>Creates a <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified initial size and case sensitivity.</summary>
		/// <param name="initialSize">The approximate number of entries that the <see cref="T:System.Collections.Specialized.HybridDictionary" /> can initially contain. </param>
		/// <param name="caseInsensitive">A Boolean that denotes whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is case-insensitive. </param>
		// Token: 0x0600077E RID: 1918 RVA: 0x00017060 File Offset: 0x00015260
		public HybridDictionary(int initialSize, bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
			IComparer comparer = (!caseInsensitive) ? null : CaseInsensitiveComparer.DefaultInvariant;
			IHashCodeProvider hcp = (!caseInsensitive) ? null : CaseInsensitiveHashCodeProvider.DefaultInvariant;
			if (initialSize <= 10)
			{
				this.list = new ListDictionary(comparer);
			}
			else
			{
				this.hashtable = new Hashtable(initialSize, hcp, comparer);
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x0600077F RID: 1919 RVA: 0x000170C8 File Offset: 0x000152C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x000170D0 File Offset: 0x000152D0
		private IDictionary inner
		{
			get
			{
				IDictionary result;
				if (this.list == null)
				{
					IDictionary dictionary = this.hashtable;
					result = dictionary;
				}
				else
				{
					result = this.list;
				}
				return result;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000170FC File Offset: 0x000152FC
		public int Count
		{
			get
			{
				return this.inner.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001710C File Offset: 0x0001530C
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00017110 File Offset: 0x00015310
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00017114 File Offset: 0x00015314
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new entry using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x1700018F RID: 399
		public object this[object key]
		{
			get
			{
				return this.inner[key];
			}
			set
			{
				this.inner[key] = value;
				if (this.list != null && this.Count > 10)
				{
					this.Switch();
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00017158 File Offset: 0x00015358
		public ICollection Keys
		{
			get
			{
				return this.inner.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00017168 File Offset: 0x00015368
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001716C File Offset: 0x0001536C
		public ICollection Values
		{
			get
			{
				return this.inner.Values;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.HybridDictionary" />. </exception>
		// Token: 0x0600078A RID: 1930 RVA: 0x0001717C File Offset: 0x0001537C
		public void Add(object key, object value)
		{
			this.inner.Add(key, value);
			if (this.list != null && this.Count > 10)
			{
				this.Switch();
			}
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		// Token: 0x0600078B RID: 1931 RVA: 0x000171AC File Offset: 0x000153AC
		public void Clear()
		{
			this.inner.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.HybridDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x0600078C RID: 1932 RVA: 0x000171BC File Offset: 0x000153BC
		public bool Contains(object key)
		{
			return this.inner.Contains(key);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.HybridDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.HybridDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x0600078D RID: 1933 RVA: 0x000171CC File Offset: 0x000153CC
		public void CopyTo(Array array, int index)
		{
			this.inner.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x0600078E RID: 1934 RVA: 0x000171DC File Offset: 0x000153DC
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x0600078F RID: 1935 RVA: 0x000171EC File Offset: 0x000153EC
		public void Remove(object key)
		{
			this.inner.Remove(key);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000171FC File Offset: 0x000153FC
		private void Switch()
		{
			IComparer comparer = (!this.caseInsensitive) ? null : CaseInsensitiveComparer.DefaultInvariant;
			IHashCodeProvider hcp = (!this.caseInsensitive) ? null : CaseInsensitiveHashCodeProvider.DefaultInvariant;
			this.hashtable = new Hashtable(this.list, hcp, comparer);
			this.list.Clear();
			this.list = null;
		}

		// Token: 0x04000203 RID: 515
		private const int switchAfter = 10;

		// Token: 0x04000204 RID: 516
		private bool caseInsensitive;

		// Token: 0x04000205 RID: 517
		private Hashtable hashtable;

		// Token: 0x04000206 RID: 518
		private ListDictionary list;
	}
}
