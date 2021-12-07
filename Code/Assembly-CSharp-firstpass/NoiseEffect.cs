using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
[AddComponentMenu("Image Effects/Noise")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class NoiseEffect : MonoBehaviour
{
	// Token: 0x06000032 RID: 50 RVA: 0x000030A4 File Offset: 0x000012A4
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.shaderRGB == null || this.shaderYUV == null)
		{
			Debug.Log("Noise shaders are not set up! Disabling noise effect.");
			base.enabled = false;
		}
		else if (!this.shaderRGB.isSupported)
		{
			base.enabled = false;
		}
		else if (!this.shaderYUV.isSupported)
		{
			this.rgbFallback = true;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000033 RID: 51 RVA: 0x00003130 File Offset: 0x00001330
	protected Material material
	{
		get
		{
			if (this.m_MaterialRGB == null)
			{
				this.m_MaterialRGB = new Material(this.shaderRGB);
				this.m_MaterialRGB.hideFlags = HideFlags.HideAndDontSave;
			}
			if (this.m_MaterialYUV == null && !this.rgbFallback)
			{
				this.m_MaterialYUV = new Material(this.shaderYUV);
				this.m_MaterialYUV.hideFlags = HideFlags.HideAndDontSave;
			}
			return (this.rgbFallback || this.monochrome) ? this.m_MaterialRGB : this.m_MaterialYUV;
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000031D0 File Offset: 0x000013D0
	protected void OnDisable()
	{
		if (this.m_MaterialRGB)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialRGB);
		}
		if (this.m_MaterialYUV)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialYUV);
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003214 File Offset: 0x00001414
	private void SanitizeParameters()
	{
		this.grainIntensityMin = Mathf.Clamp(this.grainIntensityMin, 0f, 5f);
		this.grainIntensityMax = Mathf.Clamp(this.grainIntensityMax, 0f, 5f);
		this.scratchIntensityMin = Mathf.Clamp(this.scratchIntensityMin, 0f, 5f);
		this.scratchIntensityMax = Mathf.Clamp(this.scratchIntensityMax, 0f, 5f);
		this.scratchFPS = Mathf.Clamp(this.scratchFPS, 1f, 30f);
		this.scratchJitter = Mathf.Clamp(this.scratchJitter, 0f, 1f);
		this.grainSize = Mathf.Clamp(this.grainSize, 0.1f, 50f);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000032E0 File Offset: 0x000014E0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SanitizeParameters();
		if (this.scratchTimeLeft <= 0f)
		{
			this.scratchTimeLeft = UnityEngine.Random.value * 2f / this.scratchFPS;
			this.scratchX = UnityEngine.Random.value;
			this.scratchY = UnityEngine.Random.value;
		}
		this.scratchTimeLeft -= Time.deltaTime;
		Material material = this.material;
		material.SetTexture("_GrainTex", this.grainTexture);
		material.SetTexture("_ScratchTex", this.scratchTexture);
		float num = 1f / this.grainSize;
		material.SetVector("_GrainOffsetScale", new Vector4(UnityEngine.Random.value, UnityEngine.Random.value, (float)Screen.width / (float)this.grainTexture.width * num, (float)Screen.height / (float)this.grainTexture.height * num));
		material.SetVector("_ScratchOffsetScale", new Vector4(this.scratchX + UnityEngine.Random.value * this.scratchJitter, this.scratchY + UnityEngine.Random.value * this.scratchJitter, (float)Screen.width / (float)this.scratchTexture.width, (float)Screen.height / (float)this.scratchTexture.height));
		material.SetVector("_Intensity", new Vector4(UnityEngine.Random.Range(this.grainIntensityMin, this.grainIntensityMax), UnityEngine.Random.Range(this.scratchIntensityMin, this.scratchIntensityMax), 0f, 0f));
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x04000027 RID: 39
	public bool monochrome = true;

	// Token: 0x04000028 RID: 40
	private bool rgbFallback;

	// Token: 0x04000029 RID: 41
	public float grainIntensityMin = 0.1f;

	// Token: 0x0400002A RID: 42
	public float grainIntensityMax = 0.2f;

	// Token: 0x0400002B RID: 43
	public float grainSize = 2f;

	// Token: 0x0400002C RID: 44
	public float scratchIntensityMin = 0.05f;

	// Token: 0x0400002D RID: 45
	public float scratchIntensityMax = 0.25f;

	// Token: 0x0400002E RID: 46
	public float scratchFPS = 10f;

	// Token: 0x0400002F RID: 47
	public float scratchJitter = 0.01f;

	// Token: 0x04000030 RID: 48
	public Texture grainTexture;

	// Token: 0x04000031 RID: 49
	public Texture scratchTexture;

	// Token: 0x04000032 RID: 50
	public Shader shaderRGB;

	// Token: 0x04000033 RID: 51
	public Shader shaderYUV;

	// Token: 0x04000034 RID: 52
	private Material m_MaterialRGB;

	// Token: 0x04000035 RID: 53
	private Material m_MaterialYUV;

	// Token: 0x04000036 RID: 54
	private float scratchTimeLeft;

	// Token: 0x04000037 RID: 55
	private float scratchX;

	// Token: 0x04000038 RID: 56
	private float scratchY;
}
