using System;
using System.Text;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a lightweight object that is useful for tree insert operations.</summary>
	// Token: 0x020000F1 RID: 241
	public class XmlDocumentFragment : XmlNode, IHasXmlChildNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentFragment" /> class.</summary>
		/// <param name="ownerDocument">The XML document that is the source of the fragment.</param>
		// Token: 0x06000922 RID: 2338 RVA: 0x000318A4 File Offset: 0x0002FAA4
		protected internal XmlDocumentFragment(XmlDocument doc) : base(doc)
		{
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000318B0 File Offset: 0x0002FAB0
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x000318B8 File Offset: 0x0002FAB8
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

		/// <summary>Gets or sets the markup representing the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00031994 File Offset: 0x0002FB94
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x000318C4 File Offset: 0x0002FAC4
		public override string InnerXml
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.ChildNodes.Count; i++)
				{
					stringBuilder.Append(this.ChildNodes[i].OuterXml);
				}
				return stringBuilder.ToString();
			}
			set
			{
				for (int i = 0; i < this.ChildNodes.Count; i++)
				{
					this.RemoveChild(this.ChildNodes[i]);
				}
				XmlNamespaceManager nsMgr = base.ConstructNamespaceManager();
				XmlParserContext context = new XmlParserContext(this.OwnerDocument.NameTable, nsMgr, (this.OwnerDocument.DocumentType == null) ? null : this.OwnerDocument.DocumentType.DTD, this.BaseURI, this.XmlLang, this.XmlSpace, null);
				XmlTextReader xmlTextReader = new XmlTextReader(value, XmlNodeType.Element, context);
				xmlTextReader.XmlResolver = this.OwnerDocument.Resolver;
				for (;;)
				{
					XmlNode xmlNode = this.OwnerDocument.ReadNode(xmlTextReader);
					if (xmlNode == null)
					{
						break;
					}
					this.AppendChild(xmlNode);
				}
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlDocumentFragment nodes, the local name is #document-fragment.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x000319E4 File Offset: 0x0002FBE4
		public override string LocalName
		{
			get
			{
				return "#document-fragment";
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlDocumentFragment, the name is #document-fragment.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000319EC File Offset: 0x0002FBEC
		public override string Name
		{
			get
			{
				return "#document-fragment";
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlDocumentFragment nodes, this value is XmlNodeType.DocumentFragment.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000319F4 File Offset: 0x0002FBF4
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentFragment;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The XmlDocument to which this node belongs.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000319F8 File Offset: 0x0002FBF8
		public override XmlDocument OwnerDocument
		{
			get
			{
				return base.OwnerDocument;
			}
		}

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The parent of this node.For XmlDocumentFragment nodes, this property is always null.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00031A00 File Offset: 0x0002FC00
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00031A04 File Offset: 0x0002FC04
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x0600092D RID: 2349 RVA: 0x00031A08 File Offset: 0x0002FC08
		public override XmlNode CloneNode(bool deep)
		{
			if (deep)
			{
				XmlNode xmlNode = this.FirstChild;
				while (xmlNode != null && xmlNode.HasChildNodes)
				{
					this.AppendChild(xmlNode.NextSibling.CloneNode(false));
					xmlNode = xmlNode.NextSibling;
				}
				return xmlNode;
			}
			return new XmlDocumentFragment(this.OwnerDocument);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x0600092E RID: 2350 RVA: 0x00031A60 File Offset: 0x0002FC60
		public override void WriteContentTo(XmlWriter w)
		{
			for (int i = 0; i < this.ChildNodes.Count; i++)
			{
				this.ChildNodes[i].WriteContentTo(w);
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x0600092F RID: 2351 RVA: 0x00031A9C File Offset: 0x0002FC9C
		public override void WriteTo(XmlWriter w)
		{
			for (int i = 0; i < this.ChildNodes.Count; i++)
			{
				this.ChildNodes[i].WriteTo(w);
			}
		}

		// Token: 0x040004DA RID: 1242
		private XmlLinkedNode lastLinkedChild;
	}
}
