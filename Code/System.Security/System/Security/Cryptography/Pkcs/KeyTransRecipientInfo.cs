using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> class defines key transport recipient information.        Key transport algorithms typically use the RSA algorithm, in which  an originator establishes a shared cryptographic key with a recipient by generating that key and  then transporting it to the recipient. This is in contrast to key agreement algorithms, in which the two parties that will be using a cryptographic key both take part in its generation, thereby mutually agreeing to that key.</summary>
	// Token: 0x0200001A RID: 26
	public sealed class KeyTransRecipientInfo : RecipientInfo
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004D64 File Offset: 0x00002F64
		internal KeyTransRecipientInfo(byte[] encryptedKey, AlgorithmIdentifier keyEncryptionAlgorithm, SubjectIdentifier recipientIdentifier, int version) : base(RecipientInfoType.KeyTransport)
		{
			this._encryptedKey = encryptedKey;
			this._keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this._recipientIdentifier = recipientIdentifier;
			this._version = version;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.EncryptedKey" /> property retrieves the encrypted key for this key transport recipient.</summary>
		/// <returns>An array of byte values that represents the encrypted key.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004D98 File Offset: 0x00002F98
		public override byte[] EncryptedKey
		{
			get
			{
				return this._encryptedKey;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the key encryption algorithm used to encrypt the content encryption key.</summary>
		/// <returns> An  <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" />  object that stores the key encryption algorithm identifier.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public override AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this._keyEncryptionAlgorithm;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.RecipientIdentifier" /> property retrieves the subject identifier associated with the encrypted content.</summary>
		/// <returns>A   <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" />  object that  stores the identifier of the recipient taking part in the key transport.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public override SubjectIdentifier RecipientIdentifier
		{
			get
			{
				return this._recipientIdentifier;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.Version" /> property retrieves the version of the key transport recipient. The version of the key transport recipient is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key transport algorithm.</summary>
		/// <returns>An int value that represents the version of the key transport <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public override int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0400005F RID: 95
		private byte[] _encryptedKey;

		// Token: 0x04000060 RID: 96
		private AlgorithmIdentifier _keyEncryptionAlgorithm;

		// Token: 0x04000061 RID: 97
		private SubjectIdentifier _recipientIdentifier;

		// Token: 0x04000062 RID: 98
		private int _version;
	}
}
