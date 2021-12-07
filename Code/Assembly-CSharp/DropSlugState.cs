using System;
using fsm;

// Token: 0x02000512 RID: 1298
public class DropSlugState : IState
{
	// Token: 0x060022AB RID: 8875 RVA: 0x00097E8B File Offset: 0x0009608B
	public DropSlugState(DropSlugEnemy slug)
	{
		this.Slug = slug;
	}

	// Token: 0x060022AC RID: 8876 RVA: 0x00097E9A File Offset: 0x0009609A
	public virtual void UpdateState()
	{
	}

	// Token: 0x060022AD RID: 8877 RVA: 0x00097E9C File Offset: 0x0009609C
	public virtual void OnEnter()
	{
	}

	// Token: 0x060022AE RID: 8878 RVA: 0x00097E9E File Offset: 0x0009609E
	public virtual void OnExit()
	{
	}

	// Token: 0x170005F3 RID: 1523
	// (get) Token: 0x060022AF RID: 8879 RVA: 0x00097EA0 File Offset: 0x000960A0
	public float CurrentStateTime
	{
		get
		{
			return this.Slug.Controller.StateMachine.CurrentStateTime;
		}
	}

	// Token: 0x04001D1F RID: 7455
	protected DropSlugEnemy Slug;
}
