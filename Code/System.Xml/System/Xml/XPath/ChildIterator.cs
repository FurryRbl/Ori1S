using System;

namespace System.Xml.XPath
{
	// Token: 0x02000198 RID: 408
	internal class ChildIterator : BaseIterator
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x0004E768 File Offset: 0x0004C968
		public ChildIterator(BaseIterator iter) : base(iter.NamespaceManager)
		{
			this._nav = ((iter.CurrentPosition != 0) ? iter.Current : iter.PeekNext());
			if (this._nav != null && this._nav.HasChildren)
			{
				this._nav = this._nav.Clone();
			}
			else
			{
				this._nav = null;
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0004E7DC File Offset: 0x0004C9DC
		private ChildIterator(ChildIterator other) : base(other)
		{
			this._nav = ((other._nav != null) ? other._nav.Clone() : null);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0004E808 File Offset: 0x0004CA08
		public override XPathNodeIterator Clone()
		{
			return new ChildIterator(this);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0004E810 File Offset: 0x0004CA10
		public override bool MoveNextCore()
		{
			return this._nav != null && ((this.CurrentPosition != 0) ? this._nav.MoveToNext() : this._nav.MoveToFirstChild());
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004E850 File Offset: 0x0004CA50
		public override XPathNavigator Current
		{
			get
			{
				if (this.CurrentPosition == 0)
				{
					return null;
				}
				return this._nav;
			}
		}

		// Token: 0x04000716 RID: 1814
		private XPathNavigator _nav;
	}
}
