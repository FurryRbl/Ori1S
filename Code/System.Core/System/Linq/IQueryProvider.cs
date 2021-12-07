using System;
using System.Linq.Expressions;

namespace System.Linq
{
	// Token: 0x02000034 RID: 52
	public interface IQueryProvider
	{
		// Token: 0x06000370 RID: 880
		IQueryable CreateQuery(Expression expression);

		// Token: 0x06000371 RID: 881
		object Execute(Expression expression);

		// Token: 0x06000372 RID: 882
		IQueryable<TElement> CreateQuery<TElement>(Expression expression);

		// Token: 0x06000373 RID: 883
		TResult Execute<TResult>(Expression expression);
	}
}
