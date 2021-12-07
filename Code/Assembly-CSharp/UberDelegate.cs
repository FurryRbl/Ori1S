using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class UberDelegate
{
	// Token: 0x06000531 RID: 1329 RVA: 0x0001478C File Offset: 0x0001298C
	public void Add(Action act)
	{
		if (this.m_registers == null)
		{
			this.m_registers = new List<Action>();
		}
		this.m_registers.Add(act);
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x000147BC File Offset: 0x000129BC
	public void Call()
	{
		if (this.m_registers != null)
		{
			for (int i = 0; i < this.m_registers.Count; i++)
			{
				try
				{
					Action action = this.m_registers[i];
					action();
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00014824 File Offset: 0x00012A24
	public void Remove(Action act)
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

	// Token: 0x04000416 RID: 1046
	private List<Action> m_registers;
}
