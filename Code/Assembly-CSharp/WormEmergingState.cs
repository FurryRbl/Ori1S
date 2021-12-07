using System;

// Token: 0x02000602 RID: 1538
public class WormEmergingState : WormState
{
	// Token: 0x0600267B RID: 9851 RVA: 0x000A8C5B File Offset: 0x000A6E5B
	public WormEmergingState(WormEnemy worm, TextureAnimationWithTransitions emerging, PrefabSpawner emergingEffect, SoundSource emergingSound) : base(worm)
	{
		this.m_emerging = emerging;
		this.m_emergingEffect = emergingEffect;
		this.m_emergingSound = emergingSound;
	}

	// Token: 0x0600267C RID: 9852 RVA: 0x000A8C7C File Offset: 0x000A6E7C
	public override void OnEnter()
	{
		this.Worm.Animation.Play(this.m_emerging, 0, null);
		if (this.m_emergingEffect)
		{
			this.m_emergingEffect.Spawn(null);
		}
		if (this.m_emergingSound)
		{
			this.m_emergingSound.Play();
		}
		MortarWormEnemy mortarWormEnemy = (MortarWormEnemy)this.Worm;
		if (mortarWormEnemy.WormHole)
		{
			mortarWormEnemy.WormHole.OnEmerge();
		}
	}

	// Token: 0x04002121 RID: 8481
	private readonly TextureAnimationWithTransitions m_emerging;

	// Token: 0x04002122 RID: 8482
	private readonly PrefabSpawner m_emergingEffect;

	// Token: 0x04002123 RID: 8483
	private readonly SoundSource m_emergingSound;
}
