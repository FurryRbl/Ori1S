using System;
using fsm;

// Token: 0x0200054E RID: 1358
public abstract class GroundEnemyState : IState
{
	// Token: 0x0600237C RID: 9084 RVA: 0x0009B1B5 File Offset: 0x000993B5
	public GroundEnemyState(GroundEnemy groundEnemy)
	{
		this.GroundEnemy = groundEnemy;
	}

	// Token: 0x170005FD RID: 1533
	// (get) Token: 0x0600237D RID: 9085 RVA: 0x0009B1C4 File Offset: 0x000993C4
	public float CurrentStateTime
	{
		get
		{
			return this.GroundEnemy.Controller.StateMachine.CurrentStateTime;
		}
	}

	// Token: 0x0600237E RID: 9086
	public abstract void UpdateState();

	// Token: 0x0600237F RID: 9087
	public abstract void OnEnter();

	// Token: 0x06002380 RID: 9088
	public abstract void OnExit();

	// Token: 0x04001DC6 RID: 7622
	protected GroundEnemy GroundEnemy;
}
