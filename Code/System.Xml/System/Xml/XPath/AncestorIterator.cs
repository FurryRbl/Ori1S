using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x0200019B RID: 411
	internal class AncestorIterator : SimpleIterator
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x0004E9E8 File Offset: 0x0004CBE8
		public AncestorIterator(BaseIterator iter) : base(iter)
		{
			this.startPosition = iter.Current.Clone();
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0004EA04 File Offset: 0x0004CC04
		private AncestorIterator(AncestorIterator other) : base(other, true)
		{
			this.startPosition = other.startPosition;
			if (other.navigators != null)
			{
				this.navigators = other.navigators;
			}
			this.currentPosition = other.currentPosition;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0004EA40 File Offset: 0x0004CC40
		public override XPathNodeIterator Clone()
		{
			return new AncestorIterator(this);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0004EA48 File Offset: 0x0004CC48
		private void CollectResults()
		{
			this.navigators = new ArrayList();
			XPathNavigator xpathNavigator = this.startPosition.Clone();
			while (xpathNavigator.NodeType != XPathNodeType.Root && xpathNavigator.MoveToParent())
			{
				this.navigators.Add(xpathNavigator.Clone());
			}
			this.currentPosition = this.navigators.Count;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0004EAAC File Offset: 0x0004CCAC
		public override bool MoveNextCore()
		{
			if (this.navigators == null)
			{
				this.CollectResults();
			}
			if (this.currentPosition == 0)
			{
				return false;
			}
			this._nav.MoveTo((XPathNavigator)this.navigators[--this.currentPosition]);
			return true;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0004EB08 File Offset: 0x0004CD08
		public override bool ReverseAxis
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0004EB0C File Offset: 0x0004CD0C
		public override int Count
		{
			get
			{
				if (this.navigators == null)
				{
					this.CollectResults();
				}
				return this.navigators.Count;
			}
		}

		// Token: 0x0400071A RID: 1818
		private int currentPosition;

		// Token: 0x0400071B RID: 1819
		private ArrayList navigators;

		// Token: 0x0400071C RID: 1820
		private XPathNavigator startPosition;
	}
}
