using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200005A RID: 90
	internal class XslElement : XslCompiledElement
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00016F50 File Offset: 0x00015150
		public XslElement(Compiler c) : base(c)
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00016F5C File Offset: 0x0001515C
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			c.CheckExtraAttributes("element", new string[]
			{
				"name",
				"namespace",
				"use-attribute-sets"
			});
			this.name = c.ParseAvtAttribute("name");
			this.ns = c.ParseAvtAttribute("namespace");
			this.nsDecls = c.GetNamespacesToCopy();
			this.calcName = XslAvt.AttemptPreCalc(ref this.name);
			if (this.calcName != null)
			{
				int num = this.calcName.IndexOf(':');
				if (num == 0)
				{
					throw new XsltCompileException("Invalid name attribute", null, c.Input);
				}
				this.calcPrefix = ((num >= 0) ? this.calcName.Substring(0, num) : string.Empty);
				if (num > 0)
				{
					this.calcName = this.calcName.Substring(num + 1);
				}
				try
				{
					XmlConvert.VerifyNCName(this.calcName);
					if (this.calcPrefix != string.Empty)
					{
						XmlConvert.VerifyNCName(this.calcPrefix);
					}
				}
				catch (XmlException innerException)
				{
					throw new XsltCompileException("Invalid name attribute", innerException, c.Input);
				}
				if (this.ns == null)
				{
					this.calcNs = c.Input.GetNamespace(this.calcPrefix);
					if (this.calcPrefix != string.Empty && this.calcNs == string.Empty)
					{
						throw new XsltCompileException("Invalid name attribute", null, c.Input);
					}
				}
			}
			else if (this.ns != null)
			{
				this.calcNs = XslAvt.AttemptPreCalc(ref this.ns);
			}
			this.useAttributeSets = c.ParseQNameListAttribute("use-attribute-sets");
			this.isEmptyElement = c.Input.IsEmptyElement;
			if (c.Input.MoveToFirstChild())
			{
				this.value = c.CompileTemplateContent(XPathNodeType.Element);
				c.Input.MoveToParent();
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00017190 File Offset: 0x00015390
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			string text = (this.calcName == null) ? this.name.Evaluate(p) : this.calcName;
			string text2 = (this.calcNs == null) ? ((this.ns == null) ? null : this.ns.Evaluate(p)) : this.calcNs;
			XmlQualifiedName xmlQualifiedName = XslNameUtil.FromString(text, this.nsDecls);
			string text3 = xmlQualifiedName.Name;
			if (text2 == null)
			{
				text2 = xmlQualifiedName.Namespace;
			}
			int num = text.IndexOf(':');
			if (num > 0)
			{
				this.calcPrefix = text.Substring(0, num);
			}
			else if (num == 0)
			{
				XmlConvert.VerifyNCName(string.Empty);
			}
			string text4 = (this.calcPrefix == null) ? string.Empty : this.calcPrefix;
			if (text4 != string.Empty)
			{
				XmlConvert.VerifyNCName(text4);
			}
			XmlConvert.VerifyNCName(text3);
			bool insideCDataElement = p.InsideCDataElement;
			p.PushElementState(text4, text3, text2, false);
			p.Out.WriteStartElement(text4, text3, text2);
			if (this.useAttributeSets != null)
			{
				foreach (XmlQualifiedName xmlQualifiedName2 in this.useAttributeSets)
				{
					p.ResolveAttributeSet(xmlQualifiedName2).Evaluate(p);
				}
			}
			if (this.value != null)
			{
				this.value.Evaluate(p);
			}
			if (this.isEmptyElement && this.useAttributeSets == null)
			{
				p.Out.WriteEndElement();
			}
			else
			{
				p.Out.WriteFullEndElement();
			}
			p.PopCDataState(insideCDataElement);
		}

		// Token: 0x04000250 RID: 592
		private XslAvt name;

		// Token: 0x04000251 RID: 593
		private XslAvt ns;

		// Token: 0x04000252 RID: 594
		private string calcName;

		// Token: 0x04000253 RID: 595
		private string calcNs;

		// Token: 0x04000254 RID: 596
		private string calcPrefix;

		// Token: 0x04000255 RID: 597
		private Hashtable nsDecls;

		// Token: 0x04000256 RID: 598
		private bool isEmptyElement;

		// Token: 0x04000257 RID: 599
		private XslOperation value;

		// Token: 0x04000258 RID: 600
		private XmlQualifiedName[] useAttributeSets;
	}
}
