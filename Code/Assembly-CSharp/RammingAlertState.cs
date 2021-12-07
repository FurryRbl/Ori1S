using System;

// Token: 0x02000591 RID: 1425
public class RammingAlertState : RammingEnemyState
{
	// Token: 0x06002499 RID: 9369 RVA: 0x0009FADA File Offset: 0x0009DCDA
	public RammingAlertState(RammingEnemy groundEnemy) : base(groundEnemy)
	{
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x0009FAE4 File Offset: 0x0009DCE4
	public override void OnEnter()
	{
		this.GroundEnemy.Animation.PlayLoop(this.RammingEnemy.Animations.Alert, 0, null, false);
		this.GroundEnemy.PlaySound(this.RammingEnemy.Sounds.Alert);
		this.GroundEnemy.PlatformMovement.LocalSpeedX = 0f;
		this.GroundEnemy.FacePlayer();
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x0009FB50 File Offset: 0x0009DD50
	public override void OnExit()
	{
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x0009FB52 File Offset: 0x0009DD52
	public override void UpdateState()
	{
		this.GroundEnemy.FacePlayer();
	}
}
