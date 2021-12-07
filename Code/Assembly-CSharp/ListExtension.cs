using System;
using System.Collections.Generic;

// Token: 0x02000483 RID: 1155
public static class ListExtension
{
	// Token: 0x06001F95 RID: 8085 RVA: 0x0008AFC4 File Offset: 0x000891C4
	public static bool RemoveAtUnordered<T>(this List<T> list, int index)
	{
		if (index < 0 || index >= list.Count)
		{
			return false;
		}
		list[index] = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
		return true;
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x0008B00C File Offset: 0x0008920C
	public static bool RemoveUnordered<T>(this List<T> list, T item)
	{
		int index = list.IndexOf(item);
		return list.RemoveAtUnordered(index);
	}
}
