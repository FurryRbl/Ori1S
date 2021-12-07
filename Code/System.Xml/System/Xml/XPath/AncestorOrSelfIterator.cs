using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x0200019C RID: 412
	internal class AncestorOrSelfIterator : SimpleIterator
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x0004EB2C File Offset: 0x0004CD2C
		public AncestorOrSelfIterator(BaseIterator iter) : base(iter)
		{
			this.startPosition = iter.Current.Clone();
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0004EB48 File Offset: 0x0004CD48
		private AncestorOrSelfIterator(AncestorOrSelfIterator other) : base(other, true)
		{
			this.startPosition = other.startPosition;
			if (other.navigators != null)
			{
				this.navigators = (ArrayList)other.navigators.Clone();
			}
			this.currentPosition = other.currentPosition;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0004EB98 File Offset: 0x0004CD98
		public override XPathNodeIterator Clone()
		{
			return new AncestorOrSelfIterator(this);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0004EBA0 File Offset: 0x0004CDA0
		private void CollectResults()
		{
			this.navigators = new ArrayList();
			XPathNavigator xpathNavigator = this.startPosition.Clone();
			if (!xpathNavigator.MoveToParent())
			{
				return;
			}
			while (xpathNavigator.NodeType != XPathNodeType.Root)
			{
				this.navigators.Add(xpathNavigator.Clone());
				xpathNavigator.MoveToParent();
			}
			this.currentPosition = this.navigators.Count;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0004EC0C File Offset: 0x0004CE0C
		public override bool MoveNextCore()
		{
			if (this.navigators == null)
			{
				this.CollectResults();
				if (this.startPosition.NodeType != XPathNodeType.Root)
				{
					this._nav.MoveToRoot();
					return true;
				}
			}
			if (this.currentPosition == -1)
			{
				return false;
			}
			if (this.currentPosition-- == 0)
			{
				this._nav.MoveTo(this.startPosition);
				return true;
			}
			this._nav.MoveTo((XPathNavigator)this.navigators[this.currentPosition]);
			return true;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0004ECA4 File Offset: 0x0004CEA4
		public override bool ReverseAxis
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0004ECA8 File Offset: 0x0004CEA8
		public override int Count
		{
			get
			{
				if (this.navigators == null)
				{
					this.CollectResults();
				}
				return this.navigators.Count + 1;
			}
		}

		// Token: 0x0400071D RID: 1821
		private int currentPosition;

		// Token: 0x0400071E RID: 1822
		private ArrayList navigators;

		// Token: 0x0400071F RID: 1823
		private XPathNavigator startPosition;
	}
}
