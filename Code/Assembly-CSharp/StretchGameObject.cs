using System;
using UnityEngine;

// Token: 0x02000943 RID: 2371
public class StretchGameObject : MonoBehaviour, IPooled
{
	// Token: 0x06003454 RID: 13396 RVA: 0x000DBE10 File Offset: 0x000DA010
	public void OnPoolSpawned()
	{
		this.m_lastPosition = Vector3.zero;
		this.m_stretch = 1f;
	}

	// Token: 0x06003455 RID: 13397 RVA: 0x000DBE28 File Offset: 0x000DA028
	public void Start()
	{
		this.m_lastPosition = base.transform.position;
		this.m_child = base.transform.GetChild(0);
	}

	// Token: 0x06003456 RID: 13398 RVA: 0x000DBE58 File Offset: 0x000DA058
	public void FixedUpdate()
	{
		Vector3 position = base.transform.position;
		Vector2 vector = position - this.m_lastPosition;
		if (vector.magnitude < 1E-45f)
		{
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
		}
		else
		{
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			base.transform.eulerAngles = new Vector3(0f, 0f, 0.5f * Mathf.LerpAngle(base.transform.eulerAngles.z * 2f, num * 2f, 0.5f));
			float b = this.StretchOverSpeedCurve.Evaluate(vector.magnitude / Time.deltaTime);
			this.m_stretch = Mathf.Lerp(this.m_stretch, b, 0.2f);
			base.transform.localScale = new Vector3(this.m_stretch, 1f / this.m_stretch, 1f);
		}
		this.m_lastPosition = position;
		if (this.m_child)
		{
			this.m_child.rotation = Quaternion.identity;
		}
	}

	// Token: 0x04002F3C RID: 12092
	public AnimationCurve StretchOverSpeedCurve;

	// Token: 0x04002F3D RID: 12093
	private Vector3 m_lastPosition;

	// Token: 0x04002F3E RID: 12094
	private float m_stretch = 1f;

	// Token: 0x04002F3F RID: 12095
	private Transform m_child;
}
