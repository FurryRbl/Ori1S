using System;

namespace System.Xml.XPath
{
	// Token: 0x0200019A RID: 410
	internal class PrecedingSiblingIterator : SimpleIterator
	{
		// Token: 0x06001110 RID: 4368 RVA: 0x0004E8CC File Offset: 0x0004CACC
		public PrecedingSiblingIterator(BaseIterator iter) : base(iter)
		{
			this.startPosition = iter.Current.Clone();
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004E8E8 File Offset: 0x0004CAE8
		private PrecedingSiblingIterator(PrecedingSiblingIterator other) : base(other, true)
		{
			this.startPosition = other.startPosition;
			this.started = other.started;
			this.finished = other.finished;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0004E924 File Offset: 0x0004CB24
		public override XPathNodeIterator Clone()
		{
			return new PrecedingSiblingIterator(this);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0004E92C File Offset: 0x0004CB2C
		public override bool MoveNextCore()
		{
			if (this.finished)
			{
				return false;
			}
			if (!this.started)
			{
				this.started = true;
				XPathNodeType nodeType = this._nav.NodeType;
				if (nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
				{
					this.finished = true;
					return false;
				}
				this._nav.MoveToFirst();
				if (!this._nav.IsSamePosition(this.startPosition))
				{
					return true;
				}
			}
			else if (!this._nav.MoveToNext())
			{
				this.finished = true;
				return false;
			}
			if (this._nav.ComparePosition(this.startPosition) != XmlNodeOrder.Before)
			{
				this.finished = true;
				return false;
			}
			return true;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0004E9E4 File Offset: 0x0004CBE4
		public override bool ReverseAxis
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000717 RID: 1815
		private bool finished;

		// Token: 0x04000718 RID: 1816
		private bool started;

		// Token: 0x04000719 RID: 1817
		private XPathNavigator startPosition;
	}
}
