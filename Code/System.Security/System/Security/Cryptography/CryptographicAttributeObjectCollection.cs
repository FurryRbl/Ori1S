using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Contains a set of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
	// Token: 0x0200000D RID: 13
	public sealed class CryptographicAttributeObjectCollection : IEnumerable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class.</summary>
		// Token: 0x06000030 RID: 48 RVA: 0x00003EF0 File Offset: 0x000020F0
		public CryptographicAttributeObjectCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class, adding a specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> to the collection.</summary>
		/// <param name="attribute">A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that is added to the collection.</param>
		// Token: 0x06000031 RID: 49 RVA: 0x00003F04 File Offset: 0x00002104
		public CryptographicAttributeObjectCollection(CryptographicAttributeObject attribute) : this()
		{
			this._list.Add(attribute);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an <see cref="T:System.Array" /> array, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> array that is the destination of the elements copied from this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" />. The <see cref="T:System.Array" /> array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000032 RID: 50 RVA: 0x00003F1C File Offset: 0x0000211C
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x06000033 RID: 51 RVA: 0x00003F2C File Offset: 0x0000212C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this._list);
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003F3C File Offset: 0x0000213C
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized, or thread safe.</summary>
		/// <returns>true if access to the collection is thread safe; otherwise false.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003F4C File Offset: 0x0000214C
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index.</returns>
		/// <param name="index">An <see cref="T:System.Int32" /> value that represents the zero-based index of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to retrieve.</param>
		// Token: 0x17000006 RID: 6
		public CryptographicAttributeObject this[int index]
		{
			get
			{
				return (CryptographicAttributeObject)this._list[index];
			}
		}

		/// <summary>Gets an <see cref="T:System.Object" /> object used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003F70 File Offset: 0x00002170
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the collection.</summary>
		/// <returns>true if the method returns the zero-based index of the added item; otherwise, false.</returns>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000038 RID: 56 RVA: 0x00003F74 File Offset: 0x00002174
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			AsnEncodedDataCollection values = new AsnEncodedDataCollection(asnEncodedData);
			return this.Add(new CryptographicAttributeObject(asnEncodedData.Oid, values));
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to the collection.</summary>
		/// <returns>true if the method returns the zero-based index of the added item; otherwise, false.</returns>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified item already exists in the collection.</exception>
		// Token: 0x06000039 RID: 57 RVA: 0x00003FAC File Offset: 0x000021AC
		public int Add(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			int num = -1;
			string value = attribute.Oid.Value;
			for (int i = 0; i < this._list.Count; i++)
			{
				if ((this._list[i] as CryptographicAttributeObject).Oid.Value == value)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				CryptographicAttributeObject cryptographicAttributeObject = this[num];
				foreach (AsnEncodedData asnEncodedData in attribute.Values)
				{
					cryptographicAttributeObject.Values.Add(asnEncodedData);
				}
				return num;
			}
			return this._list.Add(attribute);
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects that the collection is copied to.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> to which the collection is to be copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">null was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x0600003A RID: 58 RVA: 0x00004074 File Offset: 0x00002274
		public void CopyTo(CryptographicAttributeObject[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object for the collection.</summary>
		/// <returns>true if the method returns a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object that can be used to enumerate the collection; otherwise, false.</returns>
		// Token: 0x0600003B RID: 59 RVA: 0x00004084 File Offset: 0x00002284
		public CryptographicAttributeObjectEnumerator GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this._list);
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attribute" /> is null.</exception>
		// Token: 0x0600003C RID: 60 RVA: 0x00004094 File Offset: 0x00002294
		public void Remove(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			this._list.Remove(attribute);
		}

		// Token: 0x04000038 RID: 56
		private ArrayList _list;
	}
}
