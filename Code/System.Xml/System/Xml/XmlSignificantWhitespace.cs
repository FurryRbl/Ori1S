using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space between markup in a mixed content node or white space within an xml:space= 'preserve' scope. This is also referred to as significant white space.</summary>
	// Token: 0x0200011C RID: 284
	public class XmlSignificantWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlSignificantWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000C0C RID: 3084 RVA: 0x0003CE50 File Offset: 0x0003B050
		protected internal XmlSignificantWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this property returns #significant-whitespace.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0003CE5C File Offset: 0x0003B05C
		public override string LocalName
		{
			get
			{
				return "#significant-whitespace";
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this property returns #significant-whitespace.</returns>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0003CE64 File Offset: 0x0003B064
		public override string Name
		{
			get
			{
				return "#significant-whitespace";
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this value is XmlNodeType.SignificantWhitespace.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0003CE6C File Offset: 0x0003B06C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.SignificantWhitespace;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0003CE70 File Offset: 0x0003B070
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.SignificantWhitespace;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting Value to invalid white space characters. </exception>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0003CE74 File Offset: 0x0003B074
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x0003CE7C File Offset: 0x0003B07C
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
				base.Data = value;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0003CE9C File Offset: 0x0003B09C
		public override XmlNode ParentNode
		{
			get
			{
				return base.ParentNode;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For significant white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x06000C14 RID: 3092 RVA: 0x0003CEA4 File Offset: 0x0003B0A4
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlSignificantWhitespace(this.Data, this.OwnerDocument);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C15 RID: 3093 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C16 RID: 3094 RVA: 0x0003CEC8 File Offset: 0x0003B0C8
		public override void WriteTo(XmlWriter w)
		{
			w.WriteWhitespace(this.Data);
		}
	}
}
