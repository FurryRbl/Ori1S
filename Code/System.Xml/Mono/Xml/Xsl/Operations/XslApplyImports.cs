using System;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200004C RID: 76
	internal class XslApplyImports : XslCompiledElement
	{
		// Token: 0x0600033D RID: 829 RVA: 0x00015864 File Offset: 0x00013A64
		public XslApplyImports(Compiler c) : base(c)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00015870 File Offset: 0x00013A70
		protected override void Compile(Compiler c)
		{
			c.CheckExtraAttributes("apply-imports", new string[0]);
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000158AC File Offset: 0x00013AAC
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			p.ApplyImports();
		}
	}
}
