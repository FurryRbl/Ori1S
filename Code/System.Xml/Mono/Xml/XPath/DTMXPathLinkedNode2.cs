using System;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x0200003F RID: 63
	internal struct DTMXPathLinkedNode2
	{
		// Token: 0x040001F6 RID: 502
		public int FirstChild;

		// Token: 0x040001F7 RID: 503
		public int Parent;

		// Token: 0x040001F8 RID: 504
		public int PreviousSibling;

		// Token: 0x040001F9 RID: 505
		public int NextSibling;

		// Token: 0x040001FA RID: 506
		public int FirstAttribute;

		// Token: 0x040001FB RID: 507
		public int FirstNamespace;

		// Token: 0x040001FC RID: 508
		public XPathNodeType NodeType;

		// Token: 0x040001FD RID: 509
		public int BaseURI;

		// Token: 0x040001FE RID: 510
		public bool IsEmptyElement;

		// Token: 0x040001FF RID: 511
		public int LocalName;

		// Token: 0x04000200 RID: 512
		public int NamespaceURI;

		// Token: 0x04000201 RID: 513
		public int Prefix;

		// Token: 0x04000202 RID: 514
		public int Value;

		// Token: 0x04000203 RID: 515
		public int XmlLang;

		// Token: 0x04000204 RID: 516
		public int LineNumber;

		// Token: 0x04000205 RID: 517
		public int LinePosition;
	}
}
