using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using Mono.Security;
using Mono.Security.X509;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class enables signing and verifying of CMS/PKCS #7 messages. </summary>
	// Token: 0x0200002A RID: 42
	public sealed class SignedCms
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060000E8 RID: 232 RVA: 0x000059E0 File Offset: 0x00003BE0
		public SignedCms()
		{
			this._certs = new X509Certificate2Collection();
			this._info = new SignerInfoCollection();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified content information as the inner content.</summary>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060000E9 RID: 233 RVA: 0x00005A00 File Offset: 0x00003C00
		public SignedCms(ContentInfo content) : this(content, false)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Boolean)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified content information as the inner content and by using the detached state.</summary>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <param name="detached">A <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature. If <paramref name="detached" /> is true, the signature is detached. If <paramref name="detached" /> is false, the signature is not detached.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060000EA RID: 234 RVA: 0x00005A0C File Offset: 0x00003C0C
		public SignedCms(ContentInfo content, bool detached) : this()
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this._content = content;
			this._detached = detached;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified subject identifier type as the default subject identifier type for signers.</summary>
		/// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> member that specifies the default subject identifier type for signers.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060000EB RID: 235 RVA: 0x00005A34 File Offset: 0x00003C34
		public SignedCms(SubjectIdentifierType signerIdentifierType) : this()
		{
			this._type = signerIdentifierType;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified subject identifier type as the default subject identifier type for signers and content information as the inner content.</summary>
		/// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> member that specifies the default subject identifier type for signers.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060000EC RID: 236 RVA: 0x00005A44 File Offset: 0x00003C44
		public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo content) : this(content, false)
		{
			this._type = signerIdentifierType;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Boolean)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified subject identifier type as the default subject identifier type for signers, the content information as the inner content, and by using the detached state.</summary>
		/// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> member that specifies the default subject identifier type for signers.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <param name="detached">A <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature. If <paramref name="detached" /> is true, the signature is detached. If detached is false, the signature is not detached.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x060000ED RID: 237 RVA: 0x00005A58 File Offset: 0x00003C58
		public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo content, bool detached) : this(content, detached)
		{
			this._type = signerIdentifierType;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Certificates" /> property retrieves the certificates associated with the encoded CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the set of certificates for the encoded CMS/PKCS #7 message.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005A6C File Offset: 0x00003C6C
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this._certs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.ContentInfo" /> property retrieves the inner contents of the encoded CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the contents of the encoded CMS/PKCS #7 message.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005A74 File Offset: 0x00003C74
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

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Detached" /> property retrieves whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature. If this property is true, the signature is detached. If this property is false, the signature is not detached.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public bool Detached
		{
			get
			{
				return this._detached;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.SignerInfos" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection associated with the CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> object that represents the signer information for the CMS/PKCS #7 message.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public SignerInfoCollection SignerInfos
		{
			get
			{
				return this._info;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Version" /> property retrieves the version of the CMS/PKCS #7 message.</summary>
		/// <returns>An int value that represents the CMS/PKCS #7 message version.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)" /> method verifies the digital signatures on the signed CMS/PKCS #7 message and, optionally, validates the signers' certificates.</summary>
		/// <param name="verifySignatureOnly">A <see cref="T:System.Boolean" /> value that specifies whether only the digital signatures are verified without the signers' certificates being validated. If <paramref name="verifySignatureOnly" /> is true, only the digital signatures are verified. If it is false, the digital signatures are verified, the signers' certificates are validated, and the purposes of the certificates are validated. The purposes of a certificate are considered valid if the certificate has no key usage or if the key usage supports digital signatures or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException"> A method call was invalid for the object's current state.</exception>
		// Token: 0x060000F3 RID: 243 RVA: 0x00005AC8 File Offset: 0x00003CC8
		[MonoTODO]
		public void CheckSignature(bool verifySignatureOnly)
		{
			foreach (SignerInfo signerInfo in this._info)
			{
				signerInfo.CheckSignature(verifySignatureOnly);
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> method verifies the digital signatures on the signed CMS/PKCS #7 message by using the specified collection of certificates and, optionally, validates the signers' certificates.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that can be used to validate the certificate chain. If no additional certificates are to be used to validate the certificate chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)" /> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" />.</param>
		/// <param name="verifySignatureOnly">A <see cref="T:System.Boolean" /> value that specifies whether only the digital signatures are verified without the signers' certificates being validated. If <paramref name="verifySignatureOnly" /> is true, only the digital signatures are verified. If it is false, the digital signatures are verified, the signers' certificates are validated, and the purposes of the certificates are validated. The purposes of a certificate are considered valid if the certificate has no key usage or if the key usage supports digital signatures or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060000F4 RID: 244 RVA: 0x00005B00 File Offset: 0x00003D00
		[MonoTODO]
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			foreach (SignerInfo signerInfo in this._info)
			{
				signerInfo.CheckSignature(extraStore, verifySignatureOnly);
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckHash" /> method verifies the data integrity of the CMS/PKCS #7 message. </summary>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060000F5 RID: 245 RVA: 0x00005B38 File Offset: 0x00003D38
		[MonoTODO]
		public void CheckHash()
		{
			throw new InvalidOperationException(string.Empty);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.ComputeSignature" /> method prompts the user to select a signing certificate, creates a signature, and adds the signature to the CMS/PKCS #7 message.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates" />
		/// </PermissionSet>
		// Token: 0x060000F6 RID: 246 RVA: 0x00005B44 File Offset: 0x00003D44
		[MonoTODO]
		public void ComputeSignature()
		{
			throw new CryptographicException(string.Empty);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.ComputeSignature(System.Security.Cryptography.Pkcs.CmsSigner)" /> method creates a signature using the specified signer and adds the signature to the CMS/PKCS #7 message.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the signer.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060000F7 RID: 247 RVA: 0x00005B50 File Offset: 0x00003D50
		[MonoTODO]
		public void ComputeSignature(CmsSigner signer)
		{
			this.ComputeSignature();
		}

		/// <summary>Creates a signature using the specified signer and adds the signature to the CMS/PKCS #7 message. If the value of the silent parameter is false and the <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificate" /> property of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object specified by the signer parameter is not set to a valid certificate, this method prompts the user to select a signing certificate.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the signer.</param>
		/// <param name="silent">false to prompt the user to select a signing certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the <paramref name="silent" /> parameter is true and a signing certificate is not specified.</exception>
		// Token: 0x060000F8 RID: 248 RVA: 0x00005B58 File Offset: 0x00003D58
		[MonoTODO]
		public void ComputeSignature(CmsSigner signer, bool silent)
		{
			this.ComputeSignature();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005B60 File Offset: 0x00003D60
		private string ToString(byte[] array, bool reverse)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (reverse)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					stringBuilder.Append(array[i].ToString("X2"));
				}
			}
			else
			{
				for (int j = 0; j < array.Length; j++)
				{
					stringBuilder.Append(array[j].ToString("X2"));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005BE0 File Offset: 0x00003DE0
		private byte[] GetKeyIdentifier(Mono.Security.X509.X509Certificate x509)
		{
			Mono.Security.X509.X509Extension x509Extension = x509.Extensions["2.5.29.14"];
			if (x509Extension != null)
			{
				ASN1 asn = new ASN1(x509Extension.Value.Value);
				return asn.Value;
			}
			ASN1 asn2 = new ASN1(48);
			ASN1 asn3 = asn2.Add(new ASN1(48));
			asn3.Add(new ASN1(CryptoConfig.EncodeOID(x509.KeyAlgorithm)));
			asn3.Add(new ASN1(x509.KeyAlgorithmParameters));
			byte[] publicKey = x509.PublicKey;
			byte[] array = new byte[publicKey.Length + 1];
			Array.Copy(publicKey, 0, array, 1, publicKey.Length);
			asn2.Add(new ASN1(3, array));
			SHA1 sha = SHA1.Create();
			return sha.ComputeHash(asn2.GetBytes());
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Decode(System.Byte[])" /> method decodes an encoded <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message. Upon successful decoding, the decoded information can be retrieved from the properties of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object.</summary>
		/// <param name="encodedMessage">Array of byte values that represents the encoded CMS/PKCS #7 message to be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060000FB RID: 251 RVA: 0x00005CA4 File Offset: 0x00003EA4
		[MonoTODO("incomplete - missing attributes")]
		public void Decode(byte[] encodedMessage)
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(encodedMessage);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				throw new Exception(string.Empty);
			}
			PKCS7.SignedData signedData = new PKCS7.SignedData(contentInfo.Content);
			SubjectIdentifierType type = SubjectIdentifierType.Unknown;
			object o = null;
			X509Certificate2 certificate = null;
			if (signedData.SignerInfo.Certificate != null)
			{
				certificate = new X509Certificate2(signedData.SignerInfo.Certificate.RawData);
			}
			else if (signedData.SignerInfo.IssuerName != null && signedData.SignerInfo.SerialNumber != null)
			{
				byte[] serialNumber = signedData.SignerInfo.SerialNumber;
				Array.Reverse(serialNumber);
				type = SubjectIdentifierType.IssuerAndSerialNumber;
				X509IssuerSerial x509IssuerSerial = new X509IssuerSerial
				{
					IssuerName = signedData.SignerInfo.IssuerName,
					SerialNumber = this.ToString(serialNumber, true)
				};
				o = x509IssuerSerial;
				foreach (Mono.Security.X509.X509Certificate x509Certificate in signedData.Certificates)
				{
					if (x509Certificate.IssuerName == signedData.SignerInfo.IssuerName && this.ToString(x509Certificate.SerialNumber, true) == x509IssuerSerial.SerialNumber)
					{
						certificate = new X509Certificate2(x509Certificate.RawData);
						break;
					}
				}
			}
			else if (signedData.SignerInfo.SubjectKeyIdentifier != null)
			{
				string text = this.ToString(signedData.SignerInfo.SubjectKeyIdentifier, false);
				type = SubjectIdentifierType.SubjectKeyIdentifier;
				o = text;
				foreach (Mono.Security.X509.X509Certificate x509Certificate2 in signedData.Certificates)
				{
					if (this.ToString(this.GetKeyIdentifier(x509Certificate2), false) == text)
					{
						certificate = new X509Certificate2(x509Certificate2.RawData);
						break;
					}
				}
			}
			SignerInfo signer = new SignerInfo(signedData.SignerInfo.HashName, certificate, type, o, (int)signedData.SignerInfo.Version);
			this._info.Add(signer);
			ASN1 content = signedData.ContentInfo.Content;
			Oid oid = new Oid(signedData.ContentInfo.ContentType);
			this._content = new ContentInfo(oid, content[0].Value);
			foreach (Mono.Security.X509.X509Certificate x509Certificate3 in signedData.Certificates)
			{
				this._certs.Add(new X509Certificate2(x509Certificate3.RawData));
			}
			this._version = (int)signedData.Version;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Encode" /> method encodes the information in the object into a CMS/PKCS #7 message.</summary>
		/// <returns>An array of byte values that represents the encoded message. The encoded message can be decoded by the <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Decode(System.Byte[])" /> method.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060000FC RID: 252 RVA: 0x00005FC8 File Offset: 0x000041C8
		[MonoTODO]
		public byte[] Encode()
		{
			return null;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.RemoveSignature(System.Security.Cryptography.Pkcs.SignerInfo)" /> method removes the signature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <param name="signerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the countersignature being removed.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060000FD RID: 253 RVA: 0x00005FCC File Offset: 0x000041CC
		[MonoTODO]
		public void RemoveSignature(SignerInfo signerInfo)
		{
		}

		/// <summary>Removes the signature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.SignerInfos" /> collection. </summary>
		/// <param name="index">The zero-based index of the signature to remove.</param>
		/// <exception cref="T:System.InvalidOperationException">A CMS/PKCS #7 message is not signed, and <paramref name="index" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than the signature count minus 1.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The signature could not be removed.-or-An internal cryptographic error occurred.</exception>
		// Token: 0x060000FE RID: 254 RVA: 0x00005FD0 File Offset: 0x000041D0
		[MonoTODO]
		public void RemoveSignature(int index)
		{
		}

		// Token: 0x04000088 RID: 136
		private ContentInfo _content;

		// Token: 0x04000089 RID: 137
		private bool _detached;

		// Token: 0x0400008A RID: 138
		private SignerInfoCollection _info;

		// Token: 0x0400008B RID: 139
		private X509Certificate2Collection _certs;

		// Token: 0x0400008C RID: 140
		private SubjectIdentifierType _type;

		// Token: 0x0400008D RID: 141
		private int _version;
	}
}
