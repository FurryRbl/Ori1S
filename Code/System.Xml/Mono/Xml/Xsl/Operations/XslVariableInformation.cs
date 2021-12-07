using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200006F RID: 111
	internal class XslVariableInformation
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00019674 File Offset: 0x00017874
		public XslVariableInformation(Compiler c)
		{
			c.CheckExtraAttributes(c.Input.LocalName, new string[]
			{
				"name",
				"select"
			});
			c.AssertAttribute("name");
			this.name = c.ParseQNameAttribute("name");
			try
			{
				XmlConvert.VerifyName(this.name.Name);
			}
			catch (XmlException innerException)
			{
				throw new XsltCompileException("Variable name is not qualified name", innerException, c.Input);
			}
			string attribute = c.GetAttribute("select");
			if (attribute != null && attribute != string.Empty)
			{
				this.select = c.CompileExpression(c.GetAttribute("select"));
			}
			else if (c.Input.MoveToFirstChild())
			{
				this.content = c.CompileTemplateContent();
				c.Input.MoveToParent();
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001977C File Offset: 0x0001797C
		public object Evaluate(XslTransformProcessor p)
		{
			if (this.select != null)
			{
				object obj = p.Evaluate(this.select);
				if (obj is XPathNodeIterator)
				{
					ArrayList arrayList = new ArrayList();
					XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)obj;
					while (xpathNodeIterator.MoveNext())
					{
						XPathNavigator xpathNavigator = xpathNodeIterator.Current;
						arrayList.Add(xpathNavigator.Clone());
					}
					obj = new ListIterator(arrayList, p.XPathContext);
				}
				return obj;
			}
			if (this.content != null)
			{
				DTMXPathDocumentWriter2 dtmxpathDocumentWriter = new DTMXPathDocumentWriter2(p.Root.NameTable, 200);
				Outputter newOutput = new GenericOutputter(dtmxpathDocumentWriter, p.Outputs, null, true);
				p.PushOutput(newOutput);
				if (p.CurrentNodeset.CurrentPosition == 0)
				{
					p.NodesetMoveNext();
				}
				this.content.Evaluate(p);
				p.PopOutput();
				return dtmxpathDocumentWriter.CreateDocument().CreateNavigator();
			}
			return string.Empty;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00019860 File Offset: 0x00017A60
		public XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00019868 File Offset: 0x00017A68
		internal XPathExpression Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00019870 File Offset: 0x00017A70
		internal XslOperation Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0400029F RID: 671
		private XmlQualifiedName name;

		// Token: 0x040002A0 RID: 672
		private XPathExpression select;

		// Token: 0x040002A1 RID: 673
		private XslOperation content;
	}
}
