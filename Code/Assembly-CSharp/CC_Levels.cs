using System;
using UnityEngine;

// Token: 0x0200021D RID: 541
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Levels")]
public class CC_Levels : CC_Base
{
	// Token: 0x060012A4 RID: 4772 RVA: 0x00054F40 File Offset: 0x00053140
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.mode == 0)
		{
			base.material.SetVector("_inputMin", new Vector4(this.inputMinL / 255f, this.inputMinL / 255f, this.inputMinL / 255f, 1f));
			base.material.SetVector("_inputMax", new Vector4(this.inputMaxL / 255f, this.inputMaxL / 255f, this.inputMaxL / 255f, 1f));
			base.material.SetVector("_inputGamma", new Vector4(this.inputGammaL, this.inputGammaL, this.inputGammaL, 1f));
			base.material.SetVector("_outputMin", new Vector4(this.outputMinL / 255f, this.outputMinL / 255f, this.outputMinL / 255f, 1f));
			base.material.SetVector("_outputMax", new Vector4(this.outputMaxL / 255f, this.outputMaxL / 255f, this.outputMaxL / 255f, 1f));
		}
		else
		{
			base.material.SetVector("_inputMin", new Vector4(this.inputMinR / 255f, this.inputMinG / 255f, this.inputMinB / 255f, 1f));
			base.material.SetVector("_inputMax", new Vector4(this.inputMaxR / 255f, this.inputMaxG / 255f, this.inputMaxB / 255f, 1f));
			base.material.SetVector("_inputGamma", new Vector4(this.inputGammaR, this.inputGammaG, this.inputGammaB, 1f));
			base.material.SetVector("_outputMin", new Vector4(this.outputMinR / 255f, this.outputMinG / 255f, this.outputMinB / 255f, 1f));
			base.material.SetVector("_outputMax", new Vector4(this.outputMaxR / 255f, this.outputMaxG / 255f, this.outputMaxB / 255f, 1f));
		}
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FF9 RID: 4089
	public int mode;

	// Token: 0x04000FFA RID: 4090
	public float inputMinL;

	// Token: 0x04000FFB RID: 4091
	public float inputMaxL = 255f;

	// Token: 0x04000FFC RID: 4092
	public float inputGammaL = 1f;

	// Token: 0x04000FFD RID: 4093
	public float inputMinR;

	// Token: 0x04000FFE RID: 4094
	public float inputMaxR = 255f;

	// Token: 0x04000FFF RID: 4095
	public float inputGammaR = 1f;

	// Token: 0x04001000 RID: 4096
	public float inputMinG;

	// Token: 0x04001001 RID: 4097
	public float inputMaxG = 255f;

	// Token: 0x04001002 RID: 4098
	public float inputGammaG = 1f;

	// Token: 0x04001003 RID: 4099
	public float inputMinB;

	// Token: 0x04001004 RID: 4100
	public float inputMaxB = 255f;

	// Token: 0x04001005 RID: 4101
	public float inputGammaB = 1f;

	// Token: 0x04001006 RID: 4102
	public float outputMinL;

	// Token: 0x04001007 RID: 4103
	public float outputMaxL = 255f;

	// Token: 0x04001008 RID: 4104
	public float outputMinR;

	// Token: 0x04001009 RID: 4105
	public float outputMaxR = 255f;

	// Token: 0x0400100A RID: 4106
	public float outputMinG;

	// Token: 0x0400100B RID: 4107
	public float outputMaxG = 255f;

	// Token: 0x0400100C RID: 4108
	public float outputMinB;

	// Token: 0x0400100D RID: 4109
	public float outputMaxB = 255f;
}
