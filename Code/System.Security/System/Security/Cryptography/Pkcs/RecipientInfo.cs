using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class represents information about a CMS/PKCS #7 message recipient. The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class is an abstract class inherited by the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> and <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> classes.</summary>
	// Token: 0x02000026 RID: 38
	public abstract class RecipientInfo
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000058C8 File Offset: 0x00003AC8
		internal RecipientInfo(RecipientInfoType recipInfoType)
		{
			this._type = recipInfoType;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.EncryptedKey" /> abstract property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D4 RID: 212
		public abstract byte[] EncryptedKey { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.KeyEncryptionAlgorithm" /> abstract property retrieves the algorithm used to perform the key establishment.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that contains the value of the algorithm used to establish the key between the originator and recipient of the CMS/PKCS #7 message.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D5 RID: 213
		public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.RecipientIdentifier" /> abstract property retrieves the identifier of the recipient.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that contains the identifier of the recipient.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D6 RID: 214
		public abstract SubjectIdentifier RecipientIdentifier { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Type" /> property retrieves the type of the recipient. The type of the recipient determines which of two major protocols is used to establish a key between the originator and the recipient of a CMS/PKCS #7 message.</summary>
		/// <returns>A value of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration that defines the type of the recipient.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000058D8 File Offset: 0x00003AD8
		public RecipientInfoType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Version" /> abstract property retrieves the version of the recipient information. Derived classes automatically set this property for their objects, and the value indicates whether it is using PKCS #7 or Cryptographic Message Syntax (CMS) to protect messages. The version also implies whether the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object establishes a cryptographic key by a key agreement algorithm or a key transport algorithm.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the version of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D8 RID: 216
		public abstract int Version { get; }

		// Token: 0x04000081 RID: 129
		private RecipientInfoType _type;
	}
}
