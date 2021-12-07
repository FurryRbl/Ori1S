using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class represents a CMS/PKCS #7 structure for enveloped data.</summary>
	// Token: 0x02000017 RID: 23
	public sealed class EnvelopedCms
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x0600005F RID: 95 RVA: 0x0000491C File Offset: 0x00002B1C
		public EnvelopedCms()
		{
			this._certs = new X509Certificate2Collection();
			this._recipients = new RecipientInfoCollection();
			this._uattribs = new CryptographicAttributeObjectCollection();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information as the inner content type.</summary>
		/// <param name="contentInfo">An instance of the <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> class that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x06000060 RID: 96 RVA: 0x00004948 File Offset: 0x00002B48
		public EnvelopedCms(ContentInfo content) : this()
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this._content = content;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="contentInfo">A  <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x06000061 RID: 97 RVA: 0x00004968 File Offset: 0x00002B68
		public EnvelopedCms(ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm) : this(contentInfo)
		{
			if (encryptionAlgorithm == null)
			{
				throw new ArgumentNullException("encryptionAlgorithm");
			}
			this._identifier = encryptionAlgorithm;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type and content information. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x06000062 RID: 98 RVA: 0x0000498C File Offset: 0x00002B8C
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo) : this(contentInfo)
		{
			this._idType = recipientIdentifierType;
			if (this._idType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				this._version = 2;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type, content information, and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x06000063 RID: 99 RVA: 0x000049B0 File Offset: 0x00002BB0
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm) : this(contentInfo, encryptionAlgorithm)
		{
			this._idType = recipientIdentifierType;
			if (this._idType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				this._version = 2;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Certificates" /> property retrieves the set of certificates associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the X.509 certificates used with the enveloped CMS/PKCS #7 message. If no certificates exist, the property value is an empty collection.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000049E0 File Offset: 0x00002BE0
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this._certs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentEncryptionAlgorithm" /> property retrieves the identifier of the algorithm used to encrypt the content.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that represents the algorithm identifier.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000049E8 File Offset: 0x00002BE8
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				if (this._identifier == null)
				{
					this._identifier = new AlgorithmIdentifier();
				}
				return this._identifier;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> property retrieves the inner content information for the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the inner content information from the enveloped CMS/PKCS #7 message.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004A08 File Offset: 0x00002C08
		public ContentInfo ContentInfo
		{
			get
			{
				if (this._content == null)
				{
					Oid oid = new Oid("1.2.840.113549.1.7.1");
					this._content = new ContentInfo(oid, new byte[0]);
				}
				return this._content;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.RecipientInfos" /> property retrieves the recipient information associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection that represents the recipient information. If no recipients exist, the property value is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004A44 File Offset: 0x00002C44
		public RecipientInfoCollection RecipientInfos
		{
			get
			{
				return this._recipients;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.UnprotectedAttributes" /> property retrieves the unprotected (unencrypted) attributes associated with the enveloped CMS/PKCS #7 message. Unprotected attributes are not encrypted, and so do not have data confidentiality within an <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection" /> collection that represents the unprotected attributes. If no unprotected attributes exist, the property value is an empty collection.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004A4C File Offset: 0x00002C4C
		public CryptographicAttributeObjectCollection UnprotectedAttributes
		{
			get
			{
				return this._uattribs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Version" /> property retrieves the version of the enveloped CMS/PKCS #7 message.  </summary>
		/// <returns>An int value that represents the version of the enveloped CMS/PKCS #7 message.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004A54 File Offset: 0x00002C54
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004A5C File Offset: 0x00002C5C
		private X509IssuerSerial GetIssuerSerial(string issuer, byte[] serial)
		{
			X509IssuerSerial result = default(X509IssuerSerial);
			result.IssuerName = issuer;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in serial)
			{
				stringBuilder.Append(b.ToString("X2"));
			}
			result.SerialNumber = stringBuilder.ToString();
			return result;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decode(System.Byte[])" /> method decodes the specified enveloped CMS/PKCS #7 message and resets all member variables in the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <param name="encodedMessage">An array of byte values that represent the information to be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x0600006B RID: 107 RVA: 0x00004AC0 File Offset: 0x00002CC0
		[MonoTODO]
		public void Decode(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(encodedMessage);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.3")
			{
				throw new Exception(string.Empty);
			}
			PKCS7.EnvelopedData envelopedData = new PKCS7.EnvelopedData(contentInfo.Content);
			Oid oid = new Oid(envelopedData.ContentInfo.ContentType);
			this._content = new ContentInfo(oid, new byte[0]);
			foreach (object obj in envelopedData.RecipientInfos)
			{
				PKCS7.RecipientInfo recipientInfo = (PKCS7.RecipientInfo)obj;
				Oid algorithm = new Oid(recipientInfo.Oid);
				AlgorithmIdentifier keyEncryptionAlgorithm = new AlgorithmIdentifier(algorithm);
				SubjectIdentifier recipientIdentifier = null;
				if (recipientInfo.SubjectKeyIdentifier != null)
				{
					recipientIdentifier = new SubjectIdentifier(SubjectIdentifierType.SubjectKeyIdentifier, recipientInfo.SubjectKeyIdentifier);
				}
				else if (recipientInfo.Issuer != null && recipientInfo.Serial != null)
				{
					X509IssuerSerial issuerSerial = this.GetIssuerSerial(recipientInfo.Issuer, recipientInfo.Serial);
					recipientIdentifier = new SubjectIdentifier(SubjectIdentifierType.IssuerAndSerialNumber, issuerSerial);
				}
				KeyTransRecipientInfo ri = new KeyTransRecipientInfo(recipientInfo.Key, keyEncryptionAlgorithm, recipientIdentifier, recipientInfo.Version);
				this._recipients.Add(ri);
			}
			this._version = (int)envelopedData.Version;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method searches the current user and computer My stores for the appropriate certificate and private key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates" />
		/// </PermissionSet>
		// Token: 0x0600006C RID: 108 RVA: 0x00004C38 File Offset: 0x00002E38
		[MonoTODO]
		public void Decrypt()
		{
			throw new InvalidOperationException("not encrypted");
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information that identifies the certificate associated with the private key to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x0600006D RID: 109 RVA: 0x00004C44 File Offset: 0x00002E44
		[MonoTODO]
		public void Decrypt(RecipientInfo recipientInfo)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			this.Decrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information and by using the specified certificate collection.  The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information to use for the decryption.</param>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x0600006E RID: 110 RVA: 0x00004C60 File Offset: 0x00002E60
		[MonoTODO]
		public void Decrypt(RecipientInfo recipientInfo, X509Certificate2Collection extraStore)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.Decrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the specified certificate collection. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x0600006F RID: 111 RVA: 0x00004C98 File Offset: 0x00002E98
		[MonoTODO]
		public void Decrypt(X509Certificate2Collection extraStore)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.Decrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encode" /> method encodes the contents of the enveloped CMS/PKCS #7 message and returns it as an array of byte values. Encryption must be done before encoding.</summary>
		/// <returns>If the method succeeds, the method returns an array of byte values that represent the encoded information.If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000070 RID: 112 RVA: 0x00004CB4 File Offset: 0x00002EB4
		[MonoTODO]
		public byte[] Encode()
		{
			throw new InvalidOperationException("not encrypted");
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt" /> method encrypts the contents of the CMS/PKCS #7 message.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates" />
		/// </PermissionSet>
		// Token: 0x06000071 RID: 113 RVA: 0x00004CC0 File Offset: 0x00002EC0
		[MonoTODO]
		public void Encrypt()
		{
			if (this._content == null || this._content.Content == null || this._content.Content.Length == 0)
			{
				throw new CryptographicException("no content to encrypt");
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method encrypts the contents of the CMS/PKCS #7 message by using the specified recipient information.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient information.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000072 RID: 114 RVA: 0x00004D08 File Offset: 0x00002F08
		[MonoTODO]
		public void Encrypt(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			this.Encrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipientCollection)" /> method encrypts the contents of the CMS/PKCS #7 message by using the information for the specified list of recipients. The message is encrypted by using a message encryption key with a symmetric encryption algorithm such as triple DES. The message encryption key is then encrypted with the public key of each recipient.</summary>
		/// <param name="recipients">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection that represents the information for the list of recipients.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000073 RID: 115 RVA: 0x00004D24 File Offset: 0x00002F24
		[MonoTODO]
		public void Encrypt(CmsRecipientCollection recipients)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
		}

		// Token: 0x04000054 RID: 84
		private ContentInfo _content;

		// Token: 0x04000055 RID: 85
		private AlgorithmIdentifier _identifier;

		// Token: 0x04000056 RID: 86
		private X509Certificate2Collection _certs;

		// Token: 0x04000057 RID: 87
		private RecipientInfoCollection _recipients;

		// Token: 0x04000058 RID: 88
		private CryptographicAttributeObjectCollection _uattribs;

		// Token: 0x04000059 RID: 89
		private SubjectIdentifierType _idType;

		// Token: 0x0400005A RID: 90
		private int _version;
	}
}
