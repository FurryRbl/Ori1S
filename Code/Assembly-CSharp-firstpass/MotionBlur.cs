using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Motion Blur (Color Accumulation)")]
[ExecuteInEditMode]
public class MotionBlur : ImageEffectBase
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002EC0 File Offset: 0x000010C0
	protected override void Start()
	{
		if (!SystemInfo.supportsRenderTextures)
		{
			base.enabled = false;
			return;
		}
		base.Start();
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002EDC File Offset: 0x000010DC
	protected override void OnDisable()
	{
		base.OnDisable();
		UnityEngine.Object.DestroyImmediate(this.accumTexture);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002EF0 File Offset: 0x000010F0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.accumTexture == null || this.accumTexture.width != source.width || this.accumTexture.height != source.height)
		{
			UnityEngine.Object.DestroyImmediate(this.accumTexture);
			this.accumTexture = new RenderTexture(source.width, source.height, 0);
			this.accumTexture.name = "accumTexture";
			this.accumTexture.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit(source, this.accumTexture);
		}
		if (this.extraBlur)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
			temporary.name = "motionBlur";
			Graphics.Blit(this.accumTexture, temporary);
			Graphics.Blit(temporary, this.accumTexture);
			RenderTexture.ReleaseTemporary(temporary);
		}
		this.blurAmount = Mathf.Clamp(this.blurAmount, 0f, 0.92f);
		base.material.SetTexture("_MainTex", this.accumTexture);
		base.material.SetFloat("_AccumOrig", 1f - this.blurAmount);
		Graphics.Blit(source, this.accumTexture, base.material);
		Graphics.Blit(this.accumTexture, destination);
	}

	// Token: 0x04000024 RID: 36
	public float blurAmount = 0.8f;

	// Token: 0x04000025 RID: 37
	public bool extraBlur;

	// Token: 0x04000026 RID: 38
	private RenderTexture accumTexture;
}
