using System;

namespace System.Xml.XPath
{
	// Token: 0x020001A4 RID: 420
	internal class SimpleSlashIterator : BaseIterator
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x0004F244 File Offset: 0x0004D444
		public SimpleSlashIterator(BaseIterator left, NodeSet expr) : base(left.NamespaceManager)
		{
			this._left = left;
			this._expr = expr;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0004F260 File Offset: 0x0004D460
		private SimpleSlashIterator(SimpleSlashIterator other) : base(other)
		{
			this._expr = other._expr;
			this._left = (BaseIterator)other._left.Clone();
			if (other._right != null)
			{
				this._right = (BaseIterator)other._right.Clone();
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0004F2B8 File Offset: 0x0004D4B8
		public override XPathNodeIterator Clone()
		{
			return new SimpleSlashIterator(this);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0004F2C0 File Offset: 0x0004D4C0
		public override bool MoveNextCore()
		{
			while (this._right == null || !this._right.MoveNext())
			{
				if (!this._left.MoveNext())
				{
					return false;
				}
				this._right = this._expr.EvaluateNodeSet(this._left);
			}
			if (this._current == null)
			{
				this._current = this._right.Current.Clone();
			}
			else if (!this._current.MoveTo(this._right.Current))
			{
				this._current = this._right.Current.Clone();
			}
			return true;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0004F370 File Offset: 0x0004D570
		public override XPathNavigator Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x0400072A RID: 1834
		private NodeSet _expr;

		// Token: 0x0400072B RID: 1835
		private BaseIterator _left;

		// Token: 0x0400072C RID: 1836
		private BaseIterator _right;

		// Token: 0x0400072D RID: 1837
		private XPathNavigator _current;
	}
}
