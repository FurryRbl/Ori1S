using System;
using UnityEngine;

// Token: 0x020006D1 RID: 1745
[Serializable]
public class RigidbodyState
{
	// Token: 0x0400254C RID: 9548
	public Rigidbody Rigidbody;

	// Token: 0x0400254D RID: 9549
	public Vector3 AngularVelocity;

	// Token: 0x0400254E RID: 9550
	public Vector3 Velocity;

	// Token: 0x0400254F RID: 9551
	public Vector3 OriginalPosition;

	// Token: 0x04002550 RID: 9552
	public Quaternion OriginalRotation;

	// Token: 0x04002551 RID: 9553
	public bool WasDisabled;

	// Token: 0x04002552 RID: 9554
	public Vector3 LastPosition;

	// Token: 0x04002553 RID: 9555
	public Quaternion LastRotation;
}
