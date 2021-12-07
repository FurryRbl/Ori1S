using System;
using System.Collections.Generic;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class represents the CMS/PKCS #7 ContentInfo data structure as defined in the CMS/PKCS #7 standards document. This data structure is the basis for all CMS/PKCS #7 messages.</summary>
	// Token: 0x02000016 RID: 22
	public sealed class ContentInfo
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Byte[])" /> constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using an array of byte values as the data and a default <paramref name="object identifier" /> (OID) that represents the content type.</summary>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x06000059 RID: 89 RVA: 0x0000475C File Offset: 0x0000295C
		public ContentInfo(byte[] content) : this(new Oid("1.2.840.113549.1.7.1"), content)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Security.Cryptography.Oid,System.Byte[])" />  constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using the specified content type and an array of byte values as the data.</summary>
		/// <param name="contentType">An <see cref="T:System.Security.Cryptography.Oid" /> object that contains an <paramref name="object identifier" /> (OID) that specifies the content type of the content. This can be data, digestedData, encryptedData, envelopedData, hashedData, signedAndEnvelopedData, or signedData.  For more information, see  Remarks.</param>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x0600005A RID: 90 RVA: 0x00004770 File Offset: 0x00002970
		public ContentInfo(Oid oid, byte[] content)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this._oid = oid;
			this._content = content;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000047B4 File Offset: 0x000029B4
		~ContentInfo()
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.Content" /> property  retrieves the content of the CMS/PKCS #7 message.</summary>
		/// <returns>An array of byte values that represents the content data.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000047EC File Offset: 0x000029EC
		public byte[] Content
		{
			get
			{
				return (byte[])this._content.Clone();
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.ContentType" /> property  retrieves the <see cref="T:System.Security.Cryptography.Oid" />   object that contains the <paramref name="object identifier" /> (OID)  of the content type of the inner content of the CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" />  object that contains the OID value that represents the content type.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004800 File Offset: 0x00002A00
		public Oid ContentType
		{
			get
			{
				return this._oid;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.GetContentType(System.Byte[])" /> static method  retrieves the outer content type of the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message represented by an array of byte values.</summary>
		/// <returns>If the method succeeds, the method returns an <see cref="T:System.Security.Cryptography.Oid" /> object that contains the outer content type of the specified encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message.If the method fails, it throws an exception.</returns>
		/// <param name="encodedMessage">An array of byte values that represents the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message from which to retrieve the outer content type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred during a cryptographic operation.</exception>
		// Token: 0x0600005E RID: 94 RVA: 0x00004808 File Offset: 0x00002A08
		[MonoTODO("MS is stricter than us about the content structure")]
		public static Oid GetContentType(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			try
			{
				PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(encodedMessage);
				string contentType = contentInfo.ContentType;
				if (contentType != null)
				{
					if (ContentInfo.<>f__switch$map0 == null)
					{
						ContentInfo.<>f__switch$map0 = new Dictionary<string, int>(5)
						{
							{
								"1.2.840.113549.1.7.1",
								0
							},
							{
								"1.2.840.113549.1.7.2",
								0
							},
							{
								"1.2.840.113549.1.7.3",
								0
							},
							{
								"1.2.840.113549.1.7.5",
								0
							},
							{
								"1.2.840.113549.1.7.6",
								0
							}
						};
					}
					int num;
					if (ContentInfo.<>f__switch$map0.TryGetValue(contentType, out num))
					{
						if (num == 0)
						{
							return new Oid(contentInfo.ContentType);
						}
					}
				}
				string text = Locale.GetText("Bad ASN1 - invalid OID '{0}'");
				throw new CryptographicException(string.Format(text, contentInfo.ContentType));
			}
			catch (Exception inner)
			{
				throw new CryptographicException(Locale.GetText("Bad ASN1 - invalid structure"), inner);
			}
			Oid result;
			return result;
		}

		// Token: 0x04000051 RID: 81
		private Oid _oid;

		// Token: 0x04000052 RID: 82
		private byte[] _content;
	}
}
