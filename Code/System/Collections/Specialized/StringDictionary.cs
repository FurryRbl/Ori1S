using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Collections.Specialized
{
	/// <summary>Implements a hash table with the key and the value strongly typed to be strings rather than objects.</summary>
	// Token: 0x020000C1 RID: 193
	[System.ComponentModel.Design.Serialization.DesignerSerializer("System.Diagnostics.Design.StringDictionaryCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class StringDictionary : IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.StringDictionary" /> class.</summary>
		// Token: 0x06000869 RID: 2153 RVA: 0x00019444 File Offset: 0x00017644
		public StringDictionary()
		{
			this.contents = new Hashtable();
		}

		/// <summary>Gets the number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>The number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00019458 File Offset: 0x00017658
		public virtual int Count
		{
			get
			{
				return this.contents.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00019468 File Offset: 0x00017668
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, Get returns null, and Set creates a new entry with the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x170001D6 RID: 470
		public virtual string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key.ToLower(CultureInfo.InvariantCulture)];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key.ToLower(CultureInfo.InvariantCulture)] = value;
			}
		}

		/// <summary>Gets a collection of keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x000194E0 File Offset: 0x000176E0
		public virtual ICollection Keys
		{
			get
			{
				return this.contents.Keys;
			}
		}

		/// <summary>Gets a collection of values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000194F0 File Offset: 0x000176F0
		public virtual ICollection Values
		{
			get
			{
				return this.contents.Values;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00019500 File Offset: 0x00017700
		public virtual object SyncRoot
		{
			get
			{
				return this.contents.SyncRoot;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.StringDictionary" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x06000871 RID: 2161 RVA: 0x00019510 File Offset: 0x00017710
		public virtual void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key.ToLower(CultureInfo.InvariantCulture), value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x06000872 RID: 2162 RVA: 0x00019548 File Offset: 0x00017748
		public virtual void Clear()
		{
			this.contents.Clear();
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The key is null. </exception>
		// Token: 0x06000873 RID: 2163 RVA: 0x00019558 File Offset: 0x00017758
		public virtual bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key.ToLower(CultureInfo.InvariantCulture));
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />. The value can be null. </param>
		// Token: 0x06000874 RID: 2164 RVA: 0x00019584 File Offset: 0x00017784
		public virtual bool ContainsValue(string value)
		{
			return this.contents.ContainsValue(value);
		}

		/// <summary>Copies the string dictionary values to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the <see cref="T:System.Collections.Specialized.StringDictionary" />. </param>
		/// <param name="index">The index in the array where copying begins. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.Collections.Specialized.StringDictionary" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		// Token: 0x06000875 RID: 2165 RVA: 0x00019594 File Offset: 0x00017794
		public virtual void CopyTo(Array array, int index)
		{
			this.contents.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the string dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that iterates through the string dictionary.</returns>
		// Token: 0x06000876 RID: 2166 RVA: 0x000195A4 File Offset: 0x000177A4
		public virtual IEnumerator GetEnumerator()
		{
			return this.contents.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the string dictionary.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The key is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x06000877 RID: 2167 RVA: 0x000195B4 File Offset: 0x000177B4
		public virtual void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x04000234 RID: 564
		private Hashtable contents;
	}
}
