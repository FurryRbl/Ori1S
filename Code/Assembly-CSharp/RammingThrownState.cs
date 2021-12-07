using System;

// Token: 0x0200059B RID: 1435
public class RammingThrownState : RammingEnemyState
{
	// Token: 0x060024B8 RID: 9400 RVA: 0x000A0172 File Offset: 0x0009E372
	public RammingThrownState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
	}

	// Token: 0x060024B9 RID: 9401 RVA: 0x000A017B File Offset: 0x0009E37B
	public override void OnEnter()
	{
		this.RammingEnemy.RestartAnimationLoop(this.RammingEnemy.Animations.Thrown, 0);
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x000A0199 File Offset: 0x0009E399
	public override void OnExit()
	{
	}

	// Token: 0x060024BB RID: 9403 RVA: 0x000A019C File Offset: 0x0009E39C
	public override void UpdateState()
	{
		this.RammingEnemy.PlatformMovement.LocalSpeed *= 0.98f;
	}
}
