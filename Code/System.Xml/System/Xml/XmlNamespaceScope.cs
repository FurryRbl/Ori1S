using System;

namespace System.Xml
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x020000FF RID: 255
	public enum XmlNamespaceScope
	{
		/// <summary>All namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04000512 RID: 1298
		All,
		/// <summary>All namespaces defined in the scope of the current node, excluding the xmlns:xml namespace, which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04000513 RID: 1299
		ExcludeXml,
		/// <summary>All namespaces that are defined locally at the current node.</summary>
		// Token: 0x04000514 RID: 1300
		Local
	}
}
