using System;
using Game;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class CreditsTextPosition : BaseAnimator
{
	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x06001651 RID: 5713 RVA: 0x00062738 File Offset: 0x00060938
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.IsLooping();
		}
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x00062745 File Offset: 0x00060945
	public override void CacheOriginals()
	{
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x00062748 File Offset: 0x00060948
	public override void SampleValue(float value, bool forceSample)
	{
		this.m_weight = this.AnimationCurve.Evaluate(base.TimeToAnimationCurveTime(value));
		Camera camera = UI.Cameras.Current.Camera;
		Ray ray = camera.ScreenPointToRay(new Vector3((float)camera.pixelWidth * this.ScreenPosition.x, (float)camera.pixelHeight * this.ScreenPosition.y, 0f));
		Plane plane = new Plane(Vector3.back, Vector3.zero);
		float distance;
		plane.Raycast(ray, out distance);
		Vector3 point = ray.GetPoint(distance);
		Vector3 position = base.transform.parent.position;
		position.x = Mathf.Lerp(position.x, point.x, this.m_weight);
		position.y = Mathf.Lerp(position.y, point.y, this.m_weight);
		base.transform.position = position;
		base.transform.rotation = Quaternion.Lerp(base.transform.parent.rotation, UI.Cameras.Current.Camera.transform.rotation, this.m_weight);
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x06001654 RID: 5716 RVA: 0x0006286E File Offset: 0x00060A6E
	public override float Duration
	{
		get
		{
			return this.AnimationCurve.CurveDuration();
		}
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x0006287B File Offset: 0x00060A7B
	public override void RestoreToOriginalState()
	{
		this.m_weight = 0f;
		base.transform.localPosition = Vector3.zero;
	}

	// Token: 0x04001351 RID: 4945
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04001352 RID: 4946
	public Vector2 ScreenPosition;

	// Token: 0x04001353 RID: 4947
	private float m_weight;
}
