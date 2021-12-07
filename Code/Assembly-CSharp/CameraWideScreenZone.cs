using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000201 RID: 513
[ExecuteInEditMode]
public class CameraWideScreenZone : MonoBehaviour
{
	// Token: 0x060011CA RID: 4554 RVA: 0x00051F09 File Offset: 0x00050109
	public void OnEnable()
	{
		if (!this.m_added)
		{
			CameraWideScreenZone.All.Add(this);
			this.m_added = true;
		}
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x00051F28 File Offset: 0x00050128
	public void OnDestroy()
	{
		CameraWideScreenZone.All.Remove(this);
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x00051F38 File Offset: 0x00050138
	public float NormalizedMarginPenetration(Vector3 worldPosition)
	{
		Vector3 position = base.transform.position;
		Vector3 vector = worldPosition - position;
		Vector3 vector2 = base.transform.localScale * 0.5f;
		float a = 1f;
		float b = 1f;
		if (vector.x < -vector2.x)
		{
			a = Mathf.Clamp01(Mathf.InverseLerp(-vector2.x - this.LeftMargin, -vector2.x, vector.x));
		}
		if (vector.x > vector2.x)
		{
			a = Mathf.Clamp01(Mathf.InverseLerp(vector2.x + this.RightMargin, vector2.x, vector.x));
		}
		if (vector.y < -vector2.y)
		{
			b = Mathf.Clamp01(Mathf.InverseLerp(-vector2.y - this.BottomMargin, -vector2.y, vector.y));
		}
		if (vector.y > vector2.y)
		{
			b = Mathf.Clamp01(Mathf.InverseLerp(vector2.y + this.TopMargin, vector2.y, vector.y));
		}
		return Mathf.Min(a, b);
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x060011CD RID: 4557 RVA: 0x00052074 File Offset: 0x00050274
	public float Strength
	{
		get
		{
			return this.m_strength;
		}
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x0005207C File Offset: 0x0005027C
	public void UpdateOffset(float dt, Vector3 worldPosition)
	{
		float target = (!base.gameObject.activeInHierarchy) ? 0f : this.NormalizedMarginPenetration(worldPosition);
		this.m_strength = Mathf.MoveTowards(this.m_strength, target, dt / this.Duration);
	}

	// Token: 0x04000F4E RID: 3918
	public static List<CameraWideScreenZone> All = new List<CameraWideScreenZone>();

	// Token: 0x04000F4F RID: 3919
	private bool m_added;

	// Token: 0x04000F50 RID: 3920
	public WideScreenAdjustmentSettings WideScreenAdjustment;

	// Token: 0x04000F51 RID: 3921
	public float LeftMargin;

	// Token: 0x04000F52 RID: 3922
	public float RightMargin;

	// Token: 0x04000F53 RID: 3923
	public float TopMargin;

	// Token: 0x04000F54 RID: 3924
	public float BottomMargin;

	// Token: 0x04000F55 RID: 3925
	private float m_strength;

	// Token: 0x04000F56 RID: 3926
	public float Duration = 2f;
}
