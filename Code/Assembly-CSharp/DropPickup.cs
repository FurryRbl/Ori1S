using System;
using Game;
using UnityEngine;

// Token: 0x0200062C RID: 1580
public class DropPickup : MonoBehaviour, IDamageReciever, IPooled, ISuspendable
{
	// Token: 0x060026EB RID: 9963 RVA: 0x000A9F2D File Offset: 0x000A812D
	public void OnValidate()
	{
		this.m_legacyAnimator = base.GetComponentsInChildren<LegacyAnimator>();
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x000A9F3C File Offset: 0x000A813C
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_sphereCollider = base.GetComponent<SphereCollider>();
		this.m_radius = this.m_sphereCollider.radius;
		this.m_startGravity = this.Gravity;
		this.m_suckTowardsPlayerCurrentTime = 0f;
		this.ChangeState(this.CurrentState);
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x000A9F9C File Offset: 0x000A819C
	public void OnPoolSpawned()
	{
		this.Gravity = this.m_startGravity;
		this.m_suckTowardsPlayerCurrentTime = 0f;
		this.m_stateCurrentTime = 0f;
		this.m_radius = 0f;
		this.m_gravityWeight = 1f;
		this.m_shouldSuckTowardsPlayer = false;
		this.CurrentState = DropPickup.State.Hover;
		this.m_sphereCollider.isTrigger = false;
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x000A9FFC File Offset: 0x000A81FC
	public void ChangeState(DropPickup.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		DropPickup.State currentState = this.CurrentState;
		if (currentState != DropPickup.State.Falling)
		{
			if (currentState == DropPickup.State.Flashing)
			{
				for (int i = 0; i < this.m_legacyAnimator.Length; i++)
				{
					LegacyAnimator legacyAnimator = this.m_legacyAnimator[i];
					legacyAnimator.Restart();
				}
			}
		}
		else
		{
			this.m_gravityWeight = 0f;
		}
	}

	// Token: 0x17000626 RID: 1574
	// (get) Token: 0x060026EF RID: 9967 RVA: 0x000AA074 File Offset: 0x000A8274
	public bool PlayerInControl
	{
		get
		{
			return Characters.Sein && Characters.Sein.Controller.CanMove;
		}
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x000AA0A8 File Offset: 0x000A82A8
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case DropPickup.State.Hover:
			this.m_rigidbody.velocity *= Mathf.Pow(0.01f, Time.deltaTime);
			if (this.m_stateCurrentTime > this.HoverDuration)
			{
				this.ChangeState(DropPickup.State.Falling);
			}
			break;
		case DropPickup.State.Falling:
			if (this.m_gravityWeight < 1f)
			{
				this.m_gravityWeight = Mathf.Min(1f, this.m_gravityWeight + Time.deltaTime);
			}
			if (this.m_stateCurrentTime > this.FallingDuration)
			{
				this.ChangeState(DropPickup.State.Flashing);
			}
			this.m_rigidbody.velocity += this.m_gravityWeight * this.Gravity * Time.deltaTime;
			break;
		case DropPickup.State.Flashing:
			if (this.m_stateCurrentTime > this.FlashDuration && Characters.Ori)
			{
				PickupBase componentInChildren = base.GetComponentInChildren<PickupBase>();
				if (componentInChildren)
				{
					componentInChildren.SpawnCollectedEffect();
				}
				InstantiateUtility.Destroy(base.gameObject);
			}
			this.m_rigidbody.velocity += this.Gravity * Time.deltaTime;
			break;
		}
		if (this.PlayerInControl || this.CurrentState == DropPickup.State.Hover)
		{
			this.m_stateCurrentTime += Time.deltaTime;
		}
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x000AA225 File Offset: 0x000A8425
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x000AA230 File Offset: 0x000A8430
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_frame++;
		this.UpdateState();
		this.UpdateSuckTowardsPlayer();
		if (this.m_frame % 4 == 0)
		{
			this.UpdateWaterBehaviour();
		}
		if (this.m_inWater)
		{
			this.m_rigidbody.velocity *= 0.9f;
		}
	}

	// Token: 0x060026F3 RID: 9971 RVA: 0x000AA29C File Offset: 0x000A849C
	private void UpdateWaterBehaviour()
	{
		this.m_inWater = false;
		Vector3 position = base.transform.position;
		int count = Zones.WaterZones.Count;
		for (int i = 0; i < count; i++)
		{
			WaterZone waterZone = Zones.WaterZones[i];
			if (waterZone.Bounds.Contains(position))
			{
				this.m_inWater = true;
				return;
			}
		}
	}

	// Token: 0x060026F4 RID: 9972 RVA: 0x000AA300 File Offset: 0x000A8500
	private void UpdateSuckTowardsPlayer()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		this.m_suckTowardsPlayerCurrentTime += Time.deltaTime;
		Vector3 vector = Characters.Sein.Position - this.m_rigidbody.position;
		if (vector.magnitude < Characters.Sein.PlayerAbilities.AttractionDistance && (this.m_suckTowardsPlayerCurrentTime > 1f || Characters.Sein.PlayerAbilities.UltraMagnet.HasAbility))
		{
			this.m_shouldSuckTowardsPlayer = true;
			this.m_sphereCollider.isTrigger = true;
			this.Gravity = Vector3.zero;
		}
		if (this.m_shouldSuckTowardsPlayer)
		{
			this.m_rigidbody.WakeUp();
			this.m_rigidbody.velocity += vector.normalized * Time.deltaTime * 200f * this.SuckInForceOverDistance.Evaluate(vector.magnitude);
			this.m_rigidbody.velocity *= Mathf.Pow(0.5f, Time.deltaTime * 8f);
		}
		else if (vector.magnitude < 2.5f)
		{
			this.m_rigidbody.velocity += vector.normalized * Time.deltaTime * 50f;
			this.m_rigidbody.velocity *= Mathf.Pow(0.5f, Time.deltaTime * 8f);
		}
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x000AA4A8 File Offset: 0x000A86A8
	public void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.Lava && this.DeathEffectProvider)
		{
			GameObject original = this.DeathEffectProvider.Prefab(new DamageContext(damage));
			GameObject target = (GameObject)InstantiateUtility.Instantiate(original, base.transform.position, Quaternion.identity);
			damage.DealToComponents(target);
		}
	}

	// Token: 0x17000627 RID: 1575
	// (get) Token: 0x060026F6 RID: 9974 RVA: 0x000AA50B File Offset: 0x000A870B
	// (set) Token: 0x060026F7 RID: 9975 RVA: 0x000AA513 File Offset: 0x000A8713
	public bool IsSuspended { get; set; }

	// Token: 0x0400217A RID: 8570
	public Vector3 Gravity;

	// Token: 0x0400217B RID: 8571
	public DamageBasedPrefabProvider DeathEffectProvider;

	// Token: 0x0400217C RID: 8572
	public AnimationCurve SuckInForceOverTime;

	// Token: 0x0400217D RID: 8573
	public AnimationCurve SuckInForceOverDistance;

	// Token: 0x0400217E RID: 8574
	public float HoverDuration;

	// Token: 0x0400217F RID: 8575
	public float FallingDuration = 3f;

	// Token: 0x04002180 RID: 8576
	public float FlashDuration = 1f;

	// Token: 0x04002181 RID: 8577
	public DropPickup.State CurrentState;

	// Token: 0x04002182 RID: 8578
	private Rigidbody m_rigidbody;

	// Token: 0x04002183 RID: 8579
	private float m_gravityWeight = 1f;

	// Token: 0x04002184 RID: 8580
	private float m_suckTowardsPlayerCurrentTime;

	// Token: 0x04002185 RID: 8581
	private float m_stateCurrentTime;

	// Token: 0x04002186 RID: 8582
	private float m_radius;

	// Token: 0x04002187 RID: 8583
	private SphereCollider m_sphereCollider;

	// Token: 0x04002188 RID: 8584
	private Vector3 m_startGravity;

	// Token: 0x04002189 RID: 8585
	private bool m_shouldSuckTowardsPlayer;

	// Token: 0x0400218A RID: 8586
	[PooledSafe]
	private int m_frame;

	// Token: 0x0400218B RID: 8587
	private bool m_inWater;

	// Token: 0x0400218C RID: 8588
	[HideInInspector]
	[SerializeField]
	private LegacyAnimator[] m_legacyAnimator;

	// Token: 0x0200062D RID: 1581
	public enum State
	{
		// Token: 0x0400218F RID: 8591
		Hover,
		// Token: 0x04002190 RID: 8592
		Falling,
		// Token: 0x04002191 RID: 8593
		Flashing
	}
}
