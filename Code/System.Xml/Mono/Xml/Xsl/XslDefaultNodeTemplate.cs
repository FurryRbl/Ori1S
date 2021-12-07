using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200009C RID: 156
	internal class XslDefaultNodeTemplate : XslTemplate
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x00021830 File Offset: 0x0001FA30
		public XslDefaultNodeTemplate(XmlQualifiedName mode) : base(null)
		{
			this.mode = mode;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00021854 File Offset: 0x0001FA54
		public static XslTemplate Instance
		{
			get
			{
				return XslDefaultNodeTemplate.instance;
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002185C File Offset: 0x0001FA5C
		public override void Evaluate(XslTransformProcessor p, Hashtable withParams)
		{
			p.ApplyTemplates(p.CurrentNode.SelectChildren(XPathNodeType.All), this.mode, null);
		}

		// Token: 0x04000373 RID: 883
		private XmlQualifiedName mode;

		// Token: 0x04000374 RID: 884
		private static XslDefaultNodeTemplate instance = new XslDefaultNodeTemplate(XmlQualifiedName.Empty);
	}
}
