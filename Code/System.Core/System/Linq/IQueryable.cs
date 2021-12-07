using System;
using System.Collections;
using System.Linq.Expressions;

namespace System.Linq
{
	// Token: 0x02000029 RID: 41
	public interface IQueryable : IEnumerable
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002CD RID: 717
		Type ElementType { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002CE RID: 718
		Expression Expression { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002CF RID: 719
		IQueryProvider Provider { get; }
	}
}
