using System;
using System.Collections;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface. </summary>
	// Token: 0x0200002C RID: 44
	public sealed class SignerInfoCollection : IEnumerable, ICollection
	{
		// Token: 0x0600010E RID: 270 RVA: 0x0000608C File Offset: 0x0000428C
		internal SignerInfoCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x0600010F RID: 271 RVA: 0x000060A0 File Offset: 0x000042A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SignerInfoEnumerator(this._list);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000060B0 File Offset: 0x000042B0
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of false, which means the collection is not thread safe.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000060C0 File Offset: 0x000042C0
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object  at the specified index.</returns>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x17000050 RID: 80
		public SignerInfo this[int index]
		{
			get
			{
				return (SignerInfo)this._list[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000060D8 File Offset: 0x000042D8
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000060E8 File Offset: 0x000042E8
		internal void Add(SignerInfo signer)
		{
			this._list.Add(signer);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000115 RID: 277 RVA: 0x000060F8 File Offset: 0x000042F8
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.SignerInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000116 RID: 278 RVA: 0x00006144 File Offset: 0x00004344
		public void CopyTo(SignerInfo[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x06000117 RID: 279 RVA: 0x00006180 File Offset: 0x00004380
		public SignerInfoEnumerator GetEnumerator()
		{
			return new SignerInfoEnumerator(this._list);
		}

		// Token: 0x04000095 RID: 149
		private ArrayList _list;
	}
}
