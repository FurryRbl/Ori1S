using System;
using UnityEngine;

// Token: 0x020002B9 RID: 697
[Category("Animator")]
public class AnimatorAction : ActionMethod
{
	// Token: 0x060015E9 RID: 5609 RVA: 0x00060FDC File Offset: 0x0005F1DC
	public new void Start()
	{
		base.Start();
		if (this.Target == null)
		{
			base.enabled = false;
			return;
		}
		if (this.AnimatorsMode == AnimatorAction.FindAnimatorsMode.GameObject)
		{
			this.Animators = this.Target.GetComponents<LegacyAnimator>();
		}
		if (this.AnimatorsMode == AnimatorAction.FindAnimatorsMode.GameObjectAndChildren)
		{
			this.Animators = this.Target.GetComponentsInChildren<LegacyAnimator>();
		}
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x00061044 File Offset: 0x0005F244
	public override void Perform(IContext context)
	{
		for (int i = 0; i < this.Animators.Length; i++)
		{
			LegacyAnimator legacyAnimator = this.Animators[i];
			if (legacyAnimator.enabled)
			{
				switch (this.Command)
				{
				case AnimatorAction.PlayMode.Restart:
					legacyAnimator.Restart();
					break;
				case AnimatorAction.PlayMode.RestartReversed:
					legacyAnimator.RestartReverse();
					break;
				case AnimatorAction.PlayMode.Reverse:
					legacyAnimator.Reverse();
					break;
				case AnimatorAction.PlayMode.Stop:
					legacyAnimator.Stop();
					break;
				case AnimatorAction.PlayMode.Continue:
					legacyAnimator.Continue();
					break;
				case AnimatorAction.PlayMode.ContinueForward:
					legacyAnimator.Reversed = false;
					legacyAnimator.Continue();
					break;
				case AnimatorAction.PlayMode.ContinueReversed:
					legacyAnimator.Reversed = true;
					legacyAnimator.Continue();
					break;
				case AnimatorAction.PlayMode.StopAtStart:
					legacyAnimator.Restart();
					legacyAnimator.Stop();
					break;
				case AnimatorAction.PlayMode.StopAtEnd:
					legacyAnimator.RestartReverse();
					legacyAnimator.Stop();
					break;
				}
				legacyAnimator.Sample(legacyAnimator.CurrentTime);
			}
		}
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x060015EB RID: 5611 RVA: 0x00061144 File Offset: 0x0005F344
	private string TargetName
	{
		get
		{
			return (this.AnimatorsMode != AnimatorAction.FindAnimatorsMode.SpecifyAnimators) ? ((!this.Target) ? "unkown" : this.Target.name) : ((this.Animators.Length <= 0 || !this.Animators[0]) ? "unkown" : this.Animators[0].name);
		}
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000611C0 File Offset: 0x0005F3C0
	public override string GetNiceName()
	{
		switch (this.Command)
		{
		case AnimatorAction.PlayMode.Restart:
			return "Restart " + this.TargetName + " animator";
		case AnimatorAction.PlayMode.RestartReversed:
			return "Restart reversed " + this.TargetName + " animator";
		case AnimatorAction.PlayMode.Reverse:
			return "Reverse " + this.TargetName + " animator";
		case AnimatorAction.PlayMode.Stop:
			return "Stop " + this.TargetName + " animator";
		case AnimatorAction.PlayMode.Continue:
			return "Continue " + this.TargetName + " animator";
		case AnimatorAction.PlayMode.ContinueForward:
			return "Continue " + this.TargetName + " animator forward";
		case AnimatorAction.PlayMode.ContinueReversed:
			return "Continue " + this.TargetName + " animator reversed";
		case AnimatorAction.PlayMode.StopAtStart:
			return "Stop " + this.TargetName + " animator at start";
		case AnimatorAction.PlayMode.StopAtEnd:
			return "Stop " + this.TargetName + " animator at end";
		default:
			return base.GetNiceName();
		}
	}

	// Token: 0x040012C9 RID: 4809
	[NotNull]
	public GameObject Target;

	// Token: 0x040012CA RID: 4810
	public AnimatorAction.FindAnimatorsMode AnimatorsMode;

	// Token: 0x040012CB RID: 4811
	public AnimatorAction.PlayMode Command;

	// Token: 0x040012CC RID: 4812
	public LegacyAnimator[] Animators;

	// Token: 0x020002BA RID: 698
	public enum PlayMode
	{
		// Token: 0x040012CE RID: 4814
		Restart,
		// Token: 0x040012CF RID: 4815
		RestartReversed,
		// Token: 0x040012D0 RID: 4816
		Reverse,
		// Token: 0x040012D1 RID: 4817
		Stop,
		// Token: 0x040012D2 RID: 4818
		Continue,
		// Token: 0x040012D3 RID: 4819
		ContinueForward,
		// Token: 0x040012D4 RID: 4820
		ContinueReversed,
		// Token: 0x040012D5 RID: 4821
		StopAtStart,
		// Token: 0x040012D6 RID: 4822
		StopAtEnd
	}

	// Token: 0x020002BB RID: 699
	public enum FindAnimatorsMode
	{
		// Token: 0x040012D8 RID: 4824
		GameObject,
		// Token: 0x040012D9 RID: 4825
		GameObjectAndChildren,
		// Token: 0x040012DA RID: 4826
		SpecifyAnimators
	}
}
