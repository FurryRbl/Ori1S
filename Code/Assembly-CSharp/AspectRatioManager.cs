using System;
using Game;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class AspectRatioManager : MonoBehaviour
{
	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x06001B40 RID: 6976 RVA: 0x000755E4 File Offset: 0x000737E4
	public static float AspectRatio
	{
		get
		{
			return AspectRatioManager.m_aspectRatio;
		}
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x000755EC File Offset: 0x000737EC
	public void FixedUpdate()
	{
		float aspect = UI.Cameras.Current.Camera.aspect;
		if (!Mathf.Approximately(aspect, AspectRatioManager.AspectRatio))
		{
			AspectRatioManager.m_aspectRatio = aspect;
			AspectRatioManager.OnAspectChanged.Call();
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0007562C File Offset: 0x0007382C
	public static float ExtraPadding
	{
		get
		{
			Camera camera = UI.Cameras.Current.Camera;
			float fieldOfView = camera.fieldOfView;
			float num = -camera.transform.position.z;
			float num2 = 2f * num * Mathf.Tan(fieldOfView * 0.5f * 0.017453292f);
			float num3 = AspectRatioManager.AspectRatio * num2;
			float num4 = 1.7777778f * num2;
			return num3 - num4;
		}
	}

	// Token: 0x040017B2 RID: 6066
	public const float StandardAspectRatio = 1.7777778f;

	// Token: 0x040017B3 RID: 6067
	public static UberDelegate OnAspectChanged = new UberDelegate();

	// Token: 0x040017B4 RID: 6068
	private static float m_aspectRatio = 1.7777778f;
}
