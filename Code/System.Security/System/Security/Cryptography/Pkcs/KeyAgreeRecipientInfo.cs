using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> class defines key agreement recipient information.        Key agreement algorithms typically use the Diffie-Hellman key agreement algorithm, in which the two parties that establish a shared cryptographic key both take part in its generation and, by definition, agree on that key. This is in contrast to key transport algorithms, in which one party generates the key unilaterally and sends, or transports it, to the other party.</summary>
	// Token: 0x02000019 RID: 25
	[MonoTODO]
	public sealed class KeyAgreeRecipientInfo : RecipientInfo
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004D38 File Offset: 0x00002F38
		internal KeyAgreeRecipientInfo() : base(RecipientInfoType.KeyAgreement)
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Date" /> property retrieves the date and time of the start of the key agreement protocol by the originator.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the value of the date and time of the start of the key agreement protocol by the originator.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004D44 File Offset: 0x00002F44
		public DateTime Date
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.EncryptedKey" /> property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004D4C File Offset: 0x00002F4C
		public override byte[] EncryptedKey
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the algorithm used to perform the key agreement.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that contains the value of the algorithm used to perform the key agreement.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004D50 File Offset: 0x00002F50
		public override AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OriginatorIdentifierOrKey" /> property retrieves information about the originator of the key agreement for key agreement algorithms that warrant it.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey" /> object that contains information about the originator of the key agreement.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004D54 File Offset: 0x00002F54
		public SubjectIdentifierOrKey OriginatorIdentifierOrKey
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OtherKeyAttribute" /> property retrieves attributes of the keying material.</summary>
		/// <returns>An object that contains attributes of the keying material.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004D58 File Offset: 0x00002F58
		public CryptographicAttributeObject OtherKeyAttribute
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.RecipientIdentifier" /> property retrieves the identifier of the recipient.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that contains the identifier of the recipient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004D5C File Offset: 0x00002F5C
		public override SubjectIdentifier RecipientIdentifier
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Version" /> property retrieves the version of the key agreement recipient. This is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key agreement algorithm.</summary>
		/// <returns>An int  that represents the version of the  <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" />  object.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004D60 File Offset: 0x00002F60
		public override int Version
		{
			get
			{
				return 0;
			}
		}
	}
}
