using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200099E RID: 2462
public class ShaderWarmMeUp : MonoBehaviour
{
	// Token: 0x060035AC RID: 13740 RVA: 0x000E12B8 File Offset: 0x000DF4B8
	private void Start()
	{
		Shader.WarmupAllShaders();
	}

	// Token: 0x04003045 RID: 12357
	public List<Shader> Shaders = new List<Shader>();
}
