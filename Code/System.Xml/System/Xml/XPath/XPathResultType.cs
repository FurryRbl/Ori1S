using System;

namespace System.Xml.XPath
{
	/// <summary>Specifies the return type of the XPath expression.</summary>
	// Token: 0x0200013C RID: 316
	public enum XPathResultType
	{
		/// <summary>A numeric value.</summary>
		// Token: 0x04000690 RID: 1680
		Number,
		/// <summary>A <see cref="T:System.String" /> value.</summary>
		// Token: 0x04000691 RID: 1681
		String,
		/// <summary>A <see cref="T:System.Boolean" />true or false value.</summary>
		// Token: 0x04000692 RID: 1682
		Boolean,
		/// <summary>A node collection.</summary>
		// Token: 0x04000693 RID: 1683
		NodeSet,
		/// <summary>A tree fragment.</summary>
		// Token: 0x04000694 RID: 1684
		[MonoFIX("MS.NET: 1")]
		Navigator,
		/// <summary>Any of the XPath node types.</summary>
		// Token: 0x04000695 RID: 1685
		Any,
		/// <summary>The expression does not evaluate to the correct XPath type.</summary>
		// Token: 0x04000696 RID: 1686
		Error
	}
}
