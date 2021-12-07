using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200009E RID: 158
	internal static class ListPool<T>
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x000185F0 File Offset: 0x000167F0
		public static List<T> Get()
		{
			return ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000185FC File Offset: 0x000167FC
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x040002A5 RID: 677
		private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
