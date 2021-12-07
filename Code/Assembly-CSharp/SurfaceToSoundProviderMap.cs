using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class SurfaceToSoundProviderMap : MonoBehaviour
{
	// Token: 0x060001F0 RID: 496 RVA: 0x000083A4 File Offset: 0x000065A4
	public virtual SoundDescriptor GetSoundForMaterial(SurfaceMaterialType surfaceMaterialType, IContext context)
	{
		for (int i = 0; i < this.SoundPairs.Count; i++)
		{
			SurfaceMaterialSoundPair surfaceMaterialSoundPair = this.SoundPairs[i];
			if (surfaceMaterialSoundPair.SurfaceMaterialType == surfaceMaterialType)
			{
				return surfaceMaterialSoundPair.IndependantSoundProvider.GetSound(context);
			}
		}
		return this.SoundPairs[0].IndependantSoundProvider.GetSound(context);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000840C File Offset: 0x0000660C
	public static SurfaceMaterialType ColliderMaterialToSurfaceMaterialType(Collider collider)
	{
		if (collider == null || collider.sharedMaterial == null)
		{
			return SurfaceMaterialType.Grass;
		}
		return PhysicsMaterialManager.Instance.ColliderMaterialToSurfaceMaterialType(collider.sharedMaterial);
	}

	// Token: 0x04000193 RID: 403
	public List<SurfaceMaterialSoundPair> SoundPairs;
}
