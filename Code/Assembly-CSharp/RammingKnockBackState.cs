using System;

// Token: 0x0200058E RID: 1422
public class RammingKnockBackState : RammingEnemyState
{
	// Token: 0x06002490 RID: 9360 RVA: 0x0009F900 File Offset: 0x0009DB00
	public RammingKnockBackState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
	}

	// Token: 0x06002491 RID: 9361 RVA: 0x0009F909 File Offset: 0x0009DB09
	public override void OnEnter()
	{
		this.RammingEnemy.RestartAnimationLoop(this.RammingEnemy.Animations.KnockBack, 0);
	}

	// Token: 0x06002492 RID: 9362 RVA: 0x0009F927 File Offset: 0x0009DB27
	public override void OnExit()
	{
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x0009F92C File Offset: 0x0009DB2C
	public override void UpdateState()
	{
		this.RammingEnemy.PlatformMovement.LocalSpeedX = this.RammingEnemy.Settings.KnockBackSpeed * (float)((!this.RammingEnemy.FaceLeft) ? -1 : 1) * this.RammingEnemy.Settings.BouncingSpeedMultiplierOverTime.Evaluate(base.CurrentStateTime);
	}
}
