using System;

// Token: 0x02000534 RID: 1332
public class ChangeDetector<T> where T : IComparable
{
	// Token: 0x0600232B RID: 9003 RVA: 0x0009A18C File Offset: 0x0009838C
	public bool CheckValueChanged(T t)
	{
		if (this.RecentValue.CompareTo(t) == 0)
		{
			return false;
		}
		this.RecentValue = t;
		return true;
	}

	// Token: 0x04001DA0 RID: 7584
	public T RecentValue;
}
