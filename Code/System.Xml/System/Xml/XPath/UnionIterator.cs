using System;

namespace System.Xml.XPath
{
	// Token: 0x020001A9 RID: 425
	internal class UnionIterator : BaseIterator
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x0004FAAC File Offset: 0x0004DCAC
		public UnionIterator(BaseIterator iter, BaseIterator left, BaseIterator right) : base(iter.NamespaceManager)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0004FAC8 File Offset: 0x0004DCC8
		private UnionIterator(UnionIterator other) : base(other)
		{
			this._left = (BaseIterator)other._left.Clone();
			this._right = (BaseIterator)other._right.Clone();
			this.keepLeft = other.keepLeft;
			this.keepRight = other.keepRight;
			if (other._current != null)
			{
				this._current = other._current.Clone();
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0004FB3C File Offset: 0x0004DD3C
		public override XPathNodeIterator Clone()
		{
			return new UnionIterator(this);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0004FB44 File Offset: 0x0004DD44
		public override bool MoveNextCore()
		{
			if (!this.keepLeft)
			{
				this.keepLeft = this._left.MoveNext();
			}
			if (!this.keepRight)
			{
				this.keepRight = this._right.MoveNext();
			}
			if (!this.keepLeft && !this.keepRight)
			{
				return false;
			}
			if (!this.keepRight)
			{
				this.keepLeft = false;
				this.SetCurrent(this._left);
				return true;
			}
			if (!this.keepLeft)
			{
				this.keepRight = false;
				this.SetCurrent(this._right);
				return true;
			}
			switch (this._left.Current.ComparePosition(this._right.Current))
			{
			case XmlNodeOrder.Before:
			case XmlNodeOrder.Unknown:
				this.keepLeft = false;
				this.SetCurrent(this._left);
				return true;
			case XmlNodeOrder.After:
				this.keepRight = false;
				this.SetCurrent(this._right);
				return true;
			case XmlNodeOrder.Same:
				this.keepLeft = (this.keepRight = false);
				this.SetCurrent(this._right);
				return true;
			default:
				throw new InvalidOperationException("Should not happen.");
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0004FC6C File Offset: 0x0004DE6C
		private void SetCurrent(XPathNodeIterator iter)
		{
			if (this._current == null)
			{
				this._current = iter.Current.Clone();
			}
			else if (!this._current.MoveTo(iter.Current))
			{
				this._current = iter.Current.Clone();
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0004FCC4 File Offset: 0x0004DEC4
		public override XPathNavigator Current
		{
			get
			{
				return (this.CurrentPosition <= 0) ? null : this._current;
			}
		}

		// Token: 0x0400073A RID: 1850
		private BaseIterator _left;

		// Token: 0x0400073B RID: 1851
		private BaseIterator _right;

		// Token: 0x0400073C RID: 1852
		private bool keepLeft;

		// Token: 0x0400073D RID: 1853
		private bool keepRight;

		// Token: 0x0400073E RID: 1854
		private XPathNavigator _current;
	}
}
