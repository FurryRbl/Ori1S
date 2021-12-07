using System;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class CameraStraightLineMotion : MonoBehaviour
{
	// Token: 0x14000030 RID: 48
	// (add) Token: 0x06001B79 RID: 7033 RVA: 0x0007654A File Offset: 0x0007474A
	// (remove) Token: 0x06001B7A RID: 7034 RVA: 0x00076563 File Offset: 0x00074763
	public event Action OnMotionFinishedEvent;

	// Token: 0x06001B7B RID: 7035 RVA: 0x0007657C File Offset: 0x0007477C
	private void Awake()
	{
		this.m_transform = base.transform;
		this.m_rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x00076598 File Offset: 0x00074798
	public void UpdateMotion()
	{
		this.m_time += ((this.m_duration != 0f) ? (Time.deltaTime / this.m_duration) : 1f);
		if (this.m_time > 1f)
		{
			this.m_time = 1f;
		}
		Vector3 a = Vector3.Lerp(this.m_startPosition, this.m_endPosition, this.SmoothingCurve.Evaluate(this.m_time));
		this.m_rigidbody.velocity = (a - this.m_transform.position) / Time.deltaTime;
		if (this.m_time == 1f && this.OnMotionFinishedEvent != null)
		{
			this.OnMotionFinishedEvent();
		}
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x00076664 File Offset: 0x00074864
	public void MoveToTarget(Vector3 target, float duration)
	{
		this.m_startPosition = this.m_transform.position;
		this.m_endPosition = target;
		this.m_duration = duration;
		this.m_time = 0f;
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x06001B7F RID: 7039 RVA: 0x000766A4 File Offset: 0x000748A4
	// (set) Token: 0x06001B7E RID: 7038 RVA: 0x0007669B File Offset: 0x0007489B
	public Vector3 EndPosition
	{
		get
		{
			return this.m_endPosition;
		}
		set
		{
			this.m_endPosition = value;
		}
	}

	// Token: 0x040017E6 RID: 6118
	private Transform m_transform;

	// Token: 0x040017E7 RID: 6119
	private Rigidbody m_rigidbody;

	// Token: 0x040017E8 RID: 6120
	private Vector3 m_startPosition;

	// Token: 0x040017E9 RID: 6121
	private Vector3 m_endPosition;

	// Token: 0x040017EA RID: 6122
	private float m_time;

	// Token: 0x040017EB RID: 6123
	private float m_duration;

	// Token: 0x040017EC RID: 6124
	public AnimationCurve SmoothingCurve;
}
