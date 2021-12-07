using System;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class TimedTrigger : SaveSerialize, ISuspendable
{
	// Token: 0x0600198D RID: 6541 RVA: 0x0006DDF3 File Offset: 0x0006BFF3
	public new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x0006DE01 File Offset: 0x0006C001
	public new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x0006DE0F File Offset: 0x0006C00F
	public void Start()
	{
		this.m_time = this.StartTime;
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x0006DE20 File Offset: 0x0006C020
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.Paused)
		{
			return;
		}
		if (this.m_time <= 0f)
		{
			if (this.TriggerName != string.Empty)
			{
				if (this.Reciever)
				{
					this.Reciever.SendMessage(this.TriggerName);
				}
				else
				{
					base.SendMessage(this.TriggerName);
				}
			}
			if (this.Action)
			{
				this.Action.Perform(null);
			}
			if (!this.Repeat)
			{
				base.gameObject.SetActive(false);
			}
			this.m_time = this.Duration;
		}
		this.m_time -= Time.deltaTime;
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x0006DEED File Offset: 0x0006C0ED
	public void TriggerNow()
	{
		this.m_time = 0f;
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x0006DEFA File Offset: 0x0006C0FA
	public void Reset()
	{
		this.m_time = this.Duration;
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x0006DF08 File Offset: 0x0006C108
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_time);
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x06001994 RID: 6548 RVA: 0x0006DF16 File Offset: 0x0006C116
	// (set) Token: 0x06001995 RID: 6549 RVA: 0x0006DF1E File Offset: 0x0006C11E
	public bool IsSuspended { get; set; }

	// Token: 0x040015F3 RID: 5619
	public float Duration;

	// Token: 0x040015F4 RID: 5620
	public bool Repeat = true;

	// Token: 0x040015F5 RID: 5621
	public ActionMethod Action;

	// Token: 0x040015F6 RID: 5622
	public string TriggerName = "OnTimedTrigger";

	// Token: 0x040015F7 RID: 5623
	public Component Reciever;

	// Token: 0x040015F8 RID: 5624
	public float StartTime;

	// Token: 0x040015F9 RID: 5625
	public bool Paused;

	// Token: 0x040015FA RID: 5626
	private float m_time;
}
