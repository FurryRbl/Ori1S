using System;
using UnityEngine;

// Token: 0x020003B2 RID: 946
public class LegacyMaterialTransparencyAnimator : LegacyAnimator
{
	// Token: 0x1700046B RID: 1131
	// (get) Token: 0x06001A5F RID: 6751 RVA: 0x00071924 File Offset: 0x0006FB24
	private Material Mat
	{
		get
		{
			if (this.IsInScene)
			{
				return this.m_renderer.sharedMaterial;
			}
			this.m_madeMaterial = true;
			return this.m_renderer.material;
		}
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x0007194F File Offset: 0x0006FB4F
	public override void Awake()
	{
		base.Awake();
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x00071964 File Offset: 0x0006FB64
	public override void Start()
	{
		if (!this.m_renderer.sharedMaterial.HasProperty(this.Property))
		{
			base.enabled = false;
			return;
		}
		this.m_originalValue = this.Mat.GetColor(this.Property).a;
		base.Start();
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x000719BC File Offset: 0x0006FBBC
	protected override void AnimateIt(float value)
	{
		Color color = this.Mat.GetColor(this.Property);
		color.a = this.m_originalValue * value;
		this.m_renderer.enabled = (color.a > 0.01f);
		UberShaderAPI.SetColorCustom(this.m_renderer, color, this.Property, !this.IsInScene);
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x00071A1E File Offset: 0x0006FC1E
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x00071A2B File Offset: 0x0006FC2B
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this.m_madeMaterial)
		{
			UnityEngine.Object.DestroyObject(this.m_renderer.sharedMaterial);
		}
	}

	// Token: 0x040016D4 RID: 5844
	public string Property = "_Color";

	// Token: 0x040016D5 RID: 5845
	private float m_originalValue;

	// Token: 0x040016D6 RID: 5846
	private Renderer m_renderer;

	// Token: 0x040016D7 RID: 5847
	private bool m_madeMaterial;
}
