using System;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200005B RID: 91
	internal class XslFallback : XslCompiledElement
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0001735C File Offset: 0x0001555C
		public XslFallback(Compiler c) : base(c)
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00017368 File Offset: 0x00015568
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			c.CheckExtraAttributes("fallback", new string[0]);
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			this.children = c.CompileTemplateContent();
			c.Input.MoveToParent();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000173CC File Offset: 0x000155CC
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			this.children.Evaluate(p);
		}

		// Token: 0x04000259 RID: 601
		private XslOperation children;
	}
}
