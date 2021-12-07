using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the exclusive C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), with comments.</summary>
	// Token: 0x0200005B RID: 91
	public class XmlDsigExcC14NWithCommentsTransform : XmlDsigExcC14NTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform" /> class. </summary>
		// Token: 0x060002FB RID: 763 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public XmlDsigExcC14NWithCommentsTransform() : base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform" /> class specifying a list of namespace prefixes to canonicalize using the standard canonicalization algorithm.</summary>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		// Token: 0x060002FC RID: 764 RVA: 0x0000DE00 File Offset: 0x0000C000
		public XmlDsigExcC14NWithCommentsTransform(string inclusiveNamespacesPrefixList) : base(true, inclusiveNamespacesPrefixList)
		{
		}
	}
}
