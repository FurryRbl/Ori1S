using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), with comments.</summary>
	// Token: 0x02000058 RID: 88
	public class XmlDsigC14NWithCommentsTransform : XmlDsigC14NTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform" /> class.</summary>
		// Token: 0x060002E1 RID: 737 RVA: 0x0000D76C File Offset: 0x0000B96C
		public XmlDsigC14NWithCommentsTransform() : base(true)
		{
		}
	}
}
