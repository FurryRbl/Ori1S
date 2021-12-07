using System;
using System.Collections;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200009E RID: 158
	internal class XslDefaultTextTemplate : XslTemplate
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x000218A8 File Offset: 0x0001FAA8
		private XslDefaultTextTemplate() : base(null)
		{
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000218C0 File Offset: 0x0001FAC0
		public static XslTemplate Instance
		{
			get
			{
				return XslDefaultTextTemplate.instance;
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000218C8 File Offset: 0x0001FAC8
		public override void Evaluate(XslTransformProcessor p, Hashtable withParams)
		{
			if (p.CurrentNode.NodeType == XPathNodeType.Whitespace)
			{
				if (p.PreserveOutputWhitespace)
				{
					p.Out.WriteWhitespace(p.CurrentNode.Value);
				}
			}
			else
			{
				p.Out.WriteString(p.CurrentNode.Value);
			}
		}

		// Token: 0x04000376 RID: 886
		private static XslDefaultTextTemplate instance = new XslDefaultTextTemplate();
	}
}
