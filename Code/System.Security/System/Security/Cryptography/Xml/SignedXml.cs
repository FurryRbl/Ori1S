using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Provides a wrapper on a core XML signature object to facilitate creating XML signatures.</summary>
	// Token: 0x02000050 RID: 80
	public class SignedXml
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class.</summary>
		// Token: 0x0600026E RID: 622 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public SignedXml()
		{
			this.m_signature = new Signature();
			this.m_signature.SignedInfo = new SignedInfo();
			this.hashes = new Hashtable(2);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class from the specified XML document.</summary>
		/// <param name="document">The <see cref="T:System.Xml.XmlDocument" /> object to use to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.SignedXml" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="document" /> parameter is null.-or-The <paramref name="document" /> parameter contains a null <see cref="P:System.Xml.XmlDocument.DocumentElement" /> property.</exception>
		// Token: 0x0600026F RID: 623 RVA: 0x0000B13C File Offset: 0x0000933C
		public SignedXml(XmlDocument document) : this()
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.envdoc = document;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class from the specified <see cref="T:System.Xml.XmlElement" /> object.</summary>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> object to use to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.SignedXml" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is null. </exception>
		// Token: 0x06000270 RID: 624 RVA: 0x0000B15C File Offset: 0x0000935C
		public SignedXml(XmlElement elem) : this()
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			this.envdoc = new XmlDocument();
			this.envdoc.LoadXml(elem.OuterXml);
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that defines the XML encryption processing rules.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that defines the XML encryption processing rules.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B1AC File Offset: 0x000093AC
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000B1B4 File Offset: 0x000093B4
		[ComVisible(false)]
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

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000B1C0 File Offset: 0x000093C0
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public KeyInfo KeyInfo
		{
			get
			{
				if (this.m_signature.KeyInfo == null)
				{
					this.m_signature.KeyInfo = new KeyInfo();
				}
				return this.m_signature.KeyInfo;
			}
			set
			{
				this.m_signature.KeyInfo = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B200 File Offset: 0x00009400
		public Signature Signature
		{
			get
			{
				return this.m_signature;
			}
		}

		/// <summary>Gets the length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000B208 File Offset: 0x00009408
		public string SignatureLength
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureLength;
			}
		}

		/// <summary>Gets the signature method of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The signature method of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B21C File Offset: 0x0000941C
		public string SignatureMethod
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureMethod;
			}
		}

		/// <summary>Gets the signature value of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>A byte array that contains the signature value of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000B230 File Offset: 0x00009430
		public byte[] SignatureValue
		{
			get
			{
				return this.m_signature.SignatureValue;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B240 File Offset: 0x00009440
		public SignedInfo SignedInfo
		{
			get
			{
				return this.m_signature.SignedInfo;
			}
		}

		/// <summary>Gets or sets the asymmetric algorithm key used for signing a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The asymmetric algorithm key used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000B250 File Offset: 0x00009450
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000B258 File Offset: 0x00009458
		public AsymmetricAlgorithm SigningKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		/// <summary>Gets or sets the name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000B264 File Offset: 0x00009464
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000B26C File Offset: 0x0000946C
		public string SigningKeyName
		{
			get
			{
				return this.m_strSigningKeyName;
			}
			set
			{
				this.m_strSigningKeyName = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object to the list of objects to be signed.</summary>
		/// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object to add to the list of objects to be signed. </param>
		// Token: 0x0600027F RID: 639 RVA: 0x0000B278 File Offset: 0x00009478
		public void AddObject(DataObject dataObject)
		{
			this.m_signature.AddObject(dataObject);
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Reference" /> object to the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object that describes a digest method, digest value, and transform to use for creating an XML digital signature.</summary>
		/// <param name="reference">The  <see cref="T:System.Security.Cryptography.Xml.Reference" /> object that describes a digest method, digest value, and transform to use for creating an XML digital signature.</param>
		// Token: 0x06000280 RID: 640 RVA: 0x0000B288 File Offset: 0x00009488
		public void AddReference(Reference reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			this.m_signature.SignedInfo.AddReference(reference);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B2B8 File Offset: 0x000094B8
		private Stream ApplyTransform(Transform t, XmlDocument input)
		{
			if (t is XmlDsigXPathTransform || t is XmlDsigEnvelopedSignatureTransform || t is XmlDecryptionTransform)
			{
				input = (XmlDocument)input.Clone();
			}
			t.LoadInput(input);
			if (t is XmlDsigEnvelopedSignatureTransform)
			{
				return this.CanonicalizeOutput(t.GetOutput());
			}
			object output = t.GetOutput();
			if (output is Stream)
			{
				return (Stream)output;
			}
			if (output is XmlDocument)
			{
				MemoryStream memoryStream = new MemoryStream();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				((XmlDocument)output).WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				memoryStream.Position = 0L;
				return memoryStream;
			}
			if (output == null)
			{
				throw new NotImplementedException("This should not occur. Transform is " + t + ".");
			}
			return this.CanonicalizeOutput(output);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B388 File Offset: 0x00009588
		private Stream CanonicalizeOutput(object obj)
		{
			Transform c14NMethod = this.GetC14NMethod();
			c14NMethod.LoadInput(obj);
			return (Stream)c14NMethod.GetOutput();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B3B0 File Offset: 0x000095B0
		private XmlDocument GetManifest(Reference r)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			if (r.Uri[0] == '#')
			{
				if (this.signatureElement != null)
				{
					XmlElement idElement = this.GetIdElement(this.signatureElement.OwnerDocument, r.Uri.Substring(1));
					if (idElement == null)
					{
						throw new CryptographicException("Manifest targeted by Reference was not found: " + r.Uri.Substring(1));
					}
					xmlDocument.LoadXml(idElement.OuterXml);
					this.FixupNamespaceNodes(idElement, xmlDocument.DocumentElement, false);
				}
			}
			else if (this.xmlResolver != null)
			{
				Stream inStream = (Stream)this.xmlResolver.GetEntity(new Uri(r.Uri), null, typeof(Stream));
				xmlDocument.Load(inStream);
			}
			if (xmlDocument.FirstChild != null)
			{
				if (this.manifests == null)
				{
					this.manifests = new ArrayList();
				}
				this.manifests.Add(xmlDocument);
				return xmlDocument;
			}
			return null;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B4B4 File Offset: 0x000096B4
		private void FixupNamespaceNodes(XmlElement src, XmlElement dst, bool ignoreDefault)
		{
			foreach (object obj in src.SelectNodes("namespace::*"))
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				if (!(xmlAttribute.LocalName == "xml"))
				{
					if (!ignoreDefault || !(xmlAttribute.LocalName == "xmlns"))
					{
						dst.SetAttributeNode(dst.OwnerDocument.ImportNode(xmlAttribute, true) as XmlAttribute);
					}
				}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B574 File Offset: 0x00009774
		private byte[] GetReferenceHash(Reference r, bool check_hmac)
		{
			Stream stream = null;
			XmlDocument xmlDocument = null;
			if (r.Uri == string.Empty)
			{
				xmlDocument = this.envdoc;
			}
			else if (r.Type == "http://www.w3.org/2000/09/xmldsig#Manifest")
			{
				xmlDocument = this.GetManifest(r);
			}
			else
			{
				xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				string text = null;
				if (r.Uri.StartsWith("#xpointer"))
				{
					string text2 = string.Join(string.Empty, r.Uri.Substring(9).Split(SignedXml.whitespaceChars));
					if (text2.Length < 2 || text2[0] != '(' || text2[text2.Length - 1] != ')')
					{
						text2 = string.Empty;
					}
					else
					{
						text2 = text2.Substring(1, text2.Length - 2);
					}
					if (text2 == "/")
					{
						xmlDocument = this.envdoc;
					}
					else if (text2.Length > 6 && text2.StartsWith("id(") && text2[text2.Length - 1] == ')')
					{
						text = text2.Substring(4, text2.Length - 6);
					}
				}
				else if (r.Uri[0] == '#')
				{
					text = r.Uri.Substring(1);
				}
				else if (this.xmlResolver != null)
				{
					try
					{
						Uri absoluteUri = new Uri(r.Uri);
						stream = (Stream)this.xmlResolver.GetEntity(absoluteUri, null, typeof(Stream));
					}
					catch
					{
						stream = File.OpenRead(r.Uri);
					}
				}
				if (text != null)
				{
					XmlElement xmlElement = null;
					foreach (object obj in this.m_signature.ObjectList)
					{
						DataObject dataObject = (DataObject)obj;
						if (dataObject.Id == text)
						{
							xmlElement = dataObject.GetXml();
							xmlElement.SetAttribute("xmlns", "http://www.w3.org/2000/09/xmldsig#");
							xmlDocument.LoadXml(xmlElement.OuterXml);
							foreach (object obj2 in xmlElement.ChildNodes)
							{
								XmlNode xmlNode = (XmlNode)obj2;
								if (xmlNode.NodeType == XmlNodeType.Element)
								{
									this.FixupNamespaceNodes(xmlNode as XmlElement, xmlDocument.DocumentElement, true);
								}
							}
							break;
						}
					}
					if (xmlElement == null && this.envdoc != null)
					{
						xmlElement = this.GetIdElement(this.envdoc, text);
						if (xmlElement != null)
						{
							xmlDocument.LoadXml(xmlElement.OuterXml);
						}
					}
					if (xmlElement == null)
					{
						throw new CryptographicException(string.Format("Malformed reference object: {0}", text));
					}
				}
			}
			if (r.TransformChain.Count > 0)
			{
				foreach (object obj3 in r.TransformChain)
				{
					Transform transform = (Transform)obj3;
					if (stream == null)
					{
						stream = this.ApplyTransform(transform, xmlDocument);
					}
					else
					{
						transform.LoadInput(stream);
						object output = transform.GetOutput();
						if (output is Stream)
						{
							stream = (Stream)output;
						}
						else
						{
							stream = this.CanonicalizeOutput(output);
						}
					}
				}
			}
			else if (stream == null)
			{
				if (r.Uri[0] != '#')
				{
					stream = new MemoryStream();
					xmlDocument.Save(stream);
				}
				else
				{
					stream = this.ApplyTransform(new XmlDsigC14NTransform(), xmlDocument);
				}
			}
			HashAlgorithm hash = this.GetHash(r.DigestMethod, check_hmac);
			return (hash != null) ? hash.ComputeHash(stream) : null;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private void DigestReferences()
		{
			foreach (object obj in this.m_signature.SignedInfo.References)
			{
				Reference reference = (Reference)obj;
				if (reference.DigestMethod == null)
				{
					reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
				}
				reference.DigestValue = this.GetReferenceHash(reference, false);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BA74 File Offset: 0x00009C74
		private Transform GetC14NMethod()
		{
			Transform transform = (Transform)CryptoConfig.CreateFromName(this.m_signature.SignedInfo.CanonicalizationMethod);
			if (transform == null)
			{
				throw new CryptographicException("Unknown Canonicalization Method {0}", this.m_signature.SignedInfo.CanonicalizationMethod);
			}
			return transform;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		private Stream SignedInfoTransformed()
		{
			Transform c14NMethod = this.GetC14NMethod();
			if (this.signatureElement == null)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.LoadXml(this.m_signature.SignedInfo.GetXml().OuterXml);
				if (this.envdoc != null)
				{
					foreach (object obj in this.envdoc.DocumentElement.SelectNodes("namespace::*"))
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						if (!(xmlAttribute.LocalName == "xml"))
						{
							if (!(xmlAttribute.Prefix == xmlDocument.DocumentElement.Prefix))
							{
								xmlDocument.DocumentElement.SetAttributeNode(xmlDocument.ImportNode(xmlAttribute, true) as XmlAttribute);
							}
						}
					}
				}
				c14NMethod.LoadInput(xmlDocument);
			}
			else
			{
				XmlElement xmlElement = this.signatureElement.GetElementsByTagName("SignedInfo", "http://www.w3.org/2000/09/xmldsig#")[0] as XmlElement;
				StringWriter stringWriter = new StringWriter();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
				XmlNodeList xmlNodeList = xmlElement.SelectNodes("namespace::*");
				foreach (object obj2 in xmlNodeList)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj2;
					if (xmlAttribute2.ParentNode != xmlElement)
					{
						if (!(xmlAttribute2.LocalName == "xml"))
						{
							if (!(xmlAttribute2.Prefix == xmlElement.Prefix))
							{
								xmlAttribute2.WriteTo(xmlTextWriter);
							}
						}
					}
				}
				foreach (object obj3 in xmlElement.Attributes)
				{
					XmlNode xmlNode = (XmlNode)obj3;
					xmlNode.WriteTo(xmlTextWriter);
				}
				foreach (object obj4 in xmlElement.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj4;
					xmlNode2.WriteTo(xmlTextWriter);
				}
				xmlTextWriter.WriteEndElement();
				byte[] bytes = Encoding.UTF8.GetBytes(stringWriter.ToString());
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.Position = 0L;
				c14NMethod.LoadInput(memoryStream);
			}
			return (Stream)c14NMethod.GetOutput();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BE08 File Offset: 0x0000A008
		private HashAlgorithm GetHash(string algorithm, bool check_hmac)
		{
			HashAlgorithm hashAlgorithm = (HashAlgorithm)this.hashes[algorithm];
			if (hashAlgorithm == null)
			{
				hashAlgorithm = HashAlgorithm.Create(algorithm);
				if (hashAlgorithm == null)
				{
					throw new CryptographicException("Unknown hash algorithm: {0}", algorithm);
				}
				this.hashes.Add(algorithm, hashAlgorithm);
			}
			else
			{
				hashAlgorithm.Initialize();
			}
			if (check_hmac && hashAlgorithm is KeyedHashAlgorithm)
			{
				return null;
			}
			return hashAlgorithm;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies; otherwise, false.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the public key in the signature does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.-or- The signature description could not be created.-or The hash algorithm could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600028A RID: 650 RVA: 0x0000BE74 File Offset: 0x0000A074
		public bool CheckSignature()
		{
			return this.CheckSignatureInternal(null) != null;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000BE84 File Offset: 0x0000A084
		private bool CheckReferenceIntegrity(ArrayList referenceList)
		{
			if (referenceList == null)
			{
				return false;
			}
			foreach (object obj in referenceList)
			{
				Reference reference = (Reference)obj;
				byte[] referenceHash = this.GetReferenceHash(reference, true);
				if (!this.Compare(reference.DigestValue, referenceHash))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified key.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified key; otherwise, false.</returns>
		/// <param name="key">The implementation of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> property that holds the key to be used to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the <paramref name="key" /> parameter does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.-or- The signature description could not be created.-or The hash algorithm could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600028C RID: 652 RVA: 0x0000BF18 File Offset: 0x0000A118
		public bool CheckSignature(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.CheckSignatureInternal(key) != null;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BF38 File Offset: 0x0000A138
		private AsymmetricAlgorithm CheckSignatureInternal(AsymmetricAlgorithm key)
		{
			this.pkEnumerator = null;
			if (key != null)
			{
				if (!this.CheckSignatureWithKey(key))
				{
					return null;
				}
			}
			else
			{
				if (this.Signature.KeyInfo == null)
				{
					return null;
				}
				while ((key = this.GetPublicKey()) != null)
				{
					if (this.CheckSignatureWithKey(key))
					{
						break;
					}
				}
				this.pkEnumerator = null;
				if (key == null)
				{
					return null;
				}
			}
			if (!this.CheckReferenceIntegrity(this.m_signature.SignedInfo.References))
			{
				return null;
			}
			if (this.manifests != null)
			{
				for (int i = 0; i < this.manifests.Count; i++)
				{
					Manifest manifest = new Manifest((this.manifests[i] as XmlDocument).DocumentElement);
					if (!this.CheckReferenceIntegrity(manifest.References))
					{
						return null;
					}
				}
			}
			return key;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C020 File Offset: 0x0000A220
		private bool CheckSignatureWithKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				return false;
			}
			SignatureDescription signatureDescription = (SignatureDescription)CryptoConfig.CreateFromName(this.m_signature.SignedInfo.SignatureMethod);
			if (signatureDescription == null)
			{
				return false;
			}
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(signatureDescription.DeformatterAlgorithm);
			if (asymmetricSignatureDeformatter == null)
			{
				return false;
			}
			bool result;
			try
			{
				asymmetricSignatureDeformatter.SetKey(key);
				asymmetricSignatureDeformatter.SetHashAlgorithm(signatureDescription.DigestAlgorithm);
				HashAlgorithm hash = this.GetHash(signatureDescription.DigestAlgorithm, true);
				MemoryStream inputStream = (MemoryStream)this.SignedInfoTransformed();
				byte[] rgbHash = hash.ComputeHash(inputStream);
				result = asymmetricSignatureDeformatter.VerifySignature(rgbHash, this.m_signature.SignatureValue);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		private bool Compare(byte[] expected, byte[] actual)
		{
			bool flag = expected != null && actual != null;
			if (flag)
			{
				int num = expected.Length;
				flag = (num == actual.Length);
				if (flag)
				{
					for (int i = 0; i < num; i++)
					{
						if (expected[i] != actual[i])
						{
							return false;
						}
					}
				}
			}
			return flag;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified message authentication code (MAC) algorithm.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified MAC; otherwise, false.</returns>
		/// <param name="macAlg">The implementation of <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> that holds the MAC to be used to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="macAlg" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.HashAlgorithm.HashSize" /> property of the specified <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object is not valid.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property is null.-or- The cryptographic transform used to check the signature could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000290 RID: 656 RVA: 0x0000C14C File Offset: 0x0000A34C
		public bool CheckSignature(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			this.pkEnumerator = null;
			Stream stream = this.SignedInfoTransformed();
			if (stream == null)
			{
				return false;
			}
			byte[] array = macAlg.ComputeHash(stream);
			if (this.m_signature.SignedInfo.SignatureLength != null)
			{
				int num = int.Parse(this.m_signature.SignedInfo.SignatureLength);
				if ((num & 7) != 0)
				{
					throw new CryptographicException("Signature length must be a multiple of 8 bits.");
				}
				num >>= 3;
				if (num != this.m_signature.SignatureValue.Length)
				{
					throw new CryptographicException("Invalid signature length.");
				}
				int num2 = Math.Max(10, array.Length / 2);
				if (num < num2)
				{
					throw new CryptographicException("HMAC signature is too small");
				}
				if (num < array.Length)
				{
					byte[] array2 = new byte[num];
					Buffer.BlockCopy(array, 0, array2, 0, num);
					array = array2;
				}
			}
			return this.Compare(this.m_signature.SignatureValue, array) && this.CheckReferenceIntegrity(this.m_signature.SignedInfo.References);
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object and, optionally, whether the certificate is valid.</summary>
		/// <returns>true if the signature is valid; otherwise, false. -or-true if the signature and certificate are valid; otherwise, false. </returns>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to use to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property.</param>
		/// <param name="verifySignatureOnly">true to verify the signature only; false to verify both the signature and certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A signature description could not be created for the <paramref name="certificate" /> parameter.</exception>
		// Token: 0x06000291 RID: 657 RVA: 0x0000C258 File Offset: 0x0000A458
		[MonoTODO]
		[ComVisible(false)]
		public bool CheckSignature(X509Certificate2 certificate, bool verifySignatureOnly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature.</summary>
		/// <returns>true if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature; otherwise, false.</returns>
		/// <param name="signingKey">When this method returns, contains the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key in the signature. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="signingKey" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the public key in the signature does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.-or- The signature description could not be created.-or The hash algorithm could not be created. </exception>
		// Token: 0x06000292 RID: 658 RVA: 0x0000C260 File Offset: 0x0000A460
		public bool CheckSignatureReturningKey(out AsymmetricAlgorithm signingKey)
		{
			signingKey = this.CheckSignatureInternal(null);
			return signingKey != null;
		}

		/// <summary>Computes an XML digital signature.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SigningKey" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SigningKey" /> property is not a <see cref="T:System.Security.Cryptography.DSA" /> object or <see cref="T:System.Security.Cryptography.RSA" /> object.-or- The key could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000293 RID: 659 RVA: 0x0000C274 File Offset: 0x0000A474
		public void ComputeSignature()
		{
			if (this.key != null)
			{
				if (this.m_signature.SignedInfo.SignatureMethod == null)
				{
					this.m_signature.SignedInfo.SignatureMethod = this.key.SignatureAlgorithm;
				}
				else if (this.m_signature.SignedInfo.SignatureMethod != this.key.SignatureAlgorithm)
				{
					throw new CryptographicException("Specified SignatureAlgorithm is not supported by the signing key.");
				}
				this.DigestReferences();
				AsymmetricSignatureFormatter asymmetricSignatureFormatter = null;
				if (this.key is DSA)
				{
					asymmetricSignatureFormatter = new DSASignatureFormatter(this.key);
				}
				else if (this.key is RSA)
				{
					asymmetricSignatureFormatter = new RSAPKCS1SignatureFormatter(this.key);
				}
				if (asymmetricSignatureFormatter != null)
				{
					SignatureDescription signatureDescription = (SignatureDescription)CryptoConfig.CreateFromName(this.m_signature.SignedInfo.SignatureMethod);
					HashAlgorithm hash = this.GetHash(signatureDescription.DigestAlgorithm, false);
					byte[] rgbHash = hash.ComputeHash(this.SignedInfoTransformed());
					asymmetricSignatureFormatter.SetHashAlgorithm("SHA1");
					this.m_signature.SignatureValue = asymmetricSignatureFormatter.CreateSignature(rgbHash);
				}
				return;
			}
			throw new CryptographicException("signing key is not specified");
		}

		/// <summary>Computes an XML digital signature using the specified message authentication code (MAC) algorithm.</summary>
		/// <param name="macAlg">A <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object that holds the MAC to be used to compute the value of the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="macAlg" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object specified by the <paramref name="macAlg" /> parameter is not an instance of <see cref="T:System.Security.Cryptography.HMACSHA1" />.-or- The <see cref="P:System.Security.Cryptography.HashAlgorithm.HashSize" /> property of the specified <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object is not valid.-or- The cryptographic transform used to check the signature could not be created. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000294 RID: 660 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public void ComputeSignature(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			string text = null;
			if (macAlg is HMACSHA1)
			{
				text = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
			}
			else if (macAlg is HMACSHA256)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
			}
			else if (macAlg is HMACSHA384)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
			}
			else if (macAlg is HMACSHA512)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";
			}
			else if (macAlg is HMACRIPEMD160)
			{
				text = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";
			}
			if (text == null)
			{
				throw new CryptographicException("unsupported algorithm");
			}
			this.DigestReferences();
			this.m_signature.SignedInfo.SignatureMethod = text;
			this.m_signature.SignatureValue = macAlg.ComputeHash(this.SignedInfoTransformed());
		}

		/// <summary>Returns the <see cref="T:System.Xml.XmlElement" /> object with the specified ID from the specified <see cref="T:System.Xml.XmlDocument" /> object.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> object with the specified ID from the specified <see cref="T:System.Xml.XmlDocument" /> object, or null if it could not be found.</returns>
		/// <param name="document">The <see cref="T:System.Xml.XmlDocument" /> object to retrieve the <see cref="T:System.Xml.XmlElement" /> object from.</param>
		/// <param name="idValue">The ID of the <see cref="T:System.Xml.XmlElement" /> object to retrieve from the <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000295 RID: 661 RVA: 0x0000C468 File Offset: 0x0000A668
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			if (document == null || idValue == null)
			{
				return null;
			}
			XmlElement xmlElement = document.GetElementById(idValue);
			if (xmlElement == null)
			{
				xmlElement = (XmlElement)document.SelectSingleNode("//*[@Id='" + idValue + "']");
			}
			return xmlElement;
		}

		/// <summary>Returns the public key of a signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object that contains the public key of the signature, or null if the key cannot be found.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.KeyInfo" /> property is null.</exception>
		// Token: 0x06000296 RID: 662 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		protected virtual AsymmetricAlgorithm GetPublicKey()
		{
			if (this.m_signature.KeyInfo == null)
			{
				return null;
			}
			if (this.pkEnumerator == null)
			{
				this.pkEnumerator = this.m_signature.KeyInfo.GetEnumerator();
			}
			if (this._x509Enumerator != null)
			{
				if (this._x509Enumerator.MoveNext())
				{
					X509Certificate x509Certificate = (X509Certificate)this._x509Enumerator.Current;
					return new X509Certificate2(x509Certificate.GetRawCertData()).PublicKey.Key;
				}
				this._x509Enumerator = null;
			}
			while (this.pkEnumerator.MoveNext())
			{
				AsymmetricAlgorithm asymmetricAlgorithm = null;
				KeyInfoClause keyInfoClause = (KeyInfoClause)this.pkEnumerator.Current;
				if (keyInfoClause is DSAKeyValue)
				{
					asymmetricAlgorithm = DSA.Create();
				}
				else if (keyInfoClause is RSAKeyValue)
				{
					asymmetricAlgorithm = RSA.Create();
				}
				if (asymmetricAlgorithm != null)
				{
					asymmetricAlgorithm.FromXmlString(keyInfoClause.GetXml().InnerXml);
					return asymmetricAlgorithm;
				}
				if (keyInfoClause is KeyInfoX509Data)
				{
					this._x509Enumerator = ((KeyInfoX509Data)keyInfoClause).Certificates.GetEnumerator();
					if (this._x509Enumerator.MoveNext())
					{
						X509Certificate x509Certificate2 = (X509Certificate)this._x509Enumerator.Current;
						return new X509Certificate2(x509Certificate2.GetRawCertData()).PublicKey.Key;
					}
				}
			}
			return null;
		}

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignedInfo" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureValue" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000297 RID: 663 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public XmlElement GetXml()
		{
			return this.m_signature.GetXml(this.envdoc);
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> state from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureValue" /> property.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignedInfo" /> property.</exception>
		// Token: 0x06000298 RID: 664 RVA: 0x0000C610 File Offset: 0x0000A810
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.signatureElement = value;
			this.m_signature.LoadXml(value);
			foreach (object obj in this.m_signature.SignedInfo.References)
			{
				Reference reference = (Reference)obj;
				foreach (object obj2 in reference.TransformChain)
				{
					Transform transform = (Transform)obj2;
					if (transform is XmlDecryptionTransform)
					{
						((XmlDecryptionTransform)transform).EncryptedXml = this.EncryptedXml;
					}
				}
			}
		}

		/// <summary>Sets the current <see cref="T:System.Xml.XmlResolver" /> object.</summary>
		/// <returns>The current <see cref="T:System.Xml.XmlResolver" /> object. The defaults is a <see cref="T:System.Xml.XmlSecureResolver" /> object.</returns>
		// Token: 0x170000B4 RID: 180
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000C720 File Offset: 0x0000A920
		[ComVisible(false)]
		public XmlResolver Resolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard canonicalization algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x0400010E RID: 270
		public const string XmlDsigCanonicalizationUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard canonicalization algorithm for XML digital signatures and includes comments. This field is constant.</summary>
		// Token: 0x0400010F RID: 271
		public const string XmlDsigCanonicalizationWithCommentsUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.DSA" /> algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x04000110 RID: 272
		public const string XmlDsigDSAUrl = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.HMACSHA1" /> algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x04000111 RID: 273
		public const string XmlDsigHMACSHA1Url = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard minimal canonicalization algorithm for XML digital signatures. This field is constant.</summary>
		// Token: 0x04000112 RID: 274
		public const string XmlDsigMinimalCanonicalizationUrl = "http://www.w3.org/2000/09/xmldsig#minimal";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard namespace for XML digital signatures. This field is constant.</summary>
		// Token: 0x04000113 RID: 275
		public const string XmlDsigNamespaceUrl = "http://www.w3.org/2000/09/xmldsig#";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.RSA" /> signature method for XML digital signatures. This field is constant.</summary>
		// Token: 0x04000114 RID: 276
		public const string XmlDsigRSASHA1Url = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.SHA1" /> digest method for XML digital signatures. This field is constant.</summary>
		// Token: 0x04000115 RID: 277
		public const string XmlDsigSHA1Url = "http://www.w3.org/2000/09/xmldsig#sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the XML mode decryption transformation. This field is constant.</summary>
		// Token: 0x04000116 RID: 278
		public const string XmlDecryptionTransformUrl = "http://www.w3.org/2002/07/decrypt#XML";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the base 64 transformation. This field is constant.</summary>
		// Token: 0x04000117 RID: 279
		public const string XmlDsigBase64TransformUrl = "http://www.w3.org/2000/09/xmldsig#base64";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the Canonical XML transformation. This field is constant.</summary>
		// Token: 0x04000118 RID: 280
		public const string XmlDsigC14NTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the Canonical XML transformation, with comments. This field is constant.</summary>
		// Token: 0x04000119 RID: 281
		public const string XmlDsigC14NWithCommentsTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for enveloped signature transformation. This field is constant.</summary>
		// Token: 0x0400011A RID: 282
		public const string XmlDsigEnvelopedSignatureTransformUrl = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		/// <summary>Represents the Uniform Resource Identifier (URI) for exclusive XML canonicalization. This field is constant.</summary>
		// Token: 0x0400011B RID: 283
		public const string XmlDsigExcC14NTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#";

		/// <summary>Represents the Uniform Resource Identifier (URI) for exclusive XML canonicalization, with comments. This field is constant.</summary>
		// Token: 0x0400011C RID: 284
		public const string XmlDsigExcC14NWithCommentsTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the XML Path Language (XPath). This field is constant.</summary>
		// Token: 0x0400011D RID: 285
		public const string XmlDsigXPathTransformUrl = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		/// <summary>Represents the Uniform Resource Identifier (URI) for XSLT transformations. This field is constant.</summary>
		// Token: 0x0400011E RID: 286
		public const string XmlDsigXsltTransformUrl = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the license transform algorithm used to normalize XrML licenses for signatures.</summary>
		// Token: 0x0400011F RID: 287
		public const string XmlLicenseTransformUrl = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";

		// Token: 0x04000120 RID: 288
		private EncryptedXml encryptedXml;

		/// <summary>Represents the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object. </summary>
		// Token: 0x04000121 RID: 289
		protected Signature m_signature;

		// Token: 0x04000122 RID: 290
		private AsymmetricAlgorithm key;

		/// <summary>Represents the name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object. </summary>
		// Token: 0x04000123 RID: 291
		protected string m_strSigningKeyName;

		// Token: 0x04000124 RID: 292
		private XmlDocument envdoc;

		// Token: 0x04000125 RID: 293
		private IEnumerator pkEnumerator;

		// Token: 0x04000126 RID: 294
		private XmlElement signatureElement;

		// Token: 0x04000127 RID: 295
		private Hashtable hashes;

		// Token: 0x04000128 RID: 296
		private XmlResolver xmlResolver = new XmlUrlResolver();

		// Token: 0x04000129 RID: 297
		private ArrayList manifests;

		// Token: 0x0400012A RID: 298
		private IEnumerator _x509Enumerator;

		// Token: 0x0400012B RID: 299
		private static readonly char[] whitespaceChars = new char[]
		{
			' ',
			'\r',
			'\n',
			'\t'
		};
	}
}
