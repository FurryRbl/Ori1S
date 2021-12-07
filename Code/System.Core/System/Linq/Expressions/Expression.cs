using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000003 RID: 3
	public sealed class Expression<TDelegate> : LambdaExpression
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		internal Expression(Expression body, ReadOnlyCollection<ParameterExpression> parameters) : base(typeof(TDelegate), body, parameters)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002108 File Offset: 0x00000308
		public new TDelegate Compile()
		{
			return (TDelegate)((object)base.Compile());
		}
	}
}
