using System;

// Token: 0x020005BC RID: 1468
public class AcidSlugChargingState : AcidSlugState
{
	// Token: 0x06002546 RID: 9542 RVA: 0x000A2B14 File Offset: 0x000A0D14
	public AcidSlugChargingState(AcidSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x000A2B20 File Offset: 0x000A0D20
	public override void OnEnter()
	{
		this.AcidSlugEnemy.PlaySound(this.AcidSlugEnemy.ChargingSoundSource);
		this.AcidSlugEnemy.PlayAnimationOnce(this.AcidSlugEnemy.Animations.Charging, 0);
	}
}
