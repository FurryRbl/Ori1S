using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200005E RID: 94
	internal class XslLiteralElement : XslCompiledElement
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000176F8 File Offset: 0x000158F8
		public XslLiteralElement(Compiler c) : base(c)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00017704 File Offset: 0x00015904
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			this.prefix = c.Input.Prefix;
			string actualPrefix = c.CurrentStylesheet.GetActualPrefix(this.prefix);
			if (actualPrefix != this.prefix)
			{
				this.prefix = actualPrefix;
				this.nsUri = c.Input.GetNamespace(actualPrefix);
			}
			else
			{
				this.nsUri = c.Input.NamespaceURI;
			}
			this.localname = c.Input.LocalName;
			this.useAttributeSets = c.ParseQNameListAttribute("use-attribute-sets", "http://www.w3.org/1999/XSL/Transform");
			this.nsDecls = c.GetNamespacesToCopy();
			if (this.nsDecls.Count == 0)
			{
				this.nsDecls = null;
			}
			this.isEmptyElement = c.Input.IsEmptyElement;
			if (c.Input.MoveToFirstAttribute())
			{
				this.attrs = new ArrayList();
				do
				{
					if (!(c.Input.NamespaceURI == "http://www.w3.org/1999/XSL/Transform"))
					{
						this.attrs.Add(new XslLiteralElement.XslLiteralAttribute(c));
					}
				}
				while (c.Input.MoveToNextAttribute());
				c.Input.MoveToParent();
			}
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			this.children = c.CompileTemplateContent();
			c.Input.MoveToParent();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001787C File Offset: 0x00015A7C
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			bool insideCDataElement = p.InsideCDataElement;
			p.PushElementState(this.prefix, this.localname, this.nsUri, true);
			p.Out.WriteStartElement(this.prefix, this.localname, this.nsUri);
			if (this.useAttributeSets != null)
			{
				foreach (XmlQualifiedName name in this.useAttributeSets)
				{
					p.ResolveAttributeSet(name).Evaluate(p);
				}
			}
			if (this.attrs != null)
			{
				int count = this.attrs.Count;
				for (int j = 0; j < count; j++)
				{
					((XslLiteralElement.XslLiteralAttribute)this.attrs[j]).Evaluate(p);
				}
			}
			p.OutputLiteralNamespaceUriNodes(this.nsDecls, null, null);
			if (this.children != null)
			{
				this.children.Evaluate(p);
			}
			if (this.isEmptyElement)
			{
				p.Out.WriteEndElement();
			}
			else
			{
				p.Out.WriteFullEndElement();
			}
			p.PopCDataState(insideCDataElement);
		}

		// Token: 0x0400025F RID: 607
		private XslOperation children;

		// Token: 0x04000260 RID: 608
		private string localname;

		// Token: 0x04000261 RID: 609
		private string prefix;

		// Token: 0x04000262 RID: 610
		private string nsUri;

		// Token: 0x04000263 RID: 611
		private bool isEmptyElement;

		// Token: 0x04000264 RID: 612
		private ArrayList attrs;

		// Token: 0x04000265 RID: 613
		private XmlQualifiedName[] useAttributeSets;

		// Token: 0x04000266 RID: 614
		private Hashtable nsDecls;

		// Token: 0x0200005F RID: 95
		private class XslLiteralAttribute
		{
			// Token: 0x06000375 RID: 885 RVA: 0x000179B4 File Offset: 0x00015BB4
			public XslLiteralAttribute(Compiler c)
			{
				this.prefix = c.Input.Prefix;
				if (this.prefix.Length > 0)
				{
					string actualPrefix = c.CurrentStylesheet.GetActualPrefix(this.prefix);
					if (actualPrefix != this.prefix)
					{
						this.prefix = actualPrefix;
						XPathNavigator xpathNavigator = c.Input.Clone();
						xpathNavigator.MoveToParent();
						this.nsUri = xpathNavigator.GetNamespace(actualPrefix);
					}
					else
					{
						this.nsUri = c.Input.NamespaceURI;
					}
				}
				else
				{
					this.nsUri = string.Empty;
				}
				this.localname = c.Input.LocalName;
				this.val = new XslAvt(c.Input.Value, c);
			}

			// Token: 0x06000376 RID: 886 RVA: 0x00017A84 File Offset: 0x00015C84
			public void Evaluate(XslTransformProcessor p)
			{
				p.Out.WriteAttributeString(this.prefix, this.localname, this.nsUri, this.val.Evaluate(p));
			}

			// Token: 0x04000267 RID: 615
			private string localname;

			// Token: 0x04000268 RID: 616
			private string prefix;

			// Token: 0x04000269 RID: 617
			private string nsUri;

			// Token: 0x0400026A RID: 618
			private XslAvt val;
		}
	}
}
