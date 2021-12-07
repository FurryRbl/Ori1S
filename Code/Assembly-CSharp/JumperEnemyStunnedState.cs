using System;

// Token: 0x0200057A RID: 1402
public class JumperEnemyStunnedState : JumperEnemyState
{
	// Token: 0x06002447 RID: 9287 RVA: 0x0009E499 File Offset: 0x0009C699
	public JumperEnemyStunnedState(JumperEnemy jumperEnemy) : base(jumperEnemy)
	{
	}

	// Token: 0x06002448 RID: 9288 RVA: 0x0009E4A2 File Offset: 0x0009C6A2
	public override void UpdateState()
	{
		this.JumperEnemy.PlatformMovement.LocalSpeedX *= 0.9f;
	}

	// Token: 0x06002449 RID: 9289 RVA: 0x0009E4C0 File Offset: 0x0009C6C0
	public override void OnEnter()
	{
		this.JumperEnemy.PlayAnimationOnce(this.JumperEnemy.Animations.Confused, 1);
	}

	// Token: 0x0600244A RID: 9290 RVA: 0x0009E4DE File Offset: 0x0009C6DE
	public override void OnExit()
	{
	}
}
