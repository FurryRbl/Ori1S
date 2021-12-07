using System;
using System.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200006D RID: 109
	internal class XslText : XslCompiledElement
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x000193E8 File Offset: 0x000175E8
		public XslText(Compiler c, bool isWhitespace) : base(c)
		{
			this.isWhitespace = isWhitespace;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00019404 File Offset: 0x00017604
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			c.CheckExtraAttributes("text", new string[]
			{
				"disable-output-escaping"
			});
			this.text = c.Input.Value;
			if (c.Input.NodeType == XPathNodeType.Element)
			{
				this.disableOutputEscaping = c.ParseYesNoAttribute("disable-output-escaping", false);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001947C File Offset: 0x0001767C
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			if (!this.disableOutputEscaping)
			{
				if (this.isWhitespace)
				{
					p.Out.WriteWhitespace(this.text);
				}
				else
				{
					p.Out.WriteString(this.text);
				}
			}
			else
			{
				p.Out.WriteRaw(this.text);
			}
		}

		// Token: 0x0400029A RID: 666
		private bool disableOutputEscaping;

		// Token: 0x0400029B RID: 667
		private string text = string.Empty;

		// Token: 0x0400029C RID: 668
		private bool isWhitespace;
	}
}
