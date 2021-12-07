using System;
using Game;
using UnityEngine;

// Token: 0x020009CB RID: 2507
public class WindSegment : SaveSerialize, ISuspendable
{
	// Token: 0x060036AE RID: 13998 RVA: 0x000E5BD0 File Offset: 0x000E3DD0
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(this.Trigger));
	}

	// Token: 0x060036AF RID: 13999 RVA: 0x000E5BFF File Offset: 0x000E3DFF
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x000E5C0D File Offset: 0x000E3E0D
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_remainingTime);
		this.m_state = (WindSegment.State)ar.Serialize((int)this.m_state);
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x000E5C30 File Offset: 0x000E3E30
	public void OnEntered()
	{
		if (this.m_state != WindSegment.State.Start)
		{
			return;
		}
		this.m_state = WindSegment.State.Timed;
		this.m_remainingTime = this.MaxTime;
		WindSegment windSegment = WindShaftController.Instance.FindPrevious(this);
		if (windSegment)
		{
			windSegment.OnVisitedNextSegment();
		}
	}

	// Token: 0x060036B2 RID: 14002 RVA: 0x000E5C79 File Offset: 0x000E3E79
	public void OnVisitedNextSegment()
	{
		if (this.m_state == WindSegment.State.Timed)
		{
			this.m_remainingTime = Mathf.Min(this.m_remainingTime, this.MinTime);
		}
	}

	// Token: 0x060036B3 RID: 14003 RVA: 0x000E5CA0 File Offset: 0x000E3EA0
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_state == WindSegment.State.Timed)
		{
			this.m_remainingTime -= Time.deltaTime;
			if (this.m_remainingTime <= 0f)
			{
				this.Finish();
			}
		}
		if (this.m_state == WindSegment.State.Start && this.m_bounds.Contains(Characters.Sein.Position))
		{
			this.OnEntered();
		}
	}

	// Token: 0x060036B4 RID: 14004 RVA: 0x000E5D18 File Offset: 0x000E3F18
	public void Finish()
	{
		if (this.m_state == WindSegment.State.End)
		{
			return;
		}
		this.m_state = WindSegment.State.End;
		if (this.FinishVent)
		{
		}
	}

	// Token: 0x1700087C RID: 2172
	// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000E5D49 File Offset: 0x000E3F49
	// (set) Token: 0x060036B6 RID: 14006 RVA: 0x000E5D51 File Offset: 0x000E3F51
	public bool IsSuspended { get; set; }

	// Token: 0x04003195 RID: 12693
	public float MaxTime = 8f;

	// Token: 0x04003196 RID: 12694
	public float MinTime = 1f;

	// Token: 0x04003197 RID: 12695
	public WindVent FinishVent;

	// Token: 0x04003198 RID: 12696
	private float m_remainingTime;

	// Token: 0x04003199 RID: 12697
	public Transform Trigger;

	// Token: 0x0400319A RID: 12698
	private Rect m_bounds;

	// Token: 0x0400319B RID: 12699
	private WindSegment.State m_state;

	// Token: 0x020009CC RID: 2508
	private enum State
	{
		// Token: 0x0400319E RID: 12702
		Start,
		// Token: 0x0400319F RID: 12703
		Timed,
		// Token: 0x040031A0 RID: 12704
		End
	}
}
