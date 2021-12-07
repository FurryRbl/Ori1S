using System;
using fsm.triggers;
using UnityEngine;

// Token: 0x02000504 RID: 1284
public class DashOwlHurtState : DashOwlState
{
	// Token: 0x06002283 RID: 8835 RVA: 0x000972E8 File Offset: 0x000954E8
	public DashOwlHurtState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x000972F4 File Offset: 0x000954F4
	public override void OnEnter()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.DashOwl.Controller.StateMachine.CurrentTrigger;
		if ((float)this.DashOwl.FaceLeftSign == Mathf.Sign(onReceiveDamage.Damage.Force.x))
		{
			this.DashOwl.Animation.Play(this.DashOwl.Animations.HurtBack, 0, null);
		}
		else
		{
			this.DashOwl.Animation.Play(this.DashOwl.Animations.HurtFront, 0, null);
		}
		this.DashOwl.Controller.OnSeinNearStay();
		this.DashOwl.FlyMovement.Velocity = Vector2.zero;
	}

	// Token: 0x04001CEB RID: 7403
	private readonly TextureAnimationWithTransitions m_hurtFront;

	// Token: 0x04001CEC RID: 7404
	private readonly TextureAnimationWithTransitions m_hurtBack;
}
