using System;
using System.Collections;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.XPath;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200009B RID: 155
	internal class XslTemplate
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x000212D4 File Offset: 0x0001F4D4
		public XslTemplate(Compiler c)
		{
			if (c == null)
			{
				return;
			}
			this.style = c.CurrentStylesheet;
			c.PushScope();
			if (c.Input.Name == "template" && c.Input.NamespaceURI == "http://www.w3.org/1999/XSL/Transform" && c.Input.MoveToAttribute("mode", string.Empty))
			{
				c.Input.MoveToParent();
				if (!c.Input.MoveToAttribute("match", string.Empty))
				{
					throw new XsltCompileException("XSLT 'template' element must not have 'mode' attribute when it does not have 'match' attribute", null, c.Input);
				}
				c.Input.MoveToParent();
			}
			if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
			{
				this.name = XmlQualifiedName.Empty;
				this.match = c.CompilePattern("/", c.Input);
				this.mode = XmlQualifiedName.Empty;
			}
			else
			{
				this.name = c.ParseQNameAttribute("name");
				this.match = c.CompilePattern(c.GetAttribute("match"), c.Input);
				this.mode = c.ParseQNameAttribute("mode");
				string attribute = c.GetAttribute("priority");
				if (attribute != null)
				{
					try
					{
						this.priority = double.Parse(attribute, CultureInfo.InvariantCulture);
					}
					catch (FormatException innerException)
					{
						throw new XsltException("Invalid priority number format", innerException, c.Input);
					}
				}
			}
			this.Parse(c);
			this.stackSize = c.PopScope().VariableHighTide;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000214B8 File Offset: 0x0001F6B8
		public XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000214C0 File Offset: 0x0001F6C0
		public Pattern Match
		{
			get
			{
				return this.match;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000214C8 File Offset: 0x0001F6C8
		public XmlQualifiedName Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000214D0 File Offset: 0x0001F6D0
		public double Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000214D8 File Offset: 0x0001F6D8
		public XslStylesheet Parent
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000214E0 File Offset: 0x0001F6E0
		private void Parse(Compiler c)
		{
			if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
			{
				this.content = c.CompileTemplateContent();
				return;
			}
			if (c.Input.MoveToFirstChild())
			{
				bool flag = true;
				XPathNavigator xpathNavigator = c.Input.Clone();
				bool flag2 = false;
				for (;;)
				{
					if (flag2)
					{
						flag2 = false;
						xpathNavigator.MoveTo(c.Input);
					}
					if (c.Input.NodeType == XPathNodeType.Text)
					{
						break;
					}
					if (c.Input.NodeType == XPathNodeType.Element)
					{
						if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
						{
							goto Block_6;
						}
						if (c.Input.LocalName != "param")
						{
							goto Block_7;
						}
						if (this.parameters == null)
						{
							this.parameters = new ArrayList();
						}
						this.parameters.Add(new XslLocalParam(c));
						flag2 = true;
					}
					if (!c.Input.MoveToNext())
					{
						goto IL_106;
					}
				}
				flag = false;
				goto IL_106;
				Block_6:
				flag = false;
				goto IL_106;
				Block_7:
				flag = false;
				IL_106:
				if (!flag)
				{
					c.Input.MoveTo(xpathNavigator);
					this.content = c.CompileTemplateContent();
				}
				c.Input.MoveToParent();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00021620 File Offset: 0x0001F820
		private string LocationMessage
		{
			get
			{
				XslCompiledElementBase xslCompiledElementBase = (XslCompiledElementBase)this.content;
				return string.Format(" from\nxsl:template {0} at {1} ({2},{3})", new object[]
				{
					this.Match,
					this.style.BaseURI,
					xslCompiledElementBase.LineNumber,
					xslCompiledElementBase.LinePosition
				});
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002167C File Offset: 0x0001F87C
		private void AppendTemplateFrame(XsltException ex)
		{
			ex.AddTemplateFrame(this.LocationMessage);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0002168C File Offset: 0x0001F88C
		public virtual void Evaluate(XslTransformProcessor p, Hashtable withParams)
		{
			if (XslTransform.TemplateStackFrameError)
			{
				try
				{
					this.EvaluateCore(p, withParams);
				}
				catch (XsltException ex)
				{
					this.AppendTemplateFrame(ex);
					throw ex;
				}
				catch (Exception)
				{
					XsltException ex2 = new XsltException("Error during XSLT processing: ", null, p.CurrentNode);
					this.AppendTemplateFrame(ex2);
					throw ex2;
				}
			}
			else
			{
				this.EvaluateCore(p, withParams);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00021724 File Offset: 0x0001F924
		private void EvaluateCore(XslTransformProcessor p, Hashtable withParams)
		{
			if (XslTransform.TemplateStackFrameOutput != null)
			{
				XslTransform.TemplateStackFrameOutput.WriteLine(this.LocationMessage);
			}
			p.PushStack(this.stackSize);
			if (this.parameters != null)
			{
				if (withParams == null)
				{
					int count = this.parameters.Count;
					for (int i = 0; i < count; i++)
					{
						XslLocalParam xslLocalParam = (XslLocalParam)this.parameters[i];
						xslLocalParam.Evaluate(p);
					}
				}
				else
				{
					int count2 = this.parameters.Count;
					for (int j = 0; j < count2; j++)
					{
						XslLocalParam xslLocalParam2 = (XslLocalParam)this.parameters[j];
						object obj = withParams[xslLocalParam2.Name];
						if (obj != null)
						{
							xslLocalParam2.Override(p, obj);
						}
						else
						{
							xslLocalParam2.Evaluate(p);
						}
					}
				}
			}
			if (this.content != null)
			{
				this.content.Evaluate(p);
			}
			p.PopStack();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00021824 File Offset: 0x0001FA24
		public void Evaluate(XslTransformProcessor p)
		{
			this.Evaluate(p, null);
		}

		// Token: 0x04000369 RID: 873
		private XmlQualifiedName name;

		// Token: 0x0400036A RID: 874
		private Pattern match;

		// Token: 0x0400036B RID: 875
		private XmlQualifiedName mode;

		// Token: 0x0400036C RID: 876
		private double priority = double.NaN;

		// Token: 0x0400036D RID: 877
		private ArrayList parameters;

		// Token: 0x0400036E RID: 878
		private XslOperation content;

		// Token: 0x0400036F RID: 879
		private static int nextId;

		// Token: 0x04000370 RID: 880
		public readonly int Id = XslTemplate.nextId++;

		// Token: 0x04000371 RID: 881
		private XslStylesheet style;

		// Token: 0x04000372 RID: 882
		private int stackSize;
	}
}
