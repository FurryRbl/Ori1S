using System;

namespace System.Xml.XPath
{
	// Token: 0x02000194 RID: 404
	internal class SelfIterator : SimpleIterator
	{
		// Token: 0x060010EE RID: 4334 RVA: 0x0004E5F0 File Offset: 0x0004C7F0
		public SelfIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0004E5FC File Offset: 0x0004C7FC
		public SelfIterator(XPathNavigator nav, IXmlNamespaceResolver nsm) : base(nav, nsm)
		{
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0004E608 File Offset: 0x0004C808
		protected SelfIterator(SelfIterator other, bool clone) : base(other, true)
		{
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004E614 File Offset: 0x0004C814
		public override XPathNodeIterator Clone()
		{
			return new SelfIterator(this, true);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004E620 File Offset: 0x0004C820
		public override bool MoveNextCore()
		{
			return this.CurrentPosition == 0;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004E630 File Offset: 0x0004C830
		public override XPathNavigator Current
		{
			get
			{
				return (this.CurrentPosition != 0) ? this._nav : null;
			}
		}
	}
}
