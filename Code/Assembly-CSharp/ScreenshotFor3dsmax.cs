using System;
using UnityEngine;

// Token: 0x020001B1 RID: 433
public class ScreenshotFor3dsmax : MonoBehaviour
{
	// Token: 0x06001048 RID: 4168 RVA: 0x0004A55C File Offset: 0x0004875C
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawRectangle(base.transform.position, base.transform.localScale, new Color(0f, 0f, 0f, 0f), Color.white);
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06001049 RID: 4169 RVA: 0x0004A5A4 File Offset: 0x000487A4
	public Rect Bounds
	{
		get
		{
			Vector2 a = base.transform.localScale;
			Vector2 vector = base.transform.position - a * 0.5f;
			return new Rect(vector.x, vector.y, a.x, a.y);
		}
	}

	// Token: 0x04000D86 RID: 3462
	public string TexturePath = "3dsmaxScreenshot.png";

	// Token: 0x04000D87 RID: 3463
	public float PixelsPerUnit = 32f;

	// Token: 0x04000D88 RID: 3464
	public bool ClearAlpha = true;

	// Token: 0x04000D89 RID: 3465
	public bool ForcePowerOfTwo = true;
}
