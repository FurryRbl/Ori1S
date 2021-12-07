using System;
using Game;
using UnityEngine;

// Token: 0x02000722 RID: 1826
[ExecuteInEditMode]
public class VisibleOnWorldMap : GuidOwner
{
	// Token: 0x06002B1A RID: 11034 RVA: 0x000B8978 File Offset: 0x000B6B78
	public void FixedUpdate()
	{
		if (this.RevealWhenOnScreen && !UI.Fader.IsFadingInOrStay() && UI.Cameras.Current && UI.Cameras.Current.CameraBoundingBox.Contains(base.transform.position))
		{
			GameWorld.Instance.RevealIcon(this.MoonGuid);
		}
	}

	// Token: 0x040026B1 RID: 9905
	public WorldMapIconType Icon;

	// Token: 0x040026B2 RID: 9906
	public Vector2 Offset;

	// Token: 0x040026B3 RID: 9907
	public bool RevealWhenOnScreen = true;

	// Token: 0x040026B4 RID: 9908
	public bool IsSecret;
}
