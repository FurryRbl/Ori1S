using System;
using System.Collections.Generic;

// Token: 0x02000495 RID: 1173
public static class MaterialPropertiesUtility
{
	// Token: 0x06001FCA RID: 8138 RVA: 0x0008B8D9 File Offset: 0x00089AD9
	public static void ApplyShaderProperty(OverridableShaderProperty shaderPropertyFrom, OverridableShaderProperty shaderPropertyTo)
	{
		if (!shaderPropertyTo.Override)
		{
			shaderPropertyTo.Shader = shaderPropertyFrom.Shader;
		}
	}

	// Token: 0x06001FCB RID: 8139 RVA: 0x0008B8F4 File Offset: 0x00089AF4
	public static void ApplyTextureProperties(Dictionary<string, OverridableTextureProperty> texturePropertiesDictionaryFrom, Dictionary<string, OverridableTextureProperty> texturePropertiesDictionaryTo)
	{
		foreach (KeyValuePair<string, OverridableTextureProperty> keyValuePair in texturePropertiesDictionaryFrom)
		{
			OverridableTextureProperty overridableTextureProperty;
			if (texturePropertiesDictionaryTo.TryGetValue(keyValuePair.Key, out overridableTextureProperty))
			{
				if (!overridableTextureProperty.Override)
				{
					overridableTextureProperty.Apply(keyValuePair.Value);
				}
			}
			else
			{
				overridableTextureProperty = new OverridableTextureProperty();
				overridableTextureProperty.Apply(keyValuePair.Value);
				texturePropertiesDictionaryTo.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x0008B99C File Offset: 0x00089B9C
	public static void ApplyFloatProperties(Dictionary<string, OverridableFloatProperty> floatPropertiesDictionaryFrom, Dictionary<string, OverridableFloatProperty> floatPropertiesDictionaryTo)
	{
		foreach (KeyValuePair<string, OverridableFloatProperty> keyValuePair in floatPropertiesDictionaryFrom)
		{
			OverridableFloatProperty overridableFloatProperty;
			if (floatPropertiesDictionaryTo.TryGetValue(keyValuePair.Key, out overridableFloatProperty))
			{
				if (!overridableFloatProperty.Override)
				{
					overridableFloatProperty.Apply(keyValuePair.Value);
				}
			}
			else
			{
				overridableFloatProperty = new OverridableFloatProperty();
				overridableFloatProperty.Apply(keyValuePair.Value);
				floatPropertiesDictionaryTo.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x0008BA44 File Offset: 0x00089C44
	public static void ApplyColorProperties(Dictionary<string, OverridableColorProperty> colorPropertiesDictionaryFrom, Dictionary<string, OverridableColorProperty> colorPropertiesDictionaryTo)
	{
		foreach (KeyValuePair<string, OverridableColorProperty> keyValuePair in colorPropertiesDictionaryFrom)
		{
			OverridableColorProperty overridableColorProperty;
			if (colorPropertiesDictionaryTo.TryGetValue(keyValuePair.Key, out overridableColorProperty))
			{
				if (!overridableColorProperty.Override)
				{
					overridableColorProperty.Apply(keyValuePair.Value);
				}
			}
			else
			{
				overridableColorProperty = new OverridableColorProperty();
				overridableColorProperty.Apply(keyValuePair.Value);
				colorPropertiesDictionaryTo.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
