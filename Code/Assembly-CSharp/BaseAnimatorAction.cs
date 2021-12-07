using System;
using UnityEngine;

// Token: 0x020002BF RID: 703
[Category("BaseAnimator")]
public class BaseAnimatorAction : ActionMethod
{
	// Token: 0x060015FB RID: 5627 RVA: 0x00061480 File Offset: 0x0005F680
	public new void Start()
	{
		base.Start();
		if (this.AnimatorsMode == BaseAnimatorAction.FindAnimatorsMode.GameObject)
		{
			this.Animators = this.Target.GetComponents<BaseAnimator>();
		}
		if (this.AnimatorsMode == BaseAnimatorAction.FindAnimatorsMode.GameObjectAndChildren)
		{
			this.Animators = this.Target.GetComponentsInChildren<BaseAnimator>();
		}
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x000614CC File Offset: 0x0005F6CC
	public override void Perform(IContext context)
	{
		for (int i = 0; i < this.Animators.Length; i++)
		{
			BaseAnimator baseAnimator = this.Animators[i];
			if (baseAnimator.enabled)
			{
				baseAnimator.Initialize();
				switch (this.Command)
				{
				case BaseAnimatorAction.PlayMode.Restart:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.SetForward();
					baseAnimator.AnimatorDriver.Restart();
					break;
				case BaseAnimatorAction.PlayMode.RestartReversed:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.SetBackwards();
					baseAnimator.AnimatorDriver.Restart();
					break;
				case BaseAnimatorAction.PlayMode.Reverse:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.Reverse();
					break;
				case BaseAnimatorAction.PlayMode.Stop:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.Stop();
					break;
				case BaseAnimatorAction.PlayMode.Continue:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.Resume();
					break;
				case BaseAnimatorAction.PlayMode.ContinueForward:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.SetForward();
					baseAnimator.AnimatorDriver.Resume();
					break;
				case BaseAnimatorAction.PlayMode.ContinueReversed:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.SetBackwards();
					baseAnimator.AnimatorDriver.Resume();
					break;
				case BaseAnimatorAction.PlayMode.StopAtStart:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.Pause();
					baseAnimator.AnimatorDriver.GoToStart();
					break;
				case BaseAnimatorAction.PlayMode.StopAtEnd:
					baseAnimator.Initialize();
					baseAnimator.AnimatorDriver.Pause();
					baseAnimator.AnimatorDriver.GoToEnd();
					break;
				}
			}
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x060015FD RID: 5629 RVA: 0x00061650 File Offset: 0x0005F850
	private string TargetName
	{
		get
		{
			return (this.AnimatorsMode != BaseAnimatorAction.FindAnimatorsMode.SpecifyAnimators) ? ((!this.Target) ? "unkown" : this.Target.name) : ((this.Animators.Length <= 0 || !this.Animators[0]) ? "unkown" : this.Animators[0].name);
		}
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x000616CC File Offset: 0x0005F8CC
	public override string GetNiceName()
	{
		switch (this.Command)
		{
		case BaseAnimatorAction.PlayMode.Restart:
			return "Restart " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.RestartReversed:
			return "Restart reversed " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.Reverse:
			return "Reverse " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.Stop:
			return "Stop " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.Continue:
			return "Continue " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.ContinueForward:
			return "Continue forward " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.ContinueReversed:
			return "Continue reversed " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.StopAtStart:
			return "Stop at start " + this.TargetName + " BaseAnimator";
		case BaseAnimatorAction.PlayMode.StopAtEnd:
			return "Stop at end " + this.TargetName + " BaseAnimator";
		default:
			return base.GetNiceName();
		}
	}

	// Token: 0x040012E2 RID: 4834
	[NotNull]
	public GameObject Target;

	// Token: 0x040012E3 RID: 4835
	public BaseAnimatorAction.FindAnimatorsMode AnimatorsMode;

	// Token: 0x040012E4 RID: 4836
	public BaseAnimatorAction.PlayMode Command;

	// Token: 0x040012E5 RID: 4837
	public BaseAnimator[] Animators;

	// Token: 0x020002C0 RID: 704
	public enum PlayMode
	{
		// Token: 0x040012E7 RID: 4839
		Restart,
		// Token: 0x040012E8 RID: 4840
		RestartReversed,
		// Token: 0x040012E9 RID: 4841
		Reverse,
		// Token: 0x040012EA RID: 4842
		Stop,
		// Token: 0x040012EB RID: 4843
		Continue,
		// Token: 0x040012EC RID: 4844
		ContinueForward,
		// Token: 0x040012ED RID: 4845
		ContinueReversed,
		// Token: 0x040012EE RID: 4846
		StopAtStart,
		// Token: 0x040012EF RID: 4847
		StopAtEnd
	}

	// Token: 0x020002C1 RID: 705
	public enum FindAnimatorsMode
	{
		// Token: 0x040012F1 RID: 4849
		GameObject,
		// Token: 0x040012F2 RID: 4850
		GameObjectAndChildren,
		// Token: 0x040012F3 RID: 4851
		SpecifyAnimators
	}
}
