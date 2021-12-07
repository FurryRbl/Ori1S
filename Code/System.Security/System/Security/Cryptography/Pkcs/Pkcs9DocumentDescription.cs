using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class defines the description of the content of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000021 RID: 33
	public sealed class Pkcs9DocumentDescription : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class.</summary>
		// Token: 0x060000B5 RID: 181 RVA: 0x000052A4 File Offset: 0x000034A4
		public Pkcs9DocumentDescription()
		{
			this.Oid = new Oid("1.3.6.1.4.1.311.88.2.2", null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.String)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="documentDescription">An instance of the <see cref="T:System.String" />  class that specifies the description for the CMS/PKCS #7 message.</param>
		// Token: 0x060000B6 RID: 182 RVA: 0x000052C0 File Offset: 0x000034C0
		public Pkcs9DocumentDescription(string documentDescription)
		{
			if (documentDescription == null)
			{
				throw new ArgumentNullException("documentName");
			}
			this.Oid = new Oid("1.3.6.1.4.1.311.88.2.2", null);
			this._desc = documentDescription;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified array of byte values as the encoded description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentDescription">An array of byte values that specifies the encoded description of the CMS/PKCS #7 message.</param>
		// Token: 0x060000B7 RID: 183 RVA: 0x00005308 File Offset: 0x00003508
		public Pkcs9DocumentDescription(byte[] encodedDocumentDescription)
		{
			if (encodedDocumentDescription == null)
			{
				throw new ArgumentNullException("encodedDocumentDescription");
			}
			this.Oid = new Oid("1.3.6.1.4.1.311.88.2.2", null);
			base.RawData = encodedDocumentDescription;
			this.Decode(encodedDocumentDescription);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.DocumentDescription" /> property retrieves the document description.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document description.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000534C File Offset: 0x0000354C
		public string DocumentDescription
		{
			get
			{
				return this._desc;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000B9 RID: 185 RVA: 0x00005354 File Offset: 0x00003554
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.Decode(base.RawData);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000536C File Offset: 0x0000356C
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
			this._desc = Encoding.Unicode.GetString(value, 0, num);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000053B8 File Offset: 0x000035B8
		internal byte[] Encode()
		{
			ASN1 asn = new ASN1(4, Encoding.Unicode.GetBytes(this._desc + '\0'));
			return asn.GetBytes();
		}

		// Token: 0x04000072 RID: 114
		internal const string oid = "1.3.6.1.4.1.311.88.2.2";

		// Token: 0x04000073 RID: 115
		internal const string friendlyName = null;

		// Token: 0x04000074 RID: 116
		private string _desc;
	}
}
