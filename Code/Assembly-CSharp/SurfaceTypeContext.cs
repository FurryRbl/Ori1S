using System;

// Token: 0x020002A7 RID: 679
public class SurfaceTypeContext : IContext, ISurfaceContext
{
	// Token: 0x060015A7 RID: 5543 RVA: 0x00060033 File Offset: 0x0005E233
	public SurfaceTypeContext(SurfaceMaterialType surfaceMaterialType)
	{
		this.SurfaceMaterialType = surfaceMaterialType;
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00060042 File Offset: 0x0005E242
	// (set) Token: 0x060015A9 RID: 5545 RVA: 0x0006004A File Offset: 0x0005E24A
	public SurfaceMaterialType SurfaceMaterialType { get; private set; }
}
