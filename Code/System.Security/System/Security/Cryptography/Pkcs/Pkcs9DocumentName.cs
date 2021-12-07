using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class defines the name of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000022 RID: 34
	public sealed class Pkcs9DocumentName : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class.</summary>
		// Token: 0x060000BC RID: 188 RVA: 0x000053F0 File Offset: 0x000035F0
		public Pkcs9DocumentName()
		{
			this.Oid = new Oid("1.3.6.1.4.1.311.88.2.1", null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.String)" /> constructor creates an instance of the  <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified name for the CMS/PKCS #7 message.</summary>
		/// <param name="documentName">A  <see cref="T:System.String" />   object that specifies the name for the CMS/PKCS #7 message.</param>
		// Token: 0x060000BD RID: 189 RVA: 0x0000540C File Offset: 0x0000360C
		public Pkcs9DocumentName(string documentName)
		{
			if (documentName == null)
			{
				throw new ArgumentNullException("documentName");
			}
			this.Oid = new Oid("1.3.6.1.4.1.311.88.2.1", null);
			this._name = documentName;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified array of byte values as the encoded name of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentName">An array of byte values that specifies the encoded name of the CMS/PKCS #7 message.</param>
		// Token: 0x060000BE RID: 190 RVA: 0x00005454 File Offset: 0x00003654
		public Pkcs9DocumentName(byte[] encodedDocumentName)
		{
			if (encodedDocumentName == null)
			{
				throw new ArgumentNullException("encodedDocumentName");
			}
			this.Oid = new Oid("1.3.6.1.4.1.311.88.2.1", null);
			base.RawData = encodedDocumentName;
			this.Decode(encodedDocumentName);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.DocumentName" /> property retrieves the document name.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document name.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005498 File Offset: 0x00003698
		public string DocumentName
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000C0 RID: 192 RVA: 0x000054A0 File Offset: 0x000036A0
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.Decode(base.RawData);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000054B8 File Offset: 0x000036B8
		internal void Decode(byte[] attribute)
		{
			if (attribute[0] != 4)
			{
				return;
			}
			ASN1 asn = new ASN1(attribute);
			byte[] value = asn.Value;
			int num = value.Length;
			if (value[num - 2] == 0)
			{
				num -= 2;
			}
			this._name = Encoding.Unicode.GetString(value, 0, num);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005504 File Offset: 0x00003704
		internal byte[] Encode()
		{
			ASN1 asn = new ASN1(4, Encoding.Unicode.GetBytes(this._name + '\0'));
			return asn.GetBytes();
		}

		// Token: 0x04000075 RID: 117
		internal const string oid = "1.3.6.1.4.1.311.88.2.1";

		// Token: 0x04000076 RID: 118
		internal const string friendlyName = null;

		// Token: 0x04000077 RID: 119
		private string _name;
	}
}
