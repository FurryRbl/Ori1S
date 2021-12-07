using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space in element content.</summary>
	// Token: 0x02000129 RID: 297
	public class XmlWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000D6F RID: 3439 RVA: 0x00043294 File Offset: 0x00041494
		protected internal XmlWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlWhitespace nodes, this property returns #whitespace.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000432A0 File Offset: 0x000414A0
		public override string LocalName
		{
			get
			{
				return "#whitespace";
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlWhitespace nodes, this property returns #whitespace.</returns>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x000432A8 File Offset: 0x000414A8
		public override string Name
		{
			get
			{
				return "#whitespace";
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>For XmlWhitespace nodes, the value is <see cref="F:System.Xml.XmlNodeType.Whitespace" />.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000432B0 File Offset: 0x000414B0
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Whitespace;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000432B4 File Offset: 0x000414B4
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.Whitespace;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting <see cref="P:System.Xml.XmlWhitespace.Value" /> to invalid white space characters. </exception>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000432B8 File Offset: 0x000414B8
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x000432C0 File Offset: 0x000414C0
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				if (!XmlChar.IsWhitespace(value))
				{
					throw new ArgumentException("Invalid whitespace characters.");
				}
				this.Data = value;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000432E0 File Offset: 0x000414E0
		public override XmlNode ParentNode
		{
			get
			{
				return base.ParentNode;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x06000D77 RID: 3447 RVA: 0x000432E8 File Offset: 0x000414E8
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlWhitespace(this.Data, this.OwnerDocument);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save. </param>
		// Token: 0x06000D78 RID: 3448 RVA: 0x000432FC File Offset: 0x000414FC
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save.</param>
		// Token: 0x06000D79 RID: 3449 RVA: 0x00043300 File Offset: 0x00041500
		public override void WriteTo(XmlWriter w)
		{
			w.WriteWhitespace(this.Data);
		}
	}
}
