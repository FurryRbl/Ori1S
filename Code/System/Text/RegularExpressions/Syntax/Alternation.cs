using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A3 RID: 1187
	internal class Alternation : CompositeExpression
	{
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x00092D50 File Offset: 0x00090F50
		public ExpressionCollection Alternatives
		{
			get
			{
				return base.Expressions;
			}
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x00092D58 File Offset: 0x00090F58
		public void AddAlternative(Expression e)
		{
			this.Alternatives.Add(e);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00092D68 File Offset: 0x00090F68
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			foreach (object obj in this.Alternatives)
			{
				Expression expression = (Expression)obj;
				LinkRef linkRef2 = cmp.NewLink();
				cmp.EmitBranch(linkRef2);
				expression.Compile(cmp, reverse);
				cmp.EmitJump(linkRef);
				cmp.ResolveLink(linkRef2);
				cmp.EmitBranchEnd();
			}
			cmp.EmitFalse();
			cmp.ResolveLink(linkRef);
			cmp.EmitAlternationEnd();
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00092E18 File Offset: 0x00091018
		public override void GetWidth(out int min, out int max)
		{
			base.GetWidth(out min, out max, this.Alternatives.Count);
		}
	}
}
