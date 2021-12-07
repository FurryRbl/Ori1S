using System;
using Game;
using UnityEngine;

// Token: 0x020008CC RID: 2252
public class FlyToUserInterfaceSpot : Suspendable
{
	// Token: 0x06003225 RID: 12837 RVA: 0x000D4854 File Offset: 0x000D2A54
	public void Start()
	{
		this.m_transform = base.transform;
		this.m_startPosition = this.m_transform.position;
		this.m_time = 0f;
	}

	// Token: 0x06003226 RID: 12838 RVA: 0x000D4889 File Offset: 0x000D2A89
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_time += Time.deltaTime / this.Duration;
	}

	// Token: 0x06003227 RID: 12839 RVA: 0x000D48B0 File Offset: 0x000D2AB0
	private void LateUpdate()
	{
		float num = this.TimeCurve.Evaluate(this.m_time);
		Ray ray = UI.Cameras.Current.Camera.ViewportPointToRay(this.ScreenPosition);
		Plane plane = new Plane(Vector3.back, Vector3.zero);
		float d;
		plane.Raycast(ray, out d);
		Vector3 a = this.m_startPosition;
		a += this.BiezerOut * num;
		Vector3 vector = ray.origin + ray.direction * d;
		vector += this.BiezerIn * (1f - num);
		this.m_transform.position = Vector3.Lerp(a, vector, num);
	}

	// Token: 0x170007F5 RID: 2037
	// (get) Token: 0x06003228 RID: 12840 RVA: 0x000D4978 File Offset: 0x000D2B78
	// (set) Token: 0x06003229 RID: 12841 RVA: 0x000D4980 File Offset: 0x000D2B80
	public override bool IsSuspended { get; set; }

	// Token: 0x04002D2C RID: 11564
	public Vector2 ScreenPosition;

	// Token: 0x04002D2D RID: 11565
	public float Duration;

	// Token: 0x04002D2E RID: 11566
	public AnimationCurve TimeCurve;

	// Token: 0x04002D2F RID: 11567
	private Vector3 m_startPosition;

	// Token: 0x04002D30 RID: 11568
	private Transform m_transform;

	// Token: 0x04002D31 RID: 11569
	private float m_time;

	// Token: 0x04002D32 RID: 11570
	public Vector2 BiezerIn;

	// Token: 0x04002D33 RID: 11571
	public Vector2 BiezerOut;
}
