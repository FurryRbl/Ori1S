using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;EncryptedKey&gt; element in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200003A RID: 58
	public sealed class EncryptedKey : EncryptedType
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> class.</summary>
		// Token: 0x06000154 RID: 340 RVA: 0x00007114 File Offset: 0x00005314
		public EncryptedKey()
		{
			this.referenceList = new ReferenceList();
		}

		/// <summary>Gets or sets the optional &lt;CarriedKeyName&gt; element in XML encryption.</summary>
		/// <returns>A string that represents a name for the key value.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007128 File Offset: 0x00005328
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00007130 File Offset: 0x00005330
		public string CarriedKeyName
		{
			get
			{
				return this.carriedKeyName;
			}
			set
			{
				this.carriedKeyName = value;
			}
		}

		/// <summary>Gets or sets the optional Recipient attribute in XML encryption.</summary>
		/// <returns>A string representing the value of the Recipient attribute.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000713C File Offset: 0x0000533C
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00007144 File Offset: 0x00005344
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
			set
			{
				this.recipient = value;
			}
		}

		/// <summary>Gets or sets the &lt;ReferenceList&gt; element in XML encryption.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007150 File Offset: 0x00005350
		public ReferenceList ReferenceList
		{
			get
			{
				return this.referenceList;
			}
		}

		/// <summary>Adds a &lt;DataReference&gt; element to the &lt;ReferenceList&gt; element.</summary>
		/// <param name="dataReference">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> object to add to the <see cref="P:System.Security.Cryptography.Xml.EncryptedKey.ReferenceList" /> property.</param>
		// Token: 0x0600015A RID: 346 RVA: 0x00007158 File Offset: 0x00005358
		public void AddReference(DataReference dataReference)
		{
			this.ReferenceList.Add(dataReference);
		}

		/// <summary>Adds a &lt;KeyReference&gt; element to the &lt;ReferenceList&gt; element.</summary>
		/// <param name="keyReference">A <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to add to the <see cref="P:System.Security.Cryptography.Xml.EncryptedKey.ReferenceList" /> property.</param>
		// Token: 0x0600015B RID: 347 RVA: 0x00007168 File Offset: 0x00005368
		public void AddReference(KeyReference keyReference)
		{
			this.ReferenceList.Add(keyReference);
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> that represents the &lt;EncryptedKey&gt; element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> value is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600015C RID: 348 RVA: 0x00007178 File Offset: 0x00005378
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007188 File Offset: 0x00005388
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.CipherData == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			XmlElement xmlElement = document.CreateElement("EncryptedKey", "http://www.w3.org/2001/04/xmlenc#");
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
			if (this.ReferenceList.Count > 0)
			{
				XmlElement xmlElement3 = document.CreateElement("ReferenceList", "http://www.w3.org/2001/04/xmlenc#");
				foreach (object obj2 in this.ReferenceList)
				{
					EncryptedReference encryptedReference = (EncryptedReference)obj2;
					xmlElement3.AppendChild(encryptedReference.GetXml(document));
				}
				xmlElement.AppendChild(xmlElement3);
			}
			if (this.CarriedKeyName != null)
			{
				XmlElement xmlElement4 = document.CreateElement("CarriedKeyName", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement4.InnerText = this.CarriedKeyName;
				xmlElement.AppendChild(xmlElement4);
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
			if (this.Recipient != null)
			{
				xmlElement.SetAttribute("Recipient", this.Recipient);
			}
			return xmlElement;
		}

		/// <summary>Loads the specified XML information into the &lt;EncryptedKey&gt; element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> representing an XML element to use for the &lt;EncryptedKey&gt; element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a <see cref="T:System.Security.Cryptography.Xml.CipherData" />  element.</exception>
		// Token: 0x0600015E RID: 350 RVA: 0x00007410 File Offset: 0x00005610
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptedKey" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed EncryptedKey element.");
			}
			this.EncryptionMethod = null;
			this.EncryptionMethod = null;
			this.EncryptionProperties.Clear();
			this.ReferenceList.Clear();
			this.CarriedKeyName = null;
			this.Id = null;
			this.Type = null;
			this.MimeType = null;
			this.Encoding = null;
			this.Recipient = null;
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
					case "ReferenceList":
						foreach (object obj3 in ((XmlElement)xmlNode).ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj3;
							if (!(xmlNode2 is XmlWhitespace))
							{
								string localName2 = xmlNode2.LocalName;
								if (localName2 != null)
								{
									if (EncryptedKey.<>f__switch$map6 == null)
									{
										EncryptedKey.<>f__switch$map6 = new Dictionary<string, int>(2)
										{
											{
												"DataReference",
												0
											},
											{
												"KeyReference",
												1
											}
										};
									}
									int num2;
									if (EncryptedKey.<>f__switch$map6.TryGetValue(localName2, out num2))
									{
										if (num2 != 0)
										{
											if (num2 == 1)
											{
												KeyReference keyReference = new KeyReference();
												keyReference.LoadXml((XmlElement)xmlNode2);
												this.AddReference(keyReference);
											}
										}
										else
										{
											DataReference dataReference = new DataReference();
											dataReference.LoadXml((XmlElement)xmlNode2);
											this.AddReference(dataReference);
										}
									}
								}
							}
						}
						break;
					case "CarriedKeyName":
						this.CarriedKeyName = ((XmlElement)xmlNode).InnerText;
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
			if (value.HasAttribute("Recipient"))
			{
				this.Encoding = value.Attributes["Recipient"].Value;
			}
		}

		// Token: 0x040000B0 RID: 176
		private string carriedKeyName;

		// Token: 0x040000B1 RID: 177
		private string recipient;

		// Token: 0x040000B2 RID: 178
		private ReferenceList referenceList;
	}
}
