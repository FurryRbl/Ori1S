using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000027 RID: 39
	public interface IOrderedQueryable<T> : IEnumerable, IOrderedQueryable, IQueryable, IQueryable<T>, IEnumerable<T>
	{
	}
}
