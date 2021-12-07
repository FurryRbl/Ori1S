using System;
using System.Globalization;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class defines the signing date and time of a signature. A <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> object can  be used as an authenticated attribute of a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" />  object when an authenticated date and time are to accompany a digital signature.</summary>
	// Token: 0x02000024 RID: 36
	public sealed class Pkcs9SigningTime : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class.</summary>
		// Token: 0x060000C9 RID: 201 RVA: 0x00005698 File Offset: 0x00003898
		public Pkcs9SigningTime()
		{
			this.Oid = new Oid("1.2.840.113549.1.9.5", "Signing Time");
			this._signingTime = DateTime.Now;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.DateTime)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class by using the specified signing date and time.</summary>
		/// <param name="signingTime">A <see cref="T:System.DateTime" />  structure that represents the signing date and time of the signature.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x000056D8 File Offset: 0x000038D8
		public Pkcs9SigningTime(DateTime signingTime)
		{
			this.Oid = new Oid("1.2.840.113549.1.9.5", "Signing Time");
			this._signingTime = signingTime;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class by using the specified array of byte values as the encoded signing date and time of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedSigningTime">An array of byte values that specifies the encoded signing date and time of the CMS/PKCS #7 message.</param>
		// Token: 0x060000CB RID: 203 RVA: 0x00005714 File Offset: 0x00003914
		public Pkcs9SigningTime(byte[] encodedSigningTime)
		{
			if (encodedSigningTime == null)
			{
				throw new ArgumentNullException("encodedSigningTime");
			}
			this.Oid = new Oid("1.2.840.113549.1.9.5", "Signing Time");
			base.RawData = encodedSigningTime;
			this.Decode(encodedSigningTime);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.SigningTime" /> property retrieves a <see cref="T:System.DateTime" /> structure that represents the date and time that the message was signed.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure that contains the date and time the document was signed.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000575C File Offset: 0x0000395C
		public DateTime SigningTime
		{
			get
			{
				return this._signingTime;
			}
		}

		/// <summary>Copies information from a <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000CD RID: 205 RVA: 0x00005764 File Offset: 0x00003964
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.Decode(asnEncodedData.RawData);
			base.Oid = asnEncodedData.Oid;
			base.RawData = asnEncodedData.RawData;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000057A8 File Offset: 0x000039A8
		internal void Decode(byte[] attribute)
		{
			if (attribute[0] != 23)
			{
				throw new CryptographicException(Locale.GetText("Only UTCTIME is supported."));
			}
			ASN1 asn = new ASN1(attribute);
			byte[] value = asn.Value;
			string @string = Encoding.ASCII.GetString(value, 0, value.Length - 1);
			this._signingTime = DateTime.ParseExact(@string, "yyMMddHHmmss", null);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005804 File Offset: 0x00003A04
		internal byte[] Encode()
		{
			if (this._signingTime.Year <= 1600)
			{
				throw new ArgumentOutOfRangeException("<= 1600");
			}
			if (this._signingTime.Year < 1950 || this._signingTime.Year >= 2050)
			{
				throw new CryptographicException("[1950,2049]");
			}
			string s = this._signingTime.ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
			ASN1 asn = new ASN1(23, Encoding.ASCII.GetBytes(s));
			return asn.GetBytes();
		}

		// Token: 0x0400007C RID: 124
		internal const string oid = "1.2.840.113549.1.9.5";

		// Token: 0x0400007D RID: 125
		internal const string friendlyName = "Signing Time";

		// Token: 0x0400007E RID: 126
		private DateTime _signingTime;
	}
}
