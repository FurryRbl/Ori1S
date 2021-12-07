using System;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType" /> class defines the type of the content of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000020 RID: 32
	public sealed class Pkcs9ContentType : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9ContentType.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType" /> class.</summary>
		// Token: 0x060000AE RID: 174 RVA: 0x00005130 File Offset: 0x00003330
		public Pkcs9ContentType()
		{
			this.Oid = new Oid("1.2.840.113549.1.9.3", "Content Type");
			this._encoded = null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005160 File Offset: 0x00003360
		internal Pkcs9ContentType(string contentType)
		{
			this.Oid = new Oid("1.2.840.113549.1.9.3", "Content Type");
			this._contentType = new Oid(contentType);
			base.RawData = this.Encode();
			this._encoded = null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000051A8 File Offset: 0x000033A8
		internal Pkcs9ContentType(byte[] encodedContentType)
		{
			if (encodedContentType == null)
			{
				throw new ArgumentNullException("encodedContentType");
			}
			this.Oid = new Oid("1.2.840.113549.1.9.3", "Content Type");
			base.RawData = encodedContentType;
			this.Decode(encodedContentType);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9ContentType.ContentType" /> property gets an <see cref="T:System.Security.Cryptography.Oid" /> object that contains the content type.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.Oid" /> object that contains the content type.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000051F0 File Offset: 0x000033F0
		public Oid ContentType
		{
			get
			{
				if (this._encoded != null)
				{
					this.Decode(this._encoded);
				}
				return this._contentType;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000B2 RID: 178 RVA: 0x00005210 File Offset: 0x00003410
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this._encoded = asnEncodedData.RawData;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005228 File Offset: 0x00003428
		internal void Decode(byte[] attribute)
		{
			if (attribute == null || attribute[0] != 6)
			{
				throw new CryptographicException(Locale.GetText("Expected an OID."));
			}
			ASN1 asn = new ASN1(attribute);
			this._contentType = new Oid(ASN1Convert.ToOid(asn));
			this._encoded = null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005274 File Offset: 0x00003474
		internal byte[] Encode()
		{
			if (this._contentType == null)
			{
				return null;
			}
			return ASN1Convert.FromOid(this._contentType.Value).GetBytes();
		}

		// Token: 0x0400006E RID: 110
		internal const string oid = "1.2.840.113549.1.9.3";

		// Token: 0x0400006F RID: 111
		internal const string friendlyName = "Content Type";

		// Token: 0x04000070 RID: 112
		private Oid _contentType;

		// Token: 0x04000071 RID: 113
		private byte[] _encoded;
	}
}
