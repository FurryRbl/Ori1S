using System;

// Token: 0x020005E5 RID: 1509
public class SpitterEnemyStunnedState : SpitterEnemyState
{
	// Token: 0x06002603 RID: 9731 RVA: 0x000A6BCD File Offset: 0x000A4DCD
	public SpitterEnemyStunnedState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x06002604 RID: 9732 RVA: 0x000A6BD6 File Offset: 0x000A4DD6
	public override void UpdateState()
	{
		this.SpitterEnemy.PlatformMovement.LocalSpeedX *= 1f - this.SpitterEnemy.Settings.ThrownDrag;
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x000A6C05 File Offset: 0x000A4E05
	public override void OnEnter()
	{
		this.SpitterEnemy.PlatformMovement.LocalSpeedX *= 0.9f;
		this.SpitterEnemy.RestartAnimationLoop(this.SpitterEnemy.Animations.Stunned, 0);
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x000A6C3F File Offset: 0x000A4E3F
	public override void OnExit()
	{
	}
}
