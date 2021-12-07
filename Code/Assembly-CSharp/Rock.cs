using System;
using Game;
using UnityEngine;

// Token: 0x020009AE RID: 2478
public class Rock : MonoBehaviour, IAttackable, IBashAttackable, ISuspendable
{
	// Token: 0x060035F7 RID: 13815 RVA: 0x000E29DC File Offset: 0x000E0BDC
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_lifeRemaining -= Time.deltaTime;
		if (this.m_lifeRemaining < 0f && this.OnHitGround)
		{
			this.OnHitGround.Perform(null);
		}
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x000E2A34 File Offset: 0x000E0C34
	public void Awake()
	{
		this.m_transform = base.transform;
		Targets.Attackables.Add(this);
		SuspensionManager.Register(this);
		this.m_forceExplodeOnContact = false;
	}

	// Token: 0x060035F9 RID: 13817 RVA: 0x000E2A65 File Offset: 0x000E0C65
	public void OnDestroy()
	{
		Targets.Attackables.Remove(this);
		SuspensionManager.Unregister(this);
	}

	// Token: 0x1700086E RID: 2158
	// (get) Token: 0x060035FA RID: 13818 RVA: 0x000E2A79 File Offset: 0x000E0C79
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x060035FB RID: 13819 RVA: 0x000E2A86 File Offset: 0x000E0C86
	public bool IsDead()
	{
		return !base.gameObject.activeInHierarchy;
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x000E2A96 File Offset: 0x000E0C96
	public bool CanBeChargeFlamed()
	{
		return false;
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x000E2A99 File Offset: 0x000E0C99
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x060035FE RID: 13822 RVA: 0x000E2A9C File Offset: 0x000E0C9C
	public bool CanBeGrenaded()
	{
		return false;
	}

	// Token: 0x060035FF RID: 13823 RVA: 0x000E2A9F File Offset: 0x000E0C9F
	public bool CanBeStomped()
	{
		return false;
	}

	// Token: 0x06003600 RID: 13824 RVA: 0x000E2AA4 File Offset: 0x000E0CA4
	public void Start()
	{
		if (base.GetComponent<Polygon>())
		{
			base.GetComponent<Polygon>().UpdateComponents();
		}
		this.m_lifeRemaining = this.Lifetime;
	}

	// Token: 0x06003601 RID: 13825 RVA: 0x000E2AD8 File Offset: 0x000E0CD8
	public bool CanBeBashed()
	{
		return this.Bashable;
	}

	// Token: 0x06003602 RID: 13826 RVA: 0x000E2AE0 File Offset: 0x000E0CE0
	public bool CanBeSpiritFlamed()
	{
		return false;
	}

	// Token: 0x06003603 RID: 13827 RVA: 0x000E2AE3 File Offset: 0x000E0CE3
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x06003604 RID: 13828 RVA: 0x000E2AE6 File Offset: 0x000E0CE6
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x06003605 RID: 13829 RVA: 0x000E2AE9 File Offset: 0x000E0CE9
	public void OnEnterBash()
	{
		this.m_forceExplodeOnContact = true;
	}

	// Token: 0x06003606 RID: 13830 RVA: 0x000E2AF2 File Offset: 0x000E0CF2
	public void OnBashHighlight()
	{
		if (this.OnBashHighlightAction)
		{
			this.OnBashHighlightAction.Perform(null);
		}
	}

	// Token: 0x06003607 RID: 13831 RVA: 0x000E2B10 File Offset: 0x000E0D10
	public void OnBashDehighlight()
	{
	}

	// Token: 0x1700086F RID: 2159
	// (get) Token: 0x06003608 RID: 13832 RVA: 0x000E2B12 File Offset: 0x000E0D12
	public int BashPriority
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x06003609 RID: 13833 RVA: 0x000E2B15 File Offset: 0x000E0D15
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision();
	}

	// Token: 0x0600360A RID: 13834 RVA: 0x000E2B1D File Offset: 0x000E0D1D
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision();
	}

	// Token: 0x0600360B RID: 13835 RVA: 0x000E2B28 File Offset: 0x000E0D28
	private void OnCollision()
	{
		if ((RockExplodeZone.IsInsideAZone(base.transform.position) || this.m_forceExplodeOnContact || this.ExplodeOnContact) && this.OnHitGround)
		{
			this.OnHitGround.Perform(null);
		}
	}

	// Token: 0x17000870 RID: 2160
	// (get) Token: 0x0600360C RID: 13836 RVA: 0x000E2B7C File Offset: 0x000E0D7C
	// (set) Token: 0x0600360D RID: 13837 RVA: 0x000E2B84 File Offset: 0x000E0D84
	public bool IsSuspended { get; set; }

	// Token: 0x04003092 RID: 12434
	private Transform m_transform;

	// Token: 0x04003093 RID: 12435
	public ActionSequence OnBashHighlightAction;

	// Token: 0x04003094 RID: 12436
	public ActionMethod OnHitGround;

	// Token: 0x04003095 RID: 12437
	public float Lifetime = 10f;

	// Token: 0x04003096 RID: 12438
	private float m_lifeRemaining;

	// Token: 0x04003097 RID: 12439
	private bool m_forceExplodeOnContact;

	// Token: 0x04003098 RID: 12440
	public bool ExplodeOnContact;

	// Token: 0x04003099 RID: 12441
	public bool Bashable = true;
}
