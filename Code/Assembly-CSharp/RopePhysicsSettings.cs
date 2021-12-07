using System;

// Token: 0x020006D7 RID: 1751
[Serializable]
public class RopePhysicsSettings
{
	// Token: 0x0400256D RID: 9581
	public float AngularSpringDamping = 0.1f;

	// Token: 0x0400256E RID: 9582
	public float AngularSpringStiffness = 200f;

	// Token: 0x0400256F RID: 9583
	public float LengthSpringDamping = 0.1f;

	// Token: 0x04002570 RID: 9584
	public float LengthSpringStiffness = 50000f;

	// Token: 0x04002571 RID: 9585
	public float LinkDrag = 0.1f;

	// Token: 0x04002572 RID: 9586
	public float LinkMass = 1f;

	// Token: 0x04002573 RID: 9587
	public int PhysicsIterationCount = 10;
}
