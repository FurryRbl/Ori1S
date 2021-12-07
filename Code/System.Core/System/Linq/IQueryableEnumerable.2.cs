using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000021 RID: 33
	internal interface IQueryableEnumerable<TElement> : IQueryableEnumerable, IEnumerable, IOrderedQueryable, IQueryable, IQueryable<TElement>, IEnumerable<TElement>, IOrderedQueryable<TElement>
	{
	}
}
