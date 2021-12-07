using System;
using Core;

// Token: 0x02000956 RID: 2390
public class Cheat
{
	// Token: 0x060034A0 RID: 13472 RVA: 0x000DCDCC File Offset: 0x000DAFCC
	public Cheat(Input.InputButtonProcessor[] combination, Action action, Func<bool> condition)
	{
		this.m_combination = combination;
		this.m_action = action;
		this.m_condition = condition;
	}

	// Token: 0x17000848 RID: 2120
	// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000DCDE9 File Offset: 0x000DAFE9
	public Input.InputButtonProcessor[] Combination
	{
		get
		{
			return this.m_combination;
		}
	}

	// Token: 0x060034A2 RID: 13474 RVA: 0x000DCDF4 File Offset: 0x000DAFF4
	public void PerformCheat()
	{
		if (this.m_condition == null || this.m_condition())
		{
			this.m_action();
		}
	}

	// Token: 0x04002F73 RID: 12147
	private readonly Input.InputButtonProcessor[] m_combination;

	// Token: 0x04002F74 RID: 12148
	private readonly Action m_action;

	// Token: 0x04002F75 RID: 12149
	private readonly Func<bool> m_condition;

	// Token: 0x04002F76 RID: 12150
	public bool Processing;
}
