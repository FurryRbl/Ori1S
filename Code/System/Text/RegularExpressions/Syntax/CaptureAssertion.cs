using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A1 RID: 1185
	internal class CaptureAssertion : Assertion
	{
		// Token: 0x06002A95 RID: 10901 RVA: 0x00092A94 File Offset: 0x00090C94
		public CaptureAssertion(Literal l)
		{
			this.literal = l;
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x00092AA4 File Offset: 0x00090CA4
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x00092AAC File Offset: 0x00090CAC
		public CapturingGroup CapturingGroup
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x00092AB8 File Offset: 0x00090CB8
		public override void Compile(ICompiler cmp, bool reverse)
		{
			if (this.group == null)
			{
				this.Alternate.Compile(cmp, reverse);
				return;
			}
			int index = this.group.Index;
			LinkRef linkRef = cmp.NewLink();
			if (base.FalseExpression == null)
			{
				cmp.EmitIfDefined(index, linkRef);
				base.TrueExpression.Compile(cmp, reverse);
			}
			else
			{
				LinkRef linkRef2 = cmp.NewLink();
				cmp.EmitIfDefined(index, linkRef2);
				base.TrueExpression.Compile(cmp, reverse);
				cmp.EmitJump(linkRef);
				cmp.ResolveLink(linkRef2);
				base.FalseExpression.Compile(cmp, reverse);
			}
			cmp.ResolveLink(linkRef);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x00092B54 File Offset: 0x00090D54
		public override bool IsComplex()
		{
			if (this.group == null)
			{
				return this.Alternate.IsComplex();
			}
			return (base.TrueExpression != null && base.TrueExpression.IsComplex()) || (base.FalseExpression != null && base.FalseExpression.IsComplex()) || base.GetFixedWidth() <= 0;
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x00092BC0 File Offset: 0x00090DC0
		private ExpressionAssertion Alternate
		{
			get
			{
				if (this.alternate == null)
				{
					this.alternate = new ExpressionAssertion();
					this.alternate.TrueExpression = base.TrueExpression;
					this.alternate.FalseExpression = base.FalseExpression;
					this.alternate.TestExpression = this.literal;
				}
				return this.alternate;
			}
		}

		// Token: 0x04001B04 RID: 6916
		private ExpressionAssertion alternate;

		// Token: 0x04001B05 RID: 6917
		private CapturingGroup group;

		// Token: 0x04001B06 RID: 6918
		private Literal literal;
	}
}
