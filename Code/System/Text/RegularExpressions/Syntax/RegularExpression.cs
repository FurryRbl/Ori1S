using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200049B RID: 1179
	internal class RegularExpression : Group
	{
		// Token: 0x06002A6F RID: 10863 RVA: 0x00092574 File Offset: 0x00090774
		public RegularExpression()
		{
			this.group_count = 0;
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x00092584 File Offset: 0x00090784
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x0009258C File Offset: 0x0009078C
		public int GroupCount
		{
			get
			{
				return this.group_count;
			}
			set
			{
				this.group_count = value;
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x00092598 File Offset: 0x00090798
		public override void Compile(ICompiler cmp, bool reverse)
		{
			int min;
			int max;
			this.GetWidth(out min, out max);
			cmp.EmitInfo(this.group_count, min, max);
			AnchorInfo anchorInfo = this.GetAnchorInfo(reverse);
			LinkRef linkRef = cmp.NewLink();
			cmp.EmitAnchor(reverse, anchorInfo.Offset, linkRef);
			if (anchorInfo.IsPosition)
			{
				cmp.EmitPosition(anchorInfo.Position);
			}
			else if (anchorInfo.IsSubstring)
			{
				cmp.EmitString(anchorInfo.Substring, anchorInfo.IgnoreCase, reverse);
			}
			cmp.EmitTrue();
			cmp.ResolveLink(linkRef);
			base.Compile(cmp, reverse);
			cmp.EmitTrue();
		}

		// Token: 0x04001AFD RID: 6909
		private int group_count;
	}
}
