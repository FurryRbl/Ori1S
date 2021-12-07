using System;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.Xsl;

namespace Mono.Xml.XPath
{
	// Token: 0x02000044 RID: 68
	internal class LocationPathPattern : Pattern
	{
		// Token: 0x06000299 RID: 665 RVA: 0x000136C8 File Offset: 0x000118C8
		public LocationPathPattern(NodeTest nodeTest)
		{
			this.nodeTest = nodeTest;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000136D8 File Offset: 0x000118D8
		public LocationPathPattern(ExprFilter filter)
		{
			this.filter = filter;
			while (!(filter.expr is NodeTest))
			{
				filter = (ExprFilter)filter.expr;
			}
			this.nodeTest = (NodeTest)filter.expr;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00013728 File Offset: 0x00011928
		internal void SetPreviousPattern(Pattern prev, bool isAncestor)
		{
			LocationPathPattern lastPathPattern = this.LastPathPattern;
			lastPathPattern.patternPrevious = (LocationPathPattern)prev;
			lastPathPattern.isAncestor = isAncestor;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00013750 File Offset: 0x00011950
		public override double DefaultPriority
		{
			get
			{
				if (this.patternPrevious != null || this.filter != null)
				{
					return 0.5;
				}
				NodeNameTest nodeNameTest = this.nodeTest as NodeNameTest;
				if (nodeNameTest == null)
				{
					return -0.5;
				}
				if (nodeNameTest.Name.Name == "*" || nodeNameTest.Name.Name.Length == 0)
				{
					return -0.25;
				}
				return 0.0;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000137DC File Offset: 0x000119DC
		public override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return this.nodeTest.EvaluatedNodeType;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000137EC File Offset: 0x000119EC
		public override bool Matches(XPathNavigator node, XsltContext ctx)
		{
			if (!this.nodeTest.Match(ctx, node))
			{
				return false;
			}
			if (this.nodeTest is NodeTypeTest && ((NodeTypeTest)this.nodeTest).type == XPathNodeType.All && (node.NodeType == XPathNodeType.Root || node.NodeType == XPathNodeType.Attribute))
			{
				return false;
			}
			if (this.filter == null && this.patternPrevious == null)
			{
				return true;
			}
			XPathNavigator navCache;
			if (this.patternPrevious != null)
			{
				navCache = ((XsltCompiledContext)ctx).GetNavCache(this, node);
				if (this.isAncestor)
				{
					while (navCache.MoveToParent())
					{
						if (this.patternPrevious.Matches(navCache, ctx))
						{
							goto IL_D9;
						}
					}
					return false;
				}
				navCache.MoveToParent();
				if (!this.patternPrevious.Matches(navCache, ctx))
				{
					return false;
				}
			}
			IL_D9:
			if (this.filter == null)
			{
				return true;
			}
			if (!this.filter.IsPositional && !(this.filter.expr is ExprFilter))
			{
				return this.filter.pred.EvaluateBoolean(new NullIterator(node, ctx));
			}
			navCache = ((XsltCompiledContext)ctx).GetNavCache(this, node);
			navCache.MoveToParent();
			BaseIterator baseIterator = this.filter.EvaluateNodeSet(new NullIterator(navCache, ctx));
			while (baseIterator.MoveNext())
			{
				if (node.IsSamePosition(baseIterator.Current))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00013968 File Offset: 0x00011B68
		public override string ToString()
		{
			string text = string.Empty;
			if (this.patternPrevious != null)
			{
				text = this.patternPrevious.ToString() + ((!this.isAncestor) ? "/" : "//");
			}
			if (this.filter != null)
			{
				text += this.filter.ToString();
			}
			else
			{
				text += this.nodeTest.ToString();
			}
			return text;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000139E8 File Offset: 0x00011BE8
		public LocationPathPattern LastPathPattern
		{
			get
			{
				LocationPathPattern locationPathPattern = this;
				while (locationPathPattern.patternPrevious != null)
				{
					locationPathPattern = locationPathPattern.patternPrevious;
				}
				return locationPathPattern;
			}
		}

		// Token: 0x04000214 RID: 532
		private LocationPathPattern patternPrevious;

		// Token: 0x04000215 RID: 533
		private bool isAncestor;

		// Token: 0x04000216 RID: 534
		private NodeTest nodeTest;

		// Token: 0x04000217 RID: 535
		private ExprFilter filter;
	}
}
