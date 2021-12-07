using System;
using Mono.Xml;

namespace System.Xml
{
	/// <summary>Represents an entity declaration, such as &lt;!ENTITY... &gt;.</summary>
	// Token: 0x020000E6 RID: 230
	public class XmlEntity : XmlNode, IHasXmlChildNode
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0002D198 File Offset: 0x0002B398
		internal XmlEntity(string name, string NDATA, string publicId, string systemId, XmlDocument doc) : base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.NDATA = NDATA;
			this.publicId = publicId;
			this.systemId = systemId;
			this.baseUri = doc.BaseURI;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0002D224 File Offset: 0x0002B424
		XmlLinkedNode IHasXmlChildNode.LastLinkedChild
		{
			get
			{
				if (this.lastLinkedChild != null)
				{
					return this.lastLinkedChild;
				}
				if (!this.contentAlreadySet)
				{
					this.contentAlreadySet = true;
					this.SetEntityContent();
				}
				return this.lastLinkedChild;
			}
			set
			{
				this.lastLinkedChild = value;
			}
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0002D230 File Offset: 0x0002B430
		public override string BaseURI
		{
			get
			{
				return this.baseUri;
			}
		}

		/// <summary>Gets the concatenated values of the entity node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0002D238 File Offset: 0x0002B438
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0002D240 File Offset: 0x0002B440
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				throw new InvalidOperationException("This operation is not supported.");
			}
		}

		/// <summary>Gets the markup representing the children of this node.</summary>
		/// <returns>For XmlEntity nodes, String.Empty is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0002D24C File Offset: 0x0002B44C
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0002D254 File Offset: 0x0002B454
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				throw new InvalidOperationException("This operation is not supported.");
			}
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlEntity nodes are read-only, this property always returns true.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002D260 File Offset: 0x0002B460
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the name of the node without the namespace prefix.</summary>
		/// <returns>For XmlEntity nodes, this property returns the name of the entity.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002D264 File Offset: 0x0002B464
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the name of the node.</summary>
		/// <returns>The name of the entity.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002D26C File Offset: 0x0002B46C
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>The node type. For XmlEntity nodes, the value is XmlNodeType.Entity.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0002D274 File Offset: 0x0002B474
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Entity;
			}
		}

		/// <summary>Gets the name of the optional NDATA attribute on the entity declaration.</summary>
		/// <returns>The name of the NDATA attribute. If there is no NDATA, null is returned.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0002D278 File Offset: 0x0002B478
		public string NotationName
		{
			get
			{
				if (this.NDATA == null)
				{
					return null;
				}
				return this.NDATA;
			}
		}

		/// <summary>Gets the markup representing this node and all its children.</summary>
		/// <returns>For XmlEntity nodes, String.Empty is returned.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0002D290 File Offset: 0x0002B490
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the value of the public identifier on the entity declaration.</summary>
		/// <returns>The public identifier on the entity. If there is no public identifier, null is returned.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0002D298 File Offset: 0x0002B498
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the entity declaration.</summary>
		/// <returns>The system identifier on the entity. If there is no system identifier, null is returned.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0002D2A0 File Offset: 0x0002B4A0
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Creates a duplicate of this node. Entity nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlEntity" /> object throws an exception.</summary>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
		/// <exception cref="T:System.InvalidOperationException">Entity nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlEntity" /> object throws an exception.</exception>
		// Token: 0x06000808 RID: 2056 RVA: 0x0002D2A8 File Offset: 0x0002B4A8
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException("This operation is not supported.");
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlEntity nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000809 RID: 2057 RVA: 0x0002D2B4 File Offset: 0x0002B4B4
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlEntity nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x0600080A RID: 2058 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
		public override void WriteTo(XmlWriter w)
		{
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002D2BC File Offset: 0x0002B4BC
		private void SetEntityContent()
		{
			if (this.lastLinkedChild != null)
			{
				return;
			}
			XmlDocumentType documentType = this.OwnerDocument.DocumentType;
			if (documentType == null)
			{
				return;
			}
			DTDEntityDeclaration dtdentityDeclaration = documentType.DTD.EntityDecls[this.name];
			if (dtdentityDeclaration == null)
			{
				return;
			}
			XmlNamespaceManager nsMgr = base.ConstructNamespaceManager();
			XmlParserContext context = new XmlParserContext(this.OwnerDocument.NameTable, nsMgr, (documentType == null) ? null : documentType.DTD, this.BaseURI, this.XmlLang, this.XmlSpace, null);
			XmlTextReader xmlTextReader = new XmlTextReader(dtdentityDeclaration.EntityValue, XmlNodeType.Element, context);
			xmlTextReader.XmlResolver = this.OwnerDocument.Resolver;
			for (;;)
			{
				XmlNode xmlNode = this.OwnerDocument.ReadNode(xmlTextReader);
				if (xmlNode == null)
				{
					break;
				}
				base.InsertBefore(xmlNode, null, false, false);
			}
		}

		// Token: 0x0400049E RID: 1182
		private string name;

		// Token: 0x0400049F RID: 1183
		private string NDATA;

		// Token: 0x040004A0 RID: 1184
		private string publicId;

		// Token: 0x040004A1 RID: 1185
		private string systemId;

		// Token: 0x040004A2 RID: 1186
		private string baseUri;

		// Token: 0x040004A3 RID: 1187
		private XmlLinkedNode lastLinkedChild;

		// Token: 0x040004A4 RID: 1188
		private bool contentAlreadySet;
	}
}
