using System;

// Token: 0x020001DC RID: 476
public class SurfaceBasedSoundProvider : SoundProvider
{
	// Token: 0x060010D6 RID: 4310 RVA: 0x0004CF01 File Offset: 0x0004B101
	public override SoundDescriptor GetSound(IContext context)
	{
		return this.SoundProviderMap.GetSoundForMaterial(this.SurfaceProvider.GetSurfaceType(), context);
	}

	// Token: 0x04000E80 RID: 3712
	public SurfaceProvider SurfaceProvider;

	// Token: 0x04000E81 RID: 3713
	public SurfaceToSoundProviderMap SoundProviderMap;
}
