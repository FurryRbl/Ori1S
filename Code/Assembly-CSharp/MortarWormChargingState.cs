using System;

// Token: 0x02000601 RID: 1537
public class MortarWormChargingState : WormChargingState
{
	// Token: 0x06002679 RID: 9849 RVA: 0x000A8C01 File Offset: 0x000A6E01
	public MortarWormChargingState(MortarWormEnemy mortarWormEnemy, TextureAnimationWithTransitions charging, PrefabSpawner chargingEffect, SoundSource chargingSound) : base(mortarWormEnemy, charging, chargingEffect)
	{
		this.m_chargingEffect = chargingEffect;
		this.m_chargingSound = chargingSound;
	}

	// Token: 0x0600267A RID: 9850 RVA: 0x000A8C1B File Offset: 0x000A6E1B
	public override void OnEnter()
	{
		if (this.m_chargingEffect)
		{
			this.m_chargingEffect.Spawn(null);
		}
		if (this.m_chargingSound)
		{
			this.m_chargingSound.Play();
		}
		base.OnEnter();
	}

	// Token: 0x0400211F RID: 8479
	private readonly PrefabSpawner m_chargingEffect;

	// Token: 0x04002120 RID: 8480
	private readonly SoundSource m_chargingSound;
}
