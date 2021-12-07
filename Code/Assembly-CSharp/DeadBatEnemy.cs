using System;
using Core;
using UnityEngine;

// Token: 0x0200050E RID: 1294
public class DeadBatEnemy : MonoBehaviour, IContextReciever, IDamageReciever, ISuspendable
{
	// Token: 0x0600229A RID: 8858 RVA: 0x00097A5C File Offset: 0x00095C5C
	public void OnReceiveContext(IContext context)
	{
		if (context is IDamageContext)
		{
			Damage damage = ((IDamageContext)context).Damage;
			this.OnRecieveDamage(damage);
		}
	}

	// Token: 0x0600229B RID: 8859 RVA: 0x00097A88 File Offset: 0x00095C88
	public void OnRecieveDamage(Damage damage)
	{
		this.Velocity = this.Velocity.magnitude * damage.Force.normalized;
	}

	// Token: 0x170005F2 RID: 1522
	// (get) Token: 0x0600229C RID: 8860 RVA: 0x00097ABE File Offset: 0x00095CBE
	// (set) Token: 0x0600229D RID: 8861 RVA: 0x00097AC6 File Offset: 0x00095CC6
	public bool IsSuspended { get; set; }

	// Token: 0x0600229E RID: 8862 RVA: 0x00097AD0 File Offset: 0x00095CD0
	public void ChangeState(DeadBatEnemy.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		switch (this.CurrentState)
		{
		case DeadBatEnemy.State.KnockedBack:
			this.Animation.PlayLoop(this.Animations.Hurtling, 0, null, false);
			this.Animation.Play(this.Animations.DeathPlummet, 1, null);
			break;
		case DeadBatEnemy.State.Plummit:
			this.Animation.PlayLoop(this.Animations.Hurtling, 0, null, false);
			break;
		case DeadBatEnemy.State.PlummitLand:
			this.Animation.PlayLoop(this.Animations.DeathPlummetLand, 1, null, false);
			Sound.Play(this.HitGroundSoundProvider.GetSoundForMaterial(SurfaceMaterialType.Grass, null), base.transform.position, null);
			break;
		}
	}

	// Token: 0x0600229F RID: 8863 RVA: 0x00097BA4 File Offset: 0x00095DA4
	public void UpdateState()
	{
		DeadBatEnemy.State currentState = this.CurrentState;
		if (currentState == DeadBatEnemy.State.KnockedBack)
		{
			this.Sprite.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(this.m_rigidbody.velocity) + 90f + this.KnockedBackRotationCurve.Evaluate(this.m_stateCurrentTime));
			this.m_gravityVelocity += Vector3.down * this.Gravity * this.KnockedBackGravityCurve.Evaluate(this.m_stateCurrentTime) * Time.deltaTime;
			this.m_rigidbody.velocity = this.Velocity * this.KnockedBackSpeedCurve.Evaluate(this.m_stateCurrentTime) + this.m_gravityVelocity;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x060022A0 RID: 8864 RVA: 0x00097C94 File Offset: 0x00095E94
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollisionStay(collision);
	}

	// Token: 0x060022A1 RID: 8865 RVA: 0x00097CA0 File Offset: 0x00095EA0
	public void OnCollisionStay(Collision collision)
	{
		if (this.CurrentState != DeadBatEnemy.State.PlummitLand)
		{
			Vector3 vector = PhysicsHelper.CalculateAverageNormalFromContactPoints(collision.contacts);
			if (Vector3.Dot(Vector3.up, vector) > Mathf.Cos(0.7853982f))
			{
				this.OnHitFloor(vector);
				return;
			}
		}
	}

	// Token: 0x060022A2 RID: 8866 RVA: 0x00097CE8 File Offset: 0x00095EE8
	public void OnHitFloor(Vector3 normal)
	{
		if (this.CurrentState == DeadBatEnemy.State.PlummitLand)
		{
			return;
		}
		this.Sprite.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(-normal) + 90f);
		this.ChangeState(DeadBatEnemy.State.PlummitLand);
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

	// Token: 0x060022A3 RID: 8867 RVA: 0x00097DA6 File Offset: 0x00095FA6
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_transform = base.transform;
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x00097DC6 File Offset: 0x00095FC6
	public void Start()
	{
		this.ChangeState(this.CurrentState);
	}

	// Token: 0x060022A5 RID: 8869 RVA: 0x00097DD4 File Offset: 0x00095FD4
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060022A6 RID: 8870 RVA: 0x00097DDC File Offset: 0x00095FDC
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

	// Token: 0x04001D06 RID: 7430
	public CharacterAnimationSystem Animation;

	// Token: 0x04001D07 RID: 7431
	public DeadBatEnemy.DeathBatEnemyAnimations Animations;

	// Token: 0x04001D08 RID: 7432
	public DeadBatEnemy.State CurrentState;

	// Token: 0x04001D09 RID: 7433
	public LegacyAnimator FadeOutAnimator;

	// Token: 0x04001D0A RID: 7434
	public float Gravity;

	// Token: 0x04001D0B RID: 7435
	public SurfaceToSoundProviderMap HitGroundSoundProvider;

	// Token: 0x04001D0C RID: 7436
	public float KnockedBackDuration = 0.3333f;

	// Token: 0x04001D0D RID: 7437
	public AnimationCurve KnockedBackGravityCurve;

	// Token: 0x04001D0E RID: 7438
	public AnimationCurve KnockedBackRotationCurve;

	// Token: 0x04001D0F RID: 7439
	public AnimationCurve KnockedBackSpeedCurve;

	// Token: 0x04001D10 RID: 7440
	public GameObject LandOnGroundImpactEffect;

	// Token: 0x04001D11 RID: 7441
	public Transform Sprite;

	// Token: 0x04001D12 RID: 7442
	public Vector3 Velocity;

	// Token: 0x04001D13 RID: 7443
	private Vector3 m_gravityVelocity;

	// Token: 0x04001D14 RID: 7444
	private Rigidbody m_rigidbody;

	// Token: 0x04001D15 RID: 7445
	private float m_stateCurrentTime;

	// Token: 0x04001D16 RID: 7446
	private Transform m_transform;

	// Token: 0x0200050F RID: 1295
	[Serializable]
	public class DeathBatEnemyAnimations
	{
		// Token: 0x04001D18 RID: 7448
		public TextureAnimationWithTransitions DeathPlummet;

		// Token: 0x04001D19 RID: 7449
		public TextureAnimationWithTransitions DeathPlummetLand;

		// Token: 0x04001D1A RID: 7450
		public TextureAnimationWithTransitions Hurtling;
	}

	// Token: 0x02000510 RID: 1296
	public enum State
	{
		// Token: 0x04001D1C RID: 7452
		KnockedBack,
		// Token: 0x04001D1D RID: 7453
		Plummit,
		// Token: 0x04001D1E RID: 7454
		PlummitLand
	}
}
