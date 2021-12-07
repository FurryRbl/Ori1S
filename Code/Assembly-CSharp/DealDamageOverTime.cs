using System;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class DealDamageOverTime : MonoBehaviour, ISuspendable
{
	// Token: 0x06001AAE RID: 6830 RVA: 0x00072F07 File Offset: 0x00071107
	public void OnDisable()
	{
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x00072F0F File Offset: 0x0007110F
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_transform = base.transform;
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x00072F23 File Offset: 0x00071123
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x00072F2B File Offset: 0x0007112B
	public void Start()
	{
		this.m_remainingTime = (float)this.DamageDuration;
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x00072F3C File Offset: 0x0007113C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_remainingTime -= Time.deltaTime;
		if (this.m_remainingTime > 0f)
		{
			Damage damage = new Damage(this.DamageAmount / (float)this.DamageDuration * Time.deltaTime, Vector2.zero, this.m_transform.position, this.DamageType, base.gameObject);
			damage.DealToComponents(base.gameObject);
		}
		if (this.DestroyWhenAllDamageDealt && this.m_remainingTime <= 0f)
		{
			UnityEngine.Object.DestroyObject(this);
		}
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00072FDA File Offset: 0x000711DA
	// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00072FE2 File Offset: 0x000711E2
	public bool IsSuspended { get; set; }

	// Token: 0x04001723 RID: 5923
	public DamageType DamageType;

	// Token: 0x04001724 RID: 5924
	public float DamageAmount;

	// Token: 0x04001725 RID: 5925
	public int DamageDuration;

	// Token: 0x04001726 RID: 5926
	public bool DestroyWhenAllDamageDealt = true;

	// Token: 0x04001727 RID: 5927
	private float m_remainingTime;

	// Token: 0x04001728 RID: 5928
	private Transform m_transform;
}
