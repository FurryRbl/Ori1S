using System;

// Token: 0x0200060D RID: 1549
public class WormChargingState : WormState
{
	// Token: 0x06002697 RID: 9879 RVA: 0x000A9583 File Offset: 0x000A7783
	public WormChargingState(WormEnemy worm, TextureAnimationWithTransitions charging, PrefabSpawner chargingEffect) : base(worm)
	{
		this.m_charging = charging;
		this.m_chargingEffect = chargingEffect;
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x000A959C File Offset: 0x000A779C
	public override void OnEnter()
	{
		this.Worm.Animation.Play(this.m_charging, 0, null);
		this.m_chargingEffect.Spawn(null);
	}

	// Token: 0x0400214F RID: 8527
	private readonly TextureAnimationWithTransitions m_charging;

	// Token: 0x04002150 RID: 8528
	private readonly PrefabSpawner m_chargingEffect;
}
