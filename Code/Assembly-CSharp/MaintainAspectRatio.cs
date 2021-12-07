using System;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class MaintainAspectRatio : MonoBehaviour
{
	// Token: 0x06001B90 RID: 7056 RVA: 0x000769CA File Offset: 0x00074BCA
	public void Awake()
	{
		this.m_camera = base.GetComponent<Camera>();
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x000769D8 File Offset: 0x00074BD8
	public void LateUpdate()
	{
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = Mathf.Clamp(num, 1.7777778f, 2.389f);
		float num3 = num / num2;
		if (num3 < 1f)
		{
			this.m_camera.rect = new Rect(0f, 0.5f - num3 * 0.5f, 1f, num3);
		}
		else
		{
			num3 = 1f / num3;
			this.m_camera.rect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
		}
	}

	// Token: 0x040017F7 RID: 6135
	private Camera m_camera;
}
