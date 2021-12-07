using System;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class SeinStandingOnEdge : CharacterState, ISeinReceiver
{
	// Token: 0x170004D6 RID: 1238
	// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0007E665 File Offset: 0x0007C865
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170004D7 RID: 1239
	// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0007E677 File Offset: 0x0007C877
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170004D8 RID: 1240
	// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0007E689 File Offset: 0x0007C889
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x170004D9 RID: 1241
	// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0007E6A0 File Offset: 0x0007C8A0
	public bool ShouldStandingOnEdgeFacingBackwardsAnimationPlay
	{
		get
		{
			return this.ShouldStandingOnEdgeFacingBackwardsAnimationKeepPlaying();
		}
	}

	// Token: 0x170004DA RID: 1242
	// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0007E6A8 File Offset: 0x0007C8A8
	public bool ShouldStandingOnEdgeFacingForwardsAnimationPlay
	{
		get
		{
			return this.ShouldStandingOnEdgeFacingForwardsAnimationKeepPlaying();
		}
	}

	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0007E6B0 File Offset: 0x0007C8B0
	public bool StandingOnEdge
	{
		get
		{
			return this.StandingOnEdgeBackwards || this.StandingOnEdgeForwards;
		}
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x0007E6C6 File Offset: 0x0007C8C6
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.StandingOnEdge = this;
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x0007E6E0 File Offset: 0x0007C8E0
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x0007E6EC File Offset: 0x0007C8EC
	public override void UpdateCharacterState()
	{
		if (base.Active)
		{
			this.StandingOnEdgeBackwards = (this.PlatformMovement.IsOnGround && !this.PlatformMovement.MovingHorizontally && !this.StandingOnEdgeRayHit((!this.SpriteMirror.FaceLeft) ? (-this.Distance) : this.Distance));
			this.StandingOnEdgeForwards = (this.PlatformMovement.IsOnGround && !this.PlatformMovement.MovingHorizontally && !this.StandingOnEdgeRayHit((!this.SpriteMirror.FaceLeft) ? this.Distance : (-this.Distance)));
			if (this.ShouldStandingOnEdgeFacingBackwardsAnimationPlay)
			{
				this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.FacingEdgeBackwardsAnimation, 9, new Func<bool>(this.ShouldStandingOnEdgeFacingBackwardsAnimationKeepPlaying), false);
			}
			else if (this.ShouldStandingOnEdgeFacingForwardsAnimationPlay)
			{
				this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.FacingEdgeAnimation, 9, new Func<bool>(this.ShouldStandingOnEdgeFacingForwardsAnimationKeepPlaying), false);
			}
		}
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x0007E827 File Offset: 0x0007CA27
	public bool ShouldStandingOnEdgeFacingBackwardsAnimationKeepPlaying()
	{
		return this.StandingOnEdgeBackwards;
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x0007E82F File Offset: 0x0007CA2F
	public bool ShouldStandingOnEdgeFacingForwardsAnimationKeepPlaying()
	{
		return this.StandingOnEdgeForwards;
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x0007E838 File Offset: 0x0007CA38
	public bool StandingOnEdgeRayHit(float offset)
	{
		RaycastHit raycastHit;
		return this.Sein.Controller.RayTest(this.PlatformMovement.FeetPosition + this.PlatformMovement.GravityBinormal * offset, this.PlatformMovement.GravityDirection * (this.PlatformMovement.CapsuleCollider.radius + 1f), out raycastHit);
	}

	// Token: 0x0400191B RID: 6427
	public float Distance = 0.3f;

	// Token: 0x0400191C RID: 6428
	public TextureAnimationWithTransitions FacingEdgeAnimation;

	// Token: 0x0400191D RID: 6429
	public TextureAnimationWithTransitions FacingEdgeBackwardsAnimation;

	// Token: 0x0400191E RID: 6430
	public SeinCharacter Sein;

	// Token: 0x0400191F RID: 6431
	public bool StandingOnEdgeBackwards;

	// Token: 0x04001920 RID: 6432
	public bool StandingOnEdgeForwards;
}
