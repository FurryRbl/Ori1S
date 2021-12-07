using System;

namespace System.Xml.XPath
{
	// Token: 0x02000195 RID: 405
	internal class NullIterator : SelfIterator
	{
		// Token: 0x060010F4 RID: 4340 RVA: 0x0004E64C File Offset: 0x0004C84C
		public NullIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004E658 File Offset: 0x0004C858
		public NullIterator(XPathNavigator nav) : this(nav, null)
		{
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0004E664 File Offset: 0x0004C864
		public NullIterator(XPathNavigator nav, IXmlNamespaceResolver nsm) : base(nav, nsm)
		{
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004E670 File Offset: 0x0004C870
		private NullIterator(NullIterator other) : base(other, true)
		{
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004E67C File Offset: 0x0004C87C
		public override XPathNodeIterator Clone()
		{
			return new NullIterator(this);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004E684 File Offset: 0x0004C884
		public override bool MoveNextCore()
		{
			return false;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0004E688 File Offset: 0x0004C888
		public override int CurrentPosition
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0004E68C File Offset: 0x0004C88C
		public override XPathNavigator Current
		{
			get
			{
				return this._nav;
			}
		}
	}
}
