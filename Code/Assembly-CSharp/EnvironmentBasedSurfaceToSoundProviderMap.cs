using System;
using Game;

// Token: 0x020001D2 RID: 466
public class EnvironmentBasedSurfaceToSoundProviderMap : SurfaceToSoundProviderMap
{
	// Token: 0x060010C4 RID: 4292 RVA: 0x0004CAD8 File Offset: 0x0004ACD8
	public override SoundDescriptor GetSoundForMaterial(SurfaceMaterialType surfaceMaterialType, IContext context)
	{
		if (World.CurrentArea != null && World.CurrentArea.Area != null)
		{
			foreach (AreaNameSurfaceToSoundProviderMapCouple areaNameSurfaceToSoundProviderMapCouple in this.Providers)
			{
				if (World.CurrentArea.Area.AreaIdentifier == areaNameSurfaceToSoundProviderMapCouple.AreaName)
				{
					return areaNameSurfaceToSoundProviderMapCouple.SurfaceToSoundProviderMap.GetSoundForMaterial(surfaceMaterialType, context);
				}
			}
		}
		return this.DefaultSoundProvider.GetSoundForMaterial(surfaceMaterialType, context);
	}

	// Token: 0x04000E44 RID: 3652
	public SurfaceToSoundProviderMap DefaultSoundProvider;

	// Token: 0x04000E45 RID: 3653
	public AreaNameSurfaceToSoundProviderMapCouple[] Providers;
}
