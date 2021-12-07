using System;
using UnityEngine;

// Token: 0x02000524 RID: 1316
public class FishBashedState : FishState
{
	// Token: 0x060022FD RID: 8957 RVA: 0x000995FD File Offset: 0x000977FD
	public FishBashedState(FishEnemy fish) : base(fish)
	{
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x00099608 File Offset: 0x00097808
	public override void UpdateState()
	{
		if (this.Fish.OutOfWater())
		{
			this.Fish.ApplyGravity();
			this.Fish.Angle = Mathf.MoveTowardsAngle(this.Fish.Angle, 270f, 300f * Time.deltaTime);
		}
		else
		{
			this.Fish.FlyMovement.VelocityY *= 0.95f;
		}
		this.Fish.FlyMovement.VelocityX *= 0.95f;
		this.Fish.UpdateSpriteRotation();
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x000996A4 File Offset: 0x000978A4
	public override void OnEnter()
	{
		this.Fish.Angle = MoonMath.Angle.AngleFromVector(-this.Fish.FlyMovement.Velocity);
		this.Fish.Animation.Play(this.Fish.Animations.Bashed, 0, null);
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x000996F9 File Offset: 0x000978F9
	public override void OnExit()
	{
	}
}
