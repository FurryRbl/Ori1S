using System;
using System.Xml.Xsl;

namespace System.Xml.XPath
{
	// Token: 0x0200018D RID: 397
	internal class ExprVariable : Expression
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x0004DA9C File Offset: 0x0004BC9C
		public ExprVariable(XmlQualifiedName name, IStaticXsltContext ctx)
		{
			if (ctx != null)
			{
				name = ctx.LookupQName(name.ToString());
				this.resolvedName = true;
			}
			this._name = name;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0004DAD4 File Offset: 0x0004BCD4
		public override string ToString()
		{
			return "$" + this._name.ToString();
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0004DAEC File Offset: 0x0004BCEC
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004DAF0 File Offset: 0x0004BCF0
		public override XPathResultType GetReturnType(BaseIterator iter)
		{
			return XPathResultType.Any;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0004DAF4 File Offset: 0x0004BCF4
		public override object Evaluate(BaseIterator iter)
		{
			XsltContext xsltContext = iter.NamespaceManager as XsltContext;
			if (xsltContext == null)
			{
				throw new XPathException(string.Format("XSLT context is required to resolve variable. Current namespace manager in current node-set '{0}' is '{1}'", iter.GetType(), (iter.NamespaceManager == null) ? null : iter.NamespaceManager.GetType()));
			}
			IXsltContextVariable xsltContextVariable;
			if (this.resolvedName)
			{
				xsltContextVariable = xsltContext.ResolveVariable(this._name);
			}
			else
			{
				xsltContextVariable = xsltContext.ResolveVariable(new XmlQualifiedName(this._name.Name, this._name.Namespace));
			}
			if (xsltContextVariable == null)
			{
				throw new XPathException("variable " + this._name.ToString() + " not found");
			}
			object obj = xsltContextVariable.Evaluate(xsltContext);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				return (!(xpathNodeIterator is BaseIterator)) ? new WrapperIterator(xpathNodeIterator, iter.NamespaceManager) : xpathNodeIterator;
			}
			return obj;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		internal override bool Peer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000705 RID: 1797
		protected XmlQualifiedName _name;

		// Token: 0x04000706 RID: 1798
		protected bool resolvedName;
	}
}
