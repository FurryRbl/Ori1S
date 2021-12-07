using System;
using Game;
using UnityEngine;

// Token: 0x02000342 RID: 834
public class SpiritTreeTextLocationController : MonoBehaviour
{
	// Token: 0x060017E4 RID: 6116 RVA: 0x0006684A File Offset: 0x00064A4A
	public void Start()
	{
		this.m_worldPosition = base.transform.position;
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x00066860 File Offset: 0x00064A60
	public void Update()
	{
		Camera camera = UI.Cameras.Current.Camera;
		Ray ray = camera.ScreenPointToRay(new Vector3((float)Screen.width * this.ScreenPosition.x, (float)Screen.height * this.ScreenPosition.y, 0f));
		Plane plane = new Plane(Vector3.back, Vector3.zero);
		float distance;
		plane.Raycast(ray, out distance);
		Vector3 point = ray.GetPoint(distance);
		Vector3 worldPosition = this.m_worldPosition;
		worldPosition.x = Mathf.Lerp(this.m_worldPosition.x, point.x, this.ScreenWeight);
		worldPosition.y = Mathf.Lerp(this.m_worldPosition.y, point.y, this.ScreenWeight);
		base.transform.position = worldPosition;
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x00066930 File Offset: 0x00064B30
	public void StartScrolling()
	{
		TransformAnimator componentInChildren = base.transform.GetComponentInChildren<TransformAnimator>();
		componentInChildren.Initialize();
		componentInChildren.AnimatorDriver.Restart();
	}

	// Token: 0x0400149E RID: 5278
	private Vector3 m_worldPosition;

	// Token: 0x0400149F RID: 5279
	public AnimationCurve ScreenPositionInfluenceCurve;

	// Token: 0x040014A0 RID: 5280
	public Vector2 ScreenPosition;

	// Token: 0x040014A1 RID: 5281
	public float ScreenWeight = 0.5f;
}
