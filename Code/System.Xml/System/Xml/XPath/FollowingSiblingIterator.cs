using System;

namespace System.Xml.XPath
{
	// Token: 0x02000199 RID: 409
	internal class FollowingSiblingIterator : SimpleIterator
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x0004E868 File Offset: 0x0004CA68
		public FollowingSiblingIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0004E874 File Offset: 0x0004CA74
		private FollowingSiblingIterator(FollowingSiblingIterator other) : base(other, true)
		{
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0004E880 File Offset: 0x0004CA80
		public override XPathNodeIterator Clone()
		{
			return new FollowingSiblingIterator(this);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0004E888 File Offset: 0x0004CA88
		public override bool MoveNextCore()
		{
			XPathNodeType nodeType = this._nav.NodeType;
			return nodeType != XPathNodeType.Attribute && nodeType != XPathNodeType.Namespace && this._nav.MoveToNext();
		}
	}
}
