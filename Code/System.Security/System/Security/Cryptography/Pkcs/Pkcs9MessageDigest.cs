using System;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest" /> class defines the message digest of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000023 RID: 35
	public sealed class Pkcs9MessageDigest : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest" /> class.</summary>
		// Token: 0x060000C3 RID: 195 RVA: 0x0000553C File Offset: 0x0000373C
		public Pkcs9MessageDigest()
		{
			this.Oid = new Oid("1.2.840.113549.1.9.4", "Message Digest");
			this._encoded = null;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000556C File Offset: 0x0000376C
		internal Pkcs9MessageDigest(byte[] messageDigest, bool encoded)
		{
			if (messageDigest == null)
			{
				throw new ArgumentNullException("messageDigest");
			}
			if (encoded)
			{
				this.Oid = new Oid("1.2.840.113549.1.9.4", "Message Digest");
				base.RawData = messageDigest;
				this.Decode(messageDigest);
			}
			else
			{
				this.Oid = new Oid("1.2.840.113549.1.9.4", "Message Digest");
				this._messageDigest = (byte[])this._messageDigest.Clone();
				base.RawData = this.Encode();
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.MessageDigest" /> property retrieves the message digest.</summary>
		/// <returns>An array of byte values that contains the message digest.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000055F8 File Offset: 0x000037F8
		public byte[] MessageDigest
		{
			get
			{
				if (this._encoded != null)
				{
					this.Decode(this._encoded);
				}
				return this._messageDigest;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000C6 RID: 198 RVA: 0x00005618 File Offset: 0x00003818
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this._encoded = asnEncodedData.RawData;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005630 File Offset: 0x00003830
		internal void Decode(byte[] attribute)
		{
			if (attribute == null || attribute[0] != 4)
			{
				throw new CryptographicException(Locale.GetText("Expected an OCTETSTRING."));
			}
			ASN1 asn = new ASN1(attribute);
			this._messageDigest = asn.Value;
			this._encoded = null;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005678 File Offset: 0x00003878
		internal byte[] Encode()
		{
			ASN1 asn = new ASN1(4, this._messageDigest);
			return asn.GetBytes();
		}

		// Token: 0x04000078 RID: 120
		internal const string oid = "1.2.840.113549.1.9.4";

		// Token: 0x04000079 RID: 121
		internal const string friendlyName = "Message Digest";

		// Token: 0x0400007A RID: 122
		private byte[] _messageDigest;

		// Token: 0x0400007B RID: 123
		private byte[] _encoded;
	}
}
