using System;

// Token: 0x020005DE RID: 1502
public class SpitterEnemyIdleState : SpitterEnemyState
{
	// Token: 0x060025E2 RID: 9698 RVA: 0x000A64F0 File Offset: 0x000A46F0
	public SpitterEnemyIdleState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x000A64F9 File Offset: 0x000A46F9
	public override void UpdateState()
	{
		this.SpitterEnemy.PlatformMovement.LocalSpeedX = MoonMath.Movement.DecelerateSpeed(this.GroundEnemy.PlatformMovement.LocalSpeedX, this.SpitterEnemy.Settings.Deceleration);
	}

	// Token: 0x060025E4 RID: 9700 RVA: 0x000A6530 File Offset: 0x000A4730
	public override void OnEnter()
	{
		this.GroundEnemy.Animation.PlayLoop(this.SpitterEnemy.Animations.Idle, 0, null, false);
		this.GroundEnemy.PlaySound(this.SpitterEnemy.IdleSound);
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x000A6577 File Offset: 0x000A4777
	public override void OnExit()
	{
		this.GroundEnemy.StopSound(this.SpitterEnemy.IdleSound);
	}
}
