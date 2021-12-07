using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
	// Token: 0x0200002E RID: 46
	public sealed class SubjectIdentifier
	{
		// Token: 0x0600011D RID: 285 RVA: 0x000061E8 File Offset: 0x000043E8
		internal SubjectIdentifier(SubjectIdentifierType type, object value)
		{
			this._type = type;
			this._value = value;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property retrieves the type of subject identifier. The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" />  enumeration that identifies the type of subject.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006200 File Offset: 0x00004400
		public SubjectIdentifierType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property retrieves the value of the subject identifier. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property to determine the type of subject identifier, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property.<see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> propertyObjectIssuerAndSerialNumber<see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" />SubjectKeyIdentifier<see cref="T:System.String" /></returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006208 File Offset: 0x00004408
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000097 RID: 151
		private SubjectIdentifierType _type;

		// Token: 0x04000098 RID: 152
		private object _value;
	}
}
