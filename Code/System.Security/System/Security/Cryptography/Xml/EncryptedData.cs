using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;EncryptedData&gt; element in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000039 RID: 57
	public sealed class EncryptedData : EncryptedType
	{
		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> that represents the &lt;EncryptedData&gt; element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> value is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000151 RID: 337 RVA: 0x00006C58 File Offset: 0x00004E58
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006C68 File Offset: 0x00004E68
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.CipherData == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			XmlElement xmlElement = document.CreateElement("EncryptedData", "http://www.w3.org/2001/04/xmlenc#");
			if (this.EncryptionMethod != null)
			{
				xmlElement.AppendChild(this.EncryptionMethod.GetXml(document));
			}
			if (base.KeyInfo != null)
			{
				xmlElement.AppendChild(document.ImportNode(base.KeyInfo.GetXml(), true));
			}
			if (this.CipherData != null)
			{
				xmlElement.AppendChild(this.CipherData.GetXml(document));
			}
			if (this.EncryptionProperties.Count > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("EncryptionProperties", "http://www.w3.org/2001/04/xmlenc#");
				foreach (object obj in this.EncryptionProperties)
				{
					EncryptionProperty encryptionProperty = (EncryptionProperty)obj;
					xmlElement2.AppendChild(encryptionProperty.GetXml(document));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			if (this.Id != null)
			{
				xmlElement.SetAttribute("Id", this.Id);
			}
			if (this.Type != null)
			{
				xmlElement.SetAttribute("Type", this.Type);
			}
			if (this.MimeType != null)
			{
				xmlElement.SetAttribute("MimeType", this.MimeType);
			}
			if (this.Encoding != null)
			{
				xmlElement.SetAttribute("Encoding", this.Encoding);
			}
			return xmlElement;
		}

		/// <summary>Loads XML information into the &lt;EncryptedData&gt; element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use for the &lt;EncryptedData&gt; element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a &lt;CypherData&gt; node.</exception>
		// Token: 0x06000153 RID: 339 RVA: 0x00006E04 File Offset: 0x00005004
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptedData" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed EncryptedData element.");
			}
			this.EncryptionMethod = null;
			this.EncryptionMethod = null;
			this.EncryptionProperties.Clear();
			this.Id = null;
			this.Type = null;
			this.MimeType = null;
			this.Encoding = null;
			foreach (object obj in value.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode is XmlWhitespace))
				{
					string localName = xmlNode.LocalName;
					switch (localName)
					{
					case "EncryptionMethod":
						this.EncryptionMethod = new EncryptionMethod();
						this.EncryptionMethod.LoadXml((XmlElement)xmlNode);
						break;
					case "KeyInfo":
						base.KeyInfo = new KeyInfo();
						base.KeyInfo.LoadXml((XmlElement)xmlNode);
						break;
					case "CipherData":
						this.CipherData = new CipherData();
						this.CipherData.LoadXml((XmlElement)xmlNode);
						break;
					case "EncryptionProperties":
						foreach (object obj2 in ((XmlElement)xmlNode).GetElementsByTagName("EncryptionProperty", "http://www.w3.org/2001/04/xmlenc#"))
						{
							XmlElement elemProp = (XmlElement)obj2;
							this.EncryptionProperties.Add(new EncryptionProperty(elemProp));
						}
						break;
					}
				}
			}
			if (value.HasAttribute("Id"))
			{
				this.Id = value.Attributes["Id"].Value;
			}
			if (value.HasAttribute("Type"))
			{
				this.Type = value.Attributes["Type"].Value;
			}
			if (value.HasAttribute("MimeType"))
			{
				this.MimeType = value.Attributes["MimeType"].Value;
			}
			if (value.HasAttribute("Encoding"))
			{
				this.Encoding = value.Attributes["Encoding"].Value;
			}
		}
	}
}
