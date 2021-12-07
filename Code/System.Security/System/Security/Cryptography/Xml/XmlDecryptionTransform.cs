using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Specifies the order of XML Digital Signature and XML Encryption operations when both are performed on the same document.</summary>
	// Token: 0x02000055 RID: 85
	public class XmlDecryptionTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> class. </summary>
		// Token: 0x060002C2 RID: 706 RVA: 0x0000CE98 File Offset: 0x0000B098
		public XmlDecryptionTransform()
		{
			base.Algorithm = "http://www.w3.org/2002/07/decrypt#XML";
			this.encryptedXml = new EncryptedXml();
			this.exceptUris = new ArrayList();
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public EncryptedXml EncryptedXml
		{
			get
			{
				return this.encryptedXml;
			}
			set
			{
				this.encryptedXml = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		public override Type[] InputTypes
		{
			get
			{
				if (this.inputTypes == null)
				{
					this.inputTypes = new Type[]
					{
						typeof(Stream),
						typeof(XmlDocument)
					};
				}
				return this.inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000CF14 File Offset: 0x0000B114
		public override Type[] OutputTypes
		{
			get
			{
				if (this.outputTypes == null)
				{
					this.outputTypes = new Type[]
					{
						typeof(XmlDocument)
					};
				}
				return this.outputTypes;
			}
		}

		/// <summary>Adds a Uniform Resource Identifier (URI) to exclude from processing.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) to exclude from processing</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is null.</exception>
		// Token: 0x060002C7 RID: 711 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public void AddExceptUri(string uri)
		{
			this.exceptUris.Add(uri);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000CF5C File Offset: 0x0000B15C
		private void ClearExceptUris()
		{
			this.exceptUris.Clear();
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060002C9 RID: 713 RVA: 0x0000CF6C File Offset: 0x0000B16C
		[MonoTODO("Verify")]
		protected override XmlNodeList GetInnerXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateElement("DecryptionTransform"));
			foreach (object obj in this.exceptUris)
			{
				XmlElement xmlElement = xmlDocument.CreateElement("Except", "http://www.w3.org/2002/07/decrypt#");
				xmlElement.Attributes.Append(xmlDocument.CreateAttribute("URI", "http://www.w3.org/2002/07/decrypt#"));
				xmlElement.Attributes["URI", "http://www.w3.org/2002/07/decrypt#"].Value = (string)obj;
				xmlDocument.DocumentElement.AppendChild(xmlElement);
			}
			return xmlDocument.GetElementsByTagName("Except", "http://www.w3.org/2002/07/decrypt#");
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A decryption key could not be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x060002CA RID: 714 RVA: 0x0000D054 File Offset: 0x0000B254
		[MonoTODO("Verify processing of ExceptURIs")]
		public override object GetOutput()
		{
			XmlDocument xmlDocument;
			if (this.inputObj is Stream)
			{
				xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.XmlResolver = base.GetResolver();
				xmlDocument.Load(new XmlSignatureStreamReader(new StreamReader(this.inputObj as Stream)));
			}
			else
			{
				if (!(this.inputObj is XmlDocument))
				{
					throw new NullReferenceException();
				}
				xmlDocument = (this.inputObj as XmlDocument);
			}
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("EncryptedData", "http://www.w3.org/2001/04/xmlenc#");
			foreach (object obj in elementsByTagName)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode == xmlDocument.DocumentElement && this.exceptUris.Contains("#xpointer(/)"))
				{
					break;
				}
				foreach (object obj2 in this.exceptUris)
				{
					string text = (string)obj2;
					if (this.IsTargetElement((XmlElement)xmlNode, text.Substring(1)))
					{
						break;
					}
				}
				EncryptedData encryptedData = new EncryptedData();
				encryptedData.LoadXml((XmlElement)xmlNode);
				SymmetricAlgorithm decryptionKey = this.EncryptedXml.GetDecryptionKey(encryptedData, encryptedData.EncryptionMethod.KeyAlgorithm);
				this.EncryptedXml.ReplaceData((XmlElement)xmlNode, this.EncryptedXml.DecryptData(encryptedData, decryptionKey));
			}
			return xmlDocument;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		// Token: 0x060002CB RID: 715 RVA: 0x0000D230 File Offset: 0x0000B430
		public override object GetOutput(Type type)
		{
			if (type == typeof(Stream))
			{
				return this.GetOutput();
			}
			throw new ArgumentException("type");
		}

		/// <summary>Determines whether the ID attribute of an <see cref="T:System.Xml.XmlElement" /> object matches a specified value.</summary>
		/// <returns>true if the ID attribute of the <paramref name="inputElement" /> parameter matches the <paramref name="idValue" /> parameter; otherwise, false. </returns>
		/// <param name="inputElement">An <see cref="T:System.Xml.XmlElement" /> object with an ID attribute to compare with <paramref name="idValue" />.</param>
		/// <param name="idValue">The value to compare with the ID attribute of <paramref name="inputElement" />.</param>
		// Token: 0x060002CC RID: 716 RVA: 0x0000D254 File Offset: 0x0000B454
		[MonoTODO("verify")]
		protected virtual bool IsTargetElement(XmlElement inputElement, string idValue)
		{
			return inputElement != null && idValue != null && inputElement.Attributes["id"].Value == idValue;
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is null.-or-The Uniform Resource Identifier (URI) value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> was not found.-or-The length of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is 0. -or-The first character of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is not '#'.  </exception>
		// Token: 0x060002CD RID: 717 RVA: 0x0000D28C File Offset: 0x0000B48C
		[MonoTODO("This doesn't seem to work in .NET")]
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new NullReferenceException();
			}
			this.ClearExceptUris();
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement.NamespaceURI.Equals("http://www.w3.org/2002/07/decrypt#") && xmlElement.LocalName.Equals("Except"))
				{
					string value = xmlElement.Attributes["URI", "http://www.w3.org/2002/07/decrypt#"].Value;
					if (!value.StartsWith("#"))
					{
						throw new CryptographicException("A Uri attribute is required for a CipherReference element.");
					}
					this.AddExceptUri(value);
				}
			}
		}

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002CE RID: 718 RVA: 0x0000D374 File Offset: 0x0000B574
		public override void LoadInput(object obj)
		{
			this.inputObj = obj;
		}

		// Token: 0x04000132 RID: 306
		private const string NamespaceUri = "http://www.w3.org/2002/07/decrypt#";

		// Token: 0x04000133 RID: 307
		private EncryptedXml encryptedXml;

		// Token: 0x04000134 RID: 308
		private Type[] inputTypes;

		// Token: 0x04000135 RID: 309
		private Type[] outputTypes;

		// Token: 0x04000136 RID: 310
		private object inputObj;

		// Token: 0x04000137 RID: 311
		private ArrayList exceptUris;
	}
}
