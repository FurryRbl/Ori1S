using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class PlatformBehaviour : MonoBehaviour
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600006D RID: 109 RVA: 0x00003654 File Offset: 0x00001854
	public SurfaceMaterialType WallSurfaceMaterialType
	{
		get
		{
			if (this.PlatformMovement.WallLeft.IsOn)
			{
				PlatformMovementListOfColliders platformMovementListOfColliders = this.PlatformMovementListOfColliders;
				return this.m_wallSurfaceMaterialType = SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(platformMovementListOfColliders.WallLeftCollider);
			}
			if (this.PlatformMovement.WallRight.IsOn)
			{
				PlatformMovementListOfColliders platformMovementListOfColliders2 = this.PlatformMovementListOfColliders;
				return this.m_wallSurfaceMaterialType = SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(platformMovementListOfColliders2.WallRightCollider);
			}
			return this.m_wallSurfaceMaterialType;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600006E RID: 110 RVA: 0x000036CC File Offset: 0x000018CC
	public SurfaceMaterialType GroundSurfaceMaterialType
	{
		get
		{
			if (this.PlatformMovement.Ground.IsOn)
			{
				PlatformMovementListOfColliders platformMovementListOfColliders = this.PlatformMovementListOfColliders;
				return this.m_groundSurfaceMaterialType = SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(platformMovementListOfColliders.GroundCollider);
			}
			return this.m_groundSurfaceMaterialType;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600006F RID: 111 RVA: 0x00003710 File Offset: 0x00001910
	public SurfaceMaterialType CeilingSurfaceMaterialType
	{
		get
		{
			if (this.PlatformMovement.Ceiling.IsOn)
			{
				PlatformMovementListOfColliders platformMovementListOfColliders = this.PlatformMovementListOfColliders;
				return this.m_ceilingSurfaceMaterialType = SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(platformMovementListOfColliders.CeilingCollider);
			}
			return this.m_ceilingSurfaceMaterialType;
		}
	}

	// Token: 0x0400007D RID: 125
	public PlatformMovement PlatformMovement;

	// Token: 0x0400007E RID: 126
	public CharacterLeftRightMovement LeftRightMovement;

	// Token: 0x0400007F RID: 127
	public CharacterGravity Gravity;

	// Token: 0x04000080 RID: 128
	public CharacterGravityToGround GravityToGround;

	// Token: 0x04000081 RID: 129
	public PlatformMovementListOfColliders PlatformMovementListOfColliders;

	// Token: 0x04000082 RID: 130
	public PlatformMovementForce Force;

	// Token: 0x04000083 RID: 131
	public CharacterCapsuleController CapsuleController;

	// Token: 0x04000084 RID: 132
	public CharacterInstantStop InstantStop;

	// Token: 0x04000085 RID: 133
	public CharacterAirNoDeceleration AirNoDeceleration;

	// Token: 0x04000086 RID: 134
	public CharacterUpwardsDeceleration UpwardsDeceleration;

	// Token: 0x04000087 RID: 135
	public CharacterApplyFrictionToSpeed ApplyFrictionToSpeed;

	// Token: 0x04000088 RID: 136
	public CharacterJumpSustain JumpSustain;

	// Token: 0x04000089 RID: 137
	public CharacterVisuals Visuals;

	// Token: 0x0400008A RID: 138
	private SurfaceMaterialType m_wallSurfaceMaterialType = SurfaceMaterialType.Rock;

	// Token: 0x0400008B RID: 139
	private SurfaceMaterialType m_groundSurfaceMaterialType = SurfaceMaterialType.Rock;

	// Token: 0x0400008C RID: 140
	private SurfaceMaterialType m_ceilingSurfaceMaterialType = SurfaceMaterialType.Rock;
}
