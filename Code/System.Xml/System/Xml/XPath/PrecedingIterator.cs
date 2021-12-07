using System;

namespace System.Xml.XPath
{
	// Token: 0x020001A0 RID: 416
	internal class PrecedingIterator : SimpleIterator
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x0004EF80 File Offset: 0x0004D180
		public PrecedingIterator(BaseIterator iter) : base(iter)
		{
			this.startPosition = iter.Current.Clone();
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0004EF9C File Offset: 0x0004D19C
		private PrecedingIterator(PrecedingIterator other) : base(other, true)
		{
			this.startPosition = other.startPosition;
			this.started = other.started;
			this.finished = other.finished;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004EFD8 File Offset: 0x0004D1D8
		public override XPathNodeIterator Clone()
		{
			return new PrecedingIterator(this);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override bool MoveNextCore()
		{
			if (this.finished)
			{
				return false;
			}
			if (!this.started)
			{
				this.started = true;
				this._nav.MoveToRoot();
			}
			bool flag = true;
			while (flag)
			{
				if (!this._nav.MoveToFirstChild())
				{
					while (!this._nav.MoveToNext())
					{
						if (!this._nav.MoveToParent())
						{
							this.finished = true;
							return false;
						}
					}
				}
				if (!this._nav.IsDescendant(this.startPosition))
				{
					break;
				}
			}
			if (this._nav.ComparePosition(this.startPosition) != XmlNodeOrder.Before)
			{
				this.finished = true;
				return false;
			}
			return true;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004F0B0 File Offset: 0x0004D2B0
		public override bool ReverseAxis
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000725 RID: 1829
		private bool finished;

		// Token: 0x04000726 RID: 1830
		private bool started;

		// Token: 0x04000727 RID: 1831
		private XPathNavigator startPosition;
	}
}
