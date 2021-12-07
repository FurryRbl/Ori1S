using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> classes used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200003F RID: 63
	public sealed class EncryptionPropertyCollection : IEnumerable, IList, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> class.</summary>
		// Token: 0x060001B0 RID: 432 RVA: 0x00008CE0 File Offset: 0x00006EE0
		public EncryptionPropertyCollection()
		{
			this.list = new ArrayList();
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x17000076 RID: 118
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (EncryptionProperty)value;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x060001B3 RID: 435 RVA: 0x00008D10 File Offset: 0x00006F10
		bool IList.Contains(object value)
		{
			return this.Contains((EncryptionProperty)value);
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x060001B4 RID: 436 RVA: 0x00008D20 File Offset: 0x00006F20
		int IList.Add(object value)
		{
			return this.Add((EncryptionProperty)value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x060001B5 RID: 437 RVA: 0x00008D30 File Offset: 0x00006F30
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EncryptionProperty)value);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x060001B6 RID: 438 RVA: 0x00008D40 File Offset: 0x00006F40
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EncryptionProperty)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x060001B7 RID: 439 RVA: 0x00008D50 File Offset: 0x00006F50
		void IList.Remove(object value)
		{
			this.Remove((EncryptionProperty)value);
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008D60 File Offset: 0x00006F60
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size; otherwise, false.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008D70 File Offset: 0x00006F70
		public bool IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only; otherwise, false. </returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008D80 File Offset: 0x00006F80
		public bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008D90 File Offset: 0x00006F90
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		// Token: 0x1700007B RID: 123
		[IndexerName("ItemOf")]
		public EncryptionProperty this[int index]
		{
			get
			{
				return (EncryptionProperty)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The position at which the new element is inserted.</returns>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x060001BF RID: 447 RVA: 0x00008DD4 File Offset: 0x00006FD4
		public int Add(EncryptionProperty value)
		{
			return this.list.Add(value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		// Token: 0x060001C0 RID: 448 RVA: 0x00008DE4 File Offset: 0x00006FE4
		public void Clear()
		{
			this.list.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object contains a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object is found in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. </param>
		// Token: 0x060001C1 RID: 449 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public bool Contains(EncryptionProperty value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x060001C2 RID: 450 RVA: 0x00008E04 File Offset: 0x00007004
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060001C3 RID: 451 RVA: 0x00008E14 File Offset: 0x00007014
		public void CopyTo(EncryptionProperty[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x060001C4 RID: 452 RVA: 0x00008E24 File Offset: 0x00007024
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x060001C5 RID: 453 RVA: 0x00008E34 File Offset: 0x00007034
		public int IndexOf(EncryptionProperty value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to insert into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x060001C6 RID: 454 RVA: 0x00008E44 File Offset: 0x00007044
		public void Insert(int index, EncryptionProperty value)
		{
			this.list.Insert(index, value);
		}

		/// <summary>Returns the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		// Token: 0x060001C7 RID: 455 RVA: 0x00008E54 File Offset: 0x00007054
		public EncryptionProperty Item(int index)
		{
			return (EncryptionProperty)this.list[index];
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to remove from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x060001C8 RID: 456 RVA: 0x00008E68 File Offset: 0x00007068
		public void Remove(EncryptionProperty value)
		{
			this.list.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects to remove.</param>
		// Token: 0x060001C9 RID: 457 RVA: 0x00008E78 File Offset: 0x00007078
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x040000E1 RID: 225
		private ArrayList list;
	}
}
