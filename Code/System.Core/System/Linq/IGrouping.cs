using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000025 RID: 37
	public interface IGrouping<TKey, TElement> : IEnumerable, IEnumerable<TElement>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002CB RID: 715
		TKey Key { get; }
	}
}
