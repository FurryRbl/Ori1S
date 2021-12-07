using System;
using fsm;

// Token: 0x0200060E RID: 1550
public class WormState : IState
{
	// Token: 0x06002699 RID: 9881 RVA: 0x000A95CF File Offset: 0x000A77CF
	public WormState(WormEnemy worm)
	{
		this.Worm = worm;
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x000A95DE File Offset: 0x000A77DE
	public virtual void UpdateState()
	{
	}

	// Token: 0x0600269B RID: 9883 RVA: 0x000A95E0 File Offset: 0x000A77E0
	public virtual void OnEnter()
	{
	}

	// Token: 0x0600269C RID: 9884 RVA: 0x000A95E2 File Offset: 0x000A77E2
	public virtual void OnExit()
	{
	}

	// Token: 0x17000622 RID: 1570
	// (get) Token: 0x0600269D RID: 9885 RVA: 0x000A95E4 File Offset: 0x000A77E4
	public float CurrentStateTime
	{
		get
		{
			return this.Worm.Controller.StateMachine.CurrentStateTime;
		}
	}

	// Token: 0x04002151 RID: 8529
	protected WormEnemy Worm;
}
