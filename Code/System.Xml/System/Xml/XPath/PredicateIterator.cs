using System;

namespace System.Xml.XPath
{
	// Token: 0x020001A7 RID: 423
	internal class PredicateIterator : BaseIterator
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x0004F838 File Offset: 0x0004DA38
		public PredicateIterator(BaseIterator iter, Expression pred) : base(iter.NamespaceManager)
		{
			this._iter = iter;
			this._pred = pred;
			this.resType = pred.GetReturnType(iter);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0004F864 File Offset: 0x0004DA64
		private PredicateIterator(PredicateIterator other) : base(other)
		{
			this._iter = (BaseIterator)other._iter.Clone();
			this._pred = other._pred;
			this.resType = other.resType;
			this.finished = other.finished;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0004F8B4 File Offset: 0x0004DAB4
		public override XPathNodeIterator Clone()
		{
			return new PredicateIterator(this);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0004F8BC File Offset: 0x0004DABC
		public override bool MoveNextCore()
		{
			if (this.finished)
			{
				return false;
			}
			while (this._iter.MoveNext())
			{
				XPathResultType xpathResultType = this.resType;
				if (xpathResultType != XPathResultType.Number)
				{
					if (xpathResultType != XPathResultType.Any)
					{
						if (!this._pred.EvaluateBoolean(this._iter))
						{
							continue;
						}
					}
					else
					{
						object obj = this._pred.Evaluate(this._iter);
						if (obj is double)
						{
							if ((double)obj != (double)this._iter.ComparablePosition)
							{
								continue;
							}
						}
						else if (!XPathFunctions.ToBoolean(obj))
						{
							continue;
						}
					}
				}
				else if (this._pred.EvaluateNumber(this._iter) != (double)this._iter.ComparablePosition)
				{
					continue;
				}
				return true;
			}
			this.finished = true;
			return false;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0004F9B0 File Offset: 0x0004DBB0
		public override XPathNavigator Current
		{
			get
			{
				return (this.CurrentPosition != 0) ? this._iter.Current : null;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0004F9D0 File Offset: 0x0004DBD0
		public override bool ReverseAxis
		{
			get
			{
				return this._iter.ReverseAxis;
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
		public override string ToString()
		{
			return this._iter.GetType().FullName;
		}

		// Token: 0x04000735 RID: 1845
		private BaseIterator _iter;

		// Token: 0x04000736 RID: 1846
		private Expression _pred;

		// Token: 0x04000737 RID: 1847
		private XPathResultType resType;

		// Token: 0x04000738 RID: 1848
		private bool finished;
	}
}
