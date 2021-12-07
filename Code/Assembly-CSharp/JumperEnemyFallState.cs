using System;

// Token: 0x02000577 RID: 1399
public class JumperEnemyFallState : JumperEnemyState
{
	// Token: 0x0600243A RID: 9274 RVA: 0x0009E381 File Offset: 0x0009C581
	public JumperEnemyFallState(JumperEnemy jumperEnemy) : base(jumperEnemy)
	{
	}

	// Token: 0x0600243B RID: 9275 RVA: 0x0009E38A File Offset: 0x0009C58A
	public override void UpdateState()
	{
	}

	// Token: 0x0600243C RID: 9276 RVA: 0x0009E38C File Offset: 0x0009C58C
	public override void OnEnter()
	{
		this.JumperEnemy.Animation.PlayLoop(this.JumperEnemy.Animations.Fall, 0, () => !this.GroundEnemy.PlatformMovement.IsOnGround, false);
	}

	// Token: 0x0600243D RID: 9277 RVA: 0x0009E3BD File Offset: 0x0009C5BD
	public override void OnExit()
	{
	}
}
