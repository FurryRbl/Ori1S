using System;

// Token: 0x02000590 RID: 1424
public class RammingIdleState : RammingEnemyState
{
	// Token: 0x06002495 RID: 9365 RVA: 0x0009FA4F File Offset: 0x0009DC4F
	public RammingIdleState(RammingEnemy groundEnemy) : base(groundEnemy)
	{
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x0009FA58 File Offset: 0x0009DC58
	public override void UpdateState()
	{
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x0009FA5C File Offset: 0x0009DC5C
	public override void OnEnter()
	{
		this.GroundEnemy.Animation.PlayLoop(this.RammingEnemy.Animations.Idle, 0, null, false);
		this.GroundEnemy.PlaySound(this.RammingEnemy.Sounds.Idle);
		this.GroundEnemy.PlatformMovement.LocalSpeedX = 0f;
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x0009FABD File Offset: 0x0009DCBD
	public override void OnExit()
	{
		this.GroundEnemy.StopSound(this.RammingEnemy.Sounds.Idle);
	}
}
