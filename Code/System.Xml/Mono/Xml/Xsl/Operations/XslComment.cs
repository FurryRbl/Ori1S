using System;
using System.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000055 RID: 85
	internal class XslComment : XslCompiledElement
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00016710 File Offset: 0x00014910
		public XslComment(Compiler c) : base(c)
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001671C File Offset: 0x0001491C
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			c.CheckExtraAttributes("comment", new string[0]);
			if (c.Input.MoveToFirstChild())
			{
				this.value = c.CompileTemplateContent(XPathNodeType.Comment);
				c.Input.MoveToParent();
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00016780 File Offset: 0x00014980
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			p.Out.WriteComment((this.value != null) ? this.value.EvaluateAsString(p) : string.Empty);
		}

		// Token: 0x04000248 RID: 584
		private XslOperation value;
	}
}
