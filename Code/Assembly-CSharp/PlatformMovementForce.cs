using System;
using UnityEngine;

// Token: 0x0200042D RID: 1069
public class PlatformMovementForce : MonoBehaviour, ISeinReceiver
{
	// Token: 0x17000503 RID: 1283
	// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x00083674 File Offset: 0x00081874
	public PlatformBehaviour PlatformBehaviour
	{
		get
		{
			return this.Sein.PlatformBehaviour;
		}
	}

	// Token: 0x17000504 RID: 1284
	// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x00083681 File Offset: 0x00081881
	public PlatformMovementListOfColliders ListOfColliders
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovementListOfColliders;
		}
	}

	// Token: 0x17000505 RID: 1285
	// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0008368E File Offset: 0x0008188E
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x0008369B File Offset: 0x0008189B
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x000836A4 File Offset: 0x000818A4
	public bool ShouldApplyForces(Rigidbody rigidbody)
	{
		return !(rigidbody == null) && !rigidbody.GetComponent<PushPullBlock>();
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x000836C8 File Offset: 0x000818C8
	public void ApplyGroundForce(Vector3 force, ForceMode forceMode)
	{
		for (int i = 0; i < this.ListOfColliders.GroundColliders.Count; i++)
		{
			Collider collider = this.ListOfColliders.GroundColliders[i];
			if (collider != null)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (this.ShouldApplyForces(attachedRigidbody))
				{
					attachedRigidbody.AddForceAtPosition(this.PlatformMovement.LocalToWorld(force), this.PlatformMovement.FeetPosition, forceMode);
				}
			}
		}
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x00083750 File Offset: 0x00081950
	public void ApplyCeilingForce(Vector3 force, ForceMode forceMode)
	{
		for (int i = 0; i < this.ListOfColliders.CeilingColliders.Count; i++)
		{
			Collider collider = this.ListOfColliders.CeilingColliders[i];
			if (collider != null)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (this.ShouldApplyForces(attachedRigidbody))
				{
					attachedRigidbody.AddForceAtPosition(this.PlatformMovement.LocalToWorld(force), this.PlatformMovement.FeetPosition, forceMode);
				}
			}
		}
	}

	// Token: 0x06001DCD RID: 7629 RVA: 0x000837D8 File Offset: 0x000819D8
	public void ApplyWallLeftForce(Vector3 force, ForceMode forceMode)
	{
		for (int i = 0; i < this.ListOfColliders.WallLeftColliders.Count; i++)
		{
			Collider collider = this.ListOfColliders.WallLeftColliders[i];
			if (collider != null)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (this.ShouldApplyForces(attachedRigidbody))
				{
					attachedRigidbody.AddForceAtPosition(this.PlatformMovement.LocalToWorld(force), this.PlatformMovement.FeetPosition, forceMode);
				}
			}
		}
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x00083860 File Offset: 0x00081A60
	public void ApplyWallRightForce(Vector3 force)
	{
		for (int i = 0; i < this.ListOfColliders.WallRightColliders.Count; i++)
		{
			Collider collider = this.ListOfColliders.WallRightColliders[i];
			if (collider != null)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (this.ShouldApplyForces(attachedRigidbody))
				{
					attachedRigidbody.AddForceAtPosition(this.PlatformMovement.LocalToWorld(force), this.PlatformMovement.FeetPosition, ForceMode.Force);
				}
			}
		}
	}

	// Token: 0x040019A2 RID: 6562
	public SeinCharacter Sein;
}
