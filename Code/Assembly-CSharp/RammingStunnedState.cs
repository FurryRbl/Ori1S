using System;

// Token: 0x02000595 RID: 1429
public class RammingStunnedState : RammingEnemyState
{
	// Token: 0x060024AA RID: 9386 RVA: 0x0009FE82 File Offset: 0x0009E082
	public RammingStunnedState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x0009FE8B File Offset: 0x0009E08B
	public override void OnEnter()
	{
		this.RammingEnemy.RestartAnimationLoop(this.RammingEnemy.Animations.Stunned, 0);
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x0009FEA9 File Offset: 0x0009E0A9
	public override void OnExit()
	{
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x0009FEAB File Offset: 0x0009E0AB
	public override void UpdateState()
	{
		this.RammingEnemy.PlatformMovement.LocalSpeedX *= 0.9f;
	}
}
