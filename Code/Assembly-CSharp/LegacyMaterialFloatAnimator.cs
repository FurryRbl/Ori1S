using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public class LegacyMaterialFloatAnimator : LegacyAnimator
{
	// Token: 0x06001A59 RID: 6745 RVA: 0x0007178C File Offset: 0x0006F98C
	public override void Start()
	{
		foreach (Renderer renderer in (!this.AnimateChildren) ? base.GetComponents<Renderer>() : base.GetComponentsInChildren<Renderer>())
		{
			Material material;
			if (this.IsInScene)
			{
				material = renderer.sharedMaterial;
			}
			else
			{
				material = renderer.material;
				this.m_madeMaterial = true;
			}
			if (material.HasProperty(this.Property))
			{
				this.m_rendererData.Add(new LegacyMaterialFloatAnimator.RendererData(material.GetFloat(this.Property), renderer));
			}
		}
		base.Start();
	}

	// Token: 0x06001A5A RID: 6746 RVA: 0x0007182C File Offset: 0x0006FA2C
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this.m_madeMaterial)
		{
			foreach (Renderer renderer in (!this.AnimateChildren) ? base.GetComponents<Renderer>() : base.GetComponentsInChildren<Renderer>())
			{
				UnityEngine.Object.DestroyObject(renderer.sharedMaterial);
			}
		}
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x0007188C File Offset: 0x0006FA8C
	protected override void AnimateIt(float value)
	{
		for (int i = 0; i < this.m_rendererData.Count; i++)
		{
			LegacyMaterialFloatAnimator.RendererData rendererData = this.m_rendererData[i];
			UberShaderAPI.SetFloat(rendererData.Renderer, Mathf.Lerp(rendererData.OriginalValue, this.Value, value), this.Property, !this.IsInScene);
		}
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x000718EE File Offset: 0x0006FAEE
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x040016CD RID: 5837
	public bool AnimateChildren;

	// Token: 0x040016CE RID: 5838
	public float Value;

	// Token: 0x040016CF RID: 5839
	public string Property;

	// Token: 0x040016D0 RID: 5840
	private readonly List<LegacyMaterialFloatAnimator.RendererData> m_rendererData = new List<LegacyMaterialFloatAnimator.RendererData>();

	// Token: 0x040016D1 RID: 5841
	private bool m_madeMaterial;

	// Token: 0x020003B1 RID: 945
	public class RendererData
	{
		// Token: 0x06001A5D RID: 6749 RVA: 0x000718FB File Offset: 0x0006FAFB
		public RendererData(float originalValue, Renderer renderer)
		{
			this.OriginalValue = originalValue;
			this.Renderer = renderer;
		}

		// Token: 0x040016D2 RID: 5842
		public float OriginalValue;

		// Token: 0x040016D3 RID: 5843
		public Renderer Renderer;
	}
}
