using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200002C RID: 44
	public interface ILookup<TKey, TElement> : IEnumerable, IEnumerable<IGrouping<TKey, TElement>>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002D7 RID: 727
		int Count { get; }

		// Token: 0x1700002D RID: 45
		IEnumerable<TElement> this[TKey key]
		{
			get;
		}

		// Token: 0x060002D9 RID: 729
		bool Contains(TKey key);
	}
}
