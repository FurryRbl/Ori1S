using System;
using Game;
using UnityEngine;

// Token: 0x02000305 RID: 773
[Category("Ori")]
public class OriFlyAlongCurveAction : ActionWithDuration
{
	// Token: 0x0600170A RID: 5898 RVA: 0x00063FE8 File Offset: 0x000621E8
	public override void Perform(IContext context)
	{
		Characters.Ori.MoveOriAlongCurve(this.PositionX, this.PositionY, this.PositionZ, this.Target.position, this.DurationOfMovement);
		this.m_remainingTime = this.Duration;
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x0006402E File Offset: 0x0006222E
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x0600170C RID: 5900 RVA: 0x00064035 File Offset: 0x00062235
	public override bool IsPerforming
	{
		get
		{
			return this.m_remainingTime > 0f;
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x0600170D RID: 5901 RVA: 0x00064044 File Offset: 0x00062244
	// (set) Token: 0x0600170E RID: 5902 RVA: 0x0006404C File Offset: 0x0006224C
	public override float Duration
	{
		get
		{
			return this.DurationOfMovement;
		}
		set
		{
			this.DurationOfMovement = value;
		}
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x00064055 File Offset: 0x00062255
	public void FixedUpdate()
	{
		this.m_remainingTime -= ((!this.IsSuspended) ? Time.deltaTime : 0f);
	}

	// Token: 0x040013CE RID: 5070
	public AnimationCurve PositionX;

	// Token: 0x040013CF RID: 5071
	public AnimationCurve PositionY;

	// Token: 0x040013D0 RID: 5072
	public AnimationCurve PositionZ;

	// Token: 0x040013D1 RID: 5073
	public Transform Target;

	// Token: 0x040013D2 RID: 5074
	private float m_remainingTime;

	// Token: 0x040013D3 RID: 5075
	public float DurationOfMovement;
}
