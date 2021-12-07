using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020000FC RID: 252
[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
	// Token: 0x060009EC RID: 2540 RVA: 0x0002B61F File Offset: 0x0002981F
	public void OnEnable()
	{
		UI.Cameras.Manager = this;
		this.Cameras.Clear();
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0002B632 File Offset: 0x00029832
	public void RegisterCamera(CameraController cameraController)
	{
		this.Cameras.Add(cameraController);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0002B640 File Offset: 0x00029840
	public void UnregisterCamera(CameraController cameraController)
	{
		this.Cameras.Remove(cameraController);
	}

	// Token: 0x04000836 RID: 2102
	public List<CameraController> Cameras = new List<CameraController>();
}
