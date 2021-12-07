using System;

// Token: 0x02000028 RID: 40
[Serializable]
public class GravityPlatformMovementSettings
{
	// Token: 0x060001E1 RID: 481 RVA: 0x00008150 File Offset: 0x00006350
	public void CopyFrom(GravityPlatformMovementSettings settings)
	{
		this.GravityStrength = settings.GravityStrength;
		this.GravityAngle = settings.GravityAngle;
		this.MaxFallSpeed = settings.MaxFallSpeed;
	}

	// Token: 0x04000185 RID: 389
	public float GravityStrength = 26f;

	// Token: 0x04000186 RID: 390
	public float GravityAngle;

	// Token: 0x04000187 RID: 391
	public float MaxFallSpeed = 38f;
}
