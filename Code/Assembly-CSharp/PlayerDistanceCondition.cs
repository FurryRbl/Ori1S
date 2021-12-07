using System;
using Game;
using UnityEngine;

// Token: 0x0200066E RID: 1646
public class PlayerDistanceCondition : Condition
{
	// Token: 0x06002814 RID: 10260 RVA: 0x000AE1D8 File Offset: 0x000AC3D8
	public override bool Validate(IContext context)
	{
		return Vector3.Distance(base.transform.position, UI.Cameras.Current.Target.position) < this.Distance;
	}

	// Token: 0x040022AB RID: 8875
	public float Distance = 1f;
}
