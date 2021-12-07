using System;
using UnityEngine;

// Token: 0x02000858 RID: 2136
[ExecuteInEditMode]
public class UberWaterEdge : UberWaterComponent
{
	// Token: 0x0600306A RID: 12394 RVA: 0x000CD464 File Offset: 0x000CB664
	private void OnWillRenderObject()
	{
		if (!base.Control || base.GetComponent<Renderer>() == null || base.GetComponent<Renderer>().sharedMaterial == null)
		{
			return;
		}
		base.Control.BindShaderVariablesToMaterial(base.GetComponent<Renderer>().sharedMaterial);
	}

	// Token: 0x04002BBB RID: 11195
	[Range(0f, 3f)]
	public float BottomSize = 0.25f;

	// Token: 0x04002BBC RID: 11196
	[Range(0f, 3f)]
	public float TopSize = 0.25f;
}
