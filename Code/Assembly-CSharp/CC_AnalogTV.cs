using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
[AddComponentMenu("Colorful/Analog TV")]
[ExecuteInEditMode]
public class CC_AnalogTV : CC_Base
{
	// Token: 0x0600128C RID: 4748 RVA: 0x00054744 File Offset: 0x00052944
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_phase", this.phase);
		base.material.SetFloat("_grayscale", (!this.grayscale) ? 0f : 1f);
		base.material.SetFloat("_noiseIntensity", this.noiseIntensity);
		base.material.SetFloat("_scanlinesIntensity", this.scanlinesIntensity);
		base.material.SetFloat("_scanlinesCount", (float)((int)this.scanlinesCount));
		base.material.SetFloat("_distortion", this.distortion);
		base.material.SetFloat("_cubicDistortion", this.cubicDistortion);
		base.material.SetFloat("_scale", this.scale);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FCB RID: 4043
	public float phase = 0.5f;

	// Token: 0x04000FCC RID: 4044
	public bool grayscale;

	// Token: 0x04000FCD RID: 4045
	public float noiseIntensity = 0.5f;

	// Token: 0x04000FCE RID: 4046
	public float scanlinesIntensity = 2f;

	// Token: 0x04000FCF RID: 4047
	public float scanlinesCount = 768f;

	// Token: 0x04000FD0 RID: 4048
	public float distortion = 0.2f;

	// Token: 0x04000FD1 RID: 4049
	public float cubicDistortion = 0.6f;

	// Token: 0x04000FD2 RID: 4050
	public float scale = 0.8f;
}
