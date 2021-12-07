using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Encapsulates the encryption algorithm used for XML encryption. </summary>
	// Token: 0x0200003E RID: 62
	public class EncryptionMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class. </summary>
		// Token: 0x060001A7 RID: 423 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public EncryptionMethod()
		{
			this.KeyAlgorithm = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class specifying an algorithm Uniform Resource Identifier (URI). </summary>
		/// <param name="algorithm">The Uniform Resource Identifier (URI) that describes the algorithm represented by an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class. </param>
		// Token: 0x060001A8 RID: 424 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public EncryptionMethod(string strAlgorithm)
		{
			this.KeyAlgorithm = strAlgorithm;
		}

		/// <summary>Gets or sets a Uniform Resource Identifier (URI) that describes the algorithm to use for XML encryption. </summary>
		/// <returns>A Uniform Resource Identifier (URI) that describes the algorithm to use for XML encryption.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008AC4 File Offset: 0x00006CC4
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00008ACC File Offset: 0x00006CCC
		public string KeyAlgorithm
		{
			get
			{
				return this.algorithm;
			}
			set
			{
				this.algorithm = value;
			}
		}

		/// <summary>Gets or sets the algorithm key size used for XML encryption. </summary>
		/// <returns>The algorithm key size, in bits, used for XML encryption.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Security.Cryptography.Xml.EncryptionMethod.KeySize" /> property was set to a value that was less than 0.</exception>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008AD8 File Offset: 0x00006CD8
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("The key size should be a non negative integer.");
				}
				this.keySize = value;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001AD RID: 429 RVA: 0x00008AFC File Offset: 0x00006CFC
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008B0C File Offset: 0x00006D0C
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptionMethod", "http://www.w3.org/2001/04/xmlenc#");
			if (this.KeySize != 0)
			{
				XmlElement xmlElement2 = document.CreateElement("KeySize", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.InnerText = string.Format("{0}", this.keySize);
				xmlElement.AppendChild(xmlElement2);
			}
			if (this.KeyAlgorithm != null)
			{
				xmlElement.SetAttribute("Algorithm", this.KeyAlgorithm);
			}
			return xmlElement;
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object to match.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The key size expressed in the <paramref name="value" /> parameter was less than 0. </exception>
		// Token: 0x060001AF RID: 431 RVA: 0x00008B88 File Offset: 0x00006D88
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptionMethod" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed EncryptionMethod element.");
			}
			this.KeyAlgorithm = null;
			foreach (object obj in value.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode is XmlWhitespace))
				{
					string localName = xmlNode.LocalName;
					if (localName != null)
					{
						if (EncryptionMethod.<>f__switch$mapA == null)
						{
							EncryptionMethod.<>f__switch$mapA = new Dictionary<string, int>(1)
							{
								{
									"KeySize",
									0
								}
							};
						}
						int num;
						if (EncryptionMethod.<>f__switch$mapA.TryGetValue(localName, out num))
						{
							if (num == 0)
							{
								this.KeySize = int.Parse(xmlNode.InnerText);
							}
						}
					}
				}
			}
			if (value.HasAttribute("Algorithm"))
			{
				this.KeyAlgorithm = value.Attributes["Algorithm"].Value;
			}
		}

		// Token: 0x040000DE RID: 222
		private string algorithm;

		// Token: 0x040000DF RID: 223
		private int keySize;
	}
}
