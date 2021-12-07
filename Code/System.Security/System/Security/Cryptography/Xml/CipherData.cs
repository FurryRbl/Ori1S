using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;CipherData&gt; element in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000034 RID: 52
	public sealed class CipherData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class.</summary>
		// Token: 0x06000128 RID: 296 RVA: 0x000062B4 File Offset: 0x000044B4
		public CipherData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a byte array as the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> value.</summary>
		/// <param name="cipherValue">The encrypted data to use for the &lt;CipherValue&gt; element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		// Token: 0x06000129 RID: 297 RVA: 0x000062BC File Offset: 0x000044BC
		public CipherData(byte[] cipherValue)
		{
			this.CipherValue = cipherValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</summary>
		/// <param name="cipherReference">The <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		// Token: 0x0600012A RID: 298 RVA: 0x000062CC File Offset: 0x000044CC
		public CipherData(CipherReference cipherReference)
		{
			this.CipherReference = cipherReference;
		}

		/// <summary>Gets or sets the &lt;CipherReference&gt; element.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" />  property was set to null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" />  property was set more than once.</exception>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000062DC File Offset: 0x000044DC
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000062E4 File Offset: 0x000044E4
		public CipherReference CipherReference
		{
			get
			{
				return this.cipherReference;
			}
			set
			{
				if (this.CipherValue != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this.cipherReference = value;
			}
		}

		/// <summary>Gets or sets the &lt;CipherValue&gt; element.</summary>
		/// <returns>A byte array that represents the &lt;CipherValue&gt; element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" />  property was set to null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" />  property was set more than once.</exception>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006304 File Offset: 0x00004504
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000630C File Offset: 0x0000450C
		public byte[] CipherValue
		{
			get
			{
				return this.cipherValue;
			}
			set
			{
				if (this.CipherReference != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this.cipherValue = value;
			}
		}

		/// <summary>Gets the XML values for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlElement" /> object that represents the XML information for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600012F RID: 303 RVA: 0x0000632C File Offset: 0x0000452C
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000633C File Offset: 0x0000453C
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.CipherReference == null && this.CipherValue == null)
			{
				throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
			}
			XmlElement xmlElement = document.CreateElement("CipherData", "http://www.w3.org/2001/04/xmlenc#");
			if (this.CipherReference != null)
			{
				xmlElement.AppendChild(document.ImportNode(this.cipherReference.GetXml(), true));
			}
			if (this.CipherValue != null)
			{
				XmlElement xmlElement2 = document.CreateElement("CipherValue", "http://www.w3.org/2001/04/xmlenc#");
				StreamReader streamReader = new StreamReader(new CryptoStream(new MemoryStream(this.cipherValue), new ToBase64Transform(), CryptoStreamMode.Read));
				xmlElement2.InnerText = streamReader.ReadToEnd();
				streamReader.Close();
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		/// <summary>Loads XML data from an <see cref="T:System.Xml.XmlElement" /> into a <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> that represents the XML data to load.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are null.</exception>
		// Token: 0x06000131 RID: 305 RVA: 0x000063F4 File Offset: 0x000045F4
		public void LoadXml(XmlElement value)
		{
			this.CipherReference = null;
			this.CipherValue = null;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "CipherData" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed Cipher Data element.");
			}
			foreach (object obj in value.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode is XmlWhitespace))
				{
					string localName = xmlNode.LocalName;
					if (localName != null)
					{
						if (CipherData.<>f__switch$map1 == null)
						{
							CipherData.<>f__switch$map1 = new Dictionary<string, int>(2)
							{
								{
									"CipherReference",
									0
								},
								{
									"CipherValue",
									1
								}
							};
						}
						int num;
						if (CipherData.<>f__switch$map1.TryGetValue(localName, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									this.CipherValue = Convert.FromBase64String(xmlNode.InnerText);
								}
							}
							else
							{
								this.cipherReference = new CipherReference();
								this.cipherReference.LoadXml((XmlElement)xmlNode);
							}
						}
					}
				}
			}
			if (this.CipherReference == null && this.CipherValue == null)
			{
				throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
			}
		}

		// Token: 0x040000A8 RID: 168
		private byte[] cipherValue;

		// Token: 0x040000A9 RID: 169
		private CipherReference cipherReference;
	}
}
