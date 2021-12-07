using System;
using fsm;
using fsm.triggers;
using Game;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class EntityController : SaveSerialize, INearSeinReceiver, IDamageReciever
{
	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001477 RID: 5239 RVA: 0x0005C990 File Offset: 0x0005AB90
	private SpriteEntity SpriteEntity
	{
		get
		{
			return this.Entity as SpriteEntity;
		}
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x0005C99D File Offset: 0x0005AB9D
	public void OnValidate()
	{
		this.Entity = base.transform.FindComponentUpwards<Entity>();
		this.Entity.Controller = this;
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x0005C9BC File Offset: 0x0005ABBC
	public new void Awake()
	{
		base.Awake();
		if (this.Entity == null)
		{
			this.OnValidate();
		}
		if (this.SpriteEntity && this.SpriteEntity.Animation)
		{
			this.SpriteEntity.Animation.Animator.OnAnimationEndEvent += this.OnAnimationEnd;
		}
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x0005CA2C File Offset: 0x0005AC2C
	public void FixedUpdate()
	{
		if (this.Entity.IsSuspended)
		{
			return;
		}
		if (this.m_transManager == null)
		{
			this.m_transManager = this.StateMachine.GetTransistionManager<OnFixedUpdate>();
		}
		if (this.m_transManager == null)
		{
			return;
		}
		this.StateMachine.UpdateState(Time.deltaTime);
		this.StateMachine.CurrentTrigger = null;
		this.m_transManager.Process(this.StateMachine);
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x0005CAA0 File Offset: 0x0005ACA0
	public void OnAnimationEnd(TextureAnimation anim)
	{
		this.StateMachine.Trigger<OnAnimationOrTransitionEnded>();
		if (!this.SpriteEntity.Animation.Animator.IsTransitionPlaying)
		{
			this.StateMachine.Trigger<OnAnimationEnded>();
		}
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x0005CAD2 File Offset: 0x0005ACD2
	public void OnCollisionEnter(Collision collision)
	{
		this.StateMachine.Trigger(new OnCollisionEnter(collision));
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x0005CAE5 File Offset: 0x0005ACE5
	public void OnCollisionStay(Collision collision)
	{
		this.StateMachine.Trigger(new OnCollisionStay(collision));
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x0005CAF8 File Offset: 0x0005ACF8
	public void OnCollisionExit(Collision collision)
	{
		this.StateMachine.Trigger(new OnCollisionExit(collision));
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x0005CB0C File Offset: 0x0005AD0C
	public void OnRecieveDamage(Damage damage)
	{
		if (this.OnReceiveDamage != null)
		{
			this.OnReceiveDamage(damage);
		}
		this.StateMachine.Trigger(new OnReceiveDamage(damage));
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x0005CB41 File Offset: 0x0005AD41
	public void OnNearSeinEnter()
	{
		this.m_nearSein = true;
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x0005CB4A File Offset: 0x0005AD4A
	public void OnNearSeinExit()
	{
		this.m_nearSein = false;
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001482 RID: 5250 RVA: 0x0005CB53 File Offset: 0x0005AD53
	public bool NearSein
	{
		get
		{
			return this.m_nearSein && Characters.Sein.Controller.CanMove;
		}
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x0005CB72 File Offset: 0x0005AD72
	public bool IsNearSein()
	{
		return this.NearSein;
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x0005CB7A File Offset: 0x0005AD7A
	public void OnSeinNearStay()
	{
		this.LastSeenSeinPosition = Characters.Sein.Position;
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001485 RID: 5253 RVA: 0x0005CB8C File Offset: 0x0005AD8C
	// (set) Token: 0x06001486 RID: 5254 RVA: 0x0005CB94 File Offset: 0x0005AD94
	public Vector3 LastSeenSeinPosition { get; private set; }

	// Token: 0x06001487 RID: 5255 RVA: 0x0005CB9D File Offset: 0x0005AD9D
	[ContextMenu("Current state class name")]
	public void ShowCurrentStateClassName()
	{
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x0005CB9F File Offset: 0x0005AD9F
	public override void Serialize(Archive ar)
	{
		this.StateMachine.Serialize(ar);
	}

	// Token: 0x040011DB RID: 4571
	public Entity Entity;

	// Token: 0x040011DC RID: 4572
	public StateMachine StateMachine = new StateMachine();

	// Token: 0x040011DD RID: 4573
	public Action<Damage> OnReceiveDamage;

	// Token: 0x040011DE RID: 4574
	private TransitionManager m_transManager;

	// Token: 0x040011DF RID: 4575
	private bool m_nearSein;
}
