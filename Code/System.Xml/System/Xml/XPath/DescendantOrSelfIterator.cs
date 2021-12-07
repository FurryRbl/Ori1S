using System;

namespace System.Xml.XPath
{
	// Token: 0x0200019E RID: 414
	internal class DescendantOrSelfIterator : SimpleIterator
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x0004EDAC File Offset: 0x0004CFAC
		public DescendantOrSelfIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0004EDB8 File Offset: 0x0004CFB8
		private DescendantOrSelfIterator(DescendantOrSelfIterator other) : base(other, true)
		{
			this._depth = other._depth;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004EDD0 File Offset: 0x0004CFD0
		public override XPathNodeIterator Clone()
		{
			return new DescendantOrSelfIterator(this);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0004EDD8 File Offset: 0x0004CFD8
		public override bool MoveNextCore()
		{
			if (this._finished)
			{
				return false;
			}
			if (this.CurrentPosition == 0)
			{
				return true;
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

		// Token: 0x04000722 RID: 1826
		private int _depth;

		// Token: 0x04000723 RID: 1827
		private bool _finished;
	}
}
