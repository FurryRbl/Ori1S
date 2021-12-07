using System;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000076 RID: 118
	internal struct Attribute
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00019B7C File Offset: 0x00017D7C
		public Attribute(string prefix, string namespaceUri, string localName, string value)
		{
			this.Prefix = prefix;
			this.Namespace = namespaceUri;
			this.LocalName = localName;
			this.Value = value;
		}

		// Token: 0x040002A6 RID: 678
		public string Prefix;

		// Token: 0x040002A7 RID: 679
		public string Namespace;

		// Token: 0x040002A8 RID: 680
		public string LocalName;

		// Token: 0x040002A9 RID: 681
		public string Value;
	}
}
