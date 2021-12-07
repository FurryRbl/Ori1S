using System;
using Game;

// Token: 0x020003A3 RID: 931
public class LegacyCameraSwayingAnimator : LegacySinMovementAnimator
{
	// Token: 0x06001A15 RID: 6677 RVA: 0x000704D2 File Offset: 0x0006E6D2
	protected override void AnimateIt(float value)
	{
		if (this.Target == null)
		{
			this.Target = UI.Cameras.Current.GameObject.GetComponentInChildren<SinMovement>();
		}
		base.AnimateIt(value);
	}
}
