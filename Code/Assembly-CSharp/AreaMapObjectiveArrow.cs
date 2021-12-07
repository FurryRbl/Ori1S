using System;
using Game;
using UnityEngine;

// Token: 0x0200087A RID: 2170
public class AreaMapObjectiveArrow : MonoBehaviour
{
	// Token: 0x06003102 RID: 12546 RVA: 0x000D0D54 File Offset: 0x000CEF54
	public void OnEnable()
	{
		this.Arrow.SetActive(false);
	}

	// Token: 0x06003103 RID: 12547 RVA: 0x000D0D64 File Offset: 0x000CEF64
	public void FixedUpdate()
	{
		if (Objectives.All.Count == 0)
		{
			this.Arrow.SetActive(false);
			return;
		}
		if (!Objectives.All[0].AreaMapTransform)
		{
			return;
		}
		Vector3 position = Objectives.All[0].AreaMapTransform.position;
		AreaMapUI instance = AreaMapUI.Instance;
		instance.Navigation.WorldToMapPosition(position);
		if (this.Area.Contains(position))
		{
			this.Arrow.SetActive(false);
			return;
		}
		Vector3 normalized = position.normalized;
		this.Arrow.transform.position = this.ProjectOnRect(this.Area, Vector2.zero, normalized);
		this.Arrow.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(normalized));
		this.Arrow.SetActive(true);
	}

	// Token: 0x06003104 RID: 12548 RVA: 0x000D0E60 File Offset: 0x000CF060
	public Vector2 ProjectOnRect(Rect rect, Vector2 center, Vector2 direction)
	{
		float a = (direction.x >= 0f) ? ((rect.xMax - center.x) / direction.x) : ((rect.xMin - center.x) / direction.x);
		float b = (direction.y >= 0f) ? ((rect.yMax - center.y) / direction.y) : ((rect.yMin - center.y) / direction.y);
		return center + direction * Mathf.Min(a, b);
	}

	// Token: 0x04002C52 RID: 11346
	public GameObject Arrow;

	// Token: 0x04002C53 RID: 11347
	public Rect Area;
}
