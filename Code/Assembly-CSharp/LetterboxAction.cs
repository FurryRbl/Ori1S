using System;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class LetterboxAction : ActionMethod
{
	// Token: 0x060016B9 RID: 5817 RVA: 0x0006356A File Offset: 0x0006176A
	public override void Perform(IContext context)
	{
		Letterbox.AnimationCurve = this.AnimationCurve;
		Letterbox.ShowLetterboxes = this.ShowLetterboxes;
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x00063582 File Offset: 0x00061782
	public override string GetNiceName()
	{
		return (!this.ShowLetterboxes) ? "Hide letterboxes" : "Show letterboxes";
	}

	// Token: 0x0400139A RID: 5018
	public bool ShowLetterboxes;

	// Token: 0x0400139B RID: 5019
	public AnimationCurve AnimationCurve = new AnimationCurve();
}
