using System;

// Token: 0x020005E0 RID: 1504
public class SpitterEnemyRunBackState : SpitterEnemyState
{
	// Token: 0x060025EA RID: 9706 RVA: 0x000A665B File Offset: 0x000A485B
	public SpitterEnemyRunBackState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x000A6664 File Offset: 0x000A4864
	public override void UpdateState()
	{
		float num = this.GroundEnemy.PlatformMovement.LocalSpeedX;
		num = MoonMath.Movement.AccelerateSpeed(num, this.SpitterEnemy.Settings.Acceleration, this.SpitterEnemy.Settings.RunSpeed, this.GroundEnemy.FaceLeft);
		this.GroundEnemy.PlatformMovement.LocalSpeedX = num;
		this.SpitterEnemy.FaceAwayFromPlayer();
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x000A66D0 File Offset: 0x000A48D0
	public override void OnEnter()
	{
		this.SpitterEnemy.FaceAwayFromPlayer();
		this.GroundEnemy.Animation.PlayLoop(this.SpitterEnemy.Animations.RunBack, 0, null, false);
		this.GroundEnemy.PlaySound(this.SpitterEnemy.RunAwaySound);
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x000A6722 File Offset: 0x000A4922
	public override void OnExit()
	{
		this.GroundEnemy.StopSound(this.SpitterEnemy.RunAwaySound);
	}
}
