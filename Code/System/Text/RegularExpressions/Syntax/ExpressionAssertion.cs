using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A2 RID: 1186
	internal class ExpressionAssertion : Assertion
	{
		// Token: 0x06002A9B RID: 10907 RVA: 0x00092C1C File Offset: 0x00090E1C
		public ExpressionAssertion()
		{
			base.Expressions.Add(null);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x00092C30 File Offset: 0x00090E30
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x00092C38 File Offset: 0x00090E38
		public bool Reverse
		{
			get
			{
				return this.reverse;
			}
			set
			{
				this.reverse = value;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x00092C44 File Offset: 0x00090E44
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x00092C4C File Offset: 0x00090E4C
		public bool Negate
		{
			get
			{
				return this.negate;
			}
			set
			{
				this.negate = value;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x00092C58 File Offset: 0x00090E58
		// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x00092C68 File Offset: 0x00090E68
		public Expression TestExpression
		{
			get
			{
				return base.Expressions[2];
			}
			set
			{
				base.Expressions[2] = value;
			}
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x00092C78 File Offset: 0x00090E78
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			LinkRef linkRef2 = cmp.NewLink();
			if (!this.negate)
			{
				cmp.EmitTest(linkRef, linkRef2);
			}
			else
			{
				cmp.EmitTest(linkRef2, linkRef);
			}
			this.TestExpression.Compile(cmp, this.reverse);
			cmp.EmitTrue();
			if (base.TrueExpression == null)
			{
				cmp.ResolveLink(linkRef2);
				cmp.EmitFalse();
				cmp.ResolveLink(linkRef);
			}
			else
			{
				cmp.ResolveLink(linkRef);
				base.TrueExpression.Compile(cmp, reverse);
				if (base.FalseExpression == null)
				{
					cmp.ResolveLink(linkRef2);
				}
				else
				{
					LinkRef linkRef3 = cmp.NewLink();
					cmp.EmitJump(linkRef3);
					cmp.ResolveLink(linkRef2);
					base.FalseExpression.Compile(cmp, reverse);
					cmp.ResolveLink(linkRef3);
				}
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x00092D44 File Offset: 0x00090F44
		public override bool IsComplex()
		{
			return true;
		}

		// Token: 0x04001B07 RID: 6919
		private bool reverse;

		// Token: 0x04001B08 RID: 6920
		private bool negate;
	}
}
