using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed collection of key/value pairs.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B8 RID: 440
	[ComVisible(true)]
	[Serializable]
	public abstract class DictionaryBase : IEnumerable, ICollection, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.DictionaryBase" /> class.</summary>
		// Token: 0x060016DE RID: 5854 RVA: 0x00058D08 File Offset: 0x00056F08
		protected DictionaryBase()
		{
			this.hashtable = new Hashtable();
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00058D1C File Offset: 0x00056F1C
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> object is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00058D20 File Offset: 0x00056F20
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x17000372 RID: 882
		object IDictionary.this[object key]
		{
			get
			{
				object obj = this.hashtable[key];
				this.OnGet(key, obj);
				return obj;
			}
			set
			{
				this.OnValidate(key, value);
				object obj = this.hashtable[key];
				this.OnSet(key, obj, value);
				this.hashtable[key] = value;
				try
				{
					this.OnSetComplete(key, obj, value);
				}
				catch
				{
					this.hashtable[key] = obj;
					throw;
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00058DC0 File Offset: 0x00056FC0
		ICollection IDictionary.Keys
		{
			get
			{
				return this.hashtable.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00058DD0 File Offset: 0x00056FD0
		ICollection IDictionary.Values
		{
			get
			{
				return this.hashtable.Values;
			}
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.DictionaryBase" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x060016E5 RID: 5861 RVA: 0x00058DE0 File Offset: 0x00056FE0
		void IDictionary.Add(object key, object value)
		{
			this.OnValidate(key, value);
			this.OnInsert(key, value);
			this.hashtable.Add(key, value);
			try
			{
				this.OnInsertComplete(key, value);
			}
			catch
			{
				this.hashtable.Remove(key);
				throw;
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x060016E6 RID: 5862 RVA: 0x00058E48 File Offset: 0x00057048
		void IDictionary.Remove(object key)
		{
			if (!this.hashtable.Contains(key))
			{
				return;
			}
			object value = this.hashtable[key];
			this.OnValidate(key, value);
			this.OnRemove(key, value);
			this.hashtable.Remove(key);
			try
			{
				this.OnRemoveComplete(key, value);
			}
			catch
			{
				this.hashtable[key] = value;
				throw;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.DictionaryBase" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.DictionaryBase" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x060016E7 RID: 5863 RVA: 0x00058ED0 File Offset: 0x000570D0
		bool IDictionary.Contains(object key)
		{
			return this.hashtable.Contains(key);
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00058EE0 File Offset: 0x000570E0
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.hashtable.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00058EF0 File Offset: 0x000570F0
		object ICollection.SyncRoot
		{
			get
			{
				return this.hashtable.SyncRoot;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" />.</returns>
		// Token: 0x060016EA RID: 5866 RVA: 0x00058F00 File Offset: 0x00057100
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.hashtable.GetEnumerator();
		}

		/// <summary>Clears the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016EB RID: 5867 RVA: 0x00058F10 File Offset: 0x00057110
		public void Clear()
		{
			this.OnClear();
			this.hashtable.Clear();
			this.OnClearComplete();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00058F34 File Offset: 0x00057134
		public int Count
		{
			get
			{
				return this.hashtable.Count;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00058F44 File Offset: 0x00057144
		protected IDictionary Dictionary
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00058F48 File Offset: 0x00057148
		protected Hashtable InnerHashtable
		{
			get
			{
				return this.hashtable;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.DictionaryBase" /> elements to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from the <see cref="T:System.Collections.DictionaryBase" /> instance. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.DictionaryBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.DictionaryBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016EF RID: 5871 RVA: 0x00058F50 File Offset: 0x00057150
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be possitive");
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("array is multidimensional");
			}
			int length = array.Length;
			if (index > length)
			{
				throw new ArgumentException("index is larger than array size");
			}
			if (index + this.Count > length)
			{
				throw new ArgumentException("Copy will overlflow array");
			}
			this.DoCopy(array, index);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00058FD4 File Offset: 0x000571D4
		private void DoCopy(Array array, int index)
		{
			foreach (object obj in this.hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array.SetValue(dictionaryEntry, index++);
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016F1 RID: 5873 RVA: 0x00059050 File Offset: 0x00057250
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.hashtable.GetEnumerator();
		}

		/// <summary>Performs additional custom processes before clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x060016F2 RID: 5874 RVA: 0x00059060 File Offset: 0x00057260
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x060016F3 RID: 5875 RVA: 0x00059064 File Offset: 0x00057264
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Gets the element with the specified key and value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the element with the specified key and value.</returns>
		/// <param name="key">The key of the element to get. </param>
		/// <param name="currentValue">The current value of the element associated with <paramref name="key" />. </param>
		// Token: 0x060016F4 RID: 5876 RVA: 0x00059068 File Offset: 0x00057268
		protected virtual object OnGet(object key, object currentValue)
		{
			return currentValue;
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert. </param>
		/// <param name="value">The value of the element to insert. </param>
		// Token: 0x060016F5 RID: 5877 RVA: 0x0005906C File Offset: 0x0005726C
		protected virtual void OnInsert(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert. </param>
		/// <param name="value">The value of the element to insert. </param>
		// Token: 0x060016F6 RID: 5878 RVA: 0x00059070 File Offset: 0x00057270
		protected virtual void OnInsertComplete(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate. </param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
		// Token: 0x060016F7 RID: 5879 RVA: 0x00059074 File Offset: 0x00057274
		protected virtual void OnSet(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate. </param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
		// Token: 0x060016F8 RID: 5880 RVA: 0x00059078 File Offset: 0x00057278
		protected virtual void OnSetComplete(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes before removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <param name="value">The value of the element to remove. </param>
		// Token: 0x060016F9 RID: 5881 RVA: 0x0005907C File Offset: 0x0005727C
		protected virtual void OnRemove(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <param name="value">The value of the element to remove. </param>
		// Token: 0x060016FA RID: 5882 RVA: 0x00059080 File Offset: 0x00057280
		protected virtual void OnRemoveComplete(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes when validating the element with the specified key and value.</summary>
		/// <param name="key">The key of the element to validate. </param>
		/// <param name="value">The value of the element to validate. </param>
		// Token: 0x060016FB RID: 5883 RVA: 0x00059084 File Offset: 0x00057284
		protected virtual void OnValidate(object key, object value)
		{
		}

		// Token: 0x04000878 RID: 2168
		private Hashtable hashtable;
	}
}
