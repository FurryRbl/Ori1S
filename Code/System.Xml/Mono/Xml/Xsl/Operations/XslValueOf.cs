using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200006E RID: 110
	internal class XslValueOf : XslCompiledElement
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x000194FC File Offset: 0x000176FC
		public XslValueOf(Compiler c) : base(c)
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00019508 File Offset: 0x00017708
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			c.CheckExtraAttributes("value-of", new string[]
			{
				"select",
				"disable-output-escaping"
			});
			c.AssertAttribute("select");
			this.select = c.CompileExpression(c.GetAttribute("select"));
			this.disableOutputEscaping = c.ParseYesNoAttribute("disable-output-escaping", false);
			if (c.Input.MoveToFirstChild())
			{
				for (;;)
				{
					switch (c.Input.NodeType)
					{
					case XPathNodeType.Element:
						if (c.Input.NamespaceURI == "http://www.w3.org/1999/XSL/Transform")
						{
							goto Block_3;
						}
						break;
					case XPathNodeType.Text:
					case XPathNodeType.SignificantWhitespace:
						goto IL_D2;
					}
					if (!c.Input.MoveToNext())
					{
						return;
					}
				}
				Block_3:
				IL_D2:
				throw new XsltCompileException("XSLT value-of element cannot contain any child.", null, c.Input);
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001960C File Offset: 0x0001780C
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			if (!this.disableOutputEscaping)
			{
				p.Out.WriteString(p.EvaluateString(this.select));
			}
			else
			{
				p.Out.WriteRaw(p.EvaluateString(this.select));
			}
		}

		// Token: 0x0400029D RID: 669
		private XPathExpression select;

		// Token: 0x0400029E RID: 670
		private bool disableOutputEscaping;
	}
}
