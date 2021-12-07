using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000028 RID: 40
	public interface IOrderedEnumerable<TElement> : IEnumerable, IEnumerable<TElement>
	{
		// Token: 0x060002CC RID: 716
		IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> selector, IComparer<TKey> comparer, bool descending);
	}
}
