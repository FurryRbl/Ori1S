using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an entity reference node.</summary>
	// Token: 0x020000F5 RID: 245
	public class XmlEntityReference : XmlLinkedNode, IHasXmlChildNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlEntityReference" /> class.</summary>
		/// <param name="name">The name of the entity reference; see the <see cref="P:System.Xml.XmlEntityReference.Name" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x060009A2 RID: 2466 RVA: 0x00033934 File Offset: 0x00031B34
		protected internal XmlEntityReference(string name, XmlDocument doc) : base(doc)
		{
			XmlConvert.VerifyName(name);
			this.entityName = doc.NameTable.Add(name);
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00033964 File Offset: 0x00031B64
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0003396C File Offset: 0x00031B6C
		XmlLinkedNode IHasXmlChildNode.LastLinkedChild
		{
			get
			{
				return this.lastLinkedChild;
			}
			set
			{
				this.lastLinkedChild = value;
			}
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00033978 File Offset: 0x00031B78
		public override string BaseURI
		{
			get
			{
				return base.BaseURI;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00033980 File Offset: 0x00031B80
		private XmlEntity Entity
		{
			get
			{
				XmlDocumentType documentType = this.OwnerDocument.DocumentType;
				if (documentType == null)
				{
					return null;
				}
				if (documentType.Entities == null)
				{
					return null;
				}
				return documentType.Entities.GetNamedItem(this.Name) as XmlEntity;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000339C4 File Offset: 0x00031BC4
		internal override string ChildrenBaseURI
		{
			get
			{
				XmlEntity entity = this.Entity;
				if (entity == null)
				{
					return string.Empty;
				}
				if (entity.SystemId == null || entity.SystemId.Length == 0)
				{
					return entity.BaseURI;
				}
				if (entity.BaseURI == null || entity.BaseURI.Length == 0)
				{
					return entity.SystemId;
				}
				Uri baseUri = null;
				try
				{
					baseUri = new Uri(entity.BaseURI);
				}
				catch (UriFormatException)
				{
				}
				XmlResolver resolver = this.OwnerDocument.Resolver;
				if (resolver != null)
				{
					return resolver.ResolveUri(baseUri, entity.SystemId).ToString();
				}
				return new Uri(baseUri, entity.SystemId).ToString();
			}
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlEntityReference nodes are read-only, this property always returns true.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00033A94 File Offset: 0x00031C94
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlEntityReference nodes, this property returns the name of the entity referenced.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00033A98 File Offset: 0x00031C98
		public override string LocalName
		{
			get
			{
				return this.entityName;
			}
		}

		/// <summary>Gets the name of the node.</summary>
		/// <returns>The name of the entity referenced.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00033AA0 File Offset: 0x00031CA0
		public override string Name
		{
			get
			{
				return this.entityName;
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>The node type. For XmlEntityReference nodes, the value is XmlNodeType.EntityReference.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00033AA8 File Offset: 0x00031CA8
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.EntityReference;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value of the node. For XmlEntityReference nodes, this property returns null.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting the property. </exception>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00033AAC File Offset: 0x00031CAC
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x00033AB0 File Offset: 0x00031CB0
		public override string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new XmlException("entity reference cannot be set value.");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00033ABC File Offset: 0x00031CBC
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For XmlEntityReference nodes, this method always returns an entity reference node with no children. The replacement text is set when the node is inserted into a parent. </param>
		// Token: 0x060009AF RID: 2479 RVA: 0x00033AC0 File Offset: 0x00031CC0
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlEntityReference(this.Name, this.OwnerDocument);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060009B0 RID: 2480 RVA: 0x00033AD4 File Offset: 0x00031CD4
		public override void WriteContentTo(XmlWriter w)
		{
			for (int i = 0; i < this.ChildNodes.Count; i++)
			{
				this.ChildNodes[i].WriteTo(w);
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060009B1 RID: 2481 RVA: 0x00033B10 File Offset: 0x00031D10
		public override void WriteTo(XmlWriter w)
		{
			w.WriteRaw("&");
			w.WriteName(this.Name);
			w.WriteRaw(";");
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00033B40 File Offset: 0x00031D40
		internal void SetReferencedEntityContent()
		{
			if (this.FirstChild != null)
			{
				return;
			}
			if (this.OwnerDocument.DocumentType == null)
			{
				return;
			}
			XmlEntity entity = this.Entity;
			if (entity == null)
			{
				base.InsertBefore(this.OwnerDocument.CreateTextNode(string.Empty), null, false, true);
			}
			else
			{
				for (int i = 0; i < entity.ChildNodes.Count; i++)
				{
					base.InsertBefore(entity.ChildNodes[i].CloneNode(true), null, false, true);
				}
			}
		}

		// Token: 0x040004E8 RID: 1256
		private string entityName;

		// Token: 0x040004E9 RID: 1257
		private XmlLinkedNode lastLinkedChild;
	}
}
