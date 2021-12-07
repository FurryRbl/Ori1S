using System;
using Game;
using UnityEngine;

// Token: 0x02000057 RID: 87
[ExecuteInEditMode]
public class CameraSystem : MonoBehaviour
{
	// Token: 0x06000398 RID: 920 RVA: 0x0000E95E File Offset: 0x0000CB5E
	public void Awake()
	{
		UI.Cameras.System = this;
	}

	// Token: 0x040002AD RID: 685
	public CameraCrossFadeManager CrossFadeManager;

	// Token: 0x040002AE RID: 686
	public UberPostProcessingCrossFade UberPostProcessingCrossFade;

	// Token: 0x040002AF RID: 687
	[HideInInspector]
	public GUICamera GUICamera;
}
