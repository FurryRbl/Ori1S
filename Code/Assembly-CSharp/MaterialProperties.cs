using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000494 RID: 1172
public class MaterialProperties
{
	// Token: 0x06001FC1 RID: 8129 RVA: 0x0008B618 File Offset: 0x00089818
	public void ApplyMaterialProperties(MaterialProperties materialProperties)
	{
		MaterialPropertiesUtility.ApplyShaderProperty(materialProperties.Shader, this.Shader);
		MaterialPropertiesUtility.ApplyTextureProperties(materialProperties.TextureProperties, this.TextureProperties);
		MaterialPropertiesUtility.ApplyFloatProperties(materialProperties.FloatProperties, this.FloatProperties);
		MaterialPropertiesUtility.ApplyColorProperties(materialProperties.ColorProperties, this.ColorProperties);
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x0008B66C File Offset: 0x0008986C
	public void RemovePropertiesThatArntOverwridden()
	{
		List<OverridableTextureProperty> list = new List<OverridableTextureProperty>(this.TextureProperties.Values);
		List<OverridableFloatProperty> list2 = new List<OverridableFloatProperty>(this.FloatProperties.Values);
		List<OverridableColorProperty> list3 = new List<OverridableColorProperty>(this.ColorProperties.Values);
		list.RemoveAll((OverridableTextureProperty a) => !a.Override);
		list2.RemoveAll((OverridableFloatProperty a) => !a.Override);
		list3.RemoveAll((OverridableColorProperty a) => !a.Override);
		this.TextureProperties = list.ToDictionary((OverridableTextureProperty a) => a.Name);
		this.FloatProperties = list2.ToDictionary((OverridableFloatProperty a) => a.Name);
		this.ColorProperties = list3.ToDictionary((OverridableColorProperty a) => a.Name);
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x0008B794 File Offset: 0x00089994
	public void OverrideAll()
	{
		this.Shader.Override = true;
		foreach (OverridableTextureProperty overridableTextureProperty in this.TextureProperties.Values)
		{
			overridableTextureProperty.Override = true;
		}
		foreach (OverridableFloatProperty overridableFloatProperty in this.FloatProperties.Values)
		{
			overridableFloatProperty.Override = true;
		}
		foreach (OverridableColorProperty overridableColorProperty in this.ColorProperties.Values)
		{
			overridableColorProperty.Override = true;
		}
	}

	// Token: 0x04001B5C RID: 7004
	public OverridableShaderProperty Shader = new OverridableShaderProperty();

	// Token: 0x04001B5D RID: 7005
	public Dictionary<string, OverridableTextureProperty> TextureProperties = new Dictionary<string, OverridableTextureProperty>();

	// Token: 0x04001B5E RID: 7006
	public Dictionary<string, OverridableFloatProperty> FloatProperties = new Dictionary<string, OverridableFloatProperty>();

	// Token: 0x04001B5F RID: 7007
	public Dictionary<string, OverridableColorProperty> ColorProperties = new Dictionary<string, OverridableColorProperty>();
}
