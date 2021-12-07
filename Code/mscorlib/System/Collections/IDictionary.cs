using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Represents a nongeneric collection of key/value pairs.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C3 RID: 451
	[ComVisible(true)]
	public interface IDictionary : IEnumerable, ICollection
	{
		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001769 RID: 5993
		bool IsFixedSize { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600176A RID: 5994
		bool IsReadOnly { get; }

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039B RID: 923
		object this[object key]
		{
			get;
			set;
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600176D RID: 5997
		ICollection Keys { get; }

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600176E RID: 5998
		ICollection Values { get; }

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600176F RID: 5999
		void Add(object key, object value);

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001770 RID: 6000
		void Clear();

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001771 RID: 6001
		bool Contains(object key);

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001772 RID: 6002
		IDictionaryEnumerator GetEnumerator();

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001773 RID: 6003
		void Remove(object key);
	}
}
