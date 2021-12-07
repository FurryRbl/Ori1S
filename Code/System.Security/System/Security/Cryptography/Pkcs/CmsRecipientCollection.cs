using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class represents a set of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface. </summary>
	// Token: 0x0200001C RID: 28
	public sealed class CmsRecipientCollection : IEnumerable, ICollection
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class.</summary>
		// Token: 0x06000085 RID: 133 RVA: 0x00004E2C File Offset: 0x0000302C
		public CmsRecipientCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.CmsRecipient)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class and adds the specified recipient.</summary>
		/// <param name="recipient">An instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class that represents the specified CMS/PKCS #7 recipient.</param>
		// Token: 0x06000086 RID: 134 RVA: 0x00004E40 File Offset: 0x00003040
		public CmsRecipientCollection(CmsRecipient recipient)
		{
			this._list.Add(recipient);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class and adds recipients based on the specified subject identifier and set of certificates that identify the recipients.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of subject identifier.</param>
		/// <param name="certificates">A collection that contains the certificates that identify the recipients.</param>
		// Token: 0x06000087 RID: 135 RVA: 0x00004E58 File Offset: 0x00003058
		public CmsRecipientCollection(SubjectIdentifierType recipientIdentifierType, X509Certificate2Collection certificates)
		{
			foreach (X509Certificate2 certificate in certificates)
			{
				CmsRecipient value = new CmsRecipient(recipientIdentifierType, certificate);
				this._list.Add(value);
			}
		}

		/// <summary>Returns a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x06000088 RID: 136 RVA: 0x00004EA0 File Offset: 0x000030A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CmsRecipientEnumerator(this._list);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the number of items in the collection.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004EB0 File Offset: 0x000030B0
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns false, which means that the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of false, which means that the collection is not thread safe.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004EC0 File Offset: 0x000030C0
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object at the specified index.</returns>
		/// <param name="index">An <see cref="T:System.Int32" /> value that represents the index in the collection. The index is zero based.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x17000024 RID: 36
		public CmsRecipient this[int index]
		{
			get
			{
				return (CmsRecipient)this._list[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004EE4 File Offset: 0x000030E4
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Add(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method adds a recipient to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>If the method succeeds, the method returns an <see cref="T:System.Int32" /> value that represents the zero-based position where the recipient is to be inserted.If the method fails, it throws an exception.</returns>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient to add to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="recipient" /> is null.</exception>
		// Token: 0x0600008D RID: 141 RVA: 0x00004EF4 File Offset: 0x000030F4
		public int Add(CmsRecipient recipient)
		{
			return this._list.Add(recipient);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to hold the specified elements.-or-<paramref name="array" /> does not contain the proper number of dimensions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of elements in <paramref name="array" />.</exception>
		// Token: 0x0600008E RID: 142 RVA: 0x00004F04 File Offset: 0x00003104
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Security.Cryptography.Pkcs.CmsRecipient[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index for the array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects in <paramref name="array" /> to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to hold the specified elements.-or-<paramref name="array" /> does not contain the proper number of dimensions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of elements in <paramref name="array" />.</exception>
		// Token: 0x0600008F RID: 143 RVA: 0x00004F14 File Offset: 0x00003114
		public void CopyTo(CmsRecipient[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x06000090 RID: 144 RVA: 0x00004F24 File Offset: 0x00003124
		public CmsRecipientEnumerator GetEnumerator()
		{
			return new CmsRecipientEnumerator(this._list);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Remove(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method removes a recipient from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="recipient" /> is null.</exception>
		// Token: 0x06000091 RID: 145 RVA: 0x00004F34 File Offset: 0x00003134
		public void Remove(CmsRecipient recipient)
		{
			this._list.Remove(recipient);
		}

		// Token: 0x04000065 RID: 101
		private ArrayList _list;
	}
}
