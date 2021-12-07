using System;

// Token: 0x02000576 RID: 1398
public class JumperEnemyChargingState : JumperEnemyState
{
	// Token: 0x06002436 RID: 9270 RVA: 0x0009E32C File Offset: 0x0009C52C
	public JumperEnemyChargingState(JumperEnemy jumperEnemy) : base(jumperEnemy)
	{
	}

	// Token: 0x06002437 RID: 9271 RVA: 0x0009E338 File Offset: 0x0009C538
	public override void OnEnter()
	{
		this.GroundEnemy.Animation.Play(this.JumperEnemy.Animations.JumpCharge, 0, null);
		this.GroundEnemy.PlatformMovement.LocalSpeedX = 0f;
	}

	// Token: 0x06002438 RID: 9272 RVA: 0x0009E37D File Offset: 0x0009C57D
	public override void OnExit()
	{
	}

	// Token: 0x06002439 RID: 9273 RVA: 0x0009E37F File Offset: 0x0009C57F
	public override void UpdateState()
	{
	}
}
