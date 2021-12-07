using System;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.Xsl;

namespace Mono.Xml.XPath
{
	// Token: 0x02000043 RID: 67
	internal class KeyPattern : LocationPathPattern
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0001369C File Offset: 0x0001189C
		public KeyPattern(XsltKey key) : base(null)
		{
			this.key = key;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000136AC File Offset: 0x000118AC
		public override bool Matches(XPathNavigator node, XsltContext ctx)
		{
			return this.key.PatternMatches(node, ctx);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000136BC File Offset: 0x000118BC
		public override double DefaultPriority
		{
			get
			{
				return 0.5;
			}
		}

		// Token: 0x04000213 RID: 531
		private XsltKey key;
	}
}
