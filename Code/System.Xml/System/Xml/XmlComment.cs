using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the content of an XML comment.</summary>
	// Token: 0x020000ED RID: 237
	public class XmlComment : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlComment" /> class.</summary>
		/// <param name="comment">The content of the comment element.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x0600088E RID: 2190 RVA: 0x0002F758 File Offset: 0x0002D958
		protected internal XmlComment(string comment, XmlDocument doc) : base(comment, doc)
		{
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For comment nodes, the value is #comment.</returns>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0002F764 File Offset: 0x0002D964
		public override string LocalName
		{
			get
			{
				return "#comment";
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For comment nodes, the value is #comment.</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0002F76C File Offset: 0x0002D96C
		public override string Name
		{
			get
			{
				return "#comment";
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For comment nodes, the value is XmlNodeType.Comment.</returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0002F774 File Offset: 0x0002D974
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0002F778 File Offset: 0x0002D978
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.Comment;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because comment nodes do not have children, the cloned node always includes the text content, regardless of the parameter setting. </param>
		// Token: 0x06000893 RID: 2195 RVA: 0x0002F77C File Offset: 0x0002D97C
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlComment(this.Value, this.OwnerDocument);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because comment nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000894 RID: 2196 RVA: 0x0002F79C File Offset: 0x0002D99C
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000895 RID: 2197 RVA: 0x0002F7A0 File Offset: 0x0002D9A0
		public override void WriteTo(XmlWriter w)
		{
			w.WriteComment(this.Data);
		}
	}
}
