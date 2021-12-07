using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200048E RID: 1166
[ExecuteInEditMode]
public class CustomizeMaterial : MonoBehaviour, IStrippable
{
	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0008B44A File Offset: 0x0008964A
	// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x0008B452 File Offset: 0x00089652
	public Texture LastMainTexture { get; set; }

	// Token: 0x06001FB2 RID: 8114 RVA: 0x0008B45B File Offset: 0x0008965B
	public bool DoStrip()
	{
		return true;
	}

	// Token: 0x04001B3B RID: 6971
	public Material OriginalMaterial;

	// Token: 0x04001B3C RID: 6972
	public int MaterialIndex;

	// Token: 0x04001B3D RID: 6973
	public Material InstancedMaterial;

	// Token: 0x04001B3E RID: 6974
	public bool IsInstancedMaterialPersistent;

	// Token: 0x04001B3F RID: 6975
	public OverridableShaderProperty Shader = new OverridableShaderProperty();

	// Token: 0x04001B40 RID: 6976
	public List<OverridableTextureProperty> TexturePropertiesList = new List<OverridableTextureProperty>();

	// Token: 0x04001B41 RID: 6977
	public List<OverridableFloatProperty> FloatPropertiesList = new List<OverridableFloatProperty>();

	// Token: 0x04001B42 RID: 6978
	public List<OverridableColorProperty> ColorPropertiesList = new List<OverridableColorProperty>();

	// Token: 0x04001B43 RID: 6979
	public bool SpecifyRenderQueue;

	// Token: 0x04001B44 RID: 6980
	public int RenderQueue;

	// Token: 0x04001B45 RID: 6981
	public float OffsetPositionZ;

	// Token: 0x04001B46 RID: 6982
	public Vector2 BlurScale = Vector2.zero;

	// Token: 0x04001B47 RID: 6983
	protected bool m_needsRefreshing;

	// Token: 0x04001B48 RID: 6984
	private float m_oldZ;

	// Token: 0x04001B49 RID: 6985
	private Vector3 m_oldScale;

	// Token: 0x04001B4A RID: 6986
	private Transform m_transform;

	// Token: 0x04001B4B RID: 6987
	private int m_lastLayer;

	// Token: 0x04001B4C RID: 6988
	private int m_forceUpdate;

	// Token: 0x04001B4D RID: 6989
	private GameObject m_gameObject;
}
