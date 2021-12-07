using System;
using System.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200005D RID: 93
	internal class XslIf : XslCompiledElement
	{
		// Token: 0x0600036E RID: 878 RVA: 0x000175F8 File Offset: 0x000157F8
		public XslIf(Compiler c) : base(c)
		{
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00017604 File Offset: 0x00015804
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			c.CheckExtraAttributes("if", new string[]
			{
				"test"
			});
			c.AssertAttribute("test");
			this.test = c.CompileExpression(c.GetAttribute("test"));
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			this.children = c.CompileTemplateContent();
			c.Input.MoveToParent();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00017694 File Offset: 0x00015894
		public bool EvaluateIfTrue(XslTransformProcessor p)
		{
			if (p.EvaluateBoolean(this.test))
			{
				if (this.children != null)
				{
					this.children.Evaluate(p);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000176C4 File Offset: 0x000158C4
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			this.EvaluateIfTrue(p);
		}

		// Token: 0x0400025D RID: 605
		private CompiledExpression test;

		// Token: 0x0400025E RID: 606
		private XslOperation children;
	}
}
