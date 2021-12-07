using System;
using UnityEngine;

// Token: 0x02000323 RID: 803
[Category("Obsolete")]
public class RotatingAction : ActionWithDuration
{
	// Token: 0x0600177A RID: 6010 RVA: 0x000651F0 File Offset: 0x000633F0
	public override void Perform(IContext context)
	{
		this.m_isPerforming = true;
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x000651F9 File Offset: 0x000633F9
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x0600177C RID: 6012 RVA: 0x00065200 File Offset: 0x00063400
	public override bool IsPerforming
	{
		get
		{
			return this.m_isPerforming;
		}
	}

	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x0600177D RID: 6013 RVA: 0x00065208 File Offset: 0x00063408
	// (set) Token: 0x0600177E RID: 6014 RVA: 0x00065210 File Offset: 0x00063410
	public override float Duration
	{
		get
		{
			return this.DurationOfRotation;
		}
		set
		{
			this.DurationOfRotation = value;
		}
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x00065219 File Offset: 0x00063419
	private void OniTweenComplete()
	{
		this.m_isPerforming = false;
	}

	// Token: 0x04001420 RID: 5152
	public Transform[] transformsToRotate;

	// Token: 0x04001421 RID: 5153
	public float xAxisRotationAngle;

	// Token: 0x04001422 RID: 5154
	public float yAxisRotationAngle;

	// Token: 0x04001423 RID: 5155
	public float zAxisRotationAngle = 180f;

	// Token: 0x04001424 RID: 5156
	public float DurationOfRotation = 3f;

	// Token: 0x04001425 RID: 5157
	private bool m_isPerforming;
}
