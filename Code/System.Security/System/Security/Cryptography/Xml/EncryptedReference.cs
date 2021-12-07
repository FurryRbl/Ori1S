using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class used in XML encryption from which the <see cref="T:System.Security.Cryptography.Xml.CipherReference" />, <see cref="T:System.Security.Cryptography.Xml.KeyReference" />, and <see cref="T:System.Security.Cryptography.Xml.DataReference" /> classes derive.</summary>
	// Token: 0x0200003B RID: 59
	public abstract class EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class.</summary>
		// Token: 0x0600015F RID: 351 RVA: 0x000078C4 File Offset: 0x00005AC4
		protected EncryptedReference()
		{
			this.TransformChain = new TransformChain();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is null.</exception>
		// Token: 0x06000160 RID: 352 RVA: 0x000078D8 File Offset: 0x00005AD8
		protected EncryptedReference(string uri)
		{
			this.Uri = uri;
			this.TransformChain = new TransformChain();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI) and transform chain.</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to be done on the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is null.</exception>
		// Token: 0x06000161 RID: 353 RVA: 0x000078F4 File Offset: 0x00005AF4
		protected EncryptedReference(string uri, TransformChain tc) : this()
		{
			this.Uri = uri;
			this.TransformChain = tc;
		}

		/// <summary>Gets a value that indicates whether the cache is valid.</summary>
		/// <returns>true if the cache is valid; otherwise, false.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000790C File Offset: 0x00005B0C
		[MonoTODO]
		protected internal bool CacheValid
		{
			get
			{
				return this.cacheValid;
			}
		}

		/// <summary>Gets or sets a reference type.</summary>
		/// <returns>The reference type of the encrypted data.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007914 File Offset: 0x00005B14
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000791C File Offset: 0x00005B1C
		protected string ReferenceType
		{
			get
			{
				return this.referenceType;
			}
			set
			{
				this.referenceType = value;
			}
		}

		/// <summary>Gets or sets the transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms used on the encrypted data.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007928 File Offset: 0x00005B28
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00007930 File Offset: 0x00005B30
		public TransformChain TransformChain
		{
			get
			{
				return this.tc;
			}
			set
			{
				this.tc = value;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.Uri" /> property was set to null.</exception>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000793C File Offset: 0x00005B3C
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007944 File Offset: 0x00005B44
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the current transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="transform">A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to add to the transform chain.</param>
		// Token: 0x06000169 RID: 361 RVA: 0x00007950 File Offset: 0x00005B50
		public void AddTransform(Transform transform)
		{
			this.TransformChain.Add(transform);
		}

		/// <summary>Returns the XML representation of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the values of the &lt;EncryptedReference&gt; element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.ReferenceType" /> property is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600016A RID: 362 RVA: 0x00007960 File Offset: 0x00005B60
		public virtual XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007970 File Offset: 0x00005B70
		internal virtual XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement(this.ReferenceType, "http://www.w3.org/2001/04/xmlenc#");
			xmlElement.SetAttribute("URI", this.Uri);
			if (this.TransformChain != null && this.TransformChain.Count > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("Transforms", "http://www.w3.org/2001/04/xmlenc#");
				foreach (object obj in this.TransformChain)
				{
					Transform transform = (Transform)obj;
					xmlElement2.AppendChild(document.ImportNode(transform.GetXml(), true));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		/// <summary>Loads an XML element into an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object that represents an XML element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x0600016C RID: 364 RVA: 0x00007A48 File Offset: 0x00005C48
		[MonoTODO("Make compliant.")]
		public virtual void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Uri = null;
			this.TransformChain = new TransformChain();
			foreach (object obj in value.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode is XmlWhitespace))
				{
					string localName = xmlNode.LocalName;
					if (localName != null)
					{
						if (EncryptedReference.<>f__switch$map3 == null)
						{
							EncryptedReference.<>f__switch$map3 = new Dictionary<string, int>(1)
							{
								{
									"Transforms",
									0
								}
							};
						}
						int num;
						if (EncryptedReference.<>f__switch$map3.TryGetValue(localName, out num))
						{
							if (num == 0)
							{
								foreach (object obj2 in ((XmlElement)xmlNode).GetElementsByTagName("Transform", "http://www.w3.org/2000/09/xmldsig#"))
								{
									XmlNode xmlNode2 = (XmlNode)obj2;
									string value2 = ((XmlElement)xmlNode2).Attributes["Algorithm"].Value;
									if (value2 != null)
									{
										if (EncryptedReference.<>f__switch$map2 == null)
										{
											EncryptedReference.<>f__switch$map2 = new Dictionary<string, int>(9)
											{
												{
													"http://www.w3.org/2000/09/xmldsig#base64",
													0
												},
												{
													"http://www.w3.org/TR/2001/REC-xml-c14n-20010315",
													1
												},
												{
													"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments",
													2
												},
												{
													"http://www.w3.org/2000/09/xmldsig#enveloped-signature",
													3
												},
												{
													"http://www.w3.org/TR/1999/REC-xpath-19991116",
													4
												},
												{
													"http://www.w3.org/TR/1999/REC-xslt-19991116",
													5
												},
												{
													"http://www.w3.org/2001/10/xml-exc-c14n#",
													6
												},
												{
													"http://www.w3.org/2001/10/xml-exc-c14n#WithComments",
													7
												},
												{
													"http://www.w3.org/2002/07/decrypt#XML",
													8
												}
											};
										}
										int num2;
										if (EncryptedReference.<>f__switch$map2.TryGetValue(value2, out num2))
										{
											Transform transform;
											switch (num2)
											{
											case 0:
												transform = new XmlDsigBase64Transform();
												break;
											case 1:
												transform = new XmlDsigC14NTransform();
												break;
											case 2:
												transform = new XmlDsigC14NWithCommentsTransform();
												break;
											case 3:
												transform = new XmlDsigEnvelopedSignatureTransform();
												break;
											case 4:
												transform = new XmlDsigXPathTransform();
												break;
											case 5:
												transform = new XmlDsigXsltTransform();
												break;
											case 6:
												transform = new XmlDsigExcC14NTransform();
												break;
											case 7:
												transform = new XmlDsigExcC14NWithCommentsTransform();
												break;
											case 8:
												transform = new XmlDecryptionTransform();
												break;
											default:
												continue;
											}
											transform.LoadInnerXml(((XmlElement)xmlNode2).ChildNodes);
											this.TransformChain.Add(transform);
										}
									}
								}
							}
						}
					}
				}
			}
			if (value.HasAttribute("URI"))
			{
				this.Uri = value.Attributes["URI"].Value;
			}
		}

		// Token: 0x040000B5 RID: 181
		private bool cacheValid;

		// Token: 0x040000B6 RID: 182
		private string referenceType;

		// Token: 0x040000B7 RID: 183
		private string uri;

		// Token: 0x040000B8 RID: 184
		private TransformChain tc;
	}
}
