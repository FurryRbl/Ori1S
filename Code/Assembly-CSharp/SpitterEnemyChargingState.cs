using System;

// Token: 0x020005E1 RID: 1505
public class SpitterEnemyChargingState : SpitterEnemyState
{
	// Token: 0x060025EE RID: 9710 RVA: 0x000A673A File Offset: 0x000A493A
	public SpitterEnemyChargingState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x000A6743 File Offset: 0x000A4943
	public override void UpdateState()
	{
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x000A6748 File Offset: 0x000A4948
	public override void OnEnter()
	{
		this.GroundEnemy.FacePlayer();
		this.GroundEnemy.PlatformMovement.LocalSpeedX = 0f;
		this.GroundEnemy.Animation.PlayLoop(this.SpitterEnemy.Animations.Charging, 0, null, false);
		this.GroundEnemy.PlaySound(this.SpitterEnemy.AttackSound);
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x000A67AF File Offset: 0x000A49AF
	public override void OnExit()
	{
	}
}
