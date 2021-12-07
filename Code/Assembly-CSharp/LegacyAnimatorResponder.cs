using System;
using UnityEngine;

// Token: 0x0200039C RID: 924
public class LegacyAnimatorResponder : MonoBehaviour
{
	// Token: 0x060019F5 RID: 6645 RVA: 0x0006F810 File Offset: 0x0006DA10
	public void FixedUpdate()
	{
		if (this.Animator.Stopped != this.m_wasStopped || this.Animator.Reversed != this.m_wasReversed)
		{
			if (this.StopReverseAction && ((this.Animator.Stopped && this.m_wasReversed) || (this.m_wasReversed && !this.Animator.Reversed)))
			{
				this.StopReverseAction.Perform(null);
			}
			if (this.StopAction && ((this.Animator.Stopped && !this.m_wasReversed) || (!this.m_wasReversed && this.Animator.Reversed)))
			{
				this.StopAction.Perform(null);
			}
			if (!this.Animator.Stopped)
			{
				if (this.ContinueAction && ((this.m_wasStopped && !this.Animator.Reversed) || (this.m_wasReversed && !this.Animator.Reversed)))
				{
					this.ContinueAction.Perform(null);
				}
				if (this.ContinueReverseAction && ((this.m_wasStopped && this.Animator.Reversed) || (!this.m_wasReversed && this.Animator.Reversed)))
				{
					this.ContinueReverseAction.Perform(null);
				}
			}
		}
		if (this.Animator.CurrentTime < this.ActionTime)
		{
			this.m_timeActionDone = false;
		}
		if (this.Animator.CurrentTime > this.ActionReverseTime)
		{
			this.m_reverseTimeActionDone = false;
		}
		if (this.OnReachTimeAction && this.Animator.CurrentTime >= this.ActionTime && !this.Animator.Reversed && !this.m_timeActionDone)
		{
			this.OnReachTimeAction.Perform(null);
			this.m_timeActionDone = true;
		}
		if (this.OnReachTimeReverseAction && this.Animator.CurrentTime <= this.ActionReverseTime && this.Animator.Reversed && !this.m_reverseTimeActionDone)
		{
			this.OnReachTimeReverseAction.Perform(null);
			this.m_reverseTimeActionDone = true;
		}
		this.m_wasStopped = this.Animator.Stopped;
		this.m_wasReversed = this.Animator.Reversed;
	}

	// Token: 0x04001654 RID: 5716
	public LegacyAnimator Animator;

	// Token: 0x04001655 RID: 5717
	public ActionMethod ContinueAction;

	// Token: 0x04001656 RID: 5718
	public ActionMethod ContinueReverseAction;

	// Token: 0x04001657 RID: 5719
	public ActionMethod StopAction;

	// Token: 0x04001658 RID: 5720
	public ActionMethod StopReverseAction;

	// Token: 0x04001659 RID: 5721
	public ActionMethod OnReachTimeAction;

	// Token: 0x0400165A RID: 5722
	public float ActionTime;

	// Token: 0x0400165B RID: 5723
	private bool m_timeActionDone;

	// Token: 0x0400165C RID: 5724
	public ActionMethod OnReachTimeReverseAction;

	// Token: 0x0400165D RID: 5725
	public float ActionReverseTime;

	// Token: 0x0400165E RID: 5726
	private bool m_reverseTimeActionDone;

	// Token: 0x0400165F RID: 5727
	private bool m_wasStopped = true;

	// Token: 0x04001660 RID: 5728
	private bool m_wasReversed;
}
