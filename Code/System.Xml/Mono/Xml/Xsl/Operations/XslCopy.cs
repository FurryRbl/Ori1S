using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000058 RID: 88
	internal class XslCopy : XslCompiledElement
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0001685C File Offset: 0x00014A5C
		public XslCopy(Compiler c) : base(c)
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00016868 File Offset: 0x00014A68
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			this.nsDecls = c.GetNamespacesToCopy();
			if (this.nsDecls.Count == 0)
			{
				this.nsDecls = null;
			}
			c.CheckExtraAttributes("copy", new string[]
			{
				"use-attribute-sets"
			});
			this.useAttributeSets = c.ParseQNameListAttribute("use-attribute-sets");
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			this.children = c.CompileTemplateContent();
			c.Input.MoveToParent();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00016908 File Offset: 0x00014B08
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			XPathNavigator currentNode = p.CurrentNode;
			switch (currentNode.NodeType)
			{
			case XPathNodeType.Root:
				if (p.Out.CanProcessAttributes && this.useAttributeSets != null)
				{
					foreach (XmlQualifiedName name in this.useAttributeSets)
					{
						XslAttributeSet xslAttributeSet = p.ResolveAttributeSet(name);
						if (xslAttributeSet == null)
						{
							throw new XsltException("Attribute set was not found", null, currentNode);
						}
						xslAttributeSet.Evaluate(p);
					}
				}
				if (this.children != null)
				{
					this.children.Evaluate(p);
				}
				break;
			case XPathNodeType.Element:
			{
				bool insideCDataElement = p.InsideCDataElement;
				string prefix = currentNode.Prefix;
				p.PushElementState(prefix, currentNode.LocalName, currentNode.NamespaceURI, true);
				p.Out.WriteStartElement(prefix, currentNode.LocalName, currentNode.NamespaceURI);
				if (this.useAttributeSets != null)
				{
					foreach (XmlQualifiedName name2 in this.useAttributeSets)
					{
						p.ResolveAttributeSet(name2).Evaluate(p);
					}
				}
				if (currentNode.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml))
				{
					do
					{
						if (!(currentNode.LocalName == prefix))
						{
							p.Out.WriteNamespaceDecl(currentNode.LocalName, currentNode.Value);
						}
					}
					while (currentNode.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml));
					currentNode.MoveToParent();
				}
				if (this.children != null)
				{
					this.children.Evaluate(p);
				}
				p.Out.WriteFullEndElement();
				p.PopCDataState(insideCDataElement);
				break;
			}
			case XPathNodeType.Attribute:
				p.Out.WriteAttributeString(currentNode.Prefix, currentNode.LocalName, currentNode.NamespaceURI, currentNode.Value);
				break;
			case XPathNodeType.Namespace:
				if (p.XPathContext.ElementPrefix != currentNode.Name)
				{
					p.Out.WriteNamespaceDecl(currentNode.Name, currentNode.Value);
				}
				break;
			case XPathNodeType.Text:
				p.Out.WriteString(currentNode.Value);
				break;
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
			{
				bool insideCDataSection = p.Out.InsideCDataSection;
				p.Out.InsideCDataSection = false;
				p.Out.WriteString(currentNode.Value);
				p.Out.InsideCDataSection = insideCDataSection;
				break;
			}
			case XPathNodeType.ProcessingInstruction:
				p.Out.WriteProcessingInstruction(currentNode.Name, currentNode.Value);
				break;
			case XPathNodeType.Comment:
				p.Out.WriteComment(currentNode.Value);
				break;
			}
		}

		// Token: 0x0400024C RID: 588
		private XslOperation children;

		// Token: 0x0400024D RID: 589
		private XmlQualifiedName[] useAttributeSets;

		// Token: 0x0400024E RID: 590
		private Hashtable nsDecls;
	}
}
