using System;
using UnityEngine;

// Token: 0x0200098A RID: 2442
public class PlatformMovementEnvironmentForceController : MonoBehaviour, ISuspendable
{
	// Token: 0x0600356A RID: 13674 RVA: 0x000DFEDC File Offset: 0x000DE0DC
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x0600356B RID: 13675 RVA: 0x000DFEE4 File Offset: 0x000DE0E4
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600356C RID: 13676 RVA: 0x000DFEEC File Offset: 0x000DE0EC
	public bool ShouldApplyForces(Rigidbody rigidbody)
	{
		return !(rigidbody == null);
	}

	// Token: 0x0600356D RID: 13677 RVA: 0x000DFF00 File Offset: 0x000DE100
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
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
	}

	// Token: 0x17000861 RID: 2145
	// (get) Token: 0x0600356E RID: 13678 RVA: 0x000DFFA1 File Offset: 0x000DE1A1
	// (set) Token: 0x0600356F RID: 13679 RVA: 0x000DFFA9 File Offset: 0x000DE1A9
	public bool IsSuspended { get; set; }

	// Token: 0x04002FFF RID: 12287
	public PlatformMovement PlatformMovement;

	// Token: 0x04003000 RID: 12288
	public PlatformMovementListOfColliders PlatformMovementListOfColliders;

	// Token: 0x04003001 RID: 12289
	public float WeightOfPlayerForce = 20f;
}
