using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200008C RID: 140
	internal class XslAttributeSet : XslCompiledElement
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		public XslAttributeSet(Compiler c) : base(c)
		{
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001E11C File Offset: 0x0001C31C
		public XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001E124 File Offset: 0x0001C324
		protected override void Compile(Compiler c)
		{
			this.name = c.ParseQNameAttribute("name");
			XmlQualifiedName[] array = c.ParseQNameListAttribute("use-attribute-sets");
			if (array != null)
			{
				foreach (XmlQualifiedName value in array)
				{
					this.usedAttributeSets.Add(value);
				}
			}
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			for (;;)
			{
				if (c.Input.NodeType == XPathNodeType.Element)
				{
					if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform" || c.Input.LocalName != "attribute")
					{
						break;
					}
					this.attributes.Add(new XslAttribute(c));
				}
				if (!c.Input.MoveToNext())
				{
					goto Block_5;
				}
			}
			throw new XsltCompileException("Invalid attr set content", null, c.Input);
			Block_5:
			c.Input.MoveToParent();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001E218 File Offset: 0x0001C418
		public void Merge(XslAttributeSet s)
		{
			this.attributes.AddRange(s.attributes);
			foreach (object obj in s.usedAttributeSets)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				if (!this.usedAttributeSets.Contains(xmlQualifiedName))
				{
					this.usedAttributeSets.Add(xmlQualifiedName);
				}
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		public override void Evaluate(XslTransformProcessor p)
		{
			p.SetBusy(this);
			if (this.usedAttributeSets != null)
			{
				for (int i = 0; i < this.usedAttributeSets.Count; i++)
				{
					XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)this.usedAttributeSets[i];
					XslAttributeSet xslAttributeSet = p.ResolveAttributeSet(xmlQualifiedName);
					if (xslAttributeSet == null)
					{
						throw new XsltException("Could not resolve attribute set", null, p.CurrentNode);
					}
					if (p.IsBusy(xslAttributeSet))
					{
						throw new XsltException("circular dependency", null, p.CurrentNode);
					}
					xslAttributeSet.Evaluate(p);
				}
			}
			for (int j = 0; j < this.attributes.Count; j++)
			{
				((XslAttribute)this.attributes[j]).Evaluate(p);
			}
			p.SetFree(this);
		}

		// Token: 0x0400030C RID: 780
		private XmlQualifiedName name;

		// Token: 0x0400030D RID: 781
		private ArrayList usedAttributeSets = new ArrayList();

		// Token: 0x0400030E RID: 782
		private ArrayList attributes = new ArrayList();
	}
}
