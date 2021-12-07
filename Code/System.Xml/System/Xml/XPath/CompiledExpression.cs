using System;
using System.Collections;
using System.Xml.Xsl;

namespace System.Xml.XPath
{
	// Token: 0x02000165 RID: 357
	internal class CompiledExpression : XPathExpression
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x0004B2D4 File Offset: 0x000494D4
		public CompiledExpression(string raw, Expression expr)
		{
			this._expr = expr.Optimize();
			this.rawExpression = raw;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0004B2F0 File Offset: 0x000494F0
		private CompiledExpression(CompiledExpression other)
		{
			this._nsm = other._nsm;
			this._expr = other._expr;
			this.rawExpression = other.rawExpression;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0004B328 File Offset: 0x00049528
		public override XPathExpression Clone()
		{
			return new CompiledExpression(this);
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0004B330 File Offset: 0x00049530
		public Expression ExpressionNode
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0004B338 File Offset: 0x00049538
		public override void SetContext(XmlNamespaceManager nsManager)
		{
			this._nsm = nsManager;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004B344 File Offset: 0x00049544
		public override void SetContext(IXmlNamespaceResolver nsResolver)
		{
			this._nsm = nsResolver;
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0004B350 File Offset: 0x00049550
		internal IXmlNamespaceResolver NamespaceManager
		{
			get
			{
				return this._nsm;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0004B358 File Offset: 0x00049558
		public override string Expression
		{
			get
			{
				return this.rawExpression;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0004B360 File Offset: 0x00049560
		public override XPathResultType ReturnType
		{
			get
			{
				return this._expr.ReturnType;
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0004B370 File Offset: 0x00049570
		public object Evaluate(BaseIterator iter)
		{
			if (this._sorters != null)
			{
				return this.EvaluateNodeSet(iter);
			}
			object result;
			try
			{
				result = this._expr.Evaluate(iter);
			}
			catch (XPathException)
			{
				throw;
			}
			catch (XsltException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new XPathException("Error during evaluation", innerException);
			}
			return result;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0004B418 File Offset: 0x00049618
		public XPathNodeIterator EvaluateNodeSet(BaseIterator iter)
		{
			BaseIterator baseIterator = this._expr.EvaluateNodeSet(iter);
			if (this._sorters != null)
			{
				return this._sorters.Sort(baseIterator);
			}
			return baseIterator;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0004B44C File Offset: 0x0004964C
		public double EvaluateNumber(BaseIterator iter)
		{
			return this._expr.EvaluateNumber(iter);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0004B45C File Offset: 0x0004965C
		public string EvaluateString(BaseIterator iter)
		{
			return this._expr.EvaluateString(iter);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0004B46C File Offset: 0x0004966C
		public bool EvaluateBoolean(BaseIterator iter)
		{
			return this._expr.EvaluateBoolean(iter);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0004B47C File Offset: 0x0004967C
		public override void AddSort(object obj, IComparer cmp)
		{
			if (this._sorters == null)
			{
				this._sorters = new XPathSorters();
			}
			this._sorters.Add(obj, cmp);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0004B4A4 File Offset: 0x000496A4
		public override void AddSort(object expr, XmlSortOrder orderSort, XmlCaseOrder orderCase, string lang, XmlDataType dataType)
		{
			if (this._sorters == null)
			{
				this._sorters = new XPathSorters();
			}
			this._sorters.Add(expr, orderSort, orderCase, lang, dataType);
		}

		// Token: 0x040006D2 RID: 1746
		protected IXmlNamespaceResolver _nsm;

		// Token: 0x040006D3 RID: 1747
		protected Expression _expr;

		// Token: 0x040006D4 RID: 1748
		private XPathSorters _sorters;

		// Token: 0x040006D5 RID: 1749
		private string rawExpression;
	}
}
