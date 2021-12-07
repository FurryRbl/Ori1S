using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Schema;
using System.Xml.Xsl;
using Mono.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a cursor model for navigating and editing XML data.</summary>
	// Token: 0x02000136 RID: 310
	public abstract class XPathNavigator : XPathItem, ICloneable, IXmlNamespaceResolver, IXPathNavigable
	{
		/// <summary>For a description of this member, see <see cref="M:System.Xml.XPath.XPathNavigator.Clone" />.</summary>
		/// <returns>An <see cref="T:System.Object" />.</returns>
		// Token: 0x06000E68 RID: 3688 RVA: 0x00047748 File Offset: 0x00045948
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEqualityComparer" /> used for equality comparison of <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</summary>
		/// <returns>An <see cref="T:System.Collections.IEqualityComparer" /> used for equality comparison of <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</returns>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00047750 File Offset: 0x00045950
		public static IEqualityComparer NavigatorComparer
		{
			get
			{
				return XPathNavigatorComparer.Instance;
			}
		}

		/// <summary>When overridden in a derived class, gets the base URI for the current node.</summary>
		/// <returns>The location from which the node was loaded, or <see cref="F:System.String.Empty" /> if there is no value.</returns>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000E6A RID: 3690
		public abstract string BaseURI { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XPath.XPathNavigator" /> can edit the underlying XML data.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> can edit the underlying XML data; otherwise false.</returns>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00047758 File Offset: 0x00045958
		public virtual bool CanEdit
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>Returns true if the current node has attributes; returns false if the current node has no attributes, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0004775C File Offset: 0x0004595C
		public virtual bool HasAttributes
		{
			get
			{
				if (!this.MoveToFirstAttribute())
				{
					return false;
				}
				this.MoveToParent();
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current node has any child nodes.</summary>
		/// <returns>Returns true if the current node has any child nodes; otherwise, false.</returns>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00047774 File Offset: 0x00045974
		public virtual bool HasChildren
		{
			get
			{
				if (!this.MoveToFirstChild())
				{
					return false;
				}
				this.MoveToParent();
				return true;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an empty element without an end element tag.</summary>
		/// <returns>Returns true if the current node is an empty element; otherwise, false.</returns>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000E6E RID: 3694
		public abstract bool IsEmptyElement { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node without any namespace prefix.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local name of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000E6F RID: 3695
		public abstract string LocalName { get; }

		/// <summary>When overridden in a derived class, gets the qualified name of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the qualified <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000E70 RID: 3696
		public abstract string Name { get; }

		/// <summary>When overridden in a derived class, gets the namespace URI of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI of the current node, or <see cref="F:System.String.Empty" /> if the current node has no namespace URI.</returns>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000E71 RID: 3697
		public abstract string NamespaceURI { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XmlNameTable" /> of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNameTable" /> object enabling you to get the atomized version of a <see cref="T:System.String" /> within the XML document.</returns>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000E72 RID: 3698
		public abstract XmlNameTable NameTable { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNodeType" /> of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XPath.XPathNodeType" /> values representing the current node.</returns>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000E73 RID: 3699
		public abstract XPathNodeType NodeType { get; }

		/// <summary>When overridden in a derived class, gets the namespace prefix associated with the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix associated with the current node.</returns>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000E74 RID: 3700
		public abstract string Prefix { get; }

		/// <summary>Gets the xml:lang scope for the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the xml:lang scope, or <see cref="F:System.String.Empty" /> if the current node has no xml:lang scope value to return.</returns>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0004778C File Offset: 0x0004598C
		public virtual string XmlLang
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				XPathNodeType nodeType = xpathNavigator.NodeType;
				if (nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
				{
					xpathNavigator.MoveToParent();
				}
				while (!xpathNavigator.MoveToAttribute("lang", "http://www.w3.org/XML/1998/namespace"))
				{
					if (!xpathNavigator.MoveToParent())
					{
						return string.Empty;
					}
				}
				return xpathNavigator.Value;
			}
		}

		/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x06000E76 RID: 3702
		public abstract XPathNavigator Clone();

		/// <summary>Compares the position of the current <see cref="T:System.Xml.XPath.XPathNavigator" /> with the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeOrder" /> value representing the comparative position of the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</returns>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare against.</param>
		// Token: 0x06000E77 RID: 3703 RVA: 0x000477F4 File Offset: 0x000459F4
		public virtual XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			if (this.IsSamePosition(nav))
			{
				return XmlNodeOrder.Same;
			}
			if (this.IsDescendant(nav))
			{
				return XmlNodeOrder.Before;
			}
			if (nav.IsDescendant(this))
			{
				return XmlNodeOrder.After;
			}
			XPathNavigator xpathNavigator = this.Clone();
			XPathNavigator xpathNavigator2 = nav.Clone();
			xpathNavigator.MoveToRoot();
			xpathNavigator2.MoveToRoot();
			if (!xpathNavigator.IsSamePosition(xpathNavigator2))
			{
				return XmlNodeOrder.Unknown;
			}
			xpathNavigator.MoveTo(this);
			xpathNavigator2.MoveTo(nav);
			int num = 0;
			while (xpathNavigator.MoveToParent())
			{
				num++;
			}
			xpathNavigator.MoveTo(this);
			int num2 = 0;
			while (xpathNavigator2.MoveToParent())
			{
				num2++;
			}
			xpathNavigator2.MoveTo(nav);
			int i;
			for (i = num; i > num2; i--)
			{
				xpathNavigator.MoveToParent();
			}
			for (int j = num2; j > i; j--)
			{
				xpathNavigator2.MoveToParent();
			}
			while (!xpathNavigator.IsSamePosition(xpathNavigator2))
			{
				xpathNavigator.MoveToParent();
				xpathNavigator2.MoveToParent();
				i--;
			}
			xpathNavigator.MoveTo(this);
			for (int k = num; k > i + 1; k--)
			{
				xpathNavigator.MoveToParent();
			}
			xpathNavigator2.MoveTo(nav);
			for (int l = num2; l > i + 1; l--)
			{
				xpathNavigator2.MoveToParent();
			}
			if (xpathNavigator.NodeType == XPathNodeType.Namespace)
			{
				if (xpathNavigator2.NodeType != XPathNodeType.Namespace)
				{
					return XmlNodeOrder.Before;
				}
				while (xpathNavigator.MoveToNextNamespace())
				{
					if (xpathNavigator.IsSamePosition(xpathNavigator2))
					{
						return XmlNodeOrder.Before;
					}
				}
				return XmlNodeOrder.After;
			}
			else
			{
				if (xpathNavigator2.NodeType == XPathNodeType.Namespace)
				{
					return XmlNodeOrder.After;
				}
				if (xpathNavigator.NodeType != XPathNodeType.Attribute)
				{
					while (xpathNavigator.MoveToNext())
					{
						if (xpathNavigator.IsSamePosition(xpathNavigator2))
						{
							return XmlNodeOrder.Before;
						}
					}
					return XmlNodeOrder.After;
				}
				if (xpathNavigator2.NodeType != XPathNodeType.Attribute)
				{
					return XmlNodeOrder.Before;
				}
				while (xpathNavigator.MoveToNextAttribute())
				{
					if (xpathNavigator.IsSamePosition(xpathNavigator2))
					{
						return XmlNodeOrder.Before;
					}
				}
				return XmlNodeOrder.After;
			}
		}

		/// <summary>Compiles a string representing an XPath expression and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object representing the XPath expression.</returns>
		/// <param name="xpath">A string representing an XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="xpath" /> parameter contains an XPath expression that is not valid.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E78 RID: 3704 RVA: 0x000479F0 File Offset: 0x00045BF0
		public virtual XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual XPathExpression Compile(string xpath, IStaticXsltContext ctx)
		{
			return XPathExpression.Compile(xpath, null, ctx);
		}

		/// <summary>Evaluates the specified XPath expression and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E7A RID: 3706 RVA: 0x00047A04 File Offset: 0x00045C04
		public virtual object Evaluate(string xpath)
		{
			return this.Evaluate(this.Compile(xpath));
		}

		/// <summary>Evaluates the <see cref="T:System.Xml.XPath.XPathExpression" /> and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E7B RID: 3707 RVA: 0x00047A14 File Offset: 0x00045C14
		public virtual object Evaluate(XPathExpression expr)
		{
			return this.Evaluate(expr, null);
		}

		/// <summary>Uses the supplied context to evaluate the <see cref="T:System.Xml.XPath.XPathExpression" />, and returns the typed result.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <param name="context">An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set that the evaluation is to be performed on.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E7C RID: 3708 RVA: 0x00047A20 File Offset: 0x00045C20
		public virtual object Evaluate(XPathExpression expr, XPathNodeIterator context)
		{
			return this.Evaluate(expr, context, null);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00047A2C File Offset: 0x00045C2C
		private BaseIterator ToBaseIterator(XPathNodeIterator iter, IXmlNamespaceResolver ctx)
		{
			BaseIterator baseIterator = iter as BaseIterator;
			if (baseIterator == null)
			{
				baseIterator = new WrapperIterator(iter, ctx);
			}
			return baseIterator;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00047A50 File Offset: 0x00045C50
		private object Evaluate(XPathExpression expr, XPathNodeIterator context, IXmlNamespaceResolver ctx)
		{
			CompiledExpression compiledExpression = (CompiledExpression)expr;
			if (ctx == null)
			{
				ctx = compiledExpression.NamespaceManager;
			}
			if (context == null)
			{
				context = new NullIterator(this, ctx);
			}
			BaseIterator baseIterator = this.ToBaseIterator(context, ctx);
			baseIterator.NamespaceManager = ctx;
			return compiledExpression.Evaluate(baseIterator);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00047A98 File Offset: 0x00045C98
		internal XPathNodeIterator EvaluateNodeSet(XPathExpression expr, XPathNodeIterator context, IXmlNamespaceResolver ctx)
		{
			CompiledExpression compiledExpression = (CompiledExpression)expr;
			if (ctx == null)
			{
				ctx = compiledExpression.NamespaceManager;
			}
			if (context == null)
			{
				context = new NullIterator(this, compiledExpression.NamespaceManager);
			}
			BaseIterator baseIterator = this.ToBaseIterator(context, ctx);
			baseIterator.NamespaceManager = ctx;
			return compiledExpression.EvaluateNodeSet(baseIterator);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00047AE8 File Offset: 0x00045CE8
		internal string EvaluateString(XPathExpression expr, XPathNodeIterator context, IXmlNamespaceResolver ctx)
		{
			CompiledExpression compiledExpression = (CompiledExpression)expr;
			if (ctx == null)
			{
				ctx = compiledExpression.NamespaceManager;
			}
			if (context == null)
			{
				context = new NullIterator(this, compiledExpression.NamespaceManager);
			}
			BaseIterator iter = this.ToBaseIterator(context, ctx);
			return compiledExpression.EvaluateString(iter);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00047B30 File Offset: 0x00045D30
		internal double EvaluateNumber(XPathExpression expr, XPathNodeIterator context, IXmlNamespaceResolver ctx)
		{
			CompiledExpression compiledExpression = (CompiledExpression)expr;
			if (ctx == null)
			{
				ctx = compiledExpression.NamespaceManager;
			}
			if (context == null)
			{
				context = new NullIterator(this, compiledExpression.NamespaceManager);
			}
			BaseIterator baseIterator = this.ToBaseIterator(context, ctx);
			baseIterator.NamespaceManager = ctx;
			return compiledExpression.EvaluateNumber(baseIterator);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00047B80 File Offset: 0x00045D80
		internal bool EvaluateBoolean(XPathExpression expr, XPathNodeIterator context, IXmlNamespaceResolver ctx)
		{
			CompiledExpression compiledExpression = (CompiledExpression)expr;
			if (ctx == null)
			{
				ctx = compiledExpression.NamespaceManager;
			}
			if (context == null)
			{
				context = new NullIterator(this, compiledExpression.NamespaceManager);
			}
			BaseIterator baseIterator = this.ToBaseIterator(context, ctx);
			baseIterator.NamespaceManager = ctx;
			return compiledExpression.EvaluateBoolean(baseIterator);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the specified attribute; <see cref="F:System.String.Empty" /> if a matching attribute is not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute.</param>
		// Token: 0x06000E83 RID: 3715 RVA: 0x00047BD0 File Offset: 0x00045DD0
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			if (!this.MoveToAttribute(localName, namespaceURI))
			{
				return string.Empty;
			}
			string value = this.Value;
			this.MoveToParent();
			return value;
		}

		/// <summary>Returns the value of the namespace node corresponding to the specified local name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the namespace node; <see cref="F:System.String.Empty" /> if a matching namespace node is not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		/// <param name="name">The local name of the namespace node.</param>
		// Token: 0x06000E84 RID: 3716 RVA: 0x00047C00 File Offset: 0x00045E00
		public virtual string GetNamespace(string name)
		{
			if (!this.MoveToNamespace(name))
			{
				return string.Empty;
			}
			string value = this.Value;
			this.MoveToParent();
			return value;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>Returns true if the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, false.</returns>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		// Token: 0x06000E85 RID: 3717 RVA: 0x00047C30 File Offset: 0x00045E30
		public virtual bool IsDescendant(XPathNavigator nav)
		{
			if (nav != null)
			{
				nav = nav.Clone();
				while (nav.MoveToParent())
				{
					if (this.IsSamePosition(nav))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>When overridden in a derived class, determines whether the current <see cref="T:System.Xml.XPath.XPathNavigator" /> is at the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>Returns true if the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects have the same position; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		// Token: 0x06000E86 RID: 3718
		public abstract bool IsSamePosition(XPathNavigator other);

		/// <summary>Determines whether the current node matches the specified XPath expression.</summary>
		/// <returns>Returns true if the current node matches the specified XPath expression; otherwise, false.</returns>
		/// <param name="xpath">The XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression cannot be evaluated.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E87 RID: 3719 RVA: 0x00047C60 File Offset: 0x00045E60
		public virtual bool Matches(string xpath)
		{
			return this.Matches(this.Compile(xpath));
		}

		/// <summary>Determines whether the current node matches the specified <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>Returns true if the current node matches the <see cref="T:System.Xml.XPath.XPathExpression" />; otherwise, false.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression cannot be evaluated.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E88 RID: 3720 RVA: 0x00047C70 File Offset: 0x00045E70
		public virtual bool Matches(XPathExpression expr)
		{
			Expression expression = ((CompiledExpression)expr).ExpressionNode;
			if (expression is ExprRoot)
			{
				return this.NodeType == XPathNodeType.Root;
			}
			NodeTest nodeTest = expression as NodeTest;
			if (nodeTest == null)
			{
				if (expression is ExprFilter)
				{
					do
					{
						expression = ((ExprFilter)expression).LeftHandSide;
					}
					while (expression is ExprFilter);
					if (expression is NodeTest && !((NodeTest)expression).Match(((CompiledExpression)expr).NamespaceManager, this))
					{
						return false;
					}
				}
				switch (expression.ReturnType)
				{
				case XPathResultType.NodeSet:
				case XPathResultType.Any:
				{
					XPathNodeType evaluatedNodeType = expression.EvaluatedNodeType;
					if (evaluatedNodeType == XPathNodeType.Attribute || evaluatedNodeType == XPathNodeType.Namespace)
					{
						if (this.NodeType != expression.EvaluatedNodeType)
						{
							return false;
						}
					}
					XPathNodeIterator xpathNodeIterator = this.Select(expr);
					while (xpathNodeIterator.MoveNext())
					{
						if (this.IsSamePosition(xpathNodeIterator.Current))
						{
							return true;
						}
					}
					XPathNavigator xpathNavigator = this.Clone();
					while (xpathNavigator.MoveToParent())
					{
						xpathNodeIterator = xpathNavigator.Select(expr);
						while (xpathNodeIterator.MoveNext())
						{
							if (this.IsSamePosition(xpathNodeIterator.Current))
							{
								return true;
							}
						}
					}
					return false;
				}
				}
				return false;
			}
			Axes axis = nodeTest.Axis.Axis;
			if (axis != Axes.Attribute && axis != Axes.Child)
			{
				throw new XPathException("Only child and attribute pattern are allowed for a pattern.");
			}
			return nodeTest.Match(((CompiledExpression)expr).NamespaceManager, this);
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the node that you want to move to. </param>
		// Token: 0x06000E89 RID: 3721
		public abstract bool MoveTo(XPathNavigator other);

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the attribute with the matching local name and namespace URI.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the attribute; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute; null for an empty namespace.</param>
		// Token: 0x06000E8A RID: 3722 RVA: 0x00047E0C File Offset: 0x0004600C
		public virtual bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (this.MoveToFirstAttribute())
			{
				while (!(this.LocalName == localName) || !(this.NamespaceURI == namespaceURI))
				{
					if (!this.MoveToNextAttribute())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the namespace node with the specified namespace prefix.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the specified namespace; false if a matching namespace node was not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="name">The namespace prefix of the namespace node.</param>
		// Token: 0x06000E8B RID: 3723 RVA: 0x00047E5C File Offset: 0x0004605C
		public virtual bool MoveToNamespace(string name)
		{
			if (this.MoveToFirstNamespace())
			{
				while (!(this.LocalName == name))
				{
					if (!this.MoveToNextNamespace())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first sibling node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first sibling node of the current node; false if there is no first sibling, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If the <see cref="T:System.Xml.XPath.XPathNavigator" /> is already positioned on the first sibling, <see cref="T:System.Xml.XPath.XPathNavigator" /> will return true and will not move its position.If <see cref="T:System.Xml.XPath.XPathNavigator.MoveToFirst" /> returns false because there is no first sibling, or if <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E8C RID: 3724 RVA: 0x00047E9C File Offset: 0x0004609C
		public virtual bool MoveToFirst()
		{
			return this.MoveToFirstImpl();
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the root node that the current node belongs to.</summary>
		// Token: 0x06000E8D RID: 3725 RVA: 0x00047EA4 File Offset: 0x000460A4
		public virtual void MoveToRoot()
		{
			while (this.MoveToParent())
			{
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00047EB8 File Offset: 0x000460B8
		internal bool MoveToFirstImpl()
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				return false;
			}
			if (!this.MoveToParent())
			{
				return false;
			}
			this.MoveToFirstChild();
			return true;
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first attribute of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first attribute of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E8F RID: 3727
		public abstract bool MoveToFirstAttribute();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first child node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first child node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E90 RID: 3728
		public abstract bool MoveToFirstChild();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to first namespace node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E91 RID: 3729 RVA: 0x00047EF8 File Offset: 0x000460F8
		public bool MoveToFirstNamespace()
		{
			return this.MoveToFirstNamespace(XPathNamespaceScope.All);
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first namespace node that matches the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		// Token: 0x06000E92 RID: 3730
		public abstract bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>When overridden in a derived class, moves to the node that has an attribute of type ID whose value matches the specified <see cref="T:System.String" />.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving; otherwise, false. If false, the position of the navigator is unchanged.</returns>
		/// <param name="id">A <see cref="T:System.String" /> representing the ID value of the node to which you want to move.</param>
		// Token: 0x06000E93 RID: 3731
		public abstract bool MoveToId(string id);

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E94 RID: 3732
		public abstract bool MoveToNext();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next attribute.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next attribute; false if there are no more attributes. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E95 RID: 3733
		public abstract bool MoveToNextAttribute();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E96 RID: 3734 RVA: 0x00047F04 File Offset: 0x00046104
		public bool MoveToNextNamespace()
		{
			return this.MoveToNextNamespace(XPathNamespaceScope.All);
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node matching the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		// Token: 0x06000E97 RID: 3735
		public abstract bool MoveToNextNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the parent node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the parent node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E98 RID: 3736
		public abstract bool MoveToParent();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the previous sibling node of the current node.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the previous sibling node; otherwise, false if there is no previous sibling node or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06000E99 RID: 3737
		public abstract bool MoveToPrevious();

		/// <summary>Selects a node set, using the specified XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> pointing to the selected node set.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E9A RID: 3738 RVA: 0x00047F10 File Offset: 0x00046110
		public virtual XPathNodeIterator Select(string xpath)
		{
			return this.Select(this.Compile(xpath));
		}

		/// <summary>Selects a node set using the specified <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set.</returns>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath query.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000E9B RID: 3739 RVA: 0x00047F20 File Offset: 0x00046120
		public virtual XPathNodeIterator Select(XPathExpression expr)
		{
			return this.Select(expr, null);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00047F2C File Offset: 0x0004612C
		internal XPathNodeIterator Select(XPathExpression expr, IXmlNamespaceResolver ctx)
		{
			CompiledExpression compiledExpression = (CompiledExpression)expr;
			if (ctx == null)
			{
				ctx = compiledExpression.NamespaceManager;
			}
			BaseIterator iter = new NullIterator(this, ctx);
			return compiledExpression.EvaluateNodeSet(iter);
		}

		/// <summary>Selects all the ancestor nodes of the current node that have a matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes. The returned nodes are in reverse document order.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the ancestor nodes.</param>
		/// <param name="matchSelf">To include the context node in the selection, true; otherwise, false.</param>
		// Token: 0x06000E9D RID: 3741 RVA: 0x00047F60 File Offset: 0x00046160
		public virtual XPathNodeIterator SelectAncestors(XPathNodeType type, bool matchSelf)
		{
			Axes axis = (!matchSelf) ? Axes.Ancestor : Axes.AncestorOrSelf;
			return this.SelectTest(new NodeTypeTest(axis, type));
		}

		/// <summary>Selects all the ancestor nodes of the current node that have the specified local name and namespace URI.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes. The returned nodes are in reverse document order.</returns>
		/// <param name="name">The local name of the ancestor nodes.</param>
		/// <param name="namespaceURI">The namespace URI of the ancestor nodes.</param>
		/// <param name="matchSelf">To include the context node in the selection, true; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x06000E9E RID: 3742 RVA: 0x00047F88 File Offset: 0x00046188
		public virtual XPathNodeIterator SelectAncestors(string name, string namespaceURI, bool matchSelf)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			Axes axis = (!matchSelf) ? Axes.Ancestor : Axes.AncestorOrSelf;
			XmlQualifiedName name2 = new XmlQualifiedName(name, namespaceURI);
			return this.SelectTest(new NodeNameTest(axis, name2, true));
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00047FDC File Offset: 0x000461DC
		private static IEnumerable EnumerateChildren(XPathNavigator n, XPathNodeType type)
		{
			if (!n.MoveToFirstChild())
			{
				yield break;
			}
			n.MoveToParent();
			XPathNavigator nav = n.Clone();
			nav.MoveToFirstChild();
			XPathNavigator nav2 = null;
			do
			{
				if (type == XPathNodeType.All || nav.NodeType == type)
				{
					if (nav2 == null)
					{
						nav2 = nav.Clone();
					}
					else
					{
						nav2.MoveTo(nav);
					}
					yield return nav2;
				}
			}
			while (nav.MoveToNext());
			yield break;
		}

		/// <summary>Selects all the child nodes of the current node that have the matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child nodes.</param>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00048014 File Offset: 0x00046214
		public virtual XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new WrapperIterator(new XPathNavigator.EnumerableIterator(XPathNavigator.EnumerateChildren(this, type), 0), null);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0004802C File Offset: 0x0004622C
		private static IEnumerable EnumerateChildren(XPathNavigator n, string name, string ns)
		{
			if (!n.MoveToFirstChild())
			{
				yield break;
			}
			n.MoveToParent();
			XPathNavigator nav = n.Clone();
			nav.MoveToFirstChild();
			XPathNavigator nav2 = nav.Clone();
			do
			{
				if ((name == string.Empty || nav.LocalName == name) && (ns == string.Empty || nav.NamespaceURI == ns))
				{
					nav2.MoveTo(nav);
					yield return nav2;
				}
			}
			while (nav.MoveToNext());
			yield break;
		}

		/// <summary>Selects all the child nodes of the current node that have the local name and namespace URI specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="name">The local name of the child nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the child nodes. </param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00048074 File Offset: 0x00046274
		public virtual XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			return new WrapperIterator(new XPathNavigator.EnumerableIterator(XPathNavigator.EnumerateChildren(this, name, namespaceURI), 0), null);
		}

		/// <summary>Selects all the descendant nodes of the current node that have a matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the descendant nodes.</param>
		/// <param name="matchSelf">true to include the context node in the selection; otherwise, false.</param>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x000480B8 File Offset: 0x000462B8
		public virtual XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			Axes axis = (!matchSelf) ? Axes.Descendant : Axes.DescendantOrSelf;
			return this.SelectTest(new NodeTypeTest(axis, type));
		}

		/// <summary>Selects all the descendant nodes of the current node with the local name and namespace URI specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <param name="name">The local name of the descendant nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the descendant nodes. </param>
		/// <param name="matchSelf">true to include the context node in the selection; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">null cannot be passed as a parameter.</exception>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000480E0 File Offset: 0x000462E0
		public virtual XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			Axes axis = (!matchSelf) ? Axes.Descendant : Axes.DescendantOrSelf;
			XmlQualifiedName name2 = new XmlQualifiedName(name, namespaceURI);
			return this.SelectTest(new NodeNameTest(axis, name2, true));
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00048134 File Offset: 0x00046334
		internal XPathNodeIterator SelectTest(NodeTest test)
		{
			return test.EvaluateNodeSet(new NullIterator(this));
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>A string that contains the text value of the current node.</returns>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00048144 File Offset: 0x00046344
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Verifies that the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> conforms to the XML Schema definition language (XSD) schema provided.</summary>
		/// <returns>true if no schema validation errors occurred; otherwise, false.</returns>
		/// <param name="schemas">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> containing the schemas used to validate the XML data contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that receives information about schema validation warnings and errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">A schema validation error occurred, and no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> was specified to handle validation errors.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on a node that is not an element, attribute, or the root node or there is not type information to perform validation.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="M:System.Xml.XPath.XPathNavigator.CheckValidity(System.Xml.Schema.XmlSchemaSet,System.Xml.Schema.ValidationEventHandler)" /> method was called with an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> parameter when the <see cref="T:System.Xml.XPath.XPathNavigator" /> was not positioned on the root node of the XML data.</exception>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0004814C File Offset: 0x0004634C
		public virtual bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler handler)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.NameTable = this.NameTable;
			xmlReaderSettings.SetSchemas(schemas);
			xmlReaderSettings.ValidationEventHandler += handler;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			try
			{
				XmlReader xmlReader = XmlReader.Create(this.ReadSubtree(), xmlReaderSettings);
				while (!xmlReader.EOF)
				{
					xmlReader.Read();
				}
			}
			catch (XmlSchemaValidationException)
			{
				return false;
			}
			return true;
		}

		/// <summary>Returns a copy of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> copy of this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x000481D8 File Offset: 0x000463D8
		public virtual XPathNavigator CreateNavigator()
		{
			return this.Clone();
		}

		/// <summary>Evaluates the specified XPath expression and returns the typed result, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression.</summary>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x000481E0 File Offset: 0x000463E0
		public virtual object Evaluate(string xpath, IXmlNamespaceResolver nsResolver)
		{
			return this.Evaluate(this.Compile(xpath), null, nsResolver);
		}

		/// <summary>Returns the in-scope namespaces of the current node.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IDictionary`2" /> collection of namespace names keyed by prefix.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value specifying the namespaces to return.</param>
		// Token: 0x06000EAA RID: 3754 RVA: 0x000481F4 File Offset: 0x000463F4
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			XPathNamespaceScope namespaceScope = (scope != XmlNamespaceScope.Local) ? ((scope != XmlNamespaceScope.ExcludeXml) ? XPathNamespaceScope.All : XPathNamespaceScope.ExcludeXml) : XPathNamespaceScope.Local;
			XPathNavigator xpathNavigator = this.Clone();
			if (xpathNavigator.NodeType != XPathNodeType.Element)
			{
				xpathNavigator.MoveToParent();
			}
			if (!xpathNavigator.MoveToFirstNamespace(namespaceScope))
			{
				return dictionary;
			}
			do
			{
				dictionary.Add(xpathNavigator.Name, xpathNavigator.Value);
			}
			while (xpathNavigator.MoveToNextNamespace(namespaceScope));
			return dictionary;
		}

		/// <summary>Gets the namespace URI for the specified prefix.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI assigned to the namespace prefix specified; null if no namespace URI is assigned to the prefix specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06000EAB RID: 3755 RVA: 0x0004826C File Offset: 0x0004646C
		public virtual string LookupNamespace(string prefix)
		{
			XPathNavigator xpathNavigator = this.Clone();
			if (xpathNavigator.NodeType != XPathNodeType.Element)
			{
				xpathNavigator.MoveToParent();
			}
			if (xpathNavigator.MoveToNamespace(prefix))
			{
				return xpathNavigator.Value;
			}
			return null;
		}

		/// <summary>Gets the prefix declared for the specified namespace URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix assigned to the namespace URI specified; otherwise, <see cref="F:System.String.Empty" /> if no prefix is assigned to the namespace URI specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		/// <param name="namespaceURI">The namespace URI to resolve for the prefix.</param>
		// Token: 0x06000EAC RID: 3756 RVA: 0x000482A8 File Offset: 0x000464A8
		public virtual string LookupPrefix(string namespaceUri)
		{
			XPathNavigator xpathNavigator = this.Clone();
			if (xpathNavigator.NodeType != XPathNodeType.Element)
			{
				xpathNavigator.MoveToParent();
			}
			if (!xpathNavigator.MoveToFirstNamespace())
			{
				return null;
			}
			while (!(xpathNavigator.Value == namespaceUri))
			{
				if (!xpathNavigator.MoveToNextNamespace())
				{
					return null;
				}
			}
			return xpathNavigator.Name;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00048300 File Offset: 0x00046500
		private bool MoveTo(XPathNodeIterator iter)
		{
			if (iter.MoveNext())
			{
				this.MoveTo(iter.Current);
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child node to move to.</param>
		// Token: 0x06000EAE RID: 3758 RVA: 0x00048320 File Offset: 0x00046520
		public virtual bool MoveToChild(XPathNodeType type)
		{
			return this.MoveTo(this.SelectChildren(type));
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node with the local name and namespace URI specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the child node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the child node to move to.</param>
		// Token: 0x06000EAF RID: 3759 RVA: 0x00048330 File Offset: 0x00046530
		public virtual bool MoveToChild(string localName, string namespaceURI)
		{
			return this.MoveTo(this.SelectChildren(localName, namespaceURI));
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node with the local name and namespace URI specified.</summary>
		/// <returns>Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; false if there are no more siblings, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="localName">The local name of the next sibling node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the next sibling node to move to.</param>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00048340 File Offset: 0x00046540
		public virtual bool MoveToNext(string localName, string namespaceURI)
		{
			XPathNavigator xpathNavigator = this.Clone();
			while (xpathNavigator.MoveToNext())
			{
				if (xpathNavigator.LocalName == localName && xpathNavigator.NamespaceURI == namespaceURI)
				{
					this.MoveTo(xpathNavigator);
					return true;
				}
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node that matches the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the sibling node to move to.</param>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00048394 File Offset: 0x00046594
		public virtual bool MoveToNext(XPathNodeType type)
		{
			XPathNavigator xpathNavigator = this.Clone();
			while (xpathNavigator.MoveToNext())
			{
				if (type == XPathNodeType.All || xpathNavigator.NodeType == type)
				{
					this.MoveTo(xpathNavigator);
					return true;
				}
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the element with the local name and namespace URI specified in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		// Token: 0x06000EB2 RID: 3762 RVA: 0x000483D8 File Offset: 0x000465D8
		public virtual bool MoveToFollowing(string localName, string namespaceURI)
		{
			return this.MoveToFollowing(localName, namespaceURI, null);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the element with the local name and namespace URI specified, to the boundary specified, in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x000483E4 File Offset: 0x000465E4
		public virtual bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			localName = this.NameTable.Get(localName);
			if (localName == null)
			{
				return false;
			}
			namespaceURI = this.NameTable.Get(namespaceURI);
			if (namespaceURI == null)
			{
				return false;
			}
			XPathNavigator xpathNavigator = this.Clone();
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				xpathNavigator.MoveToParent();
			}
			for (;;)
			{
				if (!xpathNavigator.MoveToFirstChild())
				{
					while (!xpathNavigator.MoveToNext())
					{
						if (!xpathNavigator.MoveToParent())
						{
							return false;
						}
					}
				}
				if (end != null && end.IsSamePosition(xpathNavigator))
				{
					return false;
				}
				if (object.ReferenceEquals(localName, xpathNavigator.LocalName) && object.ReferenceEquals(namespaceURI, xpathNavigator.NamespaceURI))
				{
					goto Block_12;
				}
			}
			return false;
			Block_12:
			this.MoveTo(xpathNavigator);
			return true;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the following element of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the element. The <see cref="T:System.Xml.XPath.XPathNodeType" /> cannot be <see cref="F:System.Xml.XPath.XPathNodeType.Attribute" /> or <see cref="F:System.Xml.XPath.XPathNodeType.Namespace" />.</param>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x000484E4 File Offset: 0x000466E4
		public virtual bool MoveToFollowing(XPathNodeType type)
		{
			return this.MoveToFollowing(type, null);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the following element of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified, to the boundary specified, in document order.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise false.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the element. The <see cref="T:System.Xml.XPath.XPathNodeType" /> cannot be <see cref="F:System.Xml.XPath.XPathNodeType.Attribute" /> or <see cref="F:System.Xml.XPath.XPathNodeType.Namespace" />.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x000484F0 File Offset: 0x000466F0
		public virtual bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			if (type == XPathNodeType.Root)
			{
				return false;
			}
			XPathNavigator xpathNavigator = this.Clone();
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				xpathNavigator.MoveToParent();
			}
			for (;;)
			{
				if (!xpathNavigator.MoveToFirstChild())
				{
					while (!xpathNavigator.MoveToNext())
					{
						if (!xpathNavigator.MoveToParent())
						{
							return false;
						}
					}
				}
				if (end != null && end.IsSamePosition(xpathNavigator))
				{
					return false;
				}
				if (type == XPathNodeType.All || xpathNavigator.NodeType == type)
				{
					goto IL_8F;
				}
			}
			return false;
			IL_8F:
			this.MoveTo(xpathNavigator);
			return true;
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlReader" /> object that contains the current node and its child nodes.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object that contains the current node and its child nodes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node or the root node.</exception>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0004859C File Offset: 0x0004679C
		public virtual XmlReader ReadSubtree()
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType != XPathNodeType.Root && nodeType != XPathNodeType.Element)
			{
				throw new InvalidOperationException(string.Format("NodeType {0} is not supported to read as a subtree of an XPathNavigator.", this.NodeType));
			}
			return new XPathNavigatorReader(this);
		}

		/// <summary>Selects a node set using the specified XPath expression with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000EB7 RID: 3767 RVA: 0x000485E4 File Offset: 0x000467E4
		public virtual XPathNodeIterator Select(string xpath, IXmlNamespaceResolver nsResolver)
		{
			return this.Select(this.Compile(xpath), nsResolver);
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified XPath query.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise, null if there are no query results.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x000485F4 File Offset: 0x000467F4
		public virtual XPathNavigator SelectSingleNode(string xpath)
		{
			return this.SelectSingleNode(xpath, null);
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object using the specified XPath query with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise null if there are no query results.</returns>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath query.</param>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00048600 File Offset: 0x00046800
		public virtual XPathNavigator SelectSingleNode(string xpath, IXmlNamespaceResolver nsResolver)
		{
			XPathExpression xpathExpression = this.Compile(xpath);
			xpathExpression.SetContext(nsResolver);
			return this.SelectSingleNode(xpathExpression);
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <see cref="T:System.Xml.XPath.XPathExpression" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise null if there are no query results.</returns>
		/// <param name="expression">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath query.</param>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x06000EBA RID: 3770 RVA: 0x00048624 File Offset: 0x00046824
		public virtual XPathNavigator SelectSingleNode(XPathExpression expression)
		{
			XPathNodeIterator xpathNodeIterator = this.Select(expression);
			if (xpathNodeIterator.MoveNext())
			{
				return xpathNodeIterator.Current;
			}
			return null;
		}

		/// <summary>Gets the current node's value as the <see cref="T:System.Type" /> specified, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the current node as the <see cref="T:System.Type" /> requested.</returns>
		/// <param name="returnType">The <see cref="T:System.Type" /> to return the current node's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The current node's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		// Token: 0x06000EBB RID: 3771 RVA: 0x0004864C File Offset: 0x0004684C
		public override object ValueAs(Type type, IXmlNamespaceResolver nsResolver)
		{
			return new XmlAtomicValue(this.Value, XmlSchemaSimpleType.XsString).ValueAs(type, nsResolver);
		}

		/// <summary>Streams the current node and its child nodes to the <see cref="T:System.Xml.XmlWriter" /> object specified.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> object to stream to.</param>
		// Token: 0x06000EBC RID: 3772 RVA: 0x00048668 File Offset: 0x00046868
		public virtual void WriteSubtree(XmlWriter writer)
		{
			writer.WriteNode(this, false);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00048674 File Offset: 0x00046874
		private static string EscapeString(string value, bool attr)
		{
			char[] anyOf = (!attr) ? XPathNavigator.escape_text_chars : XPathNavigator.escape_attr_chars;
			if (value.IndexOfAny(anyOf) < 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder(value, value.Length + 10);
			if (attr)
			{
				stringBuilder.Replace("\"", "&quot;");
			}
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace(">", "&gt;");
			if (attr)
			{
				stringBuilder.Replace("\r\n", "&#10;");
				stringBuilder.Replace("\r", "&#10;");
				stringBuilder.Replace("\n", "&#10;");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Gets or sets the markup representing the child nodes of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the markup of the child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XPath.XPathNavigator.InnerXml" /> property cannot be set.</exception>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00048730 File Offset: 0x00046930
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x00048818 File Offset: 0x00046A18
		public virtual string InnerXml
		{
			get
			{
				switch (this.NodeType)
				{
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
					return XPathNavigator.EscapeString(this.Value, true);
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					return string.Empty;
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return this.Value;
				}
				XmlReader xmlReader = this.ReadSubtree();
				xmlReader.Read();
				int num = xmlReader.Depth;
				if (this.NodeType != XPathNodeType.Root)
				{
					xmlReader.Read();
				}
				else
				{
					num = -1;
				}
				StringWriter stringWriter = new StringWriter();
				XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					Indent = true,
					ConformanceLevel = ConformanceLevel.Fragment,
					OmitXmlDeclaration = true
				});
				while (!xmlReader.EOF && xmlReader.Depth > num)
				{
					xmlWriter.WriteNode(xmlReader, false);
				}
				return stringWriter.ToString();
			}
			set
			{
				this.DeleteChildren();
				if (this.NodeType == XPathNodeType.Attribute)
				{
					this.SetValue(value);
					return;
				}
				this.AppendChild(value);
			}
		}

		/// <summary>Gets a value indicating if the current node represents an XPath node.</summary>
		/// <returns>Always returns true.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00048848 File Offset: 0x00046A48
		public sealed override bool IsNode
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the markup representing the opening and closing tags of the current node and its child nodes.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the markup representing the opening and closing tags of the current node and its child nodes.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0004884C File Offset: 0x00046A4C
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x000489B8 File Offset: 0x00046BB8
		public virtual string OuterXml
		{
			get
			{
				switch (this.NodeType)
				{
				case XPathNodeType.Attribute:
					return string.Concat(new string[]
					{
						this.Prefix,
						(this.Prefix.Length <= 0) ? string.Empty : ":",
						this.LocalName,
						"=\"",
						XPathNavigator.EscapeString(this.Value, true),
						"\""
					});
				case XPathNodeType.Namespace:
					return string.Concat(new string[]
					{
						"xmlns",
						(this.LocalName.Length <= 0) ? string.Empty : ":",
						this.LocalName,
						"=\"",
						XPathNavigator.EscapeString(this.Value, true),
						"\""
					});
				case XPathNodeType.Text:
					return XPathNavigator.EscapeString(this.Value, false);
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					return this.Value;
				default:
				{
					XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
					xmlWriterSettings.Indent = true;
					xmlWriterSettings.OmitXmlDeclaration = true;
					xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
					StringBuilder stringBuilder = new StringBuilder();
					using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
					{
						this.WriteSubtree(xmlWriter);
					}
					return stringBuilder.ToString();
				}
				}
			}
			set
			{
				switch (this.NodeType)
				{
				case XPathNodeType.Root:
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
					throw new XmlException("Setting OuterXml Root, Attribute and Namespace is not supported.");
				}
				this.DeleteSelf();
				this.AppendChild(value);
				this.MoveToFirstChild();
			}
		}

		/// <summary>Gets the schema information that has been assigned to the current node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object that contains the schema information for the current node.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00048A08 File Offset: 0x00046C08
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the current node as a boxed object of the most appropriate .NET Framework type.</summary>
		/// <returns>The current node as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00048A0C File Offset: 0x00046C0C
		public override object TypedValue
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				if (nodeType == XPathNodeType.Element || nodeType == XPathNodeType.Attribute)
				{
					if (this.XmlType != null)
					{
						XmlSchemaDatatype datatype = this.XmlType.Datatype;
						if (datatype != null)
						{
							return datatype.ParseValue(this.Value, this.NameTable, this);
						}
					}
				}
				return this.Value;
			}
		}

		/// <summary>Used by <see cref="T:System.Xml.XPath.XPathNavigator" /> implementations which provide a "virtualized" XML view over a store, to provide access to underlying objects.</summary>
		/// <returns>The default is null.</returns>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00048A74 File Offset: 0x00046C74
		public virtual object UnderlyingObject
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Boolean" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00048A78 File Offset: 0x00046C78
		public override bool ValueAsBoolean
		{
			get
			{
				return XQueryConvert.StringToBoolean(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00048A88 File Offset: 0x00046C88
		public override DateTime ValueAsDateTime
		{
			get
			{
				return XmlConvert.ToDateTime(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Double" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00048A98 File Offset: 0x00046C98
		public override double ValueAsDouble
		{
			get
			{
				return XQueryConvert.StringToDouble(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int32" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00048AA8 File Offset: 0x00046CA8
		public override int ValueAsInt
		{
			get
			{
				return XQueryConvert.StringToInt(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int64" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00048AB8 File Offset: 0x00046CB8
		public override long ValueAsLong
		{
			get
			{
				return XQueryConvert.StringToInteger(this.Value);
			}
		}

		/// <summary>Gets the .NET Framework <see cref="T:System.Type" /> of the current node.</summary>
		/// <returns>The .NET Framework <see cref="T:System.Type" /> of the current node. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00048AC8 File Offset: 0x00046CC8
		public override Type ValueType
		{
			get
			{
				return (this.SchemaInfo == null || this.SchemaInfo.SchemaType == null || this.SchemaInfo.SchemaType.Datatype == null) ? null : this.SchemaInfo.SchemaType.Datatype.ValueType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> information for the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object; default is null.</returns>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00048B20 File Offset: 0x00046D20
		public override XmlSchemaType XmlType
		{
			get
			{
				if (this.SchemaInfo != null)
				{
					return this.SchemaInfo.SchemaType;
				}
				return null;
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00048B3C File Offset: 0x00046D3C
		private XmlReader CreateFragmentReader(string fragment)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.NameTable);
			foreach (KeyValuePair<string, string> keyValuePair in this.GetNamespacesInScope(XmlNamespaceScope.All))
			{
				xmlNamespaceManager.AddNamespace(keyValuePair.Key, keyValuePair.Value);
			}
			return XmlReader.Create(new StringReader(fragment), xmlReaderSettings, new XmlParserContext(this.NameTable, xmlNamespaceManager, null, XmlSpace.None));
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create one or more new child nodes at the end of the list of child nodes of the current node. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create new child nodes at the end of the list of child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000ECE RID: 3790 RVA: 0x00048BE0 File Offset: 0x00046DE0
		public virtual XmlWriter AppendChild()
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the XML data string specified.</summary>
		/// <param name="newChild">The XML data string for the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML data string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML data string parameter is not well-formed.</exception>
		// Token: 0x06000ECF RID: 3791 RVA: 0x00048BE8 File Offset: 0x00046DE8
		public virtual void AppendChild(string xmlFragments)
		{
			this.AppendChild(this.CreateFragmentReader(xmlFragments));
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new child node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06000ED0 RID: 3792 RVA: 0x00048BF8 File Offset: 0x00046DF8
		public virtual void AppendChild(XmlReader reader)
		{
			XmlWriter xmlWriter = this.AppendChild();
			while (!reader.EOF)
			{
				xmlWriter.WriteNode(reader, false);
			}
			xmlWriter.Close();
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00048C2C File Offset: 0x00046E2C
		public virtual void AppendChild(XPathNavigator nav)
		{
			this.AppendChild(new XPathNavigatorReader(nav));
		}

		/// <summary>Creates a new child element node at the end of the list of child nodes of the current node using the namespace prefix, local name and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element node (if any).</param>
		/// <param name="localName">The local name of the new child element node (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element node (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element node. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x00048C3C File Offset: 0x00046E3C
		public virtual void AppendChildElement(string prefix, string name, string ns, string value)
		{
			XmlWriter xmlWriter = this.AppendChild();
			xmlWriter.WriteStartElement(prefix, name, ns);
			xmlWriter.WriteString(value);
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates an attribute node on the current element node using the namespace prefix, local name and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new attribute node (if any).</param>
		/// <param name="localName">The local name of the new attribute node which cannot <see cref="F:System.String.Empty" /> or null.</param>
		/// <param name="namespaceURI">The namespace URI for the new attribute node (if any).</param>
		/// <param name="value">The value of the new attribute node. If <see cref="F:System.String.Empty" /> or null are passed, an empty attribute node is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x00048C70 File Offset: 0x00046E70
		public virtual void CreateAttribute(string prefix, string localName, string namespaceURI, string value)
		{
			using (XmlWriter xmlWriter = this.CreateAttributes())
			{
				xmlWriter.WriteAttributeString(prefix, localName, namespaceURI, value);
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create new attributes on the current element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create new attributes on the current element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x00048CC0 File Offset: 0x00046EC0
		public virtual XmlWriter CreateAttributes()
		{
			throw new NotSupportedException();
		}

		/// <summary>Deletes the current node and its child nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on a node that cannot be deleted such as the root node or a namespace node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000ED5 RID: 3797 RVA: 0x00048CC8 File Offset: 0x00046EC8
		public virtual void DeleteSelf()
		{
			throw new NotSupportedException();
		}

		/// <summary>Deletes a range of sibling nodes from the current node to the node specified.</summary>
		/// <param name="lastSiblingToDelete">An <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the last sibling node in the range to delete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> specified is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.InvalidOperationException">The last node to delete specified is not a valid sibling node of the current node.</exception>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00048CD0 File Offset: 0x00046ED0
		public virtual void DeleteRange(XPathNavigator nav)
		{
			throw new NotSupportedException();
		}

		/// <summary>Replaces a range of sibling nodes from the current node to the node specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to specify the replacement range.</returns>
		/// <param name="lastSiblingToReplace">An <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the last sibling node in the range to replace.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> specified is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.InvalidOperationException">The last node to replace specified is not a valid sibling node of the current node.</exception>
		// Token: 0x06000ED7 RID: 3799 RVA: 0x00048CD8 File Offset: 0x00046ED8
		public virtual XmlWriter ReplaceRange(XPathNavigator nav)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node after the currently selected node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node after the currently selected node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00048CE0 File Offset: 0x00046EE0
		public virtual XmlWriter InsertAfter()
		{
			switch (this.NodeType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				throw new InvalidOperationException(string.Format("Insertion after {0} is not allowed.", this.NodeType));
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (xpathNavigator.MoveToNext())
			{
				return xpathNavigator.InsertBefore();
			}
			if (xpathNavigator.MoveToParent())
			{
				return xpathNavigator.AppendChild();
			}
			throw new InvalidOperationException("Could not move to parent to insert sibling node");
		}

		/// <summary>Creates a new sibling node after the currently selected node using the XML string specified.</summary>
		/// <param name="newSibling">The XML data string for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x00048D60 File Offset: 0x00046F60
		public virtual void InsertAfter(string xmlFragments)
		{
			this.InsertAfter(this.CreateFragmentReader(xmlFragments));
		}

		/// <summary>Creates a new sibling node after the currently selected node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06000EDA RID: 3802 RVA: 0x00048D70 File Offset: 0x00046F70
		public virtual void InsertAfter(XmlReader reader)
		{
			using (XmlWriter xmlWriter = this.InsertAfter())
			{
				xmlWriter.WriteNode(reader, false);
			}
		}

		/// <summary>Creates a new sibling node after the currently selected node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EDB RID: 3803 RVA: 0x00048DBC File Offset: 0x00046FBC
		public virtual void InsertAfter(XPathNavigator nav)
		{
			this.InsertAfter(new XPathNavigatorReader(nav));
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node before the currently selected node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node before the currently selected node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EDC RID: 3804 RVA: 0x00048DCC File Offset: 0x00046FCC
		public virtual XmlWriter InsertBefore()
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates a new sibling node before the currently selected node using the XML string specified.</summary>
		/// <param name="newSibling">The XML data string for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x06000EDD RID: 3805 RVA: 0x00048DD4 File Offset: 0x00046FD4
		public virtual void InsertBefore(string xmlFragments)
		{
			this.InsertBefore(this.CreateFragmentReader(xmlFragments));
		}

		/// <summary>Creates a new sibling node before the currently selected node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06000EDE RID: 3806 RVA: 0x00048DE4 File Offset: 0x00046FE4
		public virtual void InsertBefore(XmlReader reader)
		{
			using (XmlWriter xmlWriter = this.InsertBefore())
			{
				xmlWriter.WriteNode(reader, false);
			}
		}

		/// <summary>Creates a new sibling node before the currently selected node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EDF RID: 3807 RVA: 0x00048E30 File Offset: 0x00047030
		public virtual void InsertBefore(XPathNavigator nav)
		{
			this.InsertBefore(new XPathNavigatorReader(nav));
		}

		/// <summary>Creates a new sibling element after the current node using the namespace prefix, local name and namespace URI specified, with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EE0 RID: 3808 RVA: 0x00048E40 File Offset: 0x00047040
		public virtual void InsertElementAfter(string prefix, string localName, string namespaceURI, string value)
		{
			using (XmlWriter xmlWriter = this.InsertAfter())
			{
				xmlWriter.WriteElementString(prefix, localName, namespaceURI, value);
			}
		}

		/// <summary>Creates a new sibling element before the current node using the namespace prefix, local name, and namespace URI specified, with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x00048E90 File Offset: 0x00047090
		public virtual void InsertElementBefore(string prefix, string localName, string namespaceURI, string value)
		{
			using (XmlWriter xmlWriter = this.InsertBefore())
			{
				xmlWriter.WriteElementString(prefix, localName, namespaceURI, value);
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new child node at the beginning of the list of child nodes of the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new child node at the beginning of the list of child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EE2 RID: 3810 RVA: 0x00048EE0 File Offset: 0x000470E0
		public virtual XmlWriter PrependChild()
		{
			XPathNavigator xpathNavigator = this.Clone();
			if (xpathNavigator.MoveToFirstChild())
			{
				return xpathNavigator.InsertBefore();
			}
			return this.AppendChild();
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the XML string specified.</summary>
		/// <param name="newChild">The XML data string for the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x06000EE3 RID: 3811 RVA: 0x00048F0C File Offset: 0x0004710C
		public virtual void PrependChild(string xmlFragments)
		{
			this.PrependChild(this.CreateFragmentReader(xmlFragments));
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new child node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06000EE4 RID: 3812 RVA: 0x00048F1C File Offset: 0x0004711C
		public virtual void PrependChild(XmlReader reader)
		{
			using (XmlWriter xmlWriter = this.PrependChild())
			{
				xmlWriter.WriteNode(reader, false);
			}
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EE5 RID: 3813 RVA: 0x00048F68 File Offset: 0x00047168
		public virtual void PrependChild(XPathNavigator nav)
		{
			this.PrependChild(new XPathNavigatorReader(nav));
		}

		/// <summary>Creates a new child element at the beginning of the list of child nodes of the current node using the namespace prefix, local name, and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and null are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or null are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00048F78 File Offset: 0x00047178
		public virtual void PrependChildElement(string prefix, string localName, string namespaceURI, string value)
		{
			using (XmlWriter xmlWriter = this.PrependChild())
			{
				xmlWriter.WriteElementString(prefix, localName, namespaceURI, value);
			}
		}

		/// <summary>Replaces the current node with the content of the string specified.</summary>
		/// <param name="newNode">The XML data string for the new node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00048FC8 File Offset: 0x000471C8
		public virtual void ReplaceSelf(string xmlFragment)
		{
			this.ReplaceSelf(this.CreateFragmentReader(xmlFragment));
		}

		/// <summary>Replaces the current node with the contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newNode">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00048FD8 File Offset: 0x000471D8
		public virtual void ReplaceSelf(XmlReader reader)
		{
			throw new NotSupportedException();
		}

		/// <summary>Replaces the current node with the contents of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newNode">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the new node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is not well-formed.</exception>
		// Token: 0x06000EE9 RID: 3817 RVA: 0x00048FE0 File Offset: 0x000471E0
		public virtual void ReplaceSelf(XPathNavigator navigator)
		{
			this.ReplaceSelf(new XPathNavigatorReader(navigator));
		}

		/// <summary>Sets the typed value of the current node.</summary>
		/// <param name="typedValue">The new typed value of the node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support the type of the object specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value specified cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element or attribute node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EEA RID: 3818 RVA: 0x00048FF0 File Offset: 0x000471F0
		[MonoTODO]
		public virtual void SetTypedValue(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets the value of the current node.</summary>
		/// <param name="value">The new value of the node.</param>
		/// <exception cref="T:System.ArgumentNullException">The value parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on the root node, a namespace node, or the specified value is invalid.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06000EEB RID: 3819 RVA: 0x00048FF8 File Offset: 0x000471F8
		public virtual void SetValue(string value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00049000 File Offset: 0x00047200
		private void DeleteChildren()
		{
			switch (this.NodeType)
			{
			case XPathNodeType.Attribute:
				return;
			case XPathNodeType.Namespace:
				throw new InvalidOperationException("Removing namespace node content is not supported.");
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				this.DeleteSelf();
				return;
			default:
			{
				if (!this.HasChildren)
				{
					return;
				}
				XPathNavigator xpathNavigator = this.Clone();
				xpathNavigator.MoveToFirstChild();
				while (!xpathNavigator.IsSamePosition(this))
				{
					xpathNavigator.DeleteSelf();
				}
				return;
			}
			}
		}

		// Token: 0x04000685 RID: 1669
		private static readonly char[] escape_text_chars = new char[]
		{
			'&',
			'<',
			'>'
		};

		// Token: 0x04000686 RID: 1670
		private static readonly char[] escape_attr_chars = new char[]
		{
			'"',
			'&',
			'<',
			'>',
			'\r',
			'\n'
		};

		// Token: 0x02000137 RID: 311
		private class EnumerableIterator : XPathNodeIterator
		{
			// Token: 0x06000EED RID: 3821 RVA: 0x00049084 File Offset: 0x00047284
			public EnumerableIterator(IEnumerable source, int pos)
			{
				this.source = source;
				for (int i = 0; i < pos; i++)
				{
					this.MoveNext();
				}
			}

			// Token: 0x06000EEE RID: 3822 RVA: 0x000490B8 File Offset: 0x000472B8
			public override XPathNodeIterator Clone()
			{
				return new XPathNavigator.EnumerableIterator(this.source, this.pos);
			}

			// Token: 0x06000EEF RID: 3823 RVA: 0x000490CC File Offset: 0x000472CC
			public override bool MoveNext()
			{
				if (this.e == null)
				{
					this.e = this.source.GetEnumerator();
				}
				if (!this.e.MoveNext())
				{
					return false;
				}
				this.pos++;
				return true;
			}

			// Token: 0x17000434 RID: 1076
			// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00049118 File Offset: 0x00047318
			public override int CurrentPosition
			{
				get
				{
					return this.pos;
				}
			}

			// Token: 0x17000435 RID: 1077
			// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00049120 File Offset: 0x00047320
			public override XPathNavigator Current
			{
				get
				{
					return (this.pos != 0) ? ((XPathNavigator)this.e.Current) : null;
				}
			}

			// Token: 0x04000687 RID: 1671
			private IEnumerable source;

			// Token: 0x04000688 RID: 1672
			private IEnumerator e;

			// Token: 0x04000689 RID: 1673
			private int pos;
		}
	}
}
