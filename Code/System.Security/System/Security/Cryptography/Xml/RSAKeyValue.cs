using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;RSAKeyValue&gt; element of an XML signature.</summary>
	// Token: 0x0200004D RID: 77
	public class RSAKeyValue : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with a new randomly generated <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		// Token: 0x06000241 RID: 577 RVA: 0x0000A6F8 File Offset: 0x000088F8
		public RSAKeyValue()
		{
			this.rsa = RSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key. </param>
		// Token: 0x06000242 RID: 578 RVA: 0x0000A70C File Offset: 0x0000890C
		public RSAKeyValue(RSA key)
		{
			this.rsa = key;
		}

		/// <summary>Gets or sets the instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</summary>
		/// <returns>The instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A71C File Offset: 0x0000891C
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000A724 File Offset: 0x00008924
		public RSA Key
		{
			get
			{
				return this.rsa;
			}
			set
			{
				this.rsa = value;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000245 RID: 581 RVA: 0x0000A730 File Offset: 0x00008930
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.SetAttribute("xmlns", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.InnerXml = this.rsa.ToXmlString(false);
			return xmlElement;
		}

		/// <summary>Loads an <see cref="T:System.Security.Cryptography.RSA" /> key clause from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.RSA" /> key clause. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.RSA" /> key clause XML element. </exception>
		// Token: 0x06000246 RID: 582 RVA: 0x0000A778 File Offset: 0x00008978
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (value.LocalName != "KeyValue" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException("value");
			}
			this.rsa.FromXmlString(value.InnerXml);
		}

		// Token: 0x04000100 RID: 256
		private RSA rsa;
	}
}
