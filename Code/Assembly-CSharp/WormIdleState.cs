using System;

// Token: 0x0200060C RID: 1548
public class WormIdleState : WormState
{
	// Token: 0x06002695 RID: 9877 RVA: 0x000A9552 File Offset: 0x000A7752
	public WormIdleState(WormEnemy worm, TextureAnimationWithTransitions idle) : base(worm)
	{
		this.m_idle = idle;
	}

	// Token: 0x06002696 RID: 9878 RVA: 0x000A9562 File Offset: 0x000A7762
	public override void OnEnter()
	{
		this.Worm.Animation.Play(this.m_idle, 0, null);
		base.OnEnter();
	}

	// Token: 0x0400214E RID: 8526
	private readonly TextureAnimationWithTransitions m_idle;
}
