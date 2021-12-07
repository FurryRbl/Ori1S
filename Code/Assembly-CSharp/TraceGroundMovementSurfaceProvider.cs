using System;

// Token: 0x0200056D RID: 1389
public class TraceGroundMovementSurfaceProvider : SurfaceProvider
{
	// Token: 0x0600240E RID: 9230 RVA: 0x0009D6AA File Offset: 0x0009B8AA
	public override SurfaceMaterialType GetSurfaceType()
	{
		return this.TraceGroundMovement.Surface;
	}

	// Token: 0x04001E27 RID: 7719
	public TraceGroundMovement TraceGroundMovement;
}
