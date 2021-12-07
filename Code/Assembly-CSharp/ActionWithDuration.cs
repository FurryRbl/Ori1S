using System;

// Token: 0x02000258 RID: 600
public abstract class ActionWithDuration : PerformingAction, ISuspendable
{
	// Token: 0x0600142E RID: 5166 RVA: 0x0005BD84 File Offset: 0x00059F84
	public override void Awake()
	{
		SuspensionManager.Register(this);
		base.Awake();
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x0005BD92 File Offset: 0x00059F92
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		base.OnDestroy();
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x06001431 RID: 5169
	// (set) Token: 0x06001430 RID: 5168
	public abstract float Duration { get; set; }

	// Token: 0x06001432 RID: 5170 RVA: 0x0005BDA0 File Offset: 0x00059FA0
	public override void Serialize(Archive ar)
	{
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005BDA2 File Offset: 0x00059FA2
	// (set) Token: 0x06001434 RID: 5172 RVA: 0x0005BDAA File Offset: 0x00059FAA
	public bool IsSuspended { get; set; }
}
