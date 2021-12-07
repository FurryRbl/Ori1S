using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200002A RID: 42
	public interface IQueryable<T> : IEnumerable, IQueryable, IEnumerable<T>
	{
	}
}
