using System;
using UnityEngine;

// Token: 0x02000490 RID: 1168
[Serializable]
public class OverridableShaderProperty
{
	// Token: 0x06001FB4 RID: 8116 RVA: 0x0008B45E File Offset: 0x0008965E
	public OverridableShaderProperty()
	{
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x0008B466 File Offset: 0x00089666
	public OverridableShaderProperty(OverridableShaderProperty shaderProperty)
	{
		this.Override = shaderProperty.Override;
		this.Shader = shaderProperty.Shader;
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x0008B486 File Offset: 0x00089686
	public void Apply(OverridableShaderProperty shaderProperty)
	{
		this.Shader = shaderProperty.Shader;
	}

	// Token: 0x04001B4F RID: 6991
	public bool Override;

	// Token: 0x04001B50 RID: 6992
	public Shader Shader;
}
