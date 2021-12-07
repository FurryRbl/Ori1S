using System;

namespace System.Xml.XPath
{
	// Token: 0x0200019D RID: 413
	internal class DescendantIterator : SimpleIterator
	{
		// Token: 0x06001123 RID: 4387 RVA: 0x0004ECC8 File Offset: 0x0004CEC8
		public DescendantIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0004ECD4 File Offset: 0x0004CED4
		private DescendantIterator(DescendantIterator other) : base(other, true)
		{
			this._depth = other._depth;
			this._finished = other._finished;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0004ED04 File Offset: 0x0004CF04
		public override XPathNodeIterator Clone()
		{
			return new DescendantIterator(this);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0004ED0C File Offset: 0x0004CF0C
		public override bool MoveNextCore()
		{
			if (this._finished)
			{
				return false;
			}
			if (this._nav.MoveToFirstChild())
			{
				this._depth++;
				return true;
			}
			while (this._depth != 0)
			{
				if (this._nav.MoveToNext())
				{
					return true;
				}
				if (!this._nav.MoveToParent())
				{
					throw new XPathException("Current node is removed while it should not be, or there are some bugs in the XPathNavigator implementation class: " + this._nav.GetType());
				}
				this._depth--;
			}
			this._finished = true;
			return false;
		}

		// Token: 0x04000720 RID: 1824
		private int _depth;

		// Token: 0x04000721 RID: 1825
		private bool _finished;
	}
}
