using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x020001A6 RID: 422
	internal class SlashIterator : BaseIterator
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x0004F4D4 File Offset: 0x0004D6D4
		public SlashIterator(BaseIterator iter, NodeSet expr) : base(iter.NamespaceManager)
		{
			this._iterLeft = iter;
			this._expr = expr;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0004F4F0 File Offset: 0x0004D6F0
		private SlashIterator(SlashIterator other) : base(other)
		{
			this._iterLeft = (BaseIterator)other._iterLeft.Clone();
			if (other._iterRight != null)
			{
				this._iterRight = (BaseIterator)other._iterRight.Clone();
			}
			this._expr = other._expr;
			if (other._iterList != null)
			{
				this._iterList = (SortedList)other._iterList.Clone();
			}
			this._finished = other._finished;
			if (other._nextIterRight != null)
			{
				this._nextIterRight = (BaseIterator)other._nextIterRight.Clone();
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0004F598 File Offset: 0x0004D798
		public override XPathNodeIterator Clone()
		{
			return new SlashIterator(this);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0004F5A0 File Offset: 0x0004D7A0
		public override bool MoveNextCore()
		{
			if (this._finished)
			{
				return false;
			}
			if (this._iterRight == null)
			{
				if (!this._iterLeft.MoveNext())
				{
					return false;
				}
				this._iterRight = this._expr.EvaluateNodeSet(this._iterLeft);
				this._iterList = new SortedList(XPathIteratorComparer.Instance);
			}
			while (!this._iterRight.MoveNext())
			{
				if (this._iterList.Count > 0)
				{
					int index = this._iterList.Count - 1;
					this._iterRight = (BaseIterator)this._iterList.GetByIndex(index);
					this._iterList.RemoveAt(index);
					break;
				}
				if (this._nextIterRight != null)
				{
					this._iterRight = this._nextIterRight;
					this._nextIterRight = null;
					break;
				}
				if (!this._iterLeft.MoveNext())
				{
					this._finished = true;
					return false;
				}
				this._iterRight = this._expr.EvaluateNodeSet(this._iterLeft);
			}
			bool flag = true;
			while (flag)
			{
				flag = false;
				if (this._nextIterRight == null)
				{
					bool flag2 = false;
					while (this._nextIterRight == null || !this._nextIterRight.MoveNext())
					{
						if (!this._iterLeft.MoveNext())
						{
							flag2 = true;
							break;
						}
						this._nextIterRight = this._expr.EvaluateNodeSet(this._iterLeft);
					}
					if (flag2)
					{
						this._nextIterRight = null;
					}
				}
				if (this._nextIterRight != null)
				{
					XmlNodeOrder xmlNodeOrder = this._iterRight.Current.ComparePosition(this._nextIterRight.Current);
					if (xmlNodeOrder != XmlNodeOrder.After)
					{
						if (xmlNodeOrder == XmlNodeOrder.Same)
						{
							if (!this._nextIterRight.MoveNext())
							{
								this._nextIterRight = null;
							}
							else
							{
								int count = this._iterList.Count;
								this._iterList[this._nextIterRight] = this._nextIterRight;
								if (count != this._iterList.Count)
								{
									this._nextIterRight = (BaseIterator)this._iterList.GetByIndex(count);
									this._iterList.RemoveAt(count);
								}
							}
							flag = true;
						}
					}
					else
					{
						this._iterList[this._iterRight] = this._iterRight;
						this._iterRight = this._nextIterRight;
						this._nextIterRight = null;
						flag = true;
					}
				}
			}
			return true;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0004F818 File Offset: 0x0004DA18
		public override XPathNavigator Current
		{
			get
			{
				return (this.CurrentPosition != 0) ? this._iterRight.Current : null;
			}
		}

		// Token: 0x0400072F RID: 1839
		private BaseIterator _iterLeft;

		// Token: 0x04000730 RID: 1840
		private BaseIterator _iterRight;

		// Token: 0x04000731 RID: 1841
		private NodeSet _expr;

		// Token: 0x04000732 RID: 1842
		private SortedList _iterList;

		// Token: 0x04000733 RID: 1843
		private bool _finished;

		// Token: 0x04000734 RID: 1844
		private BaseIterator _nextIterRight;
	}
}
