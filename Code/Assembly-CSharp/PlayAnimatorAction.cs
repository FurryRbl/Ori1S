using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030F RID: 783
[Category("Deprecated")]
public class PlayAnimatorAction : ActionWithDuration
{
	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x06001736 RID: 5942 RVA: 0x000644AC File Offset: 0x000626AC
	private LegacyAnimator[] Animators
	{
		get
		{
			if (this.m_animators == null)
			{
				this.m_animators = this.Target.GetComponentsInChildren<LegacyAnimator>();
				if (this.PauseAnimatorsOnStart)
				{
					for (int i = 0; i < this.m_animators.Length; i++)
					{
						LegacyAnimator legacyAnimator = this.m_animators[i];
						legacyAnimator.Stop();
					}
				}
			}
			return this.m_animators;
		}
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x00064510 File Offset: 0x00062710
	public void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.AnimationDuration == 0f && this.UseAnimatorsDuration && this.Animators.Length > 0)
		{
			this.AnimationDuration = this.Animators[0].MaxTime;
		}
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x00064564 File Offset: 0x00062764
	public override void Perform(IContext context)
	{
		base.StopCoroutine("PerformActionCoroutine");
		base.StartCoroutine("PerformActionCoroutine");
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x00064580 File Offset: 0x00062780
	public override void Stop()
	{
		base.StopCoroutine("PerformActionCoroutine");
		foreach (LegacyAnimator legacyAnimator in this.Animators)
		{
			legacyAnimator.Stop();
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x0600173A RID: 5946 RVA: 0x000645BD File Offset: 0x000627BD
	public override bool IsPerforming
	{
		get
		{
			return this.m_isPerformingAction;
		}
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000645C8 File Offset: 0x000627C8
	public IEnumerator PerformActionCoroutine()
	{
		this.m_isPerformingAction = true;
		Dictionary<LegacyAnimator, float> startTimeValues = new Dictionary<LegacyAnimator, float>();
		foreach (LegacyAnimator animator in this.Animators)
		{
			if (!this.UseAnimatorsDuration && (animator.AnimationCurve.postWrapMode == WrapMode.Once || animator.AnimationCurve.postWrapMode == WrapMode.ClampForever))
			{
				animator.Speed = 1f / this.Duration;
			}
			if (this.ReverseIfAnimating && !animator.Stopped)
			{
				animator.Reverse();
			}
			else
			{
				if (this.Continue)
				{
					animator.Continue();
				}
				else if (this.Reverse)
				{
					animator.Reverse();
				}
				else if (this.PlayReverse)
				{
					animator.RestartReverse();
				}
				else if (this.ContinueForward)
				{
					animator.Continue();
					animator.Reversed = false;
				}
				else if (this.ContinueBackward)
				{
					animator.Continue();
					animator.Reversed = true;
				}
				else
				{
					animator.Restart();
				}
				startTimeValues[animator] = animator.CurrentTime;
			}
		}
		for (float t = 0f; t < this.Duration; t += ((!this.IsSuspended) ? Time.deltaTime : 0f))
		{
			yield return new WaitForFixedUpdate();
		}
		this.m_isPerformingAction = false;
		for (int i = 0; i < this.Animators.Length; i++)
		{
			LegacyAnimator animator2 = this.Animators[i];
			if (animator2.Reversed)
			{
				float sample = startTimeValues[animator2] - this.Duration;
				animator2.Sample(Mathf.Max(sample, animator2.MinTime));
			}
			else
			{
				float sample2 = startTimeValues[animator2] + this.Duration;
				animator2.Sample(Mathf.Min(sample2, animator2.MaxTime));
			}
			if (this.StopAtTimeout)
			{
				animator2.Stop();
			}
		}
		yield break;
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x0600173C RID: 5948 RVA: 0x000645E3 File Offset: 0x000627E3
	// (set) Token: 0x0600173D RID: 5949 RVA: 0x000645EB File Offset: 0x000627EB
	public override float Duration
	{
		get
		{
			return this.AnimationDuration;
		}
		set
		{
			this.AnimationDuration = value;
		}
	}

	// Token: 0x040013E9 RID: 5097
	[NotNull]
	public GameObject Target;

	// Token: 0x040013EA RID: 5098
	public bool PlayReverse;

	// Token: 0x040013EB RID: 5099
	public bool Continue;

	// Token: 0x040013EC RID: 5100
	public bool Reverse;

	// Token: 0x040013ED RID: 5101
	public bool ContinueForward;

	// Token: 0x040013EE RID: 5102
	public bool ContinueBackward;

	// Token: 0x040013EF RID: 5103
	public bool PauseAnimatorsOnStart = true;

	// Token: 0x040013F0 RID: 5104
	public bool ReverseIfAnimating;

	// Token: 0x040013F1 RID: 5105
	public bool UseAnimatorsDuration;

	// Token: 0x040013F2 RID: 5106
	public bool StopAtTimeout = true;

	// Token: 0x040013F3 RID: 5107
	private LegacyAnimator[] m_animators;

	// Token: 0x040013F4 RID: 5108
	private bool m_isPerformingAction;

	// Token: 0x040013F5 RID: 5109
	public float AnimationDuration;
}
