using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class provides signing functionality.</summary>
	// Token: 0x0200001E RID: 30
	public sealed class CmsSigner
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class by using a default subject identifier type.</summary>
		// Token: 0x06000097 RID: 151 RVA: 0x00004F9C File Offset: 0x0000319C
		public CmsSigner()
		{
			this._signer = SubjectIdentifierType.IssuerAndSerialNumber;
			this._digest = new Oid("1.3.14.3.2.26");
			this._options = X509IncludeOption.ExcludeRoot;
			this._signed = new CryptographicAttributeObjectCollection();
			this._unsigned = new CryptographicAttributeObjectCollection();
			this._coll = new X509Certificate2Collection();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified subject identifier type.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		// Token: 0x06000098 RID: 152 RVA: 0x00004FF0 File Offset: 0x000031F0
		public CmsSigner(SubjectIdentifierType signerIdentifierType) : this()
		{
			if (signerIdentifierType == SubjectIdentifierType.Unknown)
			{
				this._signer = SubjectIdentifierType.IssuerAndSerialNumber;
			}
			else
			{
				this._signer = signerIdentifierType;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signer identifier type and signing certificate.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		// Token: 0x06000099 RID: 153 RVA: 0x00005014 File Offset: 0x00003214
		public CmsSigner(SubjectIdentifierType signerIdentifierType, X509Certificate2 certificate) : this(signerIdentifierType)
		{
			this._certificate = certificate;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signing certificate.</summary>
		/// <param name="certificate">An    <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		// Token: 0x0600009A RID: 154 RVA: 0x00005024 File Offset: 0x00003224
		public CmsSigner(X509Certificate2 certificate) : this()
		{
			this._certificate = certificate;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified cryptographic service provider (CSP) parameters. <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> is useful when you know the specific CSP and private key to use for signing.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" />  object that represents the set of CSP parameters to use.</param>
		// Token: 0x0600009B RID: 155 RVA: 0x00005034 File Offset: 0x00003234
		[MonoTODO]
		public CmsSigner(CspParameters parameters) : this()
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Signed attributes are signed along with the specified content.</summary>
		/// <returns>A collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000503C File Offset: 0x0000323C
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			get
			{
				return this._signed;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificate" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00005044 File Offset: 0x00003244
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000504C File Offset: 0x0000324C
		public X509Certificate2 Certificate
		{
			get
			{
				return this._certificate;
			}
			set
			{
				this._certificate = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificates" /> property retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that contains certificates associated with the message to be signed.  </summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the collection of  certificates associated with the message to be signed.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005058 File Offset: 0x00003258
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this._coll;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.DigestAlgorithm" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" /> that represents the hash algorithm used with the signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005060 File Offset: 0x00003260
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00005068 File Offset: 0x00003268
		public Oid DigestAlgorithm
		{
			get
			{
				return this._digest;
			}
			set
			{
				this._digest = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property sets or retrieves the option that controls whether the root and entire chain associated with the signing certificate are included with the created CMS/PKCS #7 message.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> enumeration that specifies how much of the X509 certificate chain should be included in the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property can be one of the following <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> members.NameValueMeaning<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.None" />0The certificate chain is not included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.ExcludeRoot" />1The certificate chain, except for the root certificate, is included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.EndCertOnly" />2Only the end certificate is included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.WholeChain" />3The certificate chain, including the root certificate, is included.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00005074 File Offset: 0x00003274
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000507C File Offset: 0x0000327C
		public X509IncludeOption IncludeOption
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignerIdentifierType" /> property sets or retrieves the type of the identifier of the signer.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the signer.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005088 File Offset: 0x00003288
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00005090 File Offset: 0x00003290
		public SubjectIdentifierType SignerIdentifierType
		{
			get
			{
				return this._signer;
			}
			set
			{
				if (value == SubjectIdentifierType.Unknown)
				{
					throw new ArgumentException("value");
				}
				this._signer = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection" /> collection of unsigned PKCS #9 attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000050AC File Offset: 0x000032AC
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			get
			{
				return this._unsigned;
			}
		}

		// Token: 0x04000067 RID: 103
		private SubjectIdentifierType _signer;

		// Token: 0x04000068 RID: 104
		private X509Certificate2 _certificate;

		// Token: 0x04000069 RID: 105
		private X509Certificate2Collection _coll;

		// Token: 0x0400006A RID: 106
		private Oid _digest;

		// Token: 0x0400006B RID: 107
		private X509IncludeOption _options;

		// Token: 0x0400006C RID: 108
		private CryptographicAttributeObjectCollection _signed;

		// Token: 0x0400006D RID: 109
		private CryptographicAttributeObjectCollection _unsigned;
	}
}
