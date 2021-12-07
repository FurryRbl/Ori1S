using System;

// Token: 0x02000579 RID: 1401
public class JumperEnemyStompedState : JumperEnemyState
{
	// Token: 0x06002443 RID: 9283 RVA: 0x0009E441 File Offset: 0x0009C641
	public JumperEnemyStompedState(JumperEnemy jumperEnemy) : base(jumperEnemy)
	{
	}

	// Token: 0x06002444 RID: 9284 RVA: 0x0009E44A File Offset: 0x0009C64A
	public override void UpdateState()
	{
		this.JumperEnemy.PlatformMovement.LocalSpeedX *= 1f - this.JumperEnemy.Settings.ThrownDrag;
	}

	// Token: 0x06002445 RID: 9285 RVA: 0x0009E479 File Offset: 0x0009C679
	public override void OnEnter()
	{
		this.JumperEnemy.RestartAnimationLoop(this.JumperEnemy.Animations.Stomped, 1);
	}

	// Token: 0x06002446 RID: 9286 RVA: 0x0009E497 File Offset: 0x0009C697
	public override void OnExit()
	{
	}
}
