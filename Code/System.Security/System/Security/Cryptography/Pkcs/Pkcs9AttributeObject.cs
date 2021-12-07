using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>Represents an attribute used for CMS/PKCS #7 and PKCS #9 operations.</summary>
	// Token: 0x0200001F RID: 31
	public class Pkcs9AttributeObject : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class.</summary>
		// Token: 0x060000A7 RID: 167 RVA: 0x000050B4 File Offset: 0x000032B4
		public Pkcs9AttributeObject()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object as its attribute type and value.</summary>
		/// <param name="asnEncodedData">An object that contains the PKCS #9 attribute type and value to use.</param>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="Value" /> member of the <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is null.-or-The <paramref name="Value" /> member of the <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is null.</exception>
		// Token: 0x060000A8 RID: 168 RVA: 0x000050BC File Offset: 0x000032BC
		public Pkcs9AttributeObject(AsnEncodedData asnEncodedData) : base(asnEncodedData)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified <see cref="T:System.Security.Cryptography.Oid" /> object as the attribute type and a specified ASN.1 encoded data as the attribute value.</summary>
		/// <param name="oid">An object that represents the PKCS #9 attribute type.</param>
		/// <param name="encodedData">An array of byte values that represents the PKCS #9 attribute value.</param>
		// Token: 0x060000A9 RID: 169 RVA: 0x000050C8 File Offset: 0x000032C8
		public Pkcs9AttributeObject(Oid oid, byte[] encodedData)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			base.Oid = oid;
			base.RawData = encodedData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified string representation of an object identifier (OID) as the attribute type and a specified ASN.1 encoded data as the attribute value.</summary>
		/// <param name="oid">An object that contains the string representation of an OID that represents the PKCS #9 attribute type.</param>
		/// <param name="encodedData">An array of byte values that contains the PKCS #9 attribute value.</param>
		// Token: 0x060000AA RID: 170 RVA: 0x000050F0 File Offset: 0x000032F0
		public Pkcs9AttributeObject(string oid, byte[] encodedData) : base(oid, encodedData)
		{
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> object.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000050FC File Offset: 0x000032FC
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00005104 File Offset: 0x00003304
		public new Oid Oid
		{
			get
			{
				return base.Oid;
			}
			internal set
			{
				base.Oid = value;
			}
		}

		/// <summary>Copies a PKCS #9 attribute type and value for this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> from the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">An object that contains the PKCS #9 attribute type and value to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodeData" /> does not represent a compatible attribute type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		// Token: 0x060000AD RID: 173 RVA: 0x00005110 File Offset: 0x00003310
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			throw new ArgumentException("Cannot convert the PKCS#9 attribute.");
		}
	}
}
