using System;
using UnityEngine;

// Token: 0x02000650 RID: 1616
public class SmoothTransformFollower : MonoBehaviour
{
	// Token: 0x17000646 RID: 1606
	// (get) Token: 0x0600278A RID: 10122 RVA: 0x000AC0F7 File Offset: 0x000AA2F7
	private Vector3 CurrentFollowedPosition
	{
		get
		{
			return this.TargetToFollow.position + this.TargetOffset;
		}
	}

	// Token: 0x0600278B RID: 10123 RVA: 0x000AC10F File Offset: 0x000AA30F
	private void Start()
	{
		this.m_currentPosition = this.CurrentFollowedPosition;
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x000AC120 File Offset: 0x000AA320
	private void FixedUpdate()
	{
		Vector3 currentFollowedPosition = this.CurrentFollowedPosition;
		float num = Vector3.Distance(currentFollowedPosition, this.m_currentPosition);
		if (num > this.ResetDistance)
		{
			this.m_currentPosition = this.CurrentFollowedPosition;
		}
		this.m_currentPosition = Vector3.Lerp(this.m_currentPosition, currentFollowedPosition, Mathf.Pow(0.5f, Time.fixedDeltaTime * this.SpeedSmoothingFactor));
		base.transform.position = this.m_currentPosition;
		Vector3 normalized = (this.TargetToFollow.position - this.m_currentPosition).normalized;
		base.transform.eulerAngles = new Vector3(0f, 0f, this.RotationFactor * (MoonMath.Angle.AngleFromVector(normalized) + this.RotationOffset));
	}

	// Token: 0x0400221B RID: 8731
	public Transform TargetToFollow;

	// Token: 0x0400221C RID: 8732
	public Vector3 TargetOffset;

	// Token: 0x0400221D RID: 8733
	public float RotationFactor = 1f;

	// Token: 0x0400221E RID: 8734
	public float RotationOffset;

	// Token: 0x0400221F RID: 8735
	public float SpeedSmoothingFactor = 1f;

	// Token: 0x04002220 RID: 8736
	public float ResetDistance = 10f;

	// Token: 0x04002221 RID: 8737
	private Vector3 m_currentPosition;
}
