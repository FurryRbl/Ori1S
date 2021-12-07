using System;
using UnityEngine;

// Token: 0x02000965 RID: 2405
[ExecuteInEditMode]
public class DynamicLightSource : MonoBehaviour
{
	// Token: 0x060034D9 RID: 13529 RVA: 0x000DDF60 File Offset: 0x000DC160
	private void Update()
	{
		Shader.SetGlobalVector("_DynamicLightSource1", new Vector4(base.transform.position.x, base.transform.position.y, this.LightFalloffRadius, this.LightFalloffExponent));
	}

	// Token: 0x04002F98 RID: 12184
	public float LightFalloffRadius = 1f;

	// Token: 0x04002F99 RID: 12185
	public float LightFalloffExponent = 1f;
}
