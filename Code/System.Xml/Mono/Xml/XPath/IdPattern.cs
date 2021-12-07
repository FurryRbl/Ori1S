using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.Xsl;

namespace Mono.Xml.XPath
{
	// Token: 0x02000042 RID: 66
	internal class IdPattern : LocationPathPattern
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00013618 File Offset: 0x00011818
		public IdPattern(string arg0) : base(null)
		{
			this.ids = arg0.Split(XmlChar.WhitespaceChars);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00013634 File Offset: 0x00011834
		public override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return XPathNodeType.Element;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00013638 File Offset: 0x00011838
		public override bool Matches(XPathNavigator node, XsltContext ctx)
		{
			XPathNavigator navCache = ((XsltCompiledContext)ctx).GetNavCache(this, node);
			for (int i = 0; i < this.ids.Length; i++)
			{
				if (navCache.MoveToId(this.ids[i]) && navCache.IsSamePosition(node))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00013690 File Offset: 0x00011890
		public override double DefaultPriority
		{
			get
			{
				return 0.5;
			}
		}

		// Token: 0x04000212 RID: 530
		private string[] ids;
	}
}
