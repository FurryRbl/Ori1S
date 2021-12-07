using System;

// Token: 0x02000529 RID: 1321
public class FishFlopState : FishState
{
	// Token: 0x0600230F RID: 8975 RVA: 0x00099C01 File Offset: 0x00097E01
	public FishFlopState(FishEnemy fish) : base(fish)
	{
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x00099C0C File Offset: 0x00097E0C
	public override void UpdateState()
	{
		this.Fish.ApplyGravity();
		this.Fish.Angle = 0f;
		this.Fish.UpdateSpriteRotation();
		this.Fish.FlyMovement.Velocity.x = 0f;
		this.Fish.FlyMovement.Velocity.y = -5f;
	}

	// Token: 0x06002311 RID: 8977 RVA: 0x00099C73 File Offset: 0x00097E73
	public override void OnEnter()
	{
		this.Fish.Animation.PlayLoop(this.Fish.Animations.Flop, 0, null, false);
		this.Fish.Sounds.Flop.Play();
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x00099CAE File Offset: 0x00097EAE
	public override void OnExit()
	{
		this.Fish.Sounds.Flop.Stop();
	}
}
