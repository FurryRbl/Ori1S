using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class PhysicsMaterialManager : MonoBehaviour
{
	// Token: 0x060010CD RID: 4301 RVA: 0x0004CC6C File Offset: 0x0004AE6C
	public void Awake()
	{
		PhysicsMaterialManager.Instance = this;
		this.m_materials.Add(this.Wood, SurfaceMaterialType.Wood);
		this.m_materials.Add(this.Grass, SurfaceMaterialType.Grass);
		this.m_materials.Add(this.Water, SurfaceMaterialType.Water);
		this.m_materials.Add(this.Rock, SurfaceMaterialType.Rock);
		this.m_materials.Add(this.Ice, SurfaceMaterialType.Ice);
		this.m_materials.Add(this.RollingRock, SurfaceMaterialType.Rock);
		this.m_materials.Add(this.PushableBlockMoving, SurfaceMaterialType.Rock);
		this.m_materials.Add(this.Mushroom, SurfaceMaterialType.Mushroom);
		this.m_materials.Add(this.Sand, SurfaceMaterialType.Sand);
		this.m_materials.Add(this.LightDarkPlatform, SurfaceMaterialType.LightDarkPlatform);
		this.m_materials.Add(this.MovingLightDarkPlatform, SurfaceMaterialType.MovingLightDarkPlatform);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0004CD48 File Offset: 0x0004AF48
	public SurfaceMaterialType ColliderMaterialToSurfaceMaterialType(PhysicMaterial material)
	{
		if (material == null)
		{
			return SurfaceMaterialType.Grass;
		}
		SurfaceMaterialType result;
		if (this.m_materials.TryGetValue(material, out result))
		{
			return result;
		}
		return SurfaceMaterialType.Grass;
	}

	// Token: 0x04000E57 RID: 3671
	public static PhysicsMaterialManager Instance;

	// Token: 0x04000E58 RID: 3672
	public PhysicMaterial Wood;

	// Token: 0x04000E59 RID: 3673
	public PhysicMaterial Grass;

	// Token: 0x04000E5A RID: 3674
	public PhysicMaterial Water;

	// Token: 0x04000E5B RID: 3675
	public PhysicMaterial Rock;

	// Token: 0x04000E5C RID: 3676
	public PhysicMaterial Ice;

	// Token: 0x04000E5D RID: 3677
	public PhysicMaterial RollingRock;

	// Token: 0x04000E5E RID: 3678
	public PhysicMaterial PushableBlockMoving;

	// Token: 0x04000E5F RID: 3679
	public PhysicMaterial Mushroom;

	// Token: 0x04000E60 RID: 3680
	public PhysicMaterial Sand;

	// Token: 0x04000E61 RID: 3681
	public PhysicMaterial LightDarkPlatform;

	// Token: 0x04000E62 RID: 3682
	public PhysicMaterial MovingLightDarkPlatform;

	// Token: 0x04000E63 RID: 3683
	private readonly Dictionary<PhysicMaterial, SurfaceMaterialType> m_materials = new Dictionary<PhysicMaterial, SurfaceMaterialType>();
}
