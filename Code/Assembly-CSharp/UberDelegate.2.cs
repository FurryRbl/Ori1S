using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class UberDelegate<T>
{
	// Token: 0x06000789 RID: 1929 RVA: 0x0001F784 File Offset: 0x0001D984
	public void Add(Action<T> act)
	{
		if (this.m_registers == null)
		{
			this.m_registers = new List<Action<T>>();
		}
		this.m_registers.Add(act);
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
	public void Call(T act)
	{
		if (this.m_registers != null)
		{
			for (int i = 0; i < this.m_registers.Count; i++)
			{
				Action<T> action = this.m_registers[i];
				try
				{
					action(act);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.ToString());
				}
			}
		}
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0001F824 File Offset: 0x0001DA24
	public void Remove(Action<T> act)
	{
		if (this.m_registers == null)
		{
			return;
		}
		if (!this.m_registers.RemoveUnordered(act))
		{
			Debug.LogError("Cannot remove delegate!");
		}
	}

	// Token: 0x040005C4 RID: 1476
	private List<Action<T>> m_registers;
}
