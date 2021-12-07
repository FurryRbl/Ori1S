using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x020001AA RID: 426
	internal class OrderedIterator : BaseIterator
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x0004FCE0 File Offset: 0x0004DEE0
		public OrderedIterator(BaseIterator iter) : base(iter.NamespaceManager)
		{
			this.list = new ArrayList();
			while (iter.MoveNext())
			{
				XPathNavigator value = iter.Current;
				this.list.Add(value);
			}
			this.list.Sort(XPathNavigatorComparer.Instance);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0004FD40 File Offset: 0x0004DF40
		private OrderedIterator(OrderedIterator other, bool dummy) : base(other)
		{
			if (other.iter != null)
			{
				this.iter = (BaseIterator)other.iter.Clone();
			}
			this.list = other.list;
			this.index = other.index;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0004FD94 File Offset: 0x0004DF94
		public override XPathNodeIterator Clone()
		{
			return new OrderedIterator(this);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0004FD9C File Offset: 0x0004DF9C
		public override bool MoveNextCore()
		{
			if (this.iter != null)
			{
				return this.iter.MoveNext();
			}
			if (this.index++ < this.list.Count)
			{
				return true;
			}
			this.index--;
			return false;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004FDF4 File Offset: 0x0004DFF4
		public override XPathNavigator Current
		{
			get
			{
				return (this.iter == null) ? ((this.index >= 0) ? ((XPathNavigator)this.list[this.index]) : null) : this.iter.Current;
			}
		}

		// Token: 0x0400073F RID: 1855
		private BaseIterator iter;

		// Token: 0x04000740 RID: 1856
		private ArrayList list;

		// Token: 0x04000741 RID: 1857
		private int index = -1;
	}
}
