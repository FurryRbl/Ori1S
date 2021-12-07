using System;
using UnityEngine;

// Token: 0x020001FC RID: 508
[ExecuteInEditMode]
public class CameraOffsetZone : MonoBehaviour
{
	// Token: 0x060011A7 RID: 4519 RVA: 0x000516F8 File Offset: 0x0004F8F8
	[ContextMenu("Fit room bound")]
	public void FitRoomBound()
	{
		SceneRoot sceneRoot = (SceneRoot)UnityEngine.Object.FindObjectOfType(typeof(SceneRoot));
		if (sceneRoot)
		{
			Rect sceneBounds = sceneRoot.MetaData.SceneBounds;
			base.transform.position = sceneBounds.center;
			base.transform.localScale = new Vector3(sceneBounds.width, sceneBounds.height, 1f);
		}
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x0005176B File Offset: 0x0004F96B
	public void OnEnable()
	{
		CameraOffsetController.Register(this);
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x00051773 File Offset: 0x0004F973
	public void OnDisable()
	{
		CameraOffsetController.Unregister(this);
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x0005177C File Offset: 0x0004F97C
	public Bounds GetOuterBounds()
	{
		return new Bounds(base.transform.position + (new Vector3(this.RightMargin, this.TopMargin) - new Vector3(this.LeftMargin, this.BottomMargin)) / 2f, base.transform.lossyScale + new Vector3(this.LeftMargin + this.RightMargin, this.TopMargin + this.BottomMargin));
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x00051800 File Offset: 0x0004FA00
	public Bounds GetInnerBounds()
	{
		return new Bounds(base.transform.position, base.transform.localScale);
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x00051828 File Offset: 0x0004FA28
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

	// Token: 0x04000F35 RID: 3893
	public Vector3 Offset;

	// Token: 0x04000F36 RID: 3894
	public int priority;

	// Token: 0x04000F37 RID: 3895
	public AnimationCurve ZoomCurve;

	// Token: 0x04000F38 RID: 3896
	public float LeftMargin;

	// Token: 0x04000F39 RID: 3897
	public float RightMargin;

	// Token: 0x04000F3A RID: 3898
	public float TopMargin;

	// Token: 0x04000F3B RID: 3899
	public float BottomMargin;
}
