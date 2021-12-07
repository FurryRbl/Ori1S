using System;
using System.Collections;
using System.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200005C RID: 92
	internal class XslForEach : XslCompiledElement
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00017404 File Offset: 0x00015604
		public XslForEach(Compiler c) : base(c)
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00017410 File Offset: 0x00015610
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			c.CheckExtraAttributes("for-each", new string[]
			{
				"select"
			});
			c.AssertAttribute("select");
			this.select = c.CompileExpression(c.GetAttribute("select"));
			ArrayList arrayList = null;
			if (c.Input.MoveToFirstChild())
			{
				bool flag = true;
				while (c.Input.NodeType != XPathNodeType.Text)
				{
					if (c.Input.NodeType == XPathNodeType.Element)
					{
						if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
						{
							flag = false;
							goto IL_104;
						}
						if (c.Input.LocalName != "sort")
						{
							flag = false;
							goto IL_104;
						}
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(new Sort(c));
					}
					if (c.Input.MoveToNext())
					{
						continue;
					}
					IL_104:
					if (!flag)
					{
						this.children = c.CompileTemplateContent();
					}
					c.Input.MoveToParent();
					goto IL_122;
				}
				flag = false;
				goto IL_104;
			}
			IL_122:
			if (arrayList != null)
			{
				this.sortEvaluator = new XslSortEvaluator(this.select, (Sort[])arrayList.ToArray(typeof(Sort)));
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001756C File Offset: 0x0001576C
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			XPathNodeIterator xpathNodeIterator = (this.sortEvaluator == null) ? p.Select(this.select) : this.sortEvaluator.SortedSelect(p);
			while (p.NodesetMoveNext(xpathNodeIterator))
			{
				p.PushNodeset(xpathNodeIterator);
				p.PushForEachContext();
				this.children.Evaluate(p);
				p.PopForEachContext();
				p.PopNodeset();
			}
		}

		// Token: 0x0400025A RID: 602
		private XPathExpression select;

		// Token: 0x0400025B RID: 603
		private XslOperation children;

		// Token: 0x0400025C RID: 604
		private XslSortEvaluator sortEvaluator;
	}
}
