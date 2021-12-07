using System;

namespace System.Xml.XPath
{
	// Token: 0x02000197 RID: 407
	internal class ParentIterator : SimpleIterator
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x0004E704 File Offset: 0x0004C904
		public ParentIterator(BaseIterator iter) : base(iter)
		{
			this.canMove = this._nav.MoveToParent();
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0004E720 File Offset: 0x0004C920
		private ParentIterator(ParentIterator other, bool dummy) : base(other, true)
		{
			this.canMove = other.canMove;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0004E738 File Offset: 0x0004C938
		public ParentIterator(XPathNavigator nav, IXmlNamespaceResolver nsm) : base(nav, nsm)
		{
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0004E744 File Offset: 0x0004C944
		public override XPathNodeIterator Clone()
		{
			return new ParentIterator(this, true);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0004E750 File Offset: 0x0004C950
		public override bool MoveNextCore()
		{
			if (!this.canMove)
			{
				return false;
			}
			this.canMove = false;
			return true;
		}

		// Token: 0x04000715 RID: 1813
		private bool canMove;
	}
}
