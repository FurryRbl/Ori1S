using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000499 RID: 1177
	internal abstract class CompositeExpression : Expression
	{
		// Token: 0x06002A64 RID: 10852 RVA: 0x00092060 File Offset: 0x00090260
		public CompositeExpression()
		{
			this.expressions = new ExpressionCollection();
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x00092074 File Offset: 0x00090274
		protected ExpressionCollection Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0009207C File Offset: 0x0009027C
		protected void GetWidth(out int min, out int max, int count)
		{
			min = int.MaxValue;
			max = 0;
			bool flag = true;
			for (int i = 0; i < count; i++)
			{
				Expression expression = this.Expressions[i];
				if (expression != null)
				{
					flag = false;
					int num;
					int num2;
					expression.GetWidth(out num, out num2);
					if (num < min)
					{
						min = num;
					}
					if (num2 > max)
					{
						max = num2;
					}
				}
			}
			if (flag)
			{
				min = (max = 0);
			}
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000920F4 File Offset: 0x000902F4
		public override bool IsComplex()
		{
			foreach (object obj in this.Expressions)
			{
				Expression expression = (Expression)obj;
				if (expression.IsComplex())
				{
					return true;
				}
			}
			return base.GetFixedWidth() <= 0;
		}

		// Token: 0x04001AFC RID: 6908
		private ExpressionCollection expressions;
	}
}
