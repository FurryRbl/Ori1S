using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200049F RID: 1183
	internal class Repetition : CompositeExpression
	{
		// Token: 0x06002A83 RID: 10883 RVA: 0x000927B8 File Offset: 0x000909B8
		public Repetition(int min, int max, bool lazy)
		{
			base.Expressions.Add(null);
			this.min = min;
			this.max = max;
			this.lazy = lazy;
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000927E4 File Offset: 0x000909E4
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x000927F4 File Offset: 0x000909F4
		public Expression Expression
		{
			get
			{
				return base.Expressions[0];
			}
			set
			{
				base.Expressions[0] = value;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x00092804 File Offset: 0x00090A04
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x0009280C File Offset: 0x00090A0C
		public int Minimum
		{
			get
			{
				return this.min;
			}
			set
			{
				this.min = value;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x00092818 File Offset: 0x00090A18
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x00092820 File Offset: 0x00090A20
		public int Maximum
		{
			get
			{
				return this.max;
			}
			set
			{
				this.max = value;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x0009282C File Offset: 0x00090A2C
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x00092834 File Offset: 0x00090A34
		public bool Lazy
		{
			get
			{
				return this.lazy;
			}
			set
			{
				this.lazy = value;
			}
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00092840 File Offset: 0x00090A40
		public override void Compile(ICompiler cmp, bool reverse)
		{
			if (this.Expression.IsComplex())
			{
				LinkRef linkRef = cmp.NewLink();
				cmp.EmitRepeat(this.min, this.max, this.lazy, linkRef);
				this.Expression.Compile(cmp, reverse);
				cmp.EmitUntil(linkRef);
			}
			else
			{
				LinkRef linkRef2 = cmp.NewLink();
				cmp.EmitFastRepeat(this.min, this.max, this.lazy, linkRef2);
				this.Expression.Compile(cmp, reverse);
				cmp.EmitTrue();
				cmp.ResolveLink(linkRef2);
			}
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000928D0 File Offset: 0x00090AD0
		public override void GetWidth(out int min, out int max)
		{
			this.Expression.GetWidth(out min, out max);
			min *= this.min;
			if (max == 2147483647 || this.max == 65535)
			{
				max = int.MaxValue;
			}
			else
			{
				max *= this.max;
			}
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x00092928 File Offset: 0x00090B28
		public override AnchorInfo GetAnchorInfo(bool reverse)
		{
			int fixedWidth = base.GetFixedWidth();
			if (this.Minimum == 0)
			{
				return new AnchorInfo(this, fixedWidth);
			}
			AnchorInfo anchorInfo = this.Expression.GetAnchorInfo(reverse);
			if (anchorInfo.IsPosition)
			{
				return new AnchorInfo(this, anchorInfo.Offset, fixedWidth, anchorInfo.Position);
			}
			if (!anchorInfo.IsSubstring)
			{
				return new AnchorInfo(this, fixedWidth);
			}
			if (anchorInfo.IsComplete)
			{
				string substring = anchorInfo.Substring;
				StringBuilder stringBuilder = new StringBuilder(substring);
				for (int i = 1; i < this.Minimum; i++)
				{
					stringBuilder.Append(substring);
				}
				return new AnchorInfo(this, 0, fixedWidth, stringBuilder.ToString(), anchorInfo.IgnoreCase);
			}
			return new AnchorInfo(this, anchorInfo.Offset, fixedWidth, anchorInfo.Substring, anchorInfo.IgnoreCase);
		}

		// Token: 0x04001B01 RID: 6913
		private int min;

		// Token: 0x04001B02 RID: 6914
		private int max;

		// Token: 0x04001B03 RID: 6915
		private bool lazy;
	}
}
