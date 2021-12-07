using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[AddComponentMenu("Image Effects/Sepia Tone")]
[ExecuteInEditMode]
public class SepiaToneEffect : ImageEffectBase
{
	// Token: 0x06000040 RID: 64 RVA: 0x00003928 File Offset: 0x00001B28
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, base.material);
	}
}
