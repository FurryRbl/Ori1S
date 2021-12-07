using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x0200013A RID: 314
	public enum XPathNamespaceScope
	{
		/// <summary>Returns all namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x0400068B RID: 1675
		All,
		/// <summary>Returns all namespaces defined in the scope of the current node, excluding the xmlns:xml namespace. The xmlns:xml namespace is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x0400068C RID: 1676
		ExcludeXml,
		/// <summary>Returns all namespaces that are defined locally at the current node. </summary>
		// Token: 0x0400068D RID: 1677
		Local
	}
}
