using System;
using Game;
using UnityEngine;

// Token: 0x020002FF RID: 767
[Category("Camera")]
public class MoveCameraAction : PerformingAction
{
	// Token: 0x060016E3 RID: 5859 RVA: 0x00063AD0 File Offset: 0x00061CD0
	public override void Perform(IContext context)
	{
		Vector3 position = this.Target.transform.position;
		UI.Cameras.Current.MoveToTarget(position, this.MovementDuration, false);
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x00063B00 File Offset: 0x00061D00
	public override void Stop()
	{
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00063B02 File Offset: 0x00061D02
	public override bool IsPerforming
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x00063B08 File Offset: 0x00061D08
	public override string GetNiceName()
	{
		return string.Concat(new object[]
		{
			"Move camera to ",
			ActionHelper.GetName(this.Target),
			" over ",
			this.MovementDuration,
			(this.MovementDuration != 1f) ? " seconds" : " second"
		});
	}

	// Token: 0x040013B0 RID: 5040
	[NotNull]
	public GameObject Target;

	// Token: 0x040013B1 RID: 5041
	public float MovementDuration;
}
