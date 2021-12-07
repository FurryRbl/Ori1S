using System;
using UnityEngine;

// Token: 0x020003B8 RID: 952
public class LegacyTextureUVAnimator : LegacyAnimator
{
	// Token: 0x06001A78 RID: 6776 RVA: 0x000720BF File Offset: 0x000702BF
	public override void Awake()
	{
		base.Awake();
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x000720D3 File Offset: 0x000702D3
	public override void Start()
	{
		this.m_originalOffset = base.GetComponent<Renderer>().sharedMaterial.GetTextureOffset(this.TextureName);
		base.Start();
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x000720F8 File Offset: 0x000702F8
	protected override void AnimateIt(float value)
	{
		this.m_mainMaterial = ((!this.IsInScene) ? this.m_renderer.material : this.m_renderer.sharedMaterial);
		Vector2 originalOffset = this.m_originalOffset;
		originalOffset.x += value * this.UMultiplier;
		originalOffset.y += value * this.VMultiplier;
		this.m_mainMaterial.SetTextureOffset(this.TextureName, originalOffset);
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x00072176 File Offset: 0x00070376
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x040016F6 RID: 5878
	public float UMultiplier = 1f;

	// Token: 0x040016F7 RID: 5879
	public float VMultiplier = 1f;

	// Token: 0x040016F8 RID: 5880
	public string TextureName = "_MainTex";

	// Token: 0x040016F9 RID: 5881
	private Material m_mainMaterial;

	// Token: 0x040016FA RID: 5882
	private Vector2 m_originalOffset;

	// Token: 0x040016FB RID: 5883
	private Renderer m_renderer;
}
