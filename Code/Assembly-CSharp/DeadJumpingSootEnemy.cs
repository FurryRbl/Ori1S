using System;
using Core;
using UnityEngine;

// Token: 0x02000572 RID: 1394
public class DeadJumpingSootEnemy : MonoBehaviour, IContextReciever, IDamageReciever, ISuspendable
{
	// Token: 0x06002427 RID: 9255 RVA: 0x0009DF7C File Offset: 0x0009C17C
	public void OnReceiveContext(IContext context)
	{
		if (context is IDamageContext)
		{
			Damage damage = ((IDamageContext)context).Damage;
			this.OnRecieveDamage(damage);
		}
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x0009DFA8 File Offset: 0x0009C1A8
	public void OnRecieveDamage(Damage damage)
	{
		this.Velocity = this.Velocity.magnitude * (damage.Force.normalized + Vector2.up).normalized;
		this.m_thrownDirection = this.Velocity.normalized;
	}

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x06002429 RID: 9257 RVA: 0x0009E001 File Offset: 0x0009C201
	// (set) Token: 0x0600242A RID: 9258 RVA: 0x0009E009 File Offset: 0x0009C209
	public bool IsSuspended { get; set; }

	// Token: 0x0600242B RID: 9259 RVA: 0x0009E014 File Offset: 0x0009C214
	public void ChangeState(DeadJumpingSootEnemy.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		DeadJumpingSootEnemy.State currentState = this.CurrentState;
		if (currentState != DeadJumpingSootEnemy.State.Plummit)
		{
			if (currentState == DeadJumpingSootEnemy.State.PlummitLand)
			{
				this.Animation.PlayLoop(this.Animations.DeathPlummetLand, 1, null, false);
				Sound.Play(this.HitGroundSoundProvider.GetSound(null), base.transform.position, null);
			}
		}
		else
		{
			this.Animation.PlayLoop(this.Animations.DeathPlummet, 0, null, false);
		}
	}

	// Token: 0x0600242C RID: 9260 RVA: 0x0009E0A8 File Offset: 0x0009C2A8
	public void UpdateState()
	{
		DeadJumpingSootEnemy.State currentState = this.CurrentState;
		if (currentState == DeadJumpingSootEnemy.State.Plummit)
		{
			float num = 1f - Mathf.InverseLerp(0.3f, 0.6f, this.m_stateCurrentTime);
			this.Sprite.eulerAngles = new Vector3(0f, 0f, (MoonMath.Angle.AngleFromVector(this.m_thrownDirection) - 90f) * num);
			this.m_gravityVelocity += Vector3.down * this.Gravity * this.KnockedBackGravityCurve.Evaluate(this.m_stateCurrentTime) * Time.deltaTime;
			this.m_rigidbody.velocity = this.Velocity * this.KnockedBackSpeedCurve.Evaluate(this.m_stateCurrentTime) + this.m_gravityVelocity;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x0600242D RID: 9261 RVA: 0x0009E19F File Offset: 0x0009C39F
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollisionStay(collision);
	}

	// Token: 0x0600242E RID: 9262 RVA: 0x0009E1A8 File Offset: 0x0009C3A8
	public void OnCollisionStay(Collision collision)
	{
		if (this.CurrentState != DeadJumpingSootEnemy.State.PlummitLand)
		{
			Vector3 vector = PhysicsHelper.CalculateAverageNormalFromContactPoints(collision.contacts);
			if (Vector3.Dot(Vector3.up, vector) > Mathf.Cos(0.7853982f))
			{
				this.OnHitFloor(vector);
			}
		}
	}

	// Token: 0x0600242F RID: 9263 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
	public void OnHitFloor(Vector3 normal)
	{
		if (this.CurrentState == DeadJumpingSootEnemy.State.PlummitLand)
		{
			return;
		}
		this.Sprite.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(-normal) + 90f);
		this.ChangeState(DeadJumpingSootEnemy.State.PlummitLand);
		this.m_rigidbody.velocity = Vector3.zero;
		if (this.LandOnGroundImpactEffect)
		{
			InstantiateUtility.Instantiate(this.LandOnGroundImpactEffect, this.m_transform.position, Quaternion.identity);
			UnityEngine.Object.DestroyObject(this.m_rigidbody);
			UnityEngine.Object.DestroyObject(base.GetComponent<Collider>());
		}
		if (this.FadeOutAnimator)
		{
			this.FadeOutAnimator.Restart();
		}
	}

	// Token: 0x06002430 RID: 9264 RVA: 0x0009E2AE File Offset: 0x0009C4AE
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_transform = base.transform;
	}

	// Token: 0x06002431 RID: 9265 RVA: 0x0009E2CE File Offset: 0x0009C4CE
	public void Start()
	{
		this.ChangeState(this.CurrentState);
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x0009E2DC File Offset: 0x0009C4DC
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06002433 RID: 9267 RVA: 0x0009E2E4 File Offset: 0x0009C4E4
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			if (this.m_rigidbody)
			{
				this.m_rigidbody.velocity = Vector3.zero;
			}
		}
		else
		{
			this.UpdateState();
		}
	}

	// Token: 0x04001E51 RID: 7761
	public CharacterAnimationSystem Animation;

	// Token: 0x04001E52 RID: 7762
	public DeadJumpingSootEnemy.DeathJumpingSootEnemyAnimations Animations;

	// Token: 0x04001E53 RID: 7763
	public DeadJumpingSootEnemy.State CurrentState;

	// Token: 0x04001E54 RID: 7764
	public LegacyAnimator FadeOutAnimator;

	// Token: 0x04001E55 RID: 7765
	public float Gravity;

	// Token: 0x04001E56 RID: 7766
	public SoundProvider HitGroundSoundProvider;

	// Token: 0x04001E57 RID: 7767
	public float KnockedBackDuration = 0.3333f;

	// Token: 0x04001E58 RID: 7768
	public AnimationCurve KnockedBackGravityCurve;

	// Token: 0x04001E59 RID: 7769
	public AnimationCurve KnockedBackRotationCurve;

	// Token: 0x04001E5A RID: 7770
	public AnimationCurve KnockedBackSpeedCurve;

	// Token: 0x04001E5B RID: 7771
	public GameObject LandOnGroundImpactEffect;

	// Token: 0x04001E5C RID: 7772
	public Transform Sprite;

	// Token: 0x04001E5D RID: 7773
	public CharacterSpriteMirror SpriteMirror;

	// Token: 0x04001E5E RID: 7774
	public Vector3 Velocity;

	// Token: 0x04001E5F RID: 7775
	private Vector3 m_gravityVelocity;

	// Token: 0x04001E60 RID: 7776
	private Rigidbody m_rigidbody;

	// Token: 0x04001E61 RID: 7777
	private float m_stateCurrentTime;

	// Token: 0x04001E62 RID: 7778
	private Transform m_transform;

	// Token: 0x04001E63 RID: 7779
	private Vector3 m_thrownDirection;

	// Token: 0x02000573 RID: 1395
	[Serializable]
	public class DeathJumpingSootEnemyAnimations
	{
		// Token: 0x04001E65 RID: 7781
		public TextureAnimationWithTransitions DeathPlummet;

		// Token: 0x04001E66 RID: 7782
		public TextureAnimationWithTransitions DeathPlummetLand;
	}

	// Token: 0x02000574 RID: 1396
	public enum State
	{
		// Token: 0x04001E68 RID: 7784
		Plummit,
		// Token: 0x04001E69 RID: 7785
		PlummitLand
	}
}
