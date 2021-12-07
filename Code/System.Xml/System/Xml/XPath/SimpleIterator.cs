using System;

namespace System.Xml.XPath
{
	// Token: 0x02000193 RID: 403
	internal abstract class SimpleIterator : BaseIterator
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x0004E4D8 File Offset: 0x0004C6D8
		public SimpleIterator(BaseIterator iter) : base(iter.NamespaceManager)
		{
			if (iter.CurrentPosition == 0)
			{
				this.skipfirst = true;
				iter.MoveNext();
			}
			if (iter.CurrentPosition > 0)
			{
				this._nav = iter.Current.Clone();
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004E528 File Offset: 0x0004C728
		protected SimpleIterator(SimpleIterator other, bool clone) : base(other)
		{
			if (other._nav != null)
			{
				this._nav = ((!clone) ? other._nav : other._nav.Clone());
			}
			this.skipfirst = other.skipfirst;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0004E578 File Offset: 0x0004C778
		public SimpleIterator(XPathNavigator nav, IXmlNamespaceResolver nsm) : base(nsm)
		{
			this._nav = nav.Clone();
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004E590 File Offset: 0x0004C790
		public override bool MoveNext()
		{
			if (!this.skipfirst)
			{
				return base.MoveNext();
			}
			if (this._nav == null)
			{
				return false;
			}
			this.skipfirst = false;
			base.SetPosition(1);
			return true;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0004E5CC File Offset: 0x0004C7CC
		public override XPathNavigator Current
		{
			get
			{
				if (this.CurrentPosition == 0)
				{
					return null;
				}
				this._current = this._nav;
				return this._current;
			}
		}

		// Token: 0x04000711 RID: 1809
		protected readonly XPathNavigator _nav;

		// Token: 0x04000712 RID: 1810
		protected XPathNavigator _current;

		// Token: 0x04000713 RID: 1811
		private bool skipfirst;
	}
}
