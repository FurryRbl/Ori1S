using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x020001A5 RID: 421
	internal class SortedIterator : BaseIterator
	{
		// Token: 0x06001147 RID: 4423 RVA: 0x0004F378 File Offset: 0x0004D578
		public SortedIterator(BaseIterator iter) : base(iter.NamespaceManager)
		{
			this.list = new ArrayList();
			while (iter.MoveNext())
			{
				XPathNavigator xpathNavigator = iter.Current;
				this.list.Add(xpathNavigator.Clone());
			}
			if (this.list.Count == 0)
			{
				return;
			}
			XPathNavigator xpathNavigator2 = (XPathNavigator)this.list[0];
			this.list.Sort(XPathNavigatorComparer.Instance);
			for (int i = 1; i < this.list.Count; i++)
			{
				XPathNavigator xpathNavigator3 = (XPathNavigator)this.list[i];
				if (xpathNavigator2.IsSamePosition(xpathNavigator3))
				{
					this.list.RemoveAt(i);
					i--;
				}
				else
				{
					xpathNavigator2 = xpathNavigator3;
				}
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0004F448 File Offset: 0x0004D648
		public SortedIterator(SortedIterator other) : base(other)
		{
			this.list = other.list;
			base.SetPosition(other.CurrentPosition);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0004F46C File Offset: 0x0004D66C
		public override XPathNodeIterator Clone()
		{
			return new SortedIterator(this);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0004F474 File Offset: 0x0004D674
		public override bool MoveNextCore()
		{
			return this.CurrentPosition < this.list.Count;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004F48C File Offset: 0x0004D68C
		public override XPathNavigator Current
		{
			get
			{
				return (this.CurrentPosition != 0) ? ((XPathNavigator)this.list[this.CurrentPosition - 1]) : null;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0004F4C4 File Offset: 0x0004D6C4
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x0400072E RID: 1838
		private ArrayList list;
	}
}
