using System;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200006B RID: 107
	internal class XslProcessingInstruction : XslCompiledElement
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00018C98 File Offset: 0x00016E98
		public XslProcessingInstruction(Compiler c) : base(c)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00018CA4 File Offset: 0x00016EA4
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			c.CheckExtraAttributes("processing-instruction", new string[]
			{
				"name"
			});
			this.name = c.ParseAvtAttribute("name");
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			this.value = c.CompileTemplateContent(XPathNodeType.ProcessingInstruction);
			c.Input.MoveToParent();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00018D24 File Offset: 0x00016F24
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			string text = this.name.Evaluate(p);
			if (string.Compare(text, "xml", true, CultureInfo.InvariantCulture) == 0)
			{
				throw new XsltException("Processing instruction name was evaluated to \"xml\"", null, p.CurrentNode);
			}
			if (text.IndexOf(':') >= 0)
			{
				return;
			}
			p.Out.WriteProcessingInstruction(text, (this.value != null) ? this.value.EvaluateAsString(p) : string.Empty);
		}

		// Token: 0x04000291 RID: 657
		private XslAvt name;

		// Token: 0x04000292 RID: 658
		private XslOperation value;
	}
}
