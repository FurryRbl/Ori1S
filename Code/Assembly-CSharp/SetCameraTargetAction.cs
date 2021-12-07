using System;
using Game;
using UnityEngine;

// Token: 0x02000333 RID: 819
[Category("Camera")]
public class SetCameraTargetAction : ActionMethod
{
	// Token: 0x060017C1 RID: 6081 RVA: 0x0006606F File Offset: 0x0006426F
	public override void Perform(IContext context)
	{
		UI.Cameras.Current.Target = this.Target;
		UI.Cameras.Current.MoveCameraToTargetInstantly(true);
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x0006608C File Offset: 0x0006428C
	public override string GetNiceName()
	{
		return "Set camera target to " + ActionHelper.GetName(this.Target);
	}

	// Token: 0x0400146E RID: 5230
	[NotNull]
	public Transform Target;
}
