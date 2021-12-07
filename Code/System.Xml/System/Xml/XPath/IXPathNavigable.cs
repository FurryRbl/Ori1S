using System;

namespace System.Xml.XPath
{
	/// <summary>Provides an accessor to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</summary>
	// Token: 0x02000135 RID: 309
	public interface IXPathNavigable
	{
		/// <summary>Returns a new <see cref="T:System.Xml.XPath.XPathNavigator" /> object. </summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06000E65 RID: 3685
		XPathNavigator CreateNavigator();
	}
}
