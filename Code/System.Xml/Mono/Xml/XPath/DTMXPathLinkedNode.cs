using System;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x02000038 RID: 56
	internal struct DTMXPathLinkedNode
	{
		// Token: 0x04000197 RID: 407
		public int FirstChild;

		// Token: 0x04000198 RID: 408
		public int Parent;

		// Token: 0x04000199 RID: 409
		public int PreviousSibling;

		// Token: 0x0400019A RID: 410
		public int NextSibling;

		// Token: 0x0400019B RID: 411
		public int FirstAttribute;

		// Token: 0x0400019C RID: 412
		public int FirstNamespace;

		// Token: 0x0400019D RID: 413
		public XPathNodeType NodeType;

		// Token: 0x0400019E RID: 414
		public string BaseURI;

		// Token: 0x0400019F RID: 415
		public bool IsEmptyElement;

		// Token: 0x040001A0 RID: 416
		public string LocalName;

		// Token: 0x040001A1 RID: 417
		public string NamespaceURI;

		// Token: 0x040001A2 RID: 418
		public string Prefix;

		// Token: 0x040001A3 RID: 419
		public string Value;

		// Token: 0x040001A4 RID: 420
		public string XmlLang;

		// Token: 0x040001A5 RID: 421
		public int LineNumber;

		// Token: 0x040001A6 RID: 422
		public int LinePosition;
	}
}
