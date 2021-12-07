using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using Mono.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000098 RID: 152
	internal class XslModedTemplateTable
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x00020E84 File Offset: 0x0001F084
		public XslModedTemplateTable(XmlQualifiedName mode)
		{
			if (mode == null)
			{
				throw new InvalidOperationException();
			}
			this.mode = mode;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00020EBC File Offset: 0x0001F0BC
		public XmlQualifiedName Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		public void Add(XslTemplate t)
		{
			if (!double.IsNaN(t.Priority))
			{
				this.unnamedTemplates.Add(new XslModedTemplateTable.TemplateWithPriority(t, t.Priority));
			}
			else
			{
				this.Add(t, t.Match);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00020F0C File Offset: 0x0001F10C
		public void Add(XslTemplate t, Pattern p)
		{
			if (p is UnionPattern)
			{
				this.Add(t, ((UnionPattern)p).p0);
				this.Add(t, ((UnionPattern)p).p1);
				return;
			}
			this.unnamedTemplates.Add(new XslModedTemplateTable.TemplateWithPriority(t, p));
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00020F5C File Offset: 0x0001F15C
		public XslTemplate FindMatch(XPathNavigator node, XslTransformProcessor p)
		{
			if (!this.sorted)
			{
				this.unnamedTemplates.Sort();
				this.unnamedTemplates.Reverse();
				this.sorted = true;
			}
			for (int i = 0; i < this.unnamedTemplates.Count; i++)
			{
				XslModedTemplateTable.TemplateWithPriority templateWithPriority = (XslModedTemplateTable.TemplateWithPriority)this.unnamedTemplates[i];
				if (templateWithPriority.Matches(node, p))
				{
					return templateWithPriority.Template;
				}
			}
			return null;
		}

		// Token: 0x0400035F RID: 863
		private ArrayList unnamedTemplates = new ArrayList();

		// Token: 0x04000360 RID: 864
		private XmlQualifiedName mode;

		// Token: 0x04000361 RID: 865
		private bool sorted;

		// Token: 0x02000099 RID: 153
		private class TemplateWithPriority : IComparable
		{
			// Token: 0x06000526 RID: 1318 RVA: 0x00020FD4 File Offset: 0x0001F1D4
			public TemplateWithPriority(XslTemplate t, Pattern p)
			{
				this.Template = t;
				this.Pattern = p;
				this.Priority = p.DefaultPriority;
				this.TemplateID = t.Id;
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x00021010 File Offset: 0x0001F210
			public TemplateWithPriority(XslTemplate t, double p)
			{
				this.Template = t;
				this.Pattern = t.Match;
				this.Priority = p;
				this.TemplateID = t.Id;
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x0002104C File Offset: 0x0001F24C
			public int CompareTo(object o)
			{
				XslModedTemplateTable.TemplateWithPriority templateWithPriority = (XslModedTemplateTable.TemplateWithPriority)o;
				int num = this.Priority.CompareTo(templateWithPriority.Priority);
				if (num != 0)
				{
					return num;
				}
				return this.TemplateID.CompareTo(templateWithPriority.TemplateID);
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x00021098 File Offset: 0x0001F298
			public bool Matches(XPathNavigator n, XslTransformProcessor p)
			{
				return p.Matches(this.Pattern, n);
			}

			// Token: 0x04000362 RID: 866
			public readonly double Priority;

			// Token: 0x04000363 RID: 867
			public readonly XslTemplate Template;

			// Token: 0x04000364 RID: 868
			public readonly Pattern Pattern;

			// Token: 0x04000365 RID: 869
			public readonly int TemplateID;
		}
	}
}
