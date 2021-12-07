using System;
using Core;
using UnityEngine;

// Token: 0x0200056E RID: 1390
public class DeadEnemyRagdoll : MonoBehaviour, IContextReciever, IDamageReciever, ISuspendable
{
	// Token: 0x06002410 RID: 9232 RVA: 0x0009D6E0 File Offset: 0x0009B8E0
	public void OnReceiveContext(IContext context)
	{
		if (context is IDamageContext)
		{
			Damage damage = ((IDamageContext)context).Damage;
			this.OnRecieveDamage(damage);
		}
	}

	// Token: 0x06002411 RID: 9233 RVA: 0x0009D70C File Offset: 0x0009B90C
	public void OnRecieveDamage(Damage damage)
	{
		if (this.ExplodeUnlessBashed && damage.Type != DamageType.Bash && damage.Type != DamageType.Stomp && damage.Type != DamageType.StompBlast)
		{
			this.Explode();
		}
		if (damage.Force.magnitude > 0f)
		{
			this.m_startVelocity = this.StartSpeed * (damage.Force.normalized + Vector2.up).normalized + damage.Force * this.DamageForceSpeed;
		}
		this.m_thrownDirection = this.m_startVelocity.normalized;
		this.m_rigidbody.velocity = this.m_startVelocity;
		if (damage.Type == DamageType.Water)
		{
			if (this.Animations.Drown)
			{
				this.Animation.PlayLoop(this.Animations.Drown, 0, null, false);
			}
			this.ChangeState(DeadEnemyRagdoll.State.Drown);
		}
	}

	// Token: 0x17000602 RID: 1538
	// (get) Token: 0x06002412 RID: 9234 RVA: 0x0009D818 File Offset: 0x0009BA18
	// (set) Token: 0x06002413 RID: 9235 RVA: 0x0009D820 File Offset: 0x0009BA20
	public bool IsSuspended { get; set; }

	// Token: 0x06002414 RID: 9236 RVA: 0x0009D82C File Offset: 0x0009BA2C
	public void ChangeState(DeadEnemyRagdoll.State state)
	{
		this.m_currentState = state;
		this.m_currentStateTime = 0f;
		switch (this.m_currentState)
		{
		case DeadEnemyRagdoll.State.Plummit:
			this.OnEnterPlumit();
			break;
		case DeadEnemyRagdoll.State.PlummitLand:
			this.OnEnterFlatLand();
			break;
		case DeadEnemyRagdoll.State.Drown:
			this.OnEnterDrown();
			break;
		}
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x0009D88A File Offset: 0x0009BA8A
	public void OnEnterDrown()
	{
		this.m_actualVelocity.y = -40f;
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x0009D89C File Offset: 0x0009BA9C
	public void OnEnterPlumit()
	{
		this.Animation.PlayLoop(this.Animations.DeathPlummet, 0, null, false);
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x0009D8B8 File Offset: 0x0009BAB8
	public void OnEnterFlatLand()
	{
		Sound.Play(this.HitGroundSoundProvider.GetSound(null), base.transform.position, null);
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x0009D8E4 File Offset: 0x0009BAE4
	public void UpdatePlummit()
	{
		this.Sprite.eulerAngles = new Vector3(0f, 0f, this.PlumetSettings.RotationCurve.Evaluate(this.m_currentStateTime) * (MoonMath.Angle.AngleFromVector(this.m_thrownDirection) - 90f));
		this.m_gravityVelocity += Vector3.down * this.PlumetSettings.Gravity * this.PlumetSettings.GravityCurve.Evaluate(this.m_currentStateTime) * Time.deltaTime;
		this.m_actualVelocity = this.m_startVelocity * this.PlumetSettings.SpeedCurve.Evaluate(this.m_currentStateTime) + this.m_gravityVelocity;
		this.m_rigidbody.velocity = this.m_actualVelocity;
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x0009D9C8 File Offset: 0x0009BBC8
	public void UpdateDrown()
	{
		this.Sprite.eulerAngles = new Vector3(0f, 0f, 0f);
		if (this.m_rigidbody)
		{
			this.m_rigidbody.velocity = this.m_actualVelocity * this.PlumetSettings.WaterFrictionCurve.Evaluate(this.m_currentStateTime);
		}
		if (this.m_currentStateTime > this.PlumetSettings.DrownDelay && !this.m_drownAnimationPlaying)
		{
			this.m_drownAnimationPlaying = true;
			if (this.Animations.Drown)
			{
				this.Animation.PlayLoop(this.Animations.Drown, 0, null, false);
			}
		}
	}

	// Token: 0x0600241A RID: 9242 RVA: 0x0009DA88 File Offset: 0x0009BC88
	public void UpdateState()
	{
		switch (this.m_currentState)
		{
		case DeadEnemyRagdoll.State.Plummit:
			this.UpdatePlummit();
			break;
		case DeadEnemyRagdoll.State.Drown:
			this.UpdateDrown();
			break;
		}
		this.m_currentStateTime += Time.deltaTime;
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x0009DADB File Offset: 0x0009BCDB
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x0009DAE4 File Offset: 0x0009BCE4
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x0600241D RID: 9245 RVA: 0x0009DAF0 File Offset: 0x0009BCF0
	public void OnCollision(Collision collision)
	{
		if (this.m_lifeTime < 0.2f)
		{
			return;
		}
		if (this.m_currentState == DeadEnemyRagdoll.State.Plummit || (this.m_currentState == DeadEnemyRagdoll.State.Drown && !this.m_drownAnimationPlaying))
		{
			ContactPoint[] contacts = collision.contacts;
			foreach (ContactPoint contactPoint in contacts)
			{
				if (Vector3.Dot(Vector3.up, contactPoint.normal) > Mathf.Cos(0.7853982f))
				{
					this.OnHitFloor(PhysicsHelper.CalculateAverageGroundNormalFromContactPoints(contacts), collision.gameObject);
					return;
				}
			}
		}
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x0009DB90 File Offset: 0x0009BD90
	public void Explode()
	{
		InstantiateUtility.Instantiate(this.ExplodeEffect, base.transform.position, Quaternion.identity);
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x0600241F RID: 9247 RVA: 0x0009DBC4 File Offset: 0x0009BDC4
	public void OnHitFloor(Vector3 normal, GameObject targetGameObject)
	{
		if (this.m_currentState == DeadEnemyRagdoll.State.PlummitLand)
		{
			return;
		}
		if (!Physics.Raycast(base.transform.position, Vector3.down, this.RayDistance, this.RayMask))
		{
			return;
		}
		if (this.ExplodeOnGround)
		{
			this.Explode();
			return;
		}
		if (this.Animations.DeathPlummetEdgeLand)
		{
			Vector3 origin = base.transform.position + Vector3.Cross(Vector3.forward, normal) * this.m_sphereCollider.radius;
			Vector3 origin2 = base.transform.position + Vector3.Cross(Vector3.back, normal) * this.m_sphereCollider.radius;
			RaycastHit raycastHit;
			bool flag = Physics.Raycast(origin, -normal, out raycastHit, this.RayDistance, this.RayMask);
			RaycastHit raycastHit2;
			bool flag2 = Physics.Raycast(origin2, -normal, out raycastHit2, this.RayDistance, this.RayMask);
			bool flag3 = false;
			if (flag && flag2)
			{
				float num = Vector3.Distance(raycastHit.point, raycastHit2.point);
				float d = 0.1f;
				float num2 = Vector3.Distance(raycastHit.point + raycastHit.normal * d, raycastHit2.point + raycastHit2.normal * d);
				if (num2 > num)
				{
					flag3 = true;
				}
			}
			else
			{
				flag3 = true;
			}
			if (flag3 && this.Animations.DeathPlummetEdgeLand)
			{
				this.Animation.PlayLoop(this.Animations.DeathPlummetEdgeLand, 1, null, false);
			}
			else
			{
				this.Animation.PlayLoop(this.Animations.DeathPlummetFlatLand, 1, null, false);
			}
		}
		else
		{
			this.Animation.PlayLoop(this.Animations.DeathPlummetFlatLand, 1, null, false);
		}
		this.Sprite.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(-normal) + 90f);
		this.ChangeState(DeadEnemyRagdoll.State.PlummitLand);
		this.m_rigidbody.velocity = Vector3.zero;
		if (this.LandOnGroundImpactEffect)
		{
			InstantiateUtility.Instantiate(this.LandOnGroundImpactEffect, base.transform.position, Quaternion.identity);
			UnityEngine.Object.DestroyObject(this.m_rigidbody);
			UnityEngine.Object.DestroyObject(base.GetComponent<Collider>());
			FollowPositionRotation followPositionRotation = base.gameObject.GetComponent<FollowPositionRotation>();
			if (followPositionRotation == null)
			{
				followPositionRotation = base.gameObject.AddComponent<FollowPositionRotation>();
			}
			followPositionRotation.enabled = true;
			followPositionRotation.SetTarget(targetGameObject.transform);
		}
	}

	// Token: 0x06002420 RID: 9248 RVA: 0x0009DE80 File Offset: 0x0009C080
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_sphereCollider = base.GetComponent<SphereCollider>();
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x0009DEA0 File Offset: 0x0009C0A0
	public void Start()
	{
		this.ChangeState(this.m_currentState);
		this.m_lifeTime = 0f;
	}

	// Token: 0x06002422 RID: 9250 RVA: 0x0009DEB9 File Offset: 0x0009C0B9
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06002423 RID: 9251 RVA: 0x0009DEC4 File Offset: 0x0009C0C4
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
			this.m_lifeTime += 1f;
			if (WaterZone.PositionInWater(base.transform.position) && this.m_currentState == DeadEnemyRagdoll.State.Plummit)
			{
				this.ChangeState(DeadEnemyRagdoll.State.Drown);
			}
			this.UpdateState();
		}
	}

	// Token: 0x04001E28 RID: 7720
	public CharacterAnimationSystem Animation;

	// Token: 0x04001E29 RID: 7721
	public DeadEnemyRagdoll.RagdollAnimations Animations;

	// Token: 0x04001E2A RID: 7722
	public BaseAnimator FadeOutAnimator;

	// Token: 0x04001E2B RID: 7723
	public SoundProvider HitGroundSoundProvider;

	// Token: 0x04001E2C RID: 7724
	public DeadEnemyRagdoll.EnemyPlumetSettings PlumetSettings;

	// Token: 0x04001E2D RID: 7725
	public GameObject LandOnGroundImpactEffect;

	// Token: 0x04001E2E RID: 7726
	public Transform Sprite;

	// Token: 0x04001E2F RID: 7727
	public CharacterSpriteMirror SpriteMirror;

	// Token: 0x04001E30 RID: 7728
	public bool ExplodeOnGround;

	// Token: 0x04001E31 RID: 7729
	public bool ExplodeUnlessBashed;

	// Token: 0x04001E32 RID: 7730
	public GameObject ExplodeEffect;

	// Token: 0x04001E33 RID: 7731
	private Vector3 m_startVelocity;

	// Token: 0x04001E34 RID: 7732
	private Vector3 m_gravityVelocity;

	// Token: 0x04001E35 RID: 7733
	private Rigidbody m_rigidbody;

	// Token: 0x04001E36 RID: 7734
	private float m_currentStateTime;

	// Token: 0x04001E37 RID: 7735
	private DeadEnemyRagdoll.State m_currentState;

	// Token: 0x04001E38 RID: 7736
	private Vector3 m_thrownDirection;

	// Token: 0x04001E39 RID: 7737
	private Vector3 m_actualVelocity;

	// Token: 0x04001E3A RID: 7738
	private SphereCollider m_sphereCollider;

	// Token: 0x04001E3B RID: 7739
	public float StartSpeed = 35f;

	// Token: 0x04001E3C RID: 7740
	public float DamageForceSpeed = 10f;

	// Token: 0x04001E3D RID: 7741
	public float RayDistance = 3f;

	// Token: 0x04001E3E RID: 7742
	public LayerMask RayMask;

	// Token: 0x04001E3F RID: 7743
	private bool m_drownAnimationPlaying;

	// Token: 0x04001E40 RID: 7744
	private float m_lifeTime;

	// Token: 0x0200056F RID: 1391
	[Serializable]
	public class RagdollAnimations
	{
		// Token: 0x04001E42 RID: 7746
		public TextureAnimationWithTransitions DeathPlummet;

		// Token: 0x04001E43 RID: 7747
		public TextureAnimationWithTransitions DeathPlummetLoop;

		// Token: 0x04001E44 RID: 7748
		public TextureAnimationWithTransitions DeathPlummetFlatLand;

		// Token: 0x04001E45 RID: 7749
		public TextureAnimationWithTransitions DeathPlummetEdgeLand;

		// Token: 0x04001E46 RID: 7750
		public TextureAnimationWithTransitions Drown;
	}

	// Token: 0x02000570 RID: 1392
	[Serializable]
	public class EnemyPlumetSettings
	{
		// Token: 0x04001E47 RID: 7751
		public AnimationCurve RotationCurve;

		// Token: 0x04001E48 RID: 7752
		public AnimationCurve SpeedCurve;

		// Token: 0x04001E49 RID: 7753
		public AnimationCurve GravityCurve;

		// Token: 0x04001E4A RID: 7754
		public float Gravity = 100f;

		// Token: 0x04001E4B RID: 7755
		public AnimationCurve WaterFrictionCurve;

		// Token: 0x04001E4C RID: 7756
		public float DrownDelay = 2f;
	}

	// Token: 0x02000571 RID: 1393
	public enum State
	{
		// Token: 0x04001E4E RID: 7758
		Plummit,
		// Token: 0x04001E4F RID: 7759
		PlummitLand,
		// Token: 0x04001E50 RID: 7760
		Drown
	}
}
