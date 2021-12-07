using System;
using System.Collections;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000061 RID: 97
	internal class XslNotSupportedOperation : XslCompiledElement
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00017C50 File Offset: 0x00015E50
		public XslNotSupportedOperation(Compiler c) : base(c)
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00017C5C File Offset: 0x00015E5C
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			this.name = c.Input.LocalName;
			if (c.Input.MoveToFirstChild())
			{
				do
				{
					if (c.Input.NodeType == XPathNodeType.Element && !(c.Input.LocalName != "fallback") && !(c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform"))
					{
						if (this.fallbacks == null)
						{
							this.fallbacks = new ArrayList();
						}
						this.fallbacks.Add(new XslFallback(c));
					}
				}
				while (c.Input.MoveToNext());
				c.Input.MoveToParent();
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00017D34 File Offset: 0x00015F34
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			if (this.fallbacks != null)
			{
				foreach (object obj in this.fallbacks)
				{
					XslFallback xslFallback = (XslFallback)obj;
					xslFallback.Evaluate(p);
				}
				return;
			}
			throw new XsltException(string.Format("'{0}' element is not supported as a template content in XSLT 1.0.", this.name), null);
		}

		// Token: 0x0400026F RID: 623
		private string name;

		// Token: 0x04000270 RID: 624
		private ArrayList fallbacks;
	}
}
