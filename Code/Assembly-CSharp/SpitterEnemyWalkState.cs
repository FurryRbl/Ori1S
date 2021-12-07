using System;

// Token: 0x020005DF RID: 1503
public class SpitterEnemyWalkState : SpitterEnemyState
{
	// Token: 0x060025E6 RID: 9702 RVA: 0x000A658F File Offset: 0x000A478F
	public SpitterEnemyWalkState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x000A6598 File Offset: 0x000A4798
	public override void UpdateState()
	{
		float num = this.GroundEnemy.PlatformMovement.LocalSpeedX;
		num = MoonMath.Movement.AccelerateSpeed(num, this.SpitterEnemy.Settings.Acceleration, this.SpitterEnemy.Settings.WalkSpeed, this.GroundEnemy.FaceLeft);
		this.GroundEnemy.PlatformMovement.LocalSpeedX = num;
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x000A65FC File Offset: 0x000A47FC
	public override void OnEnter()
	{
		this.GroundEnemy.Animation.PlayLoop(this.SpitterEnemy.Animations.Walk, 0, null, false);
		this.GroundEnemy.PlaySound(this.SpitterEnemy.WalkSound);
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x000A6643 File Offset: 0x000A4843
	public override void OnExit()
	{
		this.GroundEnemy.StopSound(this.SpitterEnemy.WalkSound);
	}
}
