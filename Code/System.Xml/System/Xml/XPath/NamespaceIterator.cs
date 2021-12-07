using System;

namespace System.Xml.XPath
{
	// Token: 0x020001A1 RID: 417
	internal class NamespaceIterator : SimpleIterator
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x0004F0B4 File Offset: 0x0004D2B4
		public NamespaceIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0004F0C0 File Offset: 0x0004D2C0
		private NamespaceIterator(NamespaceIterator other) : base(other, true)
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004F0CC File Offset: 0x0004D2CC
		public override XPathNodeIterator Clone()
		{
			return new NamespaceIterator(this);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004F0D4 File Offset: 0x0004D2D4
		public override bool MoveNextCore()
		{
			if (this.CurrentPosition == 0)
			{
				if (this._nav.MoveToFirstNamespace())
				{
					return true;
				}
			}
			else if (this._nav.MoveToNextNamespace())
			{
				return true;
			}
			return false;
		}
	}
}
