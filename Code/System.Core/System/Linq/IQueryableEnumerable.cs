using System;
using System.Collections;

namespace System.Linq
{
	// Token: 0x02000020 RID: 32
	internal interface IQueryableEnumerable : IEnumerable, IQueryable
	{
		// Token: 0x060002AA RID: 682
		IEnumerable GetEnumerable();
	}
}
