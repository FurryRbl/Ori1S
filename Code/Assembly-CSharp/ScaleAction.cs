using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
[Category("Obsolete")]
public class ScaleAction : ActionWithDuration
{
	// Token: 0x0600178C RID: 6028 RVA: 0x000653B1 File Offset: 0x000635B1
	public new void Start()
	{
		base.Start();
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000653BC File Offset: 0x000635BC
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			foreach (Transform exists in this.transformsToRotate)
			{
				if (exists)
				{
				}
			}
		}
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x00065400 File Offset: 0x00063600
	public override void Perform(IContext context)
	{
		if (this.x)
		{
			foreach (Transform exists in this.transformsToRotate)
			{
				if (exists)
				{
				}
			}
		}
		else if (this.y)
		{
			foreach (Transform exists2 in this.transformsToRotate)
			{
				if (exists2)
				{
				}
			}
		}
		else if (this.z)
		{
			foreach (Transform exists3 in this.transformsToRotate)
			{
				if (exists3)
				{
				}
			}
		}
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000654C8 File Offset: 0x000636C8
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x06001790 RID: 6032 RVA: 0x000654CF File Offset: 0x000636CF
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x06001791 RID: 6033 RVA: 0x000654D6 File Offset: 0x000636D6
	// (set) Token: 0x06001792 RID: 6034 RVA: 0x000654DE File Offset: 0x000636DE
	public override float Duration
	{
		get
		{
			return this.DurationOfScaling;
		}
		set
		{
			this.DurationOfScaling = value;
		}
	}

	// Token: 0x0400142C RID: 5164
	public Transform[] transformsToRotate = new Transform[0];

	// Token: 0x0400142D RID: 5165
	public float MoveValue = 1f;

	// Token: 0x0400142E RID: 5166
	public float DurationOfScaling = 3f;

	// Token: 0x0400142F RID: 5167
	public bool x;

	// Token: 0x04001430 RID: 5168
	public bool y;

	// Token: 0x04001431 RID: 5169
	public bool z;
}
