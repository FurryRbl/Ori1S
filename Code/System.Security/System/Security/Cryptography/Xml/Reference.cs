using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;reference&gt; element of an XML signature.</summary>
	// Token: 0x0200004B RID: 75
	public class Reference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with default properties.</summary>
		// Token: 0x0600021A RID: 538 RVA: 0x0000A128 File Offset: 0x00008328
		public Reference()
		{
			this.chain = new TransformChain();
			this.digestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with a hash value of the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />. </param>
		// Token: 0x0600021B RID: 539 RVA: 0x0000A148 File Offset: 0x00008348
		[MonoTODO("There is no description about how it is used.")]
		public Reference(Stream stream) : this()
		{
			this.stream = stream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with the specified <see cref="T:System.Uri" />.</summary>
		/// <param name="uri">The <see cref="T:System.Uri" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />. </param>
		// Token: 0x0600021C RID: 540 RVA: 0x0000A158 File Offset: 0x00008358
		public Reference(string uri) : this()
		{
			this.uri = uri;
		}

		/// <summary>Gets or sets the digest method Uniform Resource Identifier (URI) of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest method URI of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default value is "http://www.w3.org/2000/09/xmldsig#sha1".</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000A168 File Offset: 0x00008368
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000A170 File Offset: 0x00008370
		public string DigestMethod
		{
			get
			{
				return this.digestMethod;
			}
			set
			{
				this.element = null;
				this.digestMethod = value;
			}
		}

		/// <summary>Gets or sets the digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000A180 File Offset: 0x00008380
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000A188 File Offset: 0x00008388
		public byte[] DigestValue
		{
			get
			{
				return this.digestValue;
			}
			set
			{
				this.element = null;
				this.digestValue = value;
			}
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default is null.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000A198 File Offset: 0x00008398
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.element = null;
				this.id = value;
			}
		}

		/// <summary>Gets the transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000A1B0 File Offset: 0x000083B0
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public TransformChain TransformChain
		{
			get
			{
				return this.chain;
			}
			[ComVisible(false)]
			set
			{
				this.chain = value;
			}
		}

		/// <summary>Gets or sets the type of the object being signed.</summary>
		/// <returns>The type of the object being signed.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A1C4 File Offset: 0x000083C4
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000A1CC File Offset: 0x000083CC
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.element = null;
				this.type = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A1DC File Offset: 0x000083DC
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000A1E4 File Offset: 0x000083E4
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.element = null;
				this.uri = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the list of transforms to be performed on the data before passing it to the digest algorithm.</summary>
		/// <param name="transform">The transform to be added to the list of transforms. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="transform" /> parameter is null.</exception>
		// Token: 0x06000229 RID: 553 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public void AddTransform(Transform transform)
		{
			this.chain.Add(transform);
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestMethod" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestValue" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600022A RID: 554 RVA: 0x0000A204 File Offset: 0x00008404
		public XmlElement GetXml()
		{
			if (this.element != null)
			{
				return this.element;
			}
			if (this.digestMethod == null)
			{
				throw new CryptographicException("DigestMethod");
			}
			if (this.digestValue == null)
			{
				throw new NullReferenceException("DigestValue");
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Reference", "http://www.w3.org/2000/09/xmldsig#");
			if (this.id != null)
			{
				xmlElement.SetAttribute("Id", this.id);
			}
			if (this.uri != null)
			{
				xmlElement.SetAttribute("URI", this.uri);
			}
			if (this.type != null)
			{
				xmlElement.SetAttribute("Type", this.type);
			}
			if (this.chain.Count > 0)
			{
				XmlElement xmlElement2 = xmlDocument.CreateElement("Transforms", "http://www.w3.org/2000/09/xmldsig#");
				foreach (object obj in this.chain)
				{
					Transform transform = (Transform)obj;
					XmlNode xml = transform.GetXml();
					XmlNode newChild = xmlDocument.ImportNode(xml, true);
					xmlElement2.AppendChild(newChild);
				}
				xmlElement.AppendChild(xmlElement2);
			}
			XmlElement xmlElement3 = xmlDocument.CreateElement("DigestMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.SetAttribute("Algorithm", this.digestMethod);
			xmlElement.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("DigestValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.InnerText = Convert.ToBase64String(this.digestValue);
			xmlElement.AppendChild(xmlElement4);
			return xmlElement;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A3C0 File Offset: 0x000085C0
		private string GetAttribute(XmlElement xel, string attribute)
		{
			XmlAttribute xmlAttribute = xel.Attributes[attribute];
			return (xmlAttribute == null) ? null : xmlAttribute.InnerText;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Reference" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Reference" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain any transforms.-or- The <paramref name="value" /> parameter contains an unknown transform. </exception>
		// Token: 0x0600022C RID: 556 RVA: 0x0000A3EC File Offset: 0x000085EC
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "Reference" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException();
			}
			this.id = this.GetAttribute(value, "Id");
			this.uri = this.GetAttribute(value, "URI");
			this.type = this.GetAttribute(value, "Type");
			XmlNodeList elementsByTagName = value.GetElementsByTagName("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (elementsByTagName != null && elementsByTagName.Count > 0)
			{
				foreach (object obj in elementsByTagName)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string attribute = this.GetAttribute((XmlElement)xmlNode, "Algorithm");
					Transform transform = (Transform)CryptoConfig.CreateFromName(attribute);
					if (transform == null)
					{
						throw new CryptographicException("Unknown transform {0}.", attribute);
					}
					if (xmlNode.ChildNodes.Count > 0)
					{
						transform.LoadInnerXml(xmlNode.ChildNodes);
					}
					this.AddTransform(transform);
				}
			}
			this.DigestMethod = XmlSignature.GetAttributeFromElement(value, "Algorithm", "DigestMethod");
			XmlElement childElement = XmlSignature.GetChildElement(value, "DigestValue", "http://www.w3.org/2000/09/xmldsig#");
			if (childElement != null)
			{
				this.DigestValue = Convert.FromBase64String(childElement.InnerText);
			}
			this.element = value;
		}

		// Token: 0x040000F7 RID: 247
		private TransformChain chain;

		// Token: 0x040000F8 RID: 248
		private string digestMethod;

		// Token: 0x040000F9 RID: 249
		private byte[] digestValue;

		// Token: 0x040000FA RID: 250
		private string id;

		// Token: 0x040000FB RID: 251
		private string uri;

		// Token: 0x040000FC RID: 252
		private string type;

		// Token: 0x040000FD RID: 253
		private Stream stream;

		// Token: 0x040000FE RID: 254
		private XmlElement element;
	}
}
