using System;
using Core;

// Token: 0x02000594 RID: 1428
public class RammingHitWallState : RammingEnemyState
{
	// Token: 0x060024A6 RID: 9382 RVA: 0x0009FD70 File Offset: 0x0009DF70
	public RammingHitWallState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
	}

	// Token: 0x060024A7 RID: 9383 RVA: 0x0009FD7C File Offset: 0x0009DF7C
	public override void OnEnter()
	{
		this.RammingEnemy.RestartAnimationLoop(this.RammingEnemy.Animations.HitWall, 0);
		this.RammingEnemy.FaceLeft = this.RammingEnemy.PlatformMovement.HasWallLeft;
		this.RammingEnemy.SpawnPrefab(this.RammingEnemy.HitWallEffect);
		if (this.RammingEnemy.Sounds.OnHitWall)
		{
			Sound.Play(this.RammingEnemy.Sounds.OnHitWall.GetSound(null), this.RammingEnemy.transform.position, null);
		}
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x0009FE1D File Offset: 0x0009E01D
	public override void OnExit()
	{
	}

	// Token: 0x060024A9 RID: 9385 RVA: 0x0009FE20 File Offset: 0x0009E020
	public override void UpdateState()
	{
		this.RammingEnemy.PlatformMovement.LocalSpeedX = this.RammingEnemy.Settings.HitWallStunSpeed * (float)((!this.RammingEnemy.FaceLeft) ? -1 : 1) * this.RammingEnemy.Settings.BouncingSpeedMultiplierOverTime.Evaluate(base.CurrentStateTime);
	}
}
