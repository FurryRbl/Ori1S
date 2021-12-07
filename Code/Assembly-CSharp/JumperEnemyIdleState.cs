using System;

// Token: 0x0200054F RID: 1359
public class JumperEnemyIdleState : JumperEnemyState
{
	// Token: 0x06002381 RID: 9089 RVA: 0x0009B1DB File Offset: 0x000993DB
	public JumperEnemyIdleState(JumperEnemy jumperEnemy) : base(jumperEnemy)
	{
	}

	// Token: 0x06002382 RID: 9090 RVA: 0x0009B1E4 File Offset: 0x000993E4
	public override void UpdateState()
	{
		this.GroundEnemy.PlatformMovement.LocalSpeedX = MoonMath.Movement.DecelerateSpeed(this.GroundEnemy.PlatformMovement.LocalSpeedX, 100f);
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x0009B21C File Offset: 0x0009941C
	public override void OnEnter()
	{
		this.GroundEnemy.Animation.PlayLoop(this.JumperEnemy.Animations.Idle, 0, null, false);
		this.GroundEnemy.PlaySound(this.JumperEnemy.Sounds.Idle);
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x0009B268 File Offset: 0x00099468
	public override void OnExit()
	{
		this.GroundEnemy.StopSound(this.JumperEnemy.Sounds.Idle);
	}
}
