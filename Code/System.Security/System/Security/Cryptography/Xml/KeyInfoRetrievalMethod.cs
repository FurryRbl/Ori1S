using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>References <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> objects stored at a different location when using XMLDSIG or XML encryption.</summary>
	// Token: 0x02000047 RID: 71
	public class KeyInfoRetrievalMethod : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class.</summary>
		// Token: 0x060001F3 RID: 499 RVA: 0x0000958C File Offset: 0x0000778C
		public KeyInfoRetrievalMethod()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />. </param>
		// Token: 0x060001F4 RID: 500 RVA: 0x00009594 File Offset: 0x00007794
		public KeyInfoRetrievalMethod(string strUri)
		{
			this.URI = strUri;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object and the URI that describes the type of data to retrieve.  </summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />.</param>
		/// <param name="typeName">The URI that describes the type of data to retrieve.</param>
		// Token: 0x060001F5 RID: 501 RVA: 0x000095A4 File Offset: 0x000077A4
		public KeyInfoRetrievalMethod(string strUri, string strType) : this(strUri)
		{
			this.Type = strType;
		}

		/// <summary>Gets or sets a Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</summary>
		/// <returns>A Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000095B4 File Offset: 0x000077B4
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x000095BC File Offset: 0x000077BC
		[ComVisible(false)]
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

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000095CC File Offset: 0x000077CC
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x000095D4 File Offset: 0x000077D4
		public string Uri
		{
			get
			{
				return this.URI;
			}
			set
			{
				this.element = null;
				this.URI = value;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001FA RID: 506 RVA: 0x000095E4 File Offset: 0x000077E4
		public override XmlElement GetXml()
		{
			if (this.element != null)
			{
				return this.element;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("RetrievalMethod", "http://www.w3.org/2000/09/xmldsig#");
			if (this.URI != null && this.URI.Length > 0)
			{
				xmlElement.SetAttribute("URI", this.URI);
			}
			if (this.Type != null)
			{
				xmlElement.SetAttribute("Type", this.Type);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object to match.</summary>
		/// <param name="value">The XML element that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060001FB RID: 507 RVA: 0x00009664 File Offset: 0x00007864
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (value.LocalName != "RetrievalMethod" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				this.URI = string.Empty;
			}
			else
			{
				this.URI = value.Attributes["URI"].Value;
				if (value.HasAttribute("Type"))
				{
					this.Type = value.Attributes["Type"].Value;
				}
				this.element = value;
			}
		}

		// Token: 0x040000EC RID: 236
		private string URI;

		// Token: 0x040000ED RID: 237
		private XmlElement element;

		// Token: 0x040000EE RID: 238
		private string type;
	}
}
