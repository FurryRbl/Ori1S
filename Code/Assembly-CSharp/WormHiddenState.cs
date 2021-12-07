using System;

// Token: 0x02000603 RID: 1539
public class WormHiddenState : WormState
{
	// Token: 0x0600267D RID: 9853 RVA: 0x000A8D01 File Offset: 0x000A6F01
	public WormHiddenState(WormEnemy worm, TextureAnimationWithTransitions hidden) : base(worm)
	{
		this.m_hidden = hidden;
	}

	// Token: 0x0600267E RID: 9854 RVA: 0x000A8D14 File Offset: 0x000A6F14
	public override void OnEnter()
	{
		this.Worm.Animation.Play(this.m_hidden, 0, null);
		this.Worm.DamageDealer.Activated = false;
		this.Worm.Targetting.Activated = false;
		((MortarWormEnemy)this.Worm).HideGroup.SetActive(false);
	}

	// Token: 0x0600267F RID: 9855 RVA: 0x000A8D74 File Offset: 0x000A6F74
	public override void OnExit()
	{
		this.Worm.DamageDealer.Activated = true;
		this.Worm.Targetting.Activated = true;
		((MortarWormEnemy)this.Worm).HideGroup.SetActive(true);
	}

	// Token: 0x04002124 RID: 8484
	private readonly TextureAnimationWithTransitions m_hidden;
}
