using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AC RID: 940
public class LegacyMaterialColorAnimator : LegacyAnimator
{
	// Token: 0x06001A4F RID: 6735 RVA: 0x00071328 File Offset: 0x0006F528
	public override void Start()
	{
		foreach (Renderer renderer in (!this.AnimateChildren) ? base.GetComponents<Renderer>() : base.GetComponentsInChildren<Renderer>())
		{
			if (renderer.sharedMaterial.HasProperty(this.Property))
			{
				this.m_rendererData.Add(new LegacyMaterialColorAnimator.RendererData(renderer.sharedMaterial.GetColor(this.Property), renderer));
			}
		}
		base.Start();
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x000713A8 File Offset: 0x0006F5A8
	protected override void AnimateIt(float value)
	{
		foreach (LegacyMaterialColorAnimator.RendererData rendererData in this.m_rendererData)
		{
			Color color = Color.Lerp(rendererData.OriginalValue, this.Value, value);
			Color color2 = rendererData.Renderer.sharedMaterial.GetColor(this.Property);
			Color rhs = color2;
			if (this.Red)
			{
				color2.r = color.r;
			}
			if (this.Green)
			{
				color2.g = color.g;
			}
			if (this.Blue)
			{
				color2.b = color.b;
			}
			if (this.Alpha)
			{
				color2.a = color.a;
			}
			if (color2 != rhs)
			{
				UberShaderAPI.SetColorCustom(rendererData.Renderer, color2, this.Property, !this.IsInScene);
			}
		}
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x000714B4 File Offset: 0x0006F6B4
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x040016B6 RID: 5814
	public bool AnimateChildren;

	// Token: 0x040016B7 RID: 5815
	public Color Value;

	// Token: 0x040016B8 RID: 5816
	public bool Red = true;

	// Token: 0x040016B9 RID: 5817
	public bool Green = true;

	// Token: 0x040016BA RID: 5818
	public bool Blue = true;

	// Token: 0x040016BB RID: 5819
	public bool Alpha = true;

	// Token: 0x040016BC RID: 5820
	public string Property = "_Color";

	// Token: 0x040016BD RID: 5821
	private readonly List<LegacyMaterialColorAnimator.RendererData> m_rendererData = new List<LegacyMaterialColorAnimator.RendererData>();

	// Token: 0x020003AD RID: 941
	public class RendererData
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x000714C1 File Offset: 0x0006F6C1
		public RendererData(Color originalValue, Renderer renderer)
		{
			this.OriginalValue = originalValue;
			this.Renderer = renderer;
		}

		// Token: 0x040016BE RID: 5822
		public Color OriginalValue;

		// Token: 0x040016BF RID: 5823
		public Renderer Renderer;
	}
}
