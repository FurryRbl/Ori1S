using System;
using Game;
using UnityEngine;

// Token: 0x02000058 RID: 88
[ExecuteInEditMode]
public class GUICamera : MonoBehaviour
{
	// Token: 0x0600039A RID: 922 RVA: 0x0000E96E File Offset: 0x0000CB6E
	public void Awake()
	{
		UI.Cameras.System.GUICamera = this;
		this.Camera = base.GetComponent<Camera>();
	}

	// Token: 0x0600039B RID: 923 RVA: 0x0000E988 File Offset: 0x0000CB88
	public Vector3 ScreenToWorldPoint(Vector3 position)
	{
		Vector3 position2 = UI.Cameras.Current.Camera.ScreenToViewportPoint(position);
		return this.Camera.ViewportToWorldPoint(position2);
	}

	// Token: 0x040002B0 RID: 688
	[HideInInspector]
	public Camera Camera;
}
