using System;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.Xsl;

namespace Mono.Xml.XPath
{
	// Token: 0x02000045 RID: 69
	internal abstract class Pattern
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00013A18 File Offset: 0x00011C18
		internal static Pattern Compile(string s, Compiler comp)
		{
			return Pattern.Compile(comp.patternParser.Compile(s));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00013A2C File Offset: 0x00011C2C
		internal static Pattern Compile(Expression e)
		{
			if (e is ExprUNION)
			{
				return new UnionPattern(Pattern.Compile(((ExprUNION)e).left), Pattern.Compile(((ExprUNION)e).right));
			}
			if (e is ExprRoot)
			{
				return new LocationPathPattern(new NodeTypeTest(Axes.Self, XPathNodeType.Root));
			}
			if (e is NodeTest)
			{
				return new LocationPathPattern((NodeTest)e);
			}
			if (e is ExprFilter)
			{
				return new LocationPathPattern((ExprFilter)e);
			}
			if (e is ExprSLASH)
			{
				Pattern prev = Pattern.Compile(((ExprSLASH)e).left);
				LocationPathPattern locationPathPattern = (LocationPathPattern)Pattern.Compile(((ExprSLASH)e).right);
				locationPathPattern.SetPreviousPattern(prev, false);
				return locationPathPattern;
			}
			if (e is ExprSLASH2)
			{
				if (((ExprSLASH2)e).left is ExprRoot)
				{
					return Pattern.Compile(((ExprSLASH2)e).right);
				}
				Pattern prev2 = Pattern.Compile(((ExprSLASH2)e).left);
				LocationPathPattern locationPathPattern2 = (LocationPathPattern)Pattern.Compile(((ExprSLASH2)e).right);
				locationPathPattern2.SetPreviousPattern(prev2, true);
				return locationPathPattern2;
			}
			else
			{
				if (e is XPathFunctionId)
				{
					ExprLiteral exprLiteral = ((XPathFunctionId)e).Id as ExprLiteral;
					return new IdPattern(exprLiteral.Value);
				}
				if (e is XsltKey)
				{
					return new KeyPattern((XsltKey)e);
				}
				return null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00013B94 File Offset: 0x00011D94
		public virtual double DefaultPriority
		{
			get
			{
				return 0.5;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00013BA0 File Offset: 0x00011DA0
		public virtual XPathNodeType EvaluatedNodeType
		{
			get
			{
				return XPathNodeType.All;
			}
		}

		// Token: 0x060002A6 RID: 678
		public abstract bool Matches(XPathNavigator node, XsltContext ctx);
	}
}
