using System;
using System.Collections;
using Mono.Security;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200044D RID: 1101
	public sealed class X509ExtensionCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> class. </summary>
		// Token: 0x060027BB RID: 10171 RVA: 0x0007D248 File Offset: 0x0007B448
		public X509ExtensionCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0007D25C File Offset: 0x0007B45C
		internal X509ExtensionCollection(X509Certificate cert)
		{
			this._list = new ArrayList(cert.Extensions.Count);
			if (cert.Extensions.Count == 0)
			{
				return;
			}
			object[] array = new object[2];
			foreach (object obj in cert.Extensions)
			{
				X509Extension x509Extension = (X509Extension)obj;
				bool critical = x509Extension.Critical;
				string oid = x509Extension.Oid;
				byte[] rawData = null;
				ASN1 value = x509Extension.Value;
				if (value.Tag == 4 && value.Count > 0)
				{
					rawData = value[0].GetBytes();
				}
				array[0] = new AsnEncodedData(oid, rawData);
				array[1] = critical;
				X509Extension x509Extension2 = (X509Extension)CryptoConfig.CreateFromName(oid, array);
				if (x509Extension2 == null)
				{
					x509Extension2 = new X509Extension(oid, rawData, critical);
				}
				this._list.Add(x509Extension2);
			}
		}

		/// <summary>Copies the collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. </param>
		/// <param name="index">The location in the array at which copying starts. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array. </exception>
		// Token: 0x060027BD RID: 10173 RVA: 0x0007D384 File Offset: 0x0007B584
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index >= array.Length");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x060027BE RID: 10174 RVA: 0x0007D3D8 File Offset: 0x0007B5D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ExtensionEnumerator(this._list);
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An integer representing the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x0007D3E8 File Offset: 0x0007B5E8
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>true if the collection is thread safe; otherwise, false.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x0007D3F8 File Offset: 0x0007B5F8
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets an object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x0007D408 File Offset: 0x0007B608
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object at the specified index.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to retrieve. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is equal to or greater than the length of the array. </exception>
		// Token: 0x17000B2B RID: 2859
		public X509Extension this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException("index");
				}
				return (X509Extension)this._list[index];
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object whose value or friendly name is specified by an object identifier (OID).</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		/// <param name="oid">The object identifier (OID) of the extension to retrieve. </param>
		// Token: 0x17000B2C RID: 2860
		public X509Extension this[string oid]
		{
			get
			{
				if (oid == null)
				{
					throw new ArgumentNullException("oid");
				}
				if (this._list.Count == 0 || oid.Length == 0)
				{
					return null;
				}
				foreach (object obj in this._list)
				{
					X509Extension x509Extension = (X509Extension)obj;
					if (x509Extension.Oid.Value.Equals(oid))
					{
						return x509Extension;
					}
				}
				return null;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>The index at which the <paramref name="extension" /> parameter was added.</returns>
		/// <param name="extension">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" />  object to add to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="extension" /> parameter is null.</exception>
		// Token: 0x060027C4 RID: 10180 RVA: 0x0007D4EC File Offset: 0x0007B6EC
		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return this._list.Add(extension);
		}

		/// <summary>Copies a collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. </param>
		/// <param name="index">The location in the array at which copying starts. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array. </exception>
		// Token: 0x060027C5 RID: 10181 RVA: 0x0007D50C File Offset: 0x0007B70C
		public void CopyTo(X509Extension[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index >= array.Length");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x060027C6 RID: 10182 RVA: 0x0007D560 File Offset: 0x0007B760
		public X509ExtensionEnumerator GetEnumerator()
		{
			return new X509ExtensionEnumerator(this._list);
		}

		// Token: 0x04001864 RID: 6244
		private ArrayList _list;
	}
}
