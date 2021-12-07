using System;
using Game;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class SeinInsideZoneCondition : Condition
{
	// Token: 0x06001975 RID: 6517 RVA: 0x0006D9A8 File Offset: 0x0006BBA8
	public override bool Validate(IContext context)
	{
		if (Characters.Current == null)
		{
			return false;
		}
		Vector3 position = Characters.Current.Position;
		foreach (Transform transform in this.Zones)
		{
			if (new Rect
			{
				width = transform.transform.lossyScale.x,
				height = transform.transform.lossyScale.y,
				center = transform.transform.position
			}.Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040015E4 RID: 5604
	public Transform[] Zones;
}
