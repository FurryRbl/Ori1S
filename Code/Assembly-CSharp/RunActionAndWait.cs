using System;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class RunActionAndWait : ActionMethod, ISuspendable
{
	// Token: 0x06001781 RID: 6017 RVA: 0x0006522A File Offset: 0x0006342A
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x00065238 File Offset: 0x00063438
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x00065248 File Offset: 0x00063448
	public override void Perform(IContext context)
	{
		if (this.m_remaingWaitTime <= 0f)
		{
			this.Action.Perform(context);
			this.m_remaingWaitTime = this.WaitingDuration;
		}
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x00065280 File Offset: 0x00063480
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_remaingWaitTime > 0f)
		{
			this.m_remaingWaitTime -= Time.deltaTime;
			if (this.m_remaingWaitTime < 0f)
			{
				this.m_remaingWaitTime = 0f;
			}
		}
	}

	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x06001785 RID: 6021 RVA: 0x000652D6 File Offset: 0x000634D6
	// (set) Token: 0x06001786 RID: 6022 RVA: 0x000652DE File Offset: 0x000634DE
	public bool IsSuspended { get; set; }

	// Token: 0x04001426 RID: 5158
	public float WaitingDuration;

	// Token: 0x04001427 RID: 5159
	[NotNull]
	public ActionMethod Action;

	// Token: 0x04001428 RID: 5160
	private float m_remaingWaitTime;
}
