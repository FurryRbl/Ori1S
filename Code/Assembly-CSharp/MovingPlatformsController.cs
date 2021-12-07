using System;
using UnityEngine;

// Token: 0x020006E1 RID: 1761
public class MovingPlatformsController
{
	// Token: 0x06002A11 RID: 10769 RVA: 0x000B4E5F File Offset: 0x000B305F
	public MovingPlatformsController(PlatformMovement platformMovement)
	{
		this.m_platformMovement = platformMovement;
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x000B4E70 File Offset: 0x000B3070
	public void DetachFromAll()
	{
		this.m_groundPlatform = (this.m_oldGroundPlatform = null);
		this.m_wallLeftPlatform = (this.m_oldWallLeftPlatform = null);
		this.m_wallRightPlatform = (this.m_oldWallRightPlatform = null);
		this.m_ceilingPlatform = (this.m_oldCeilingPlatform = null);
		this.m_ignoreForAFrameBecauseOfUnitysDelayedCollisionCallbacks = true;
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x000B4EC4 File Offset: 0x000B30C4
	public void UpdateMovingPlatform()
	{
		this.m_ignoreForAFrameBecauseOfUnitysDelayedCollisionCallbacks = false;
		Vector3 vector = Vector3.zero;
		Vector3 vector2;
		if (this.m_platformMovement.IsOnGround)
		{
			vector2 = this.m_platformMovement.FeetPosition;
		}
		else if (this.m_platformMovement.IsOnCeiling)
		{
			vector2 = this.m_platformMovement.HeadPosition;
		}
		else
		{
			vector2 = this.m_platformMovement.Position;
		}
		if (this.m_groundPlatform)
		{
			Vector3 a = (this.m_groundPlatform.localToWorldMatrix * this.m_groundPlatformMatrix).MultiplyPoint(vector2);
			vector += a - vector2;
			this.m_groundPlatformMatrix = this.m_groundPlatform.worldToLocalMatrix;
		}
		if (this.m_ceilingPlatform)
		{
			Vector3 a = (this.m_ceilingPlatform.localToWorldMatrix * this.m_ceilingPlatformMatrix).MultiplyPoint(vector2);
			vector += a - vector2;
			this.m_ceilingPlatformMatrix = this.m_ceilingPlatform.worldToLocalMatrix;
		}
		if (this.m_wallLeftPlatform)
		{
			Vector3 a = (this.m_wallLeftPlatform.localToWorldMatrix * this.m_wallLeftPlatformMatrix).MultiplyPoint(vector2);
			Vector3 vector3 = a - vector2;
			if (Vector3.Dot(this.m_platformMovement.GravityBinormal, vector3) > 0f || !this.m_platformMovement.IsOnGround)
			{
				vector += vector3;
			}
			this.m_wallLeftPlatformMatrix = this.m_wallLeftPlatform.worldToLocalMatrix;
		}
		if (this.m_wallRightPlatform)
		{
			Vector3 a = (this.m_wallRightPlatform.localToWorldMatrix * this.m_wallRightPlatformMatrix).MultiplyPoint(vector2);
			Vector3 vector4 = a - vector2;
			if (Vector3.Dot(-this.m_platformMovement.GravityBinormal, vector4) > 0f || !this.m_platformMovement.IsOnGround)
			{
				vector += vector4;
			}
			this.m_wallRightPlatformMatrix = this.m_wallRightPlatform.worldToLocalMatrix;
		}
		if (vector.magnitude < 0.001f)
		{
			vector = Vector2.zero;
		}
		vector.z = 0f;
		if (vector.magnitude > 0f)
		{
			if (this.m_platformMovement.IsOnGround)
			{
				this.m_platformMovement.FeetPosition += vector;
			}
			else if (this.m_platformMovement.IsOnCeiling)
			{
				this.m_platformMovement.HeadPosition += vector;
			}
			else
			{
				this.m_platformMovement.Position += vector;
			}
		}
		this.m_oldGroundPlatform = this.m_groundPlatform;
		this.m_oldCeilingPlatform = this.m_ceilingPlatform;
		this.m_oldWallLeftPlatform = this.m_wallLeftPlatform;
		this.m_oldWallRightPlatform = this.m_wallRightPlatform;
		this.m_groundPlatform = null;
		this.m_ceilingPlatform = null;
		this.m_wallLeftPlatform = null;
		this.m_wallRightPlatform = null;
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x000B51C2 File Offset: 0x000B33C2
	public void OnGroundMovingPlatform(Transform platform)
	{
		if (this.m_ignoreForAFrameBecauseOfUnitysDelayedCollisionCallbacks)
		{
			return;
		}
		this.m_groundPlatform = platform;
		if (this.m_oldGroundPlatform != platform)
		{
			this.m_oldGroundPlatform = platform;
			this.m_groundPlatformMatrix = platform.worldToLocalMatrix;
		}
	}

	// Token: 0x06002A15 RID: 10773 RVA: 0x000B51FB File Offset: 0x000B33FB
	public void OnCeilingMovingPlatform(Transform platform)
	{
		if (this.m_ignoreForAFrameBecauseOfUnitysDelayedCollisionCallbacks)
		{
			return;
		}
		this.m_ceilingPlatform = platform;
		if (this.m_oldCeilingPlatform != platform)
		{
			this.m_oldCeilingPlatform = platform;
			this.m_ceilingPlatformMatrix = platform.worldToLocalMatrix;
		}
	}

	// Token: 0x06002A16 RID: 10774 RVA: 0x000B5234 File Offset: 0x000B3434
	public void OnWallLeftMovingPlatform(Transform platform)
	{
		if (this.m_ignoreForAFrameBecauseOfUnitysDelayedCollisionCallbacks)
		{
			return;
		}
		this.m_wallLeftPlatform = platform;
		if (this.m_oldWallLeftPlatform != platform)
		{
			this.m_oldWallLeftPlatform = platform;
			this.m_wallLeftPlatformMatrix = platform.worldToLocalMatrix;
		}
	}

	// Token: 0x06002A17 RID: 10775 RVA: 0x000B526D File Offset: 0x000B346D
	public void OnWallRightMovingPlatform(Transform platform)
	{
		if (this.m_ignoreForAFrameBecauseOfUnitysDelayedCollisionCallbacks)
		{
			return;
		}
		this.m_wallRightPlatform = platform;
		if (this.m_oldWallRightPlatform != platform)
		{
			this.m_oldWallRightPlatform = platform;
			this.m_wallRightPlatformMatrix = platform.worldToLocalMatrix;
		}
	}

	// Token: 0x0400258C RID: 9612
	private bool m_ignoreForAFrameBecauseOfUnitysDelayedCollisionCallbacks;

	// Token: 0x0400258D RID: 9613
	private readonly PlatformMovement m_platformMovement;

	// Token: 0x0400258E RID: 9614
	private Transform m_groundPlatform;

	// Token: 0x0400258F RID: 9615
	private Transform m_oldGroundPlatform;

	// Token: 0x04002590 RID: 9616
	private Matrix4x4 m_groundPlatformMatrix;

	// Token: 0x04002591 RID: 9617
	private Transform m_ceilingPlatform;

	// Token: 0x04002592 RID: 9618
	private Transform m_oldCeilingPlatform;

	// Token: 0x04002593 RID: 9619
	private Matrix4x4 m_ceilingPlatformMatrix;

	// Token: 0x04002594 RID: 9620
	private Transform m_wallLeftPlatform;

	// Token: 0x04002595 RID: 9621
	private Transform m_oldWallLeftPlatform;

	// Token: 0x04002596 RID: 9622
	private Matrix4x4 m_wallLeftPlatformMatrix;

	// Token: 0x04002597 RID: 9623
	private Transform m_wallRightPlatform;

	// Token: 0x04002598 RID: 9624
	private Transform m_oldWallRightPlatform;

	// Token: 0x04002599 RID: 9625
	private Matrix4x4 m_wallRightPlatformMatrix;
}
