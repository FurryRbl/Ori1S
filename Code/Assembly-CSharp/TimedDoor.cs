using System;
using UnityEngine;

// Token: 0x020008DA RID: 2266
public class TimedDoor : SaveSerialize
{
	// Token: 0x06003290 RID: 12944 RVA: 0x000D5636 File Offset: 0x000D3836
	public override void Awake()
	{
		base.Awake();
		this.m_timer = base.GetComponent<TickingTimer>();
		this.m_animator = base.GetComponent<LegacyTranslateAnimator>();
		if (this.ShutdownDoor)
		{
			this.m_animator.Speed = 5f;
		}
	}

	// Token: 0x06003291 RID: 12945 RVA: 0x000D5671 File Offset: 0x000D3871
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
		}
	}

	// Token: 0x06003292 RID: 12946 RVA: 0x000D5680 File Offset: 0x000D3880
	public void OpenTheDoor()
	{
		if (this.m_isSolved)
		{
			return;
		}
		this.m_animator.Restart();
		if (this.m_timer)
		{
			this.m_timer.RestartTimer();
			base.CancelInvoke("CloseTheDoor");
			base.Invoke("CloseTheDoor", this.m_timer.Timeout - 2f);
		}
	}

	// Token: 0x06003293 RID: 12947 RVA: 0x000D56E8 File Offset: 0x000D38E8
	public void CloseTheDoor()
	{
		if (this.ShutdownDoor)
		{
			this.m_animator.Speed = 1f;
		}
		this.m_animator.RestartReverse();
		if (this.ActionAtTimeout)
		{
			this.ActionAtTimeout.Perform(null);
		}
	}

	// Token: 0x06003294 RID: 12948 RVA: 0x000D5738 File Offset: 0x000D3938
	public void PuzzleSolved()
	{
		if (this.m_isSolved)
		{
			return;
		}
		base.CancelInvoke("CloseTheDoor");
		this.m_timer.StopTimer();
		this.m_animator.Stop();
		this.m_animator.Reversed = false;
		this.m_animator.Continue();
		this.m_isSolved = true;
		this.ActionAtSolved.Perform(null);
	}

	// Token: 0x04002D6D RID: 11629
	public AudioClip ChallengeCompleteSound;

	// Token: 0x04002D6E RID: 11630
	public ActionMethod ActionAtTimeout;

	// Token: 0x04002D6F RID: 11631
	public ActionMethod ActionAtSolved;

	// Token: 0x04002D70 RID: 11632
	public bool ShutdownDoor;

	// Token: 0x04002D71 RID: 11633
	private TickingTimer m_timer;

	// Token: 0x04002D72 RID: 11634
	private bool m_isSolved;

	// Token: 0x04002D73 RID: 11635
	private LegacyTranslateAnimator m_animator;
}
