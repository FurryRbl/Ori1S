using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000060 RID: 96
	internal class XslMessage : XslCompiledElement
	{
		// Token: 0x06000377 RID: 887 RVA: 0x00017ABC File Offset: 0x00015CBC
		public XslMessage(Compiler c) : base(c)
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00017AC8 File Offset: 0x00015CC8
		static XslMessage()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_XSLT_MESSAGE_OUTPUT");
			if (environmentVariable != null)
			{
				if (XslMessage.<>f__switch$mapA == null)
				{
					XslMessage.<>f__switch$mapA = new Dictionary<string, int>(2)
					{
						{
							"none",
							0
						},
						{
							"stderr",
							1
						}
					};
				}
				int num;
				if (XslMessage.<>f__switch$mapA.TryGetValue(environmentVariable, out num))
				{
					if (num == 0)
					{
						XslMessage.output = TextWriter.Null;
						return;
					}
					if (num == 1)
					{
						XslMessage.output = Console.Error;
						return;
					}
				}
			}
			XslMessage.output = Console.Out;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00017B68 File Offset: 0x00015D68
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			c.CheckExtraAttributes("message", new string[]
			{
				"terminate"
			});
			this.terminate = c.ParseYesNoAttribute("terminate", false);
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			this.children = c.CompileTemplateContent();
			c.Input.MoveToParent();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00017BE8 File Offset: 0x00015DE8
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			if (this.children != null)
			{
				XslMessage.output.Write(this.children.EvaluateAsString(p));
			}
			if (this.terminate)
			{
				throw new XsltException("Transformation terminated.", null, p.CurrentNode);
			}
		}

		// Token: 0x0400026B RID: 619
		private static TextWriter output;

		// Token: 0x0400026C RID: 620
		private bool terminate;

		// Token: 0x0400026D RID: 621
		private XslOperation children;
	}
}
