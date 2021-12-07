using System;
using fsm;

// Token: 0x020004F2 RID: 1266
public class DashOwlState : IState
{
	// Token: 0x0600221D RID: 8733 RVA: 0x00095DF6 File Offset: 0x00093FF6
	public DashOwlState(DashOwlEnemy dashOwl)
	{
		this.DashOwl = dashOwl;
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x00095E05 File Offset: 0x00094005
	public virtual void UpdateState()
	{
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x00095E07 File Offset: 0x00094007
	public virtual void OnEnter()
	{
	}

	// Token: 0x06002220 RID: 8736 RVA: 0x00095E09 File Offset: 0x00094009
	public virtual void OnExit()
	{
	}

	// Token: 0x170005DF RID: 1503
	// (get) Token: 0x06002221 RID: 8737 RVA: 0x00095E0B File Offset: 0x0009400B
	public float CurrentStateTime
	{
		get
		{
			return this.DashOwl.Controller.StateMachine.CurrentStateTime;
		}
	}

	// Token: 0x04001CAD RID: 7341
	protected DashOwlEnemy DashOwl;
}
