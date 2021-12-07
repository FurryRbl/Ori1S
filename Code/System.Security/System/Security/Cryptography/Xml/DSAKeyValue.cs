using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see cref="T:System.Security.Cryptography.DSA" /> private key of the &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000038 RID: 56
	public class DSAKeyValue : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with a new, randomly-generated <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		// Token: 0x0600014A RID: 330 RVA: 0x00006B70 File Offset: 0x00004D70
		public DSAKeyValue()
		{
			this.dsa = DSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of the <see cref="T:System.Security.Cryptography.DSA" /> class that holds the public key. </param>
		// Token: 0x0600014B RID: 331 RVA: 0x00006B84 File Offset: 0x00004D84
		public DSAKeyValue(DSA key)
		{
			this.dsa = key;
		}

		/// <summary>Gets or sets the key value represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</summary>
		/// <returns>The public key represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006B94 File Offset: 0x00004D94
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00006B9C File Offset: 0x00004D9C
		public DSA Key
		{
			get
			{
				return this.dsa;
			}
			set
			{
				this.dsa = value;
			}
		}

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600014E RID: 334 RVA: 0x00006BA8 File Offset: 0x00004DA8
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.SetAttribute("xmlns", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.InnerXml = this.dsa.ToXmlString(false);
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> XML element. </exception>
		// Token: 0x0600014F RID: 335 RVA: 0x00006BF0 File Offset: 0x00004DF0
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
			this.dsa.FromXmlString(value.InnerXml);
		}

		// Token: 0x040000AE RID: 174
		private DSA dsa;
	}
}
