using System;

// Token: 0x020005C8 RID: 1480
public class StarSlugChargingState : StarSlugState
{
	// Token: 0x06002565 RID: 9573 RVA: 0x000A343E File Offset: 0x000A163E
	public StarSlugChargingState(StarSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x000A3448 File Offset: 0x000A1648
	public override void OnEnter()
	{
		this.StarSlugEnemy.PlaySound(this.StarSlugEnemy.ChargingSoundSource);
		this.StarSlugEnemy.PlayAnimationOnce(this.StarSlugEnemy.Animations.Charging, 0);
	}
}
