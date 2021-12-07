using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x02000173 RID: 371
	internal abstract class RelationalExpr : ExprBoolean
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x0004C544 File Offset: 0x0004A744
		public RelationalExpr(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0004C550 File Offset: 0x0004A750
		public override bool StaticValueAsBoolean
		{
			get
			{
				return this.HasStaticValue && this.Compare(this._left.StaticValueAsNumber, this._right.StaticValueAsNumber);
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0004C58C File Offset: 0x0004A78C
		public override bool EvaluateBoolean(BaseIterator iter)
		{
			XPathResultType xpathResultType = this._left.GetReturnType(iter);
			XPathResultType xpathResultType2 = this._right.GetReturnType(iter);
			if (xpathResultType == XPathResultType.Any)
			{
				xpathResultType = Expression.GetReturnType(this._left.Evaluate(iter));
			}
			if (xpathResultType2 == XPathResultType.Any)
			{
				xpathResultType2 = Expression.GetReturnType(this._right.Evaluate(iter));
			}
			if (xpathResultType == XPathResultType.Navigator)
			{
				xpathResultType = XPathResultType.String;
			}
			if (xpathResultType2 == XPathResultType.Navigator)
			{
				xpathResultType2 = XPathResultType.String;
			}
			if (xpathResultType != XPathResultType.NodeSet && xpathResultType2 != XPathResultType.NodeSet)
			{
				return this.Compare(this._left.EvaluateNumber(iter), this._right.EvaluateNumber(iter));
			}
			bool fReverse = false;
			Expression expression;
			Expression expression2;
			if (xpathResultType != XPathResultType.NodeSet)
			{
				fReverse = true;
				expression = this._right;
				expression2 = this._left;
				XPathResultType xpathResultType3 = xpathResultType;
				xpathResultType2 = xpathResultType3;
			}
			else
			{
				expression = this._left;
				expression2 = this._right;
			}
			if (xpathResultType2 == XPathResultType.Boolean)
			{
				bool value = expression.EvaluateBoolean(iter);
				bool value2 = expression2.EvaluateBoolean(iter);
				return this.Compare(Convert.ToDouble(value), Convert.ToDouble(value2), fReverse);
			}
			BaseIterator baseIterator = expression.EvaluateNodeSet(iter);
			if (xpathResultType2 == XPathResultType.Number || xpathResultType2 == XPathResultType.String)
			{
				double arg = expression2.EvaluateNumber(iter);
				while (baseIterator.MoveNext())
				{
					if (this.Compare(XPathFunctions.ToNumber(baseIterator.Current.Value), arg, fReverse))
					{
						return true;
					}
				}
			}
			else if (xpathResultType2 == XPathResultType.NodeSet)
			{
				BaseIterator baseIterator2 = expression2.EvaluateNodeSet(iter);
				ArrayList arrayList = new ArrayList();
				while (baseIterator.MoveNext())
				{
					XPathNavigator xpathNavigator = baseIterator.Current;
					arrayList.Add(XPathFunctions.ToNumber(xpathNavigator.Value));
				}
				while (baseIterator2.MoveNext())
				{
					XPathNavigator xpathNavigator2 = baseIterator2.Current;
					double arg2 = XPathFunctions.ToNumber(xpathNavigator2.Value);
					for (int i = 0; i < arrayList.Count; i++)
					{
						if (this.Compare((double)arrayList[i], arg2))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001025 RID: 4133
		public abstract bool Compare(double arg1, double arg2);

		// Token: 0x06001026 RID: 4134 RVA: 0x0004C788 File Offset: 0x0004A988
		public bool Compare(double arg1, double arg2, bool fReverse)
		{
			if (fReverse)
			{
				return this.Compare(arg2, arg1);
			}
			return this.Compare(arg1, arg2);
		}
	}
}
