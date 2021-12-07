using System;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class IsInZoneCondition : Condition
{
	// Token: 0x06001555 RID: 5461 RVA: 0x0005EFC8 File Offset: 0x0005D1C8
	public override bool Validate(IContext context)
	{
		foreach (Transform transform in this.Zones)
		{
			Rect rect = default(Rect);
			rect.width = transform.lossyScale.x;
			rect.height = transform.lossyScale.y;
			rect.center = transform.position;
			if (rect.Contains(this.Target.position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04001281 RID: 4737
	public Transform Target;

	// Token: 0x04001282 RID: 4738
	public Transform[] Zones;
}
