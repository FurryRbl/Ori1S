using System;

namespace System.Xml.XPath
{
	// Token: 0x0200019F RID: 415
	internal class FollowingIterator : SimpleIterator
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x0004EE84 File Offset: 0x0004D084
		public FollowingIterator(BaseIterator iter) : base(iter)
		{
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0004EE90 File Offset: 0x0004D090
		private FollowingIterator(FollowingIterator other) : base(other, true)
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0004EE9C File Offset: 0x0004D09C
		public override XPathNodeIterator Clone()
		{
			return new FollowingIterator(this);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0004EEA4 File Offset: 0x0004D0A4
		public override bool MoveNextCore()
		{
			if (this._finished)
			{
				return false;
			}
			bool flag = true;
			if (this.CurrentPosition == 0)
			{
				flag = false;
				XPathNodeType nodeType = this._nav.NodeType;
				if (nodeType != XPathNodeType.Attribute && nodeType != XPathNodeType.Namespace)
				{
					if (this._nav.MoveToNext())
					{
						return true;
					}
					while (this._nav.MoveToParent())
					{
						if (this._nav.MoveToNext())
						{
							return true;
						}
					}
				}
				else
				{
					this._nav.MoveToParent();
					flag = true;
				}
			}
			if (flag)
			{
				if (this._nav.MoveToFirstChild())
				{
					return true;
				}
				while (!this._nav.MoveToNext())
				{
					if (!this._nav.MoveToParent())
					{
						goto IL_C6;
					}
				}
				return true;
			}
			IL_C6:
			this._finished = true;
			return false;
		}

		// Token: 0x04000724 RID: 1828
		private bool _finished;
	}
}
