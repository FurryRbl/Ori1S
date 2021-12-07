using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key. </summary>
	// Token: 0x0200002F RID: 47
	public sealed class SubjectIdentifierOrKey
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00006210 File Offset: 0x00004410
		internal SubjectIdentifierOrKey(SubjectIdentifierOrKeyType type, object value)
		{
			this._type = type;
			this._value = value;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property retrieves the type of subject identifier or key. The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" />  enumeration that specifies the type of subject identifier.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006228 File Offset: 0x00004428
		public SubjectIdentifierOrKeyType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property retrieves the value of the subject identifier or  key. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property to determine the type of subject identifier or key, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier or key. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property.<see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> propertyObjectIssuerAndSerialNumber<see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" />SubjectKeyIdentifier<see cref="T:System.String" />PublicKeyInfo<see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /></returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006230 File Offset: 0x00004430
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000099 RID: 153
		private SubjectIdentifierOrKeyType _type;

		// Token: 0x0400009A RID: 154
		private object _value;
	}
}
