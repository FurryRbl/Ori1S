using System;

// Token: 0x0200052A RID: 1322
public class FishFallState : FishState
{
	// Token: 0x06002313 RID: 8979 RVA: 0x00099CC5 File Offset: 0x00097EC5
	public FishFallState(FishEnemy fish) : base(fish)
	{
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x00099CD0 File Offset: 0x00097ED0
	public override void UpdateState()
	{
		this.Fish.ApplyGravity();
		this.Fish.Angle = MoonMath.Angle.AngleFromVector(this.Fish.FlyMovement.Velocity);
		this.Fish.UpdateSpriteRotation();
		this.Fish.FlyMovement.VelocityX *= 0.95f;
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x00099D30 File Offset: 0x00097F30
	public override void OnEnter()
	{
		this.Fish.Animation.PlayLoop(this.Fish.Animations.Fall, 0, null, false);
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x00099D61 File Offset: 0x00097F61
	public override void OnExit()
	{
	}
}
