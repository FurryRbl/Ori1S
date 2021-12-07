using System;
using System.Collections;
using System.Globalization;
using Mono.Xml.XPath;

namespace System.Xml.XPath
{
	// Token: 0x02000168 RID: 360
	internal class XPathSorter
	{
		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004B6F4 File Offset: 0x000498F4
		public XPathSorter(object expr, IComparer cmp)
		{
			this._expr = XPathSorter.ExpressionFromObject(expr);
			this._cmp = cmp;
			this._type = XmlDataType.Text;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0004B724 File Offset: 0x00049924
		public XPathSorter(object expr, XmlSortOrder orderSort, XmlCaseOrder orderCase, string lang, XmlDataType dataType)
		{
			this._expr = XPathSorter.ExpressionFromObject(expr);
			this._type = dataType;
			if (dataType == XmlDataType.Number)
			{
				this._cmp = new XPathSorter.XPathNumberComparer(orderSort);
			}
			else
			{
				this._cmp = new XPathSorter.XPathTextComparer(orderSort, orderCase, lang);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004B774 File Offset: 0x00049974
		private static Expression ExpressionFromObject(object expr)
		{
			if (expr is CompiledExpression)
			{
				return ((CompiledExpression)expr).ExpressionNode;
			}
			if (expr is string)
			{
				return new XPathParser().Compile((string)expr);
			}
			throw new XPathException("Invalid query object");
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0004B7B4 File Offset: 0x000499B4
		public object Evaluate(BaseIterator iter)
		{
			if (this._type == XmlDataType.Number)
			{
				return this._expr.EvaluateNumber(iter);
			}
			return this._expr.EvaluateString(iter);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0004B7EC File Offset: 0x000499EC
		public int Compare(object o1, object o2)
		{
			return this._cmp.Compare(o1, o2);
		}

		// Token: 0x040006D9 RID: 1753
		private readonly Expression _expr;

		// Token: 0x040006DA RID: 1754
		private readonly IComparer _cmp;

		// Token: 0x040006DB RID: 1755
		private readonly XmlDataType _type;

		// Token: 0x02000169 RID: 361
		private class XPathNumberComparer : IComparer
		{
			// Token: 0x06000FEC RID: 4076 RVA: 0x0004B7FC File Offset: 0x000499FC
			public XPathNumberComparer(XmlSortOrder orderSort)
			{
				this._nMulSort = ((orderSort != XmlSortOrder.Ascending) ? -1 : 1);
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x0004B818 File Offset: 0x00049A18
			int IComparer.Compare(object o1, object o2)
			{
				double num = (double)o1;
				double num2 = (double)o2;
				if (num < num2)
				{
					return -this._nMulSort;
				}
				if (num > num2)
				{
					return this._nMulSort;
				}
				if (num == num2)
				{
					return 0;
				}
				if (double.IsNaN(num))
				{
					return (!double.IsNaN(num2)) ? (-this._nMulSort) : 0;
				}
				return this._nMulSort;
			}

			// Token: 0x040006DC RID: 1756
			private int _nMulSort;
		}

		// Token: 0x0200016A RID: 362
		private class XPathTextComparer : IComparer
		{
			// Token: 0x06000FEE RID: 4078 RVA: 0x0004B884 File Offset: 0x00049A84
			public XPathTextComparer(XmlSortOrder orderSort, XmlCaseOrder orderCase, string strLang)
			{
				this._orderCase = orderCase;
				this._nMulCase = ((orderCase != XmlCaseOrder.UpperFirst) ? 1 : -1);
				this._nMulSort = ((orderSort != XmlSortOrder.Ascending) ? -1 : 1);
				if (strLang == null || strLang == string.Empty)
				{
					this._ci = CultureInfo.CurrentCulture;
				}
				else
				{
					this._ci = new CultureInfo(strLang);
				}
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x0004B8F8 File Offset: 0x00049AF8
			int IComparer.Compare(object o1, object o2)
			{
				string strA = (string)o1;
				string strB = (string)o2;
				int num = string.Compare(strA, strB, true, this._ci);
				if (num != 0 || this._orderCase == XmlCaseOrder.None)
				{
					return num * this._nMulSort;
				}
				return this._nMulSort * this._nMulCase * string.Compare(strA, strB, false, this._ci);
			}

			// Token: 0x040006DD RID: 1757
			private int _nMulSort;

			// Token: 0x040006DE RID: 1758
			private int _nMulCase;

			// Token: 0x040006DF RID: 1759
			private XmlCaseOrder _orderCase;

			// Token: 0x040006E0 RID: 1760
			private CultureInfo _ci;
		}
	}
}
