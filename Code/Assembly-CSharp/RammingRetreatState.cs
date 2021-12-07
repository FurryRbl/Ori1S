using System;

// Token: 0x02000596 RID: 1430
public class RammingRetreatState : RammingEnemyState
{
	// Token: 0x060024AE RID: 9390 RVA: 0x0009FEC9 File Offset: 0x0009E0C9
	public RammingRetreatState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x0009FED4 File Offset: 0x0009E0D4
	public override void OnEnter()
	{
		this.RammingEnemy.RestartAnimationLoop(this.RammingEnemy.Animations.Retreat, 0);
		if (this.RammingEnemy.EnemyInsideZone)
		{
			this.RammingEnemy.FaceLeft = (this.RammingEnemy.PositionToPlayerPosition.x > 0f);
		}
		else
		{
			this.RammingEnemy.FaceLeft = (this.RammingEnemy.PositionToStartPosition.x < 0f);
		}
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x0009FF5C File Offset: 0x0009E15C
	public override void OnExit()
	{
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x0009FF60 File Offset: 0x0009E160
	public override void UpdateState()
	{
		float retreatSpeed = this.RammingEnemy.Settings.RetreatSpeed;
		float num = this.RammingEnemy.Settings.RunningSpeedMultipliedOverTime.Evaluate(base.CurrentStateTime / this.RammingEnemy.Settings.AccelerationDuration);
		this.RammingEnemy.PlatformMovement.LocalSpeedX = ((!this.RammingEnemy.FaceLeft) ? retreatSpeed : (-retreatSpeed)) * num;
	}
}
