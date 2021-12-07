using System;
using fsm.triggers;

// Token: 0x020005E4 RID: 1508
public class SpitterEnemyStompedState : SpitterEnemyState
{
	// Token: 0x060025FE RID: 9726 RVA: 0x000A6B15 File Offset: 0x000A4D15
	public SpitterEnemyStompedState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x000A6B1E File Offset: 0x000A4D1E
	public override void UpdateState()
	{
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x000A6B20 File Offset: 0x000A4D20
	public override void OnEnter()
	{
		this.SpitterEnemy.RestartAnimationLoop(this.SpitterEnemy.Animations.Stomped, 0);
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x000A6B3E File Offset: 0x000A4D3E
	public override void OnExit()
	{
	}

	// Token: 0x06002602 RID: 9730 RVA: 0x000A6B40 File Offset: 0x000A4D40
	public void OnStomped()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.SpitterEnemy.Controller.StateMachine.CurrentTrigger;
		this.SpitterEnemy.PlatformMovement.WorldSpeed = onReceiveDamage.Damage.Force * 8f;
		this.SpitterEnemy.ThrownDirection = onReceiveDamage.Damage.Force.normalized;
		this.SpitterEnemy.FaceLeft = (this.SpitterEnemy.PlatformMovement.LocalSpeedX < 0f);
	}
}
