using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x0200096C RID: 2412
public class FixedUpdateScheduler : MonoBehaviour
{
	// Token: 0x060034ED RID: 13549 RVA: 0x000DE1DA File Offset: 0x000DC3DA
	public void Awake()
	{
		Schedules.FixedUpdate = this;
	}

	// Token: 0x060034EE RID: 13550 RVA: 0x000DE1E4 File Offset: 0x000DC3E4
	private void FixedUpdate()
	{
		for (int i = 0; i < this.SchedulesActions.Count; i++)
		{
			FixedUpdateScheduler.ScheduledAction scheduledAction = this.SchedulesActions[i];
			scheduledAction.Advance();
			if (scheduledAction.RemainingTime <= 0f)
			{
				this.SchedulesActions.RemoveAt(i);
				i--;
				scheduledAction.Invoke();
			}
		}
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x000DE246 File Offset: 0x000DC446
	public void ScheduleAction(ISuspendable suspendable, Action action, float time)
	{
		if (time != 0f)
		{
			this.SchedulesActions.Add(new FixedUpdateScheduler.ScheduledAction(action, suspendable, time));
		}
	}

	// Token: 0x04002FA5 RID: 12197
	public List<FixedUpdateScheduler.ScheduledAction> SchedulesActions = new List<FixedUpdateScheduler.ScheduledAction>();

	// Token: 0x0200096D RID: 2413
	public class ScheduledAction
	{
		// Token: 0x060034F0 RID: 13552 RVA: 0x000DE266 File Offset: 0x000DC466
		public ScheduledAction(Action action, ISuspendable suspendable, float remainingTime)
		{
			this.Action = action;
			this.Suspendable = suspendable;
			this.RemainingTime = remainingTime;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000DE283 File Offset: 0x000DC483
		public void Invoke()
		{
			this.Action();
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000DE290 File Offset: 0x000DC490
		public void Advance()
		{
			if (!this.Suspendable.IsSuspended)
			{
				this.RemainingTime -= Time.deltaTime;
			}
		}

		// Token: 0x04002FA6 RID: 12198
		public Action Action;

		// Token: 0x04002FA7 RID: 12199
		public ISuspendable Suspendable;

		// Token: 0x04002FA8 RID: 12200
		public float RemainingTime;
	}
}
