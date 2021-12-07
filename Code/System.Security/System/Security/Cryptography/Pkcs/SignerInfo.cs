using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> class represents a signer associated with a <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object that represents a CMS/PKCS #7 message.</summary>
	// Token: 0x0200002B RID: 43
	public sealed class SignerInfo
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005FD4 File Offset: 0x000041D4
		internal SignerInfo(string hashName, X509Certificate2 certificate, SubjectIdentifierType type, object o, int version)
		{
			this._digest = new Oid(CryptoConfig.MapNameToOID(hashName));
			this._certificate = certificate;
			this._counter = new SignerInfoCollection();
			this._signed = new CryptographicAttributeObjectCollection();
			this._unsigned = new CryptographicAttributeObjectCollection();
			this._signer = new SubjectIdentifier(type, o);
			this._version = version;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes that is associated with the signer information. Signed attributes are signed along with the rest of the message content.</summary>
		/// <returns>A collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006038 File Offset: 0x00004238
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			get
			{
				return this._signed;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Certificate" /> property retrieves the signing certificate associated with the signer information.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006040 File Offset: 0x00004240
		public X509Certificate2 Certificate
		{
			get
			{
				return this._certificate;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> property retrieves the set of counter signers associated with the signer information. </summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection that represents the counter signers for the signer information. If there are no counter signers, the property is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006048 File Offset: 0x00004248
		public SignerInfoCollection CounterSignerInfos
		{
			get
			{
				return this._counter;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.DigestAlgorithm" /> property retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used in the computation of the signatures.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006050 File Offset: 0x00004250
		public Oid DigestAlgorithm
		{
			get
			{
				return this._digest;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignerIdentifier" /> property retrieves the certificate identifier of the signer associated with the signer information.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that uniquely identifies the certificate associated with the signer information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006058 File Offset: 0x00004258
		public SubjectIdentifier SignerIdentifier
		{
			get
			{
				return this._signer;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection" /> collection of unsigned attributes that is associated with the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006060 File Offset: 0x00004260
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			get
			{
				return this._unsigned;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Version" /> property retrieves the signer information version.</summary>
		/// <returns>An int value that specifies the signer information version.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006068 File Offset: 0x00004268
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> method verifies the data integrity of the CMS/PKCS #7 message signer information. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> is a specialized method used in specific security infrastructure applications in which the subject uses the HashOnly member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration when setting up a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> does not authenticate the signer information because this method does not involve verifying a digital signature. For general-purpose checking of the integrity and authenticity of CMS/PKCS #7 message signer information and countersignatures, use the <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> or <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> methods.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000107 RID: 263 RVA: 0x00006070 File Offset: 0x00004270
		[MonoTODO]
		public void CheckHash()
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> method verifies the digital signature of the message and, optionally, validates the certificate.</summary>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is true, only the signature is verified. If <paramref name="verifySignatureOnly" /> is false, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x06000108 RID: 264 RVA: 0x00006074 File Offset: 0x00004274
		[MonoTODO]
		public void CheckSignature(bool verifySignatureOnly)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> method verifies the digital signature of the message by using the specified collection of certificates and, optionally, validates the certificate.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that can be used to validate the chain. If no additional certificates are to be used to validate the chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" />.</param>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is true, only the signature is verified. If <paramref name="verifySignatureOnly" /> is false, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x06000109 RID: 265 RVA: 0x00006078 File Offset: 0x00004278
		[MonoTODO]
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature" /> method prompts the user to select a signing certificate, creates a countersignature, and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates" />
		/// </PermissionSet>
		// Token: 0x0600010A RID: 266 RVA: 0x0000607C File Offset: 0x0000427C
		[MonoTODO]
		public void ComputeCounterSignature()
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature(System.Security.Cryptography.Pkcs.CmsSigner)" /> method creates a countersignature by using the specified signer and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the counter signer.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600010B RID: 267 RVA: 0x00006080 File Offset: 0x00004280
		[MonoTODO]
		public void ComputeCounterSignature(CmsSigner signer)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Security.Cryptography.Pkcs.SignerInfo)" /> method removes the countersignature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <param name="counterSignerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the countersignature being removed.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600010C RID: 268 RVA: 0x00006084 File Offset: 0x00004284
		[MonoTODO]
		public void RemoveCounterSignature(SignerInfo counterSignerInfo)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Int32)" /> method removes the countersignature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> collection.</summary>
		/// <param name="index">The zero-based index of the countersignature to remove.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600010D RID: 269 RVA: 0x00006088 File Offset: 0x00004288
		[MonoTODO]
		public void RemoveCounterSignature(int index)
		{
		}

		// Token: 0x0400008E RID: 142
		private SubjectIdentifier _signer;

		// Token: 0x0400008F RID: 143
		private X509Certificate2 _certificate;

		// Token: 0x04000090 RID: 144
		private Oid _digest;

		// Token: 0x04000091 RID: 145
		private SignerInfoCollection _counter;

		// Token: 0x04000092 RID: 146
		private CryptographicAttributeObjectCollection _signed;

		// Token: 0x04000093 RID: 147
		private CryptographicAttributeObjectCollection _unsigned;

		// Token: 0x04000094 RID: 148
		private int _version;
	}
}
