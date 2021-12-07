using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class OnTimerPerformAction : SaveSerialize, ISuspendable
{
	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x06001702 RID: 5890 RVA: 0x00063F00 File Offset: 0x00062100
	// (set) Token: 0x06001703 RID: 5891 RVA: 0x00063F08 File Offset: 0x00062108
	public bool IsSuspended { get; set; }

	// Token: 0x06001704 RID: 5892 RVA: 0x00063F11 File Offset: 0x00062111
	public new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x00063F1F File Offset: 0x0006211F
	public void Start()
	{
		this.m_time = this.Offset;
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x00063F2D File Offset: 0x0006212D
	public new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x00063F3C File Offset: 0x0006213C
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_time += Time.fixedDeltaTime;
		if (this.m_time >= this.Interval)
		{
			this.m_time = 0f;
			if ((this.Condition == null || (this.Condition && this.Condition.Validate(null))) && this.Action)
			{
				this.Action.Perform(null);
			}
		}
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x00063FD1 File Offset: 0x000621D1
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_time);
	}

	// Token: 0x040013C8 RID: 5064
	public ActionMethod Action;

	// Token: 0x040013C9 RID: 5065
	public float Interval = 1f;

	// Token: 0x040013CA RID: 5066
	public Condition Condition;

	// Token: 0x040013CB RID: 5067
	public float Offset;

	// Token: 0x040013CC RID: 5068
	private float m_time;
}
