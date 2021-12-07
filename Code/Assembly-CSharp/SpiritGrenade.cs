using System;
using Game;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class SpiritGrenade : MonoBehaviour, IDamageReciever, IAttackable, IBashAttackable, ISuspendable
{
	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x0600180A RID: 6154 RVA: 0x0006736D File Offset: 0x0006556D
	public bool IsInsideSpiritTorch
	{
		get
		{
			return this.m_ignitableTorch != null;
		}
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x0006737C File Offset: 0x0006557C
	public void Awake()
	{
		DamageDealer damageDealer = this.DamageDealer;
		damageDealer.OnDamageDealtEvent = (Action<GameObject, Damage>)Delegate.Combine(damageDealer.OnDamageDealtEvent, new Action<GameObject, Damage>(this.OnDamageDealt));
		DamageDealer damageDealer2 = this.DamageDealer;
		damageDealer2.ShouldDealDamage = (Func<GameObject, bool>)Delegate.Combine(damageDealer2.ShouldDealDamage, new Func<GameObject, bool>(this.ShouldDealDamage));
		SuspensionManager.Register(this);
		Targets.Attackables.Add(this);
		this.m_rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x000673F4 File Offset: 0x000655F4
	public void Start()
	{
		this.m_time = 0f;
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x00067404 File Offset: 0x00065604
	public void OnDestroy()
	{
		DamageDealer damageDealer = this.DamageDealer;
		damageDealer.OnDamageDealtEvent = (Action<GameObject, Damage>)Delegate.Remove(damageDealer.OnDamageDealtEvent, new Action<GameObject, Damage>(this.OnDamageDealt));
		DamageDealer damageDealer2 = this.DamageDealer;
		damageDealer2.ShouldDealDamage = (Func<GameObject, bool>)Delegate.Remove(damageDealer2.ShouldDealDamage, new Func<GameObject, bool>(this.ShouldDealDamage));
		SuspensionManager.Unregister(this);
		Targets.Attackables.Remove(this);
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x00067474 File Offset: 0x00065674
	public bool ShouldDealDamage(GameObject target)
	{
		if (this.IsInsideSpiritTorch)
		{
			return false;
		}
		IAttackable attackable = target.FindComponent<IAttackable>();
		return attackable as Component && attackable.CanBeGrenaded();
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x000674AD File Offset: 0x000656AD
	public void OnDamageDealt(GameObject go, Damage damage)
	{
		if (!this.IsInsideSpiritTorch && !go.GetComponent<Projectile>())
		{
			this.Explode();
		}
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x000674D0 File Offset: 0x000656D0
	public void Explode()
	{
		InstantiateUtility.Destroy(base.gameObject);
		InstantiateUtility.Instantiate(this.Explosion, base.transform.position, Quaternion.identity);
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x00067504 File Offset: 0x00065704
	public void SetTrajectory(Vector2 speed)
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.velocity = speed;
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x00067524 File Offset: 0x00065724
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_time += Time.deltaTime;
		if (this.IsInsideSpiritTorch)
		{
			this.m_rigidbody.velocity = (this.m_ignitableTorch.Position - this.m_rigidbody.position + new Vector3(0.2f, 0.4f)) * 8f;
			if (this.m_time > 0.8f)
			{
				InstantiateUtility.Destroy(base.gameObject);
			}
		}
		else
		{
			this.m_rigidbody.velocity += Vector3.down * this.Gravity * Time.deltaTime;
			IgnitableSpiritTorch ignitableSpiritTorch = IgnitableSpiritTorch.IgniteAnyTorchesNearPosition(base.transform.position);
			if (ignitableSpiritTorch)
			{
				this.m_ignitableTorch = ignitableSpiritTorch;
				this.m_time = 0f;
				return;
			}
			if (this.m_time > this.Duration)
			{
				this.Explode();
			}
		}
		if (WaterZone.PositionInWater(this.Position))
		{
			this.m_rigidbody.velocity *= 0.9f;
		}
	}

	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x06001813 RID: 6163 RVA: 0x0006765F File Offset: 0x0006585F
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x0006766C File Offset: 0x0006586C
	public bool IsDead()
	{
		return base.gameObject.activeSelf;
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x00067679 File Offset: 0x00065879
	public bool CanBeChargeFlamed()
	{
		return false;
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x0006767C File Offset: 0x0006587C
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x0006767F File Offset: 0x0006587F
	public bool CanBeGrenaded()
	{
		return false;
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x00067682 File Offset: 0x00065882
	public bool CanBeStomped()
	{
		return false;
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x00067685 File Offset: 0x00065885
	public bool CanBeBashed()
	{
		return !this.IsInsideSpiritTorch && this.Bashable;
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x0006769B File Offset: 0x0006589B
	public bool CanBeSpiritFlamed()
	{
		return false;
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x0006769E File Offset: 0x0006589E
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000676A1 File Offset: 0x000658A1
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x000676A4 File Offset: 0x000658A4
	public void OnEnterBash()
	{
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x000676A6 File Offset: 0x000658A6
	public void OnBashHighlight()
	{
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000676A8 File Offset: 0x000658A8
	public void OnBashDehighlight()
	{
	}

	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x06001820 RID: 6176 RVA: 0x000676AA File Offset: 0x000658AA
	public int BashPriority
	{
		get
		{
			return 100;
		}
	}

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x06001821 RID: 6177 RVA: 0x000676AE File Offset: 0x000658AE
	// (set) Token: 0x06001822 RID: 6178 RVA: 0x000676B6 File Offset: 0x000658B6
	public bool IsSuspended { get; set; }

	// Token: 0x06001823 RID: 6179 RVA: 0x000676C0 File Offset: 0x000658C0
	public void OnSpring(float height, Vector2 direction)
	{
		this.m_rigidbody.velocity = direction * MoonMath.Physics.SpeedFromHeightAndGravity(this.Gravity, height);
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x000676F0 File Offset: 0x000658F0
	public void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.Spikes || damage.Type == DamageType.Lava || damage.Type == DamageType.Laser || damage.Type == DamageType.Bash)
		{
			this.Explode();
		}
	}

	// Token: 0x040014C0 RID: 5312
	public float Gravity;

	// Token: 0x040014C1 RID: 5313
	public DamageDealer DamageDealer;

	// Token: 0x040014C2 RID: 5314
	public GameObject Explosion;

	// Token: 0x040014C3 RID: 5315
	public float Duration = 4f;

	// Token: 0x040014C4 RID: 5316
	private float m_time;

	// Token: 0x040014C5 RID: 5317
	private Rigidbody m_rigidbody;

	// Token: 0x040014C6 RID: 5318
	private IgnitableSpiritTorch m_ignitableTorch;

	// Token: 0x040014C7 RID: 5319
	public bool Bashable = true;
}
