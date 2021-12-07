using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the XPath node types that can be returned from the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</summary>
	// Token: 0x0200013D RID: 317
	public enum XPathNodeType
	{
		/// <summary>The root node of the XML document or node tree.</summary>
		// Token: 0x04000698 RID: 1688
		Root,
		/// <summary>An element, such as &lt;element&gt;.</summary>
		// Token: 0x04000699 RID: 1689
		Element,
		/// <summary>An attribute, such as id='123'.</summary>
		// Token: 0x0400069A RID: 1690
		Attribute,
		/// <summary>A namespace, such as xmlns="namespace".</summary>
		// Token: 0x0400069B RID: 1691
		Namespace,
		/// <summary>The text content of a node. Equivalent to the Document Object Model (DOM) Text and CDATA node types. Contains at least one character.</summary>
		// Token: 0x0400069C RID: 1692
		Text,
		/// <summary>A node with white space characters and xml:space set to preserve.</summary>
		// Token: 0x0400069D RID: 1693
		SignificantWhitespace,
		/// <summary>A node with only white space characters and no significant white space. White space characters are #x20, #x9, #xD, or #xA.</summary>
		// Token: 0x0400069E RID: 1694
		Whitespace,
		/// <summary>A processing instruction, such as &lt;?pi test?&gt;. This does not include XML declarations, which are not visible to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class. </summary>
		// Token: 0x0400069F RID: 1695
		ProcessingInstruction,
		/// <summary>A comment, such as &lt;!-- my comment --&gt;</summary>
		// Token: 0x040006A0 RID: 1696
		Comment,
		/// <summary>Any of the <see cref="T:System.Xml.XPath.XPathNodeType" /> node types.</summary>
		// Token: 0x040006A1 RID: 1697
		All
	}
}
