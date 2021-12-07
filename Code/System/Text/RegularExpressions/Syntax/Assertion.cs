using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A0 RID: 1184
	internal abstract class Assertion : CompositeExpression
	{
		// Token: 0x06002A8F RID: 10895 RVA: 0x000929F8 File Offset: 0x00090BF8
		public Assertion()
		{
			base.Expressions.Add(null);
			base.Expressions.Add(null);
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x00092A24 File Offset: 0x00090C24
		// (set) Token: 0x06002A91 RID: 10897 RVA: 0x00092A34 File Offset: 0x00090C34
		public Expression TrueExpression
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

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x00092A44 File Offset: 0x00090C44
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x00092A54 File Offset: 0x00090C54
		public Expression FalseExpression
		{
			get
			{
				return base.Expressions[1];
			}
			set
			{
				base.Expressions[1] = value;
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00092A64 File Offset: 0x00090C64
		public override void GetWidth(out int min, out int max)
		{
			base.GetWidth(out min, out max, 2);
			if (this.TrueExpression == null || this.FalseExpression == null)
			{
				min = 0;
			}
		}
	}
}
