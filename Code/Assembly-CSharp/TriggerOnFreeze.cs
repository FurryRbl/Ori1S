using System;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class TriggerOnFreeze : Trigger
{
	// Token: 0x0600199E RID: 6558 RVA: 0x0006E043 File Offset: 0x0006C243
	[UberBuildMethod]
	private void ProvideComponents()
	{
		this.m_freezable = base.gameObject.FindComponentInChildren<IFreezable>();
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x0006E056 File Offset: 0x0006C256
	private new void Awake()
	{
		base.Awake();
		this.ProvideComponents();
		this.m_freezable.OnFreezeEvent += this.OnFreeze;
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x0006E07B File Offset: 0x0006C27B
	private new void OnDestroy()
	{
		base.OnDestroy();
		this.m_freezable.OnFreezeEvent -= this.OnFreeze;
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x0006E09A File Offset: 0x0006C29A
	public void OnFreeze()
	{
		base.DoTrigger(true);
	}

	// Token: 0x04001607 RID: 5639
	public GameObject FreezableTarget;

	// Token: 0x04001608 RID: 5640
	[HideInInspector]
	[SerializeField]
	private IFreezable m_freezable;
}
