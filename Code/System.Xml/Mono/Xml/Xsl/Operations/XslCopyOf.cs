using System;
using System.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000059 RID: 89
	internal class XslCopyOf : XslCompiledElement
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00016BD0 File Offset: 0x00014DD0
		public XslCopyOf(Compiler c) : base(c)
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00016BDC File Offset: 0x00014DDC
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(c.Input);
			}
			c.CheckExtraAttributes("copy-of", new string[]
			{
				"select"
			});
			c.AssertAttribute("select");
			this.select = c.CompileExpression(c.GetAttribute("select"));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00016C40 File Offset: 0x00014E40
		private void CopyNode(XslTransformProcessor p, XPathNavigator nav)
		{
			Outputter @out = p.Out;
			switch (nav.NodeType)
			{
			case XPathNodeType.Root:
			{
				XPathNodeIterator xpathNodeIterator = nav.SelectChildren(XPathNodeType.All);
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator nav2 = xpathNodeIterator.Current;
					this.CopyNode(p, nav2);
				}
				break;
			}
			case XPathNodeType.Element:
			{
				bool insideCDataElement = p.InsideCDataElement;
				string prefix = nav.Prefix;
				string namespaceURI = nav.NamespaceURI;
				p.PushElementState(prefix, nav.LocalName, namespaceURI, false);
				@out.WriteStartElement(prefix, nav.LocalName, namespaceURI);
				if (nav.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml))
				{
					do
					{
						if (!(prefix == nav.Name))
						{
							if (nav.Name.Length != 0 || namespaceURI.Length != 0)
							{
								@out.WriteNamespaceDecl(nav.Name, nav.Value);
							}
						}
					}
					while (nav.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml));
					nav.MoveToParent();
				}
				if (nav.MoveToFirstAttribute())
				{
					do
					{
						@out.WriteAttributeString(nav.Prefix, nav.LocalName, nav.NamespaceURI, nav.Value);
					}
					while (nav.MoveToNextAttribute());
					nav.MoveToParent();
				}
				if (nav.MoveToFirstChild())
				{
					do
					{
						this.CopyNode(p, nav);
					}
					while (nav.MoveToNext());
					nav.MoveToParent();
				}
				if (nav.IsEmptyElement)
				{
					@out.WriteEndElement();
				}
				else
				{
					@out.WriteFullEndElement();
				}
				p.PopCDataState(insideCDataElement);
				break;
			}
			case XPathNodeType.Attribute:
				@out.WriteAttributeString(nav.Prefix, nav.LocalName, nav.NamespaceURI, nav.Value);
				break;
			case XPathNodeType.Namespace:
				if (nav.Name != p.XPathContext.ElementPrefix && (p.XPathContext.ElementNamespace.Length > 0 || nav.Name.Length > 0))
				{
					@out.WriteNamespaceDecl(nav.Name, nav.Value);
				}
				break;
			case XPathNodeType.Text:
				@out.WriteString(nav.Value);
				break;
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
			{
				bool insideCDataSection = @out.InsideCDataSection;
				@out.InsideCDataSection = false;
				@out.WriteString(nav.Value);
				@out.InsideCDataSection = insideCDataSection;
				break;
			}
			case XPathNodeType.ProcessingInstruction:
				@out.WriteProcessingInstruction(nav.Name, nav.Value);
				break;
			case XPathNodeType.Comment:
				@out.WriteComment(nav.Value);
				break;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00016EBC File Offset: 0x000150BC
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			object obj = p.Evaluate(this.select);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator nav = xpathNodeIterator.Current;
					this.CopyNode(p, nav);
				}
			}
			else
			{
				XPathNavigator xpathNavigator = obj as XPathNavigator;
				if (xpathNavigator != null)
				{
					this.CopyNode(p, xpathNavigator);
				}
				else
				{
					p.Out.WriteString(XPathFunctions.ToString(obj));
				}
			}
		}

		// Token: 0x0400024F RID: 591
		private XPathExpression select;
	}
}
