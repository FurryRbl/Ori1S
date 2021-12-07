using System;
using fsm;

// Token: 0x020005B9 RID: 1465
public class SlugState : IState
{
	// Token: 0x06002540 RID: 9536 RVA: 0x000A2AEF File Offset: 0x000A0CEF
	public SlugState(SlugEnemy slug)
	{
		this.Slug = slug;
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x000A2AFE File Offset: 0x000A0CFE
	public virtual void UpdateState()
	{
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x000A2B00 File Offset: 0x000A0D00
	public virtual void OnEnter()
	{
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x000A2B02 File Offset: 0x000A0D02
	public virtual void OnExit()
	{
	}

	// Token: 0x04001FD1 RID: 8145
	protected SlugEnemy Slug;
}
