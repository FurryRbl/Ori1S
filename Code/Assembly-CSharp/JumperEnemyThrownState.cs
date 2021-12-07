using System;

// Token: 0x02000578 RID: 1400
public class JumperEnemyThrownState : JumperEnemyState
{
	// Token: 0x0600243F RID: 9279 RVA: 0x0009E3D4 File Offset: 0x0009C5D4
	public JumperEnemyThrownState(JumperEnemy jumperEnemy) : base(jumperEnemy)
	{
	}

	// Token: 0x06002440 RID: 9280 RVA: 0x0009E3DD File Offset: 0x0009C5DD
	public override void UpdateState()
	{
		this.JumperEnemy.PlatformMovement.LocalSpeedX *= 1f - this.JumperEnemy.Settings.ThrownDrag;
	}

	// Token: 0x06002441 RID: 9281 RVA: 0x0009E40C File Offset: 0x0009C60C
	public override void OnEnter()
	{
		this.JumperEnemy.RestartAnimationLoop(this.JumperEnemy.Animations.Thrown, 1);
	}

	// Token: 0x06002442 RID: 9282 RVA: 0x0009E42A File Offset: 0x0009C62A
	public override void OnExit()
	{
		this.JumperEnemy.Sounds.FallOnGround.Play();
	}
}
