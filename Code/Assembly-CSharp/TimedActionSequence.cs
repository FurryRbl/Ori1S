using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000270 RID: 624
[AddComponentMenu("Event Framework/Actions/Timed action sequence")]
public class TimedActionSequence : ActionWithDuration
{
	// Token: 0x060014D4 RID: 5332 RVA: 0x0005DB6E File Offset: 0x0005BD6E
	public override void Awake()
	{
		base.Awake();
		if (this.Actions.Count == 0)
		{
			this.FindActions();
		}
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x0005DB8C File Offset: 0x0005BD8C
	public void FindActions()
	{
		this.Actions = new List<TimedActionExecuter>();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			for (int j = 0; j < child.GetComponents<TimedActionExecuter>().Length; j++)
			{
				TimedActionExecuter item = child.GetComponents<TimedActionExecuter>()[j];
				this.Actions.Add(item);
			}
		}
		this.Actions.Sort((TimedActionExecuter a, TimedActionExecuter b) => a.StartTime.CompareTo(b.StartTime));
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x0005DC23 File Offset: 0x0005BE23
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0005DC31 File Offset: 0x0005BE31
	// (set) Token: 0x060014D8 RID: 5336 RVA: 0x0005DC38 File Offset: 0x0005BE38
	public override float Duration
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x0005DC3A File Offset: 0x0005BE3A
	public override void Perform(IContext context)
	{
		base.StartCoroutine(this.RunSequence(context));
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x0005DC4C File Offset: 0x0005BE4C
	public override void Stop()
	{
		base.StopAllCoroutines();
		for (int i = 0; i < this.Actions.Count; i++)
		{
			TimedActionExecuter timedActionExecuter = this.Actions[i];
			timedActionExecuter.StopAction();
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x060014DB RID: 5339 RVA: 0x0005DC90 File Offset: 0x0005BE90
	public override bool IsPerforming
	{
		get
		{
			if (base.IsInvoking("RunActions"))
			{
				return true;
			}
			for (int i = 0; i < this.Actions.Count; i++)
			{
				TimedActionExecuter timedActionExecuter = this.Actions[i];
				if (timedActionExecuter.ActionWithDuration.IsPerforming)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x060014DC RID: 5340 RVA: 0x0005DCEB File Offset: 0x0005BEEB
	// (set) Token: 0x060014DD RID: 5341 RVA: 0x0005DCF3 File Offset: 0x0005BEF3
	public float SequenceCurrentTime { get; set; }

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x060014DE RID: 5342 RVA: 0x0005DCFC File Offset: 0x0005BEFC
	// (set) Token: 0x060014DF RID: 5343 RVA: 0x0005DD04 File Offset: 0x0005BF04
	public bool IsSequenceRunning { get; set; }

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0005DD0D File Offset: 0x0005BF0D
	// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0005DD15 File Offset: 0x0005BF15
	public int Pause { get; set; }

	// Token: 0x060014E2 RID: 5346 RVA: 0x0005DD20 File Offset: 0x0005BF20
	private IEnumerator RunSequence(IContext context)
	{
		this.IsSequenceRunning = true;
		this.SequenceCurrentTime = 0f;
		for (int i = 0; i < this.Actions.Count; i++)
		{
			TimedActionExecuter timedActionExecuter = this.Actions[i];
			while (this.SequenceCurrentTime < timedActionExecuter.StartTime)
			{
				this.SequenceCurrentTime += Time.deltaTime;
				yield return new WaitForFixedUpdate();
				while (this.IsSuspended || this.Pause > 0)
				{
					yield return new WaitForFixedUpdate();
				}
			}
			while (this.IsSuspended || this.Pause > 0)
			{
				yield return new WaitForFixedUpdate();
			}
			timedActionExecuter.ExecuteAction(context);
		}
		this.IsSequenceRunning = false;
		yield break;
	}

	// Token: 0x0400121A RID: 4634
	public List<TimedActionExecuter> Actions;

	// Token: 0x0400121B RID: 4635
	public bool ShowCreateActions;
}
