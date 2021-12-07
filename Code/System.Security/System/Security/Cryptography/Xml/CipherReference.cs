using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;CipherReference&gt; element in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000035 RID: 53
	public sealed class CipherReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> class.</summary>
		// Token: 0x06000132 RID: 306 RVA: 0x00006580 File Offset: 0x00004780
		public CipherReference()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) pointing to the encrypted data.</param>
		// Token: 0x06000133 RID: 307 RVA: 0x00006588 File Offset: 0x00004788
		public CipherReference(string uri) : base(uri)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> class using the specified Uniform Resource Identifier (URI) and transform chain information.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) pointing to the encrypted data.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted data.</param>
		// Token: 0x06000134 RID: 308 RVA: 0x00006594 File Offset: 0x00004794
		public CipherReference(string uri, TransformChain tc) : base(uri, tc)
		{
		}

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> that represents the &lt;CipherReference&gt; element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> value is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000135 RID: 309 RVA: 0x000065A0 File Offset: 0x000047A0
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument());
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000065B0 File Offset: 0x000047B0
		internal override XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("CipherReference", "http://www.w3.org/2001/04/xmlenc#");
			xmlElement.SetAttribute("URI", base.Uri);
			if (base.TransformChain != null && base.TransformChain.Count > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("Transforms", "http://www.w3.org/2001/04/xmlenc#");
				foreach (object obj in base.TransformChain)
				{
					Transform transform = (Transform)obj;
					xmlElement2.AppendChild(document.ImportNode(transform.GetXml(), true));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		/// <summary>Loads XML information into the &lt;CipherReference&gt; element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object that represents an XML element to use as the reference.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is null.</exception>
		// Token: 0x06000137 RID: 311 RVA: 0x00006688 File Offset: 0x00004888
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "CipherReference" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed CipherReference element.");
			}
			base.LoadXml(value);
		}
	}
}
