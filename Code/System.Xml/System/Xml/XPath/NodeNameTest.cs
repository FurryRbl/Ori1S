using System;
using System.Xml.Xsl;

namespace System.Xml.XPath
{
	// Token: 0x02000188 RID: 392
	internal class NodeNameTest : NodeTest
	{
		// Token: 0x0600108F RID: 4239 RVA: 0x0004D5FC File Offset: 0x0004B7FC
		public NodeNameTest(Axes axis, XmlQualifiedName name, IStaticXsltContext ctx) : base(axis)
		{
			if (ctx != null)
			{
				name = ctx.LookupQName(name.ToString());
				this.resolvedName = true;
			}
			this._name = name;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0004D628 File Offset: 0x0004B828
		public NodeNameTest(Axes axis, XmlQualifiedName name, bool resolvedName) : base(axis)
		{
			this._name = name;
			this.resolvedName = resolvedName;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004D640 File Offset: 0x0004B840
		internal NodeNameTest(NodeNameTest source, Axes axis) : base(axis)
		{
			this._name = source._name;
			this.resolvedName = source.resolvedName;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0004D664 File Offset: 0x0004B864
		public override string ToString()
		{
			return this._axis.ToString() + "::" + this._name.ToString();
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0004D694 File Offset: 0x0004B894
		public XmlQualifiedName Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0004D69C File Offset: 0x0004B89C
		public override bool Match(IXmlNamespaceResolver nsm, XPathNavigator nav)
		{
			if (nav.NodeType != this._axis.NodeType)
			{
				return false;
			}
			if (this._name.Name != string.Empty && this._name.Name != nav.LocalName)
			{
				return false;
			}
			string text = string.Empty;
			if (this._name.Namespace != string.Empty)
			{
				if (this.resolvedName)
				{
					text = this._name.Namespace;
				}
				else if (nsm != null)
				{
					text = nsm.LookupNamespace(this._name.Namespace);
				}
				if (text == null)
				{
					throw new XPathException("Invalid namespace prefix: " + this._name.Namespace);
				}
			}
			return text == nav.NamespaceURI;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0004D77C File Offset: 0x0004B97C
		public override void GetInfo(out string name, out string ns, out XPathNodeType nodetype, IXmlNamespaceResolver nsm)
		{
			nodetype = this._axis.NodeType;
			if (this._name.Name != string.Empty)
			{
				name = this._name.Name;
			}
			else
			{
				name = null;
			}
			ns = string.Empty;
			if (nsm != null && this._name.Namespace != string.Empty)
			{
				if (this.resolvedName)
				{
					ns = this._name.Namespace;
				}
				else
				{
					ns = nsm.LookupNamespace(this._name.Namespace);
				}
				if (ns == null)
				{
					throw new XPathException("Invalid namespace prefix: " + this._name.Namespace);
				}
			}
		}

		// Token: 0x040006FE RID: 1790
		protected XmlQualifiedName _name;

		// Token: 0x040006FF RID: 1791
		protected readonly bool resolvedName;
	}
}
