using System;

namespace System.Security.Cryptography
{
	/// <summary>Contains a type and a collection of values associated with that type.</summary>
	// Token: 0x0200000C RID: 12
	public sealed class CryptographicAttributeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		// Token: 0x0600002C RID: 44 RVA: 0x00003E54 File Offset: 0x00002054
		public CryptographicAttributeObject(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._oid = new Oid(oid);
			this._list = new AsnEncodedDataCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object and the set of values associated with that attribute represented by the specified <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection.</summary>
		/// <param name="oid">An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		/// <param name="values">An <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection that stores the set of values associated with the attribute represented by the <paramref name="oid" /> parameter.</param>
		// Token: 0x0600002D RID: 45 RVA: 0x00003E90 File Offset: 0x00002090
		public CryptographicAttributeObject(Oid oid, AsnEncodedDataCollection values)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._oid = new Oid(oid);
			if (values == null)
			{
				this._list = new AsnEncodedDataCollection();
			}
			else
			{
				this._list = values;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Oid" /> object that specifies the object identifier for the attribute.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Oid" /> object that specifies the object identifier for the attribute.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003EE0 File Offset: 0x000020E0
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection that contains the set of values that are associated with the attribute.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection that contains the set of values that is associated with the attribute.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003EE8 File Offset: 0x000020E8
		public AsnEncodedDataCollection Values
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x04000036 RID: 54
		private Oid _oid;

		// Token: 0x04000037 RID: 55
		private AsnEncodedDataCollection _list;
	}
}
