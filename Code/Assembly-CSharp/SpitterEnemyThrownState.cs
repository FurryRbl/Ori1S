using System;
using fsm.triggers;

// Token: 0x020005E3 RID: 1507
public class SpitterEnemyThrownState : SpitterEnemyState
{
	// Token: 0x060025F9 RID: 9721 RVA: 0x000A6A4B File Offset: 0x000A4C4B
	public SpitterEnemyThrownState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x000A6A54 File Offset: 0x000A4C54
	public override void UpdateState()
	{
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x000A6A56 File Offset: 0x000A4C56
	public override void OnEnter()
	{
		this.SpitterEnemy.RestartAnimationLoop(this.SpitterEnemy.Animations.Thrown, 0);
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x000A6A74 File Offset: 0x000A4C74
	public override void OnExit()
	{
		this.SpitterEnemy.LandSound.Play();
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x000A6A88 File Offset: 0x000A4C88
	public void OnThrow()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.SpitterEnemy.Controller.StateMachine.CurrentTrigger;
		this.SpitterEnemy.PlatformMovement.WorldSpeed = onReceiveDamage.Damage.Force * 10f;
		this.SpitterEnemy.ThrownDirection = onReceiveDamage.Damage.Force.normalized;
		this.SpitterEnemy.FaceLeft = (this.SpitterEnemy.PlatformMovement.LocalSpeedX < 0f);
	}
}
