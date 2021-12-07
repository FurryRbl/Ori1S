using System;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public struct MoonControllerColliderHit
{
	// Token: 0x04001AB6 RID: 6838
	public Collider Collider;

	// Token: 0x04001AB7 RID: 6839
	public MoonCharacterController Controller;

	// Token: 0x04001AB8 RID: 6840
	public Vector3 MoveDirection;

	// Token: 0x04001AB9 RID: 6841
	public float MoveLength;

	// Token: 0x04001ABA RID: 6842
	public Vector3 Normal;

	// Token: 0x04001ABB RID: 6843
	public Vector3 Point;

	// Token: 0x04001ABC RID: 6844
	public float Distance;

	// Token: 0x04001ABD RID: 6845
	public float ContinueMoveLength;

	// Token: 0x04001ABE RID: 6846
	public Vector3 ContinueMoveDirection;
}
