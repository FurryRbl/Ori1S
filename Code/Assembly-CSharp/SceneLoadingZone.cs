using System;
using UnityEngine;

// Token: 0x02000719 RID: 1817
[ExecuteInEditMode]
public class SceneLoadingZone : TemporyBoundaryGizmo<SceneLoadingZone>, IStrippable
{
	// Token: 0x06002B0E RID: 11022 RVA: 0x000B8354 File Offset: 0x000B6554
	public bool DoStrip()
	{
		return true;
	}
}
