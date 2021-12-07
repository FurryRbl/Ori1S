using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007A9 RID: 1961
public class PointlessListOfShaders : MonoBehaviour
{
	// Token: 0x06002D70 RID: 11632 RVA: 0x000C2258 File Offset: 0x000C0458
	[ContextMenu("Fill list")]
	public void FillList()
	{
		UberShaderPrewarmer uberShaderPrewarmer = UnityEngine.Object.FindObjectOfType<UberShaderPrewarmer>();
		for (int i = 0; i < this.Count; i++)
		{
			this.Shaders.Add(uberShaderPrewarmer.LoadedShaders[i]);
		}
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x000C2299 File Offset: 0x000C0499
	public void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002900 RID: 10496
	public List<Shader> Shaders = new List<Shader>();

	// Token: 0x04002901 RID: 10497
	public int Count;
}
