using System;
using System.Collections;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface. </summary>
	// Token: 0x02000027 RID: 39
	public sealed class RecipientInfoCollection : IEnumerable, ICollection
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000058E0 File Offset: 0x00003AE0
		internal RecipientInfoCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x060000DA RID: 218 RVA: 0x000058F4 File Offset: 0x00003AF4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RecipientInfoEnumerator(this._list);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005904 File Offset: 0x00003B04
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of false, which means the collection is not thread safe.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005914 File Offset: 0x00003B14
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index.</returns>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x1700003E RID: 62
		public RecipientInfo this[int index]
		{
			get
			{
				return (RecipientInfo)this._list[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" />  object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005938 File Offset: 0x00003B38
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005948 File Offset: 0x00003B48
		internal int Add(RecipientInfo ri)
		{
			return this._list.Add(ri);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which  the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x060000E0 RID: 224 RVA: 0x00005958 File Offset: 0x00003B58
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.RecipientInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x060000E1 RID: 225 RVA: 0x00005968 File Offset: 0x00003B68
		public void CopyTo(RecipientInfo[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x060000E2 RID: 226 RVA: 0x00005978 File Offset: 0x00003B78
		public RecipientInfoEnumerator GetEnumerator()
		{
			return new RecipientInfoEnumerator(this._list);
		}

		// Token: 0x04000082 RID: 130
		private ArrayList _list;
	}
}
