using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Wraps the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> class, it to be placed as a subelement of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
	// Token: 0x02000044 RID: 68
	public class KeyInfoEncryptedKey : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class. </summary>
		// Token: 0x060001E0 RID: 480 RVA: 0x00009418 File Offset: 0x00007618
		public KeyInfoEncryptedKey()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class using an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object.</summary>
		/// <param name="encryptedKey">An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" />  object that encapsulates an encrypted key.</param>
		// Token: 0x060001E1 RID: 481 RVA: 0x00009420 File Offset: 0x00007620
		public KeyInfoEncryptedKey(EncryptedKey ek)
		{
			this.EncryptedKey = ek;
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.KeyInfoEncryptedKey.EncryptedKey" /> property is null.</exception>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009430 File Offset: 0x00007630
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00009438 File Offset: 0x00007638
		public EncryptedKey EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
			set
			{
				this.encryptedKey = value;
			}
		}

		/// <summary>Returns an XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</summary>
		/// <returns>An XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object. </returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encrypted key is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001E4 RID: 484 RVA: 0x00009444 File Offset: 0x00007644
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009454 File Offset: 0x00007654
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.encryptedKey != null)
			{
				return this.encryptedKey.GetXml(document);
			}
			return null;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</param>
		// Token: 0x060001E6 RID: 486 RVA: 0x00009470 File Offset: 0x00007670
		public override void LoadXml(XmlElement value)
		{
			this.EncryptedKey = new EncryptedKey();
			this.EncryptedKey.LoadXml(value);
		}

		// Token: 0x040000E9 RID: 233
		private EncryptedKey encryptedKey;
	}
}
