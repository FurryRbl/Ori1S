using System;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class CharacterApplyFrictionToSpeed : CharacterState
{
	// Token: 0x170004FA RID: 1274
	// (get) Token: 0x06001D82 RID: 7554 RVA: 0x00081A45 File Offset: 0x0007FC45
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x00081A52 File Offset: 0x0007FC52
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x00081A5C File Offset: 0x0007FC5C
	public override void UpdateCharacterState()
	{
		if (this.PlatformBehaviour.PlatformMovement.IsSuspended)
		{
			return;
		}
		if (base.Active && this.SpeedFactor != 0f)
		{
			Vector3 v = this.PlatformMovement.LocalSpeed;
			float x = v.normalized.x;
			float y = v.normalized.y;
			if (MoonMath.Float.Normalize(v.x) == MoonMath.Float.Normalize(this.SpeedToSlowDown.x))
			{
				v.x = MoonMath.Float.ClampedSubtract(v.x, this.SpeedFactor * Time.deltaTime * x, 0f, 0f);
			}
			if (MoonMath.Float.Normalize(v.y) == MoonMath.Float.Normalize(this.SpeedToSlowDown.y))
			{
				v.y = MoonMath.Float.ClampedSubtract(v.y, this.SpeedFactor * Time.deltaTime * y, 0f, 0f);
			}
			this.PlatformMovement.LocalSpeed = v;
		}
	}

	// Token: 0x06001D85 RID: 7557 RVA: 0x00081B75 File Offset: 0x0007FD75
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.SpeedFactor);
		ar.Serialize(ref this.SpeedToSlowDown);
		base.Serialize(ar);
	}

	// Token: 0x0400197D RID: 6525
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x0400197E RID: 6526
	public float SpeedFactor;

	// Token: 0x0400197F RID: 6527
	public Vector3 SpeedToSlowDown;
}
