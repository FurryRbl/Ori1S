using System;

namespace System.Xml.XPath
{
	// Token: 0x020001A2 RID: 418
	internal class AttributeIterator : SimpleIterator
	{
		// Token: 0x06001138 RID: 4408 RVA: 0x0004F118 File Offset: 0x0004D318
		public AttributeIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004F124 File Offset: 0x0004D324
		private AttributeIterator(AttributeIterator other) : base(other, true)
		{
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0004F130 File Offset: 0x0004D330
		public override XPathNodeIterator Clone()
		{
			return new AttributeIterator(this);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0004F138 File Offset: 0x0004D338
		public override bool MoveNextCore()
		{
			if (this.CurrentPosition == 0)
			{
				if (this._nav.MoveToFirstAttribute())
				{
					return true;
				}
			}
			else if (this._nav.MoveToNextAttribute())
			{
				return true;
			}
			return false;
		}
	}
}
