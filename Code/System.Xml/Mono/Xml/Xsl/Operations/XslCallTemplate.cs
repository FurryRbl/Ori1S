using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000053 RID: 83
	internal class XslCallTemplate : XslCompiledElement
	{
		// Token: 0x0600034F RID: 847 RVA: 0x000162AC File Offset: 0x000144AC
		public XslCallTemplate(Compiler c) : base(c)
		{
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000162B8 File Offset: 0x000144B8
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			c.CheckExtraAttributes("call-template", new string[]
			{
				"name"
			});
			c.AssertAttribute("name");
			this.name = c.ParseQNameAttribute("name");
			if (c.Input.MoveToFirstChild())
			{
				for (;;)
				{
					switch (c.Input.NodeType)
					{
					case XPathNodeType.Element:
					{
						if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
						{
							goto Block_3;
						}
						string localName = c.Input.LocalName;
						if (localName == null)
						{
							goto IL_14A;
						}
						if (XslCallTemplate.<>f__switch$map8 == null)
						{
							XslCallTemplate.<>f__switch$map8 = new Dictionary<string, int>(1)
							{
								{
									"with-param",
									0
								}
							};
						}
						int num;
						if (!XslCallTemplate.<>f__switch$map8.TryGetValue(localName, out num))
						{
							goto IL_14A;
						}
						if (num != 0)
						{
							goto Block_7;
						}
						if (this.withParams == null)
						{
							this.withParams = new ArrayList();
						}
						this.withParams.Add(new XslVariableInformation(c));
						goto IL_188;
					}
					case XPathNodeType.SignificantWhitespace:
					case XPathNodeType.Whitespace:
					case XPathNodeType.ProcessingInstruction:
					case XPathNodeType.Comment:
						goto IL_188;
					}
					break;
					IL_188:
					if (!c.Input.MoveToNext())
					{
						goto Block_9;
					}
				}
				goto IL_161;
				Block_3:
				throw new XsltCompileException("Unexpected element", null, c.Input);
				Block_7:
				IL_14A:
				throw new XsltCompileException("Unexpected element", null, c.Input);
				IL_161:
				throw new XsltCompileException("Unexpected node type " + c.Input.NodeType, null, c.Input);
				Block_9:
				c.Input.MoveToParent();
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001646C File Offset: 0x0001466C
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			p.CallTemplate(this.name, this.withParams);
		}

		// Token: 0x04000242 RID: 578
		private XmlQualifiedName name;

		// Token: 0x04000243 RID: 579
		private ArrayList withParams;
	}
}
