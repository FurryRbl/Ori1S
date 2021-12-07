using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class SeinEnvironmentForceController : CharacterState, ISeinReceiver
{
	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x00088686 File Offset: 0x00086886
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00088698 File Offset: 0x00086898
	public PlatformMovementListOfColliders PlatformMovementListOfColliders
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovementListOfColliders;
		}
	}

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x000886AA File Offset: 0x000868AA
	public SeinGrabWall GrabWall
	{
		get
		{
			return this.Sein.Abilities.GrabWall;
		}
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x000886BC File Offset: 0x000868BC
	public bool ShouldApplyForces(Rigidbody rigidbody)
	{
		return !(rigidbody == null) && !rigidbody.GetComponent<PushPullBlock>();
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x000886E0 File Offset: 0x000868E0
	public void Start()
	{
		this.PlatformMovement.OnLandOnCeilingEvent += this.OnLandOnCeilingEvent;
		this.PlatformMovement.OnLandOnWallLeftEvent += this.OnLandOnWallEvent;
		this.PlatformMovement.OnLandOnWallRightEvent += this.OnLandOnWallEvent;
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x00088734 File Offset: 0x00086934
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.PlatformMovement.OnLandOnCeilingEvent -= this.OnLandOnCeilingEvent;
		this.PlatformMovement.OnLandOnWallLeftEvent -= this.OnLandOnWallEvent;
		this.PlatformMovement.OnLandOnWallRightEvent -= this.OnLandOnWallEvent;
	}

	// Token: 0x17000543 RID: 1347
	// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0008878C File Offset: 0x0008698C
	public bool IsGrabbingWall
	{
		get
		{
			return this.GrabWall && this.GrabWall.IsGrabbing;
		}
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x000887B8 File Offset: 0x000869B8
	public override void UpdateCharacterState()
	{
		for (int i = 0; i < this.PlatformMovementListOfColliders.GroundColliders.Count; i++)
		{
			Collider collider = this.PlatformMovementListOfColliders.GroundColliders[i];
			if (collider)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (this.ShouldApplyForces(attachedRigidbody))
				{
					attachedRigidbody.AddForceAtPosition(this.PlatformMovement.LocalToWorld(Vector3.down * this.WeightOfPlayerForce), this.PlatformMovement.FeetPosition, ForceMode.Force);
				}
			}
		}
		if (this.IsGrabbingWall)
		{
			for (int j = 0; j < this.PlatformMovementListOfColliders.WallLeftColliders.Count; j++)
			{
				Collider collider2 = this.PlatformMovementListOfColliders.WallLeftColliders[j];
				if (collider2)
				{
					Rigidbody attachedRigidbody2 = collider2.attachedRigidbody;
					if (this.ShouldApplyForces(attachedRigidbody2))
					{
						attachedRigidbody2.AddForceAtPosition(this.PlatformMovement.LocalToWorld(Vector3.down * this.WeightOfPlayerForce), this.PlatformMovement.Position, ForceMode.Force);
					}
				}
			}
			for (int k = 0; k < this.PlatformMovementListOfColliders.WallRightColliders.Count; k++)
			{
				Collider collider3 = this.PlatformMovementListOfColliders.WallRightColliders[k];
				if (collider3)
				{
					Rigidbody attachedRigidbody3 = collider3.attachedRigidbody;
					if (this.ShouldApplyForces(attachedRigidbody3))
					{
						attachedRigidbody3.AddForceAtPosition(this.PlatformMovement.LocalToWorld(Vector3.down * this.WeightOfPlayerForce), this.PlatformMovement.Position, ForceMode.Force);
					}
				}
			}
		}
	}

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00088979 File Offset: 0x00086B79
	public Vector3 SeinSpeed
	{
		get
		{
			return this.PlatformMovement.LocalSpeed;
		}
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x0008898C File Offset: 0x00086B8C
	public void OnLandOnCeilingEvent(Vector3 normal, Collider target)
	{
		Rigidbody attachedRigidbody = target.attachedRigidbody;
		if (this.ShouldApplyForces(attachedRigidbody))
		{
			Vector3 force = Vector3.ClampMagnitude(Vector3.Dot(normal, this.SeinSpeed) * normal * this.LandOnCeilingImpulsePerUnitOfSpeed, this.LandOnCeilingMaxImpulse);
			attachedRigidbody.AddForceAtPosition(force, this.PlatformMovement.FeetPosition, ForceMode.Impulse);
		}
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x000889E8 File Offset: 0x00086BE8
	public void OnLandOnWallEvent(Vector3 normal, Collider target)
	{
		Rigidbody attachedRigidbody = target.attachedRigidbody;
		if (this.ShouldApplyForces(attachedRigidbody))
		{
			Vector3 force = Vector3.ClampMagnitude(Vector3.Dot(normal, this.SeinSpeed) * normal * this.LandOnWallImpulsePerUnitOfSpeed, this.LandOnWallMaxImpluse);
			attachedRigidbody.AddForceAtPosition(force, this.PlatformMovement.FeetPosition, ForceMode.Impulse);
		}
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x00088A44 File Offset: 0x00086C44
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x04001AEA RID: 6890
	public SeinCharacter Sein;

	// Token: 0x04001AEB RID: 6891
	public float LandOnWallImpulsePerUnitOfSpeed = 20f;

	// Token: 0x04001AEC RID: 6892
	public float LandOnWallMaxImpluse = 600f;

	// Token: 0x04001AED RID: 6893
	public float LandOnCeilingImpulsePerUnitOfSpeed = 500f;

	// Token: 0x04001AEE RID: 6894
	public float LandOnCeilingMaxImpulse = 600f;

	// Token: 0x04001AEF RID: 6895
	public float WeightOfPlayerForce = 20f;
}
