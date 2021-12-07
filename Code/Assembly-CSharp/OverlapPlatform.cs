using System;
using UnityEngine;

// Token: 0x02000917 RID: 2327
public class OverlapPlatform : SaveSerialize
{
	// Token: 0x06003393 RID: 13203 RVA: 0x000D9458 File Offset: 0x000D7658
	public void FixedUpdate()
	{
		this.m_animatingFloat.Update(Time.deltaTime);
		if (this.Activated != this.m_overlap)
		{
			this.Activated = this.m_overlap;
		}
		bool isTrigger = this.m_animatingFloat.Value < 0.25f;
		this.m_collider.isTrigger = isTrigger;
		this.m_overlap = false;
	}

	// Token: 0x06003394 RID: 13204 RVA: 0x000D94BC File Offset: 0x000D76BC
	public override void Awake()
	{
		this.m_animatingFloat.Min = 0f;
		this.m_animatingFloat.Max = 0.5f;
		this.m_collider = base.GetComponentInChildren<Collider>();
		this.m_flipPlants = base.GetComponentsInChildren<FlipPlantLogic>();
	}

	// Token: 0x06003395 RID: 13205 RVA: 0x000D9501 File Offset: 0x000D7701
	public void Start()
	{
		this.Activated = false;
	}

	// Token: 0x06003396 RID: 13206 RVA: 0x000D950A File Offset: 0x000D770A
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.m_overlap = false;
		}
	}

	// Token: 0x06003397 RID: 13207 RVA: 0x000D951E File Offset: 0x000D771E
	public void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<OverlapPlatformActivator>())
		{
			this.m_overlap = true;
		}
	}

	// Token: 0x06003398 RID: 13208 RVA: 0x000D9537 File Offset: 0x000D7737
	public void OnTriggerStay(Collider collider)
	{
		if (collider.GetComponent<OverlapPlatformActivator>())
		{
			this.m_overlap = true;
		}
	}

	// Token: 0x17000823 RID: 2083
	// (get) Token: 0x0600339A RID: 13210 RVA: 0x000D964E File Offset: 0x000D784E
	// (set) Token: 0x06003399 RID: 13209 RVA: 0x000D9550 File Offset: 0x000D7750
	public bool Activated
	{
		get
		{
			return this.m_activated;
		}
		set
		{
			this.m_activated = value;
			if (this.m_activated)
			{
				foreach (FlipPlantLogic flipPlantLogic in this.m_flipPlants)
				{
					flipPlantLogic.GoUp();
				}
				foreach (BaseAnimator baseAnimator in this.BaseAnimators)
				{
					baseAnimator.AnimatorDriver.ContinueForward();
				}
				this.m_animatingFloat.Speed = 1f;
			}
			else
			{
				foreach (FlipPlantLogic flipPlantLogic2 in this.m_flipPlants)
				{
					flipPlantLogic2.GoDown();
				}
				foreach (BaseAnimator baseAnimator2 in this.BaseAnimators)
				{
					baseAnimator2.AnimatorDriver.ContinueBackwards();
				}
				this.m_animatingFloat.Speed = -1f;
			}
		}
	}

	// Token: 0x04002E91 RID: 11921
	public BaseAnimator[] BaseAnimators;

	// Token: 0x04002E92 RID: 11922
	private FlipPlantLogic[] m_flipPlants;

	// Token: 0x04002E93 RID: 11923
	private Collider m_collider;

	// Token: 0x04002E94 RID: 11924
	private bool m_activated;

	// Token: 0x04002E95 RID: 11925
	private bool m_overlap;

	// Token: 0x04002E96 RID: 11926
	private readonly AnimatingFloat m_animatingFloat = new AnimatingFloat();
}
