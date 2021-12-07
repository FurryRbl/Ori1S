using System;
using UnityEngine;

// Token: 0x02000521 RID: 1313
[Serializable]
public class FishEnemySettings
{
	// Token: 0x04001D62 RID: 7522
	public AnimationCurve IdleSpeedOverDistance;

	// Token: 0x04001D63 RID: 7523
	public AnimationCurve AttackDistanceOverTime;

	// Token: 0x04001D64 RID: 7524
	public AnimationCurve AttackAngleOverTime;

	// Token: 0x04001D65 RID: 7525
	public AnimationCurve SwimSpeedOverTime;

	// Token: 0x04001D66 RID: 7526
	public float EnterSwimRange = 5f;

	// Token: 0x04001D67 RID: 7527
	public float ExitSwimRange = 10f;

	// Token: 0x04001D68 RID: 7528
	public float AttackRange = 2f;

	// Token: 0x04001D69 RID: 7529
	public float MaxSwimDistance = 20f;

	// Token: 0x04001D6A RID: 7530
	public float MinSwimDuration = 2f;

	// Token: 0x04001D6B RID: 7531
	public float IdleSpeed = 10f;

	// Token: 0x04001D6C RID: 7532
	public float SwimSpeed = 10f;

	// Token: 0x04001D6D RID: 7533
	public float AttackSpeed = 10f;

	// Token: 0x04001D6E RID: 7534
	public float Gravity;

	// Token: 0x04001D6F RID: 7535
	public float IdleTurnSpeed = 100f;

	// Token: 0x04001D70 RID: 7536
	public float SwimTurnSpeed = 100f;

	// Token: 0x04001D71 RID: 7537
	public float AttackDuration = 0.43f;

	// Token: 0x04001D72 RID: 7538
	public float BounceDuration = 0.67f;

	// Token: 0x04001D73 RID: 7539
	public float BashDuration = 1.13f;

	// Token: 0x04001D74 RID: 7540
	public float BashSpeed = 40f;
}
