using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the text content of an element or attribute.</summary>
	// Token: 0x0200011E RID: 286
	public class XmlText : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlText" /> class.</summary>
		/// <param name="strData">The content of the node; see the <see cref="P:System.Xml.XmlText.Value" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000C17 RID: 3095 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		protected internal XmlText(string strData, XmlDocument doc) : base(strData, doc)
		{
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0003CEE4 File Offset: 0x0003B0E4
		public override string LocalName
		{
			get
			{
				return "#text";
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0003CEEC File Offset: 0x0003B0EC
		public override string Name
		{
			get
			{
				return "#text";
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For text nodes, this value is XmlNodeType.Text.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0003CEF4 File Offset: 0x0003B0F4
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0003CEF8 File Offset: 0x0003B0F8
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The content of the text node.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0003CEFC File Offset: 0x0003B0FC
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0003CF04 File Offset: 0x0003B104
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0003CF10 File Offset: 0x0003B110
		public override XmlNode ParentNode
		{
			get
			{
				return base.ParentNode;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x06000C1F RID: 3103 RVA: 0x0003CF18 File Offset: 0x0003B118
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateTextNode(this.Data);
		}

		/// <summary>Splits the node into two nodes at the specified offset, keeping both in the tree as siblings.</summary>
		/// <returns>The new node.</returns>
		/// <param name="offset">The offset at which to split the node. </param>
		// Token: 0x06000C20 RID: 3104 RVA: 0x0003CF38 File Offset: 0x0003B138
		public virtual XmlText SplitText(int offset)
		{
			XmlText xmlText = this.OwnerDocument.CreateTextNode(this.Data.Substring(offset));
			this.DeleteData(offset, this.Data.Length - offset);
			this.ParentNode.InsertAfter(xmlText, this);
			return xmlText;
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. XmlText nodes do not have children, so this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C21 RID: 3105 RVA: 0x0003CF80 File Offset: 0x0003B180
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C22 RID: 3106 RVA: 0x0003CF84 File Offset: 0x0003B184
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}
	}
}
