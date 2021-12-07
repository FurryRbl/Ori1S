using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class Trigger : SaveSerialize, ISuspendable
{
	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x0600187B RID: 6267 RVA: 0x00069082 File Offset: 0x00067282
	// (set) Token: 0x0600187C RID: 6268 RVA: 0x0006908A File Offset: 0x0006728A
	public bool IsSuspended { get; set; }

	// Token: 0x0600187D RID: 6269 RVA: 0x00069093 File Offset: 0x00067293
	public new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x000690A1 File Offset: 0x000672A1
	public new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x000690B0 File Offset: 0x000672B0
	public void DoTrigger(bool shouldCheckCondition = true)
	{
		if (this.Active)
		{
			if (this.DontTriggerWhileRunning && this.ActionToRun is ActionWithDuration && ((ActionWithDuration)this.ActionToRun).IsPerforming)
			{
				return;
			}
			if (shouldCheckCondition && this.Condition && !this.Condition.Validate(null))
			{
				return;
			}
			if (this.TriggerOnce)
			{
				this.Active = false;
			}
			base.StartCoroutine(this.ProcessTrigger());
		}
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x00069140 File Offset: 0x00067340
	private IEnumerator ProcessTrigger()
	{
		for (float t = 0f; t < this.Delay; t += ((!this.IsSuspended) ? Time.deltaTime : 0f))
		{
			yield return new WaitForFixedUpdate();
		}
		if (SceneFPSTest.IsRunning())
		{
			yield break;
		}
		if (this.ActionToRun)
		{
			this.ActionToRun.Perform(null);
		}
		yield break;
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x0006915B File Offset: 0x0006735B
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Active);
	}

	// Token: 0x04001506 RID: 5382
	public ActionMethod ActionToRun;

	// Token: 0x04001507 RID: 5383
	public bool Active = true;

	// Token: 0x04001508 RID: 5384
	public Condition Condition;

	// Token: 0x04001509 RID: 5385
	public float Delay;

	// Token: 0x0400150A RID: 5386
	public bool DontTriggerWhileRunning;

	// Token: 0x0400150B RID: 5387
	public bool TriggerOnce;

	// Token: 0x0400150C RID: 5388
	public float Wait;
}
