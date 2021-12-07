using System;

// Token: 0x02000604 RID: 1540
public class WormHidingState : WormState
{
	// Token: 0x06002680 RID: 9856 RVA: 0x000A8DB9 File Offset: 0x000A6FB9
	public WormHidingState(WormEnemy worm, TextureAnimationWithTransitions hiding, PrefabSpawner hidingEffect, SoundSource hidingSound) : base(worm)
	{
		this.m_hiding = hiding;
		this.m_hidingSound = hidingSound;
		this.m_hidingEffect = hidingEffect;
	}

	// Token: 0x06002681 RID: 9857 RVA: 0x000A8DD8 File Offset: 0x000A6FD8
	public override void OnEnter()
	{
		this.Worm.Animation.Play(this.m_hiding, 0, null);
		if (this.m_hidingEffect)
		{
			this.m_hidingEffect.Spawn(null);
		}
		if (this.m_hidingSound)
		{
			this.m_hidingSound.Play();
		}
	}

	// Token: 0x04002125 RID: 8485
	private readonly TextureAnimationWithTransitions m_hiding;

	// Token: 0x04002126 RID: 8486
	private readonly PrefabSpawner m_hidingEffect;

	// Token: 0x04002127 RID: 8487
	private readonly SoundSource m_hidingSound;
}
