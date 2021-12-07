using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x020001A8 RID: 424
	internal class ListIterator : BaseIterator
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x0004F9F4 File Offset: 0x0004DBF4
		public ListIterator(BaseIterator iter, IList list) : base(iter.NamespaceManager)
		{
			this._list = list;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0004FA0C File Offset: 0x0004DC0C
		public ListIterator(IList list, IXmlNamespaceResolver nsm) : base(nsm)
		{
			this._list = list;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0004FA1C File Offset: 0x0004DC1C
		private ListIterator(ListIterator other) : base(other)
		{
			this._list = other._list;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0004FA34 File Offset: 0x0004DC34
		public override XPathNodeIterator Clone()
		{
			return new ListIterator(this);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0004FA3C File Offset: 0x0004DC3C
		public override bool MoveNextCore()
		{
			return this.CurrentPosition < this._list.Count;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004FA58 File Offset: 0x0004DC58
		public override XPathNavigator Current
		{
			get
			{
				if (this._list.Count == 0 || this.CurrentPosition == 0)
				{
					return null;
				}
				return (XPathNavigator)this._list[this.CurrentPosition - 1];
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0004FA9C File Offset: 0x0004DC9C
		public override int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x04000739 RID: 1849
		private IList _list;
	}
}
