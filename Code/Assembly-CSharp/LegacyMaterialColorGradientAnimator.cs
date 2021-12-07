using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class LegacyMaterialColorGradientAnimator : LegacyAnimator
{
	// Token: 0x06001A54 RID: 6740 RVA: 0x00071520 File Offset: 0x0006F720
	public override void Start()
	{
		if (this.AnimateChildren)
		{
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>())
			{
				if (renderer.sharedMaterial.HasProperty(this.Property))
				{
					this.m_rendererData.Add(new LegacyMaterialColorGradientAnimator.RendererData(renderer.sharedMaterial.GetColor(this.Property), renderer));
				}
			}
		}
		else
		{
			Renderer component = base.GetComponent<Renderer>();
			if (component.sharedMaterial.HasProperty(this.Property))
			{
				this.m_rendererData.Add(new LegacyMaterialColorGradientAnimator.RendererData(component.sharedMaterial.GetColor(this.Property), component));
			}
		}
		base.Start();
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x000715DC File Offset: 0x0006F7DC
	protected override void AnimateIt(float value)
	{
		float value2 = this.GradientCurve.Evaluate(base.CurrentTime);
		int num = (int)Mathf.Lerp(0f, (float)(this.GradientPixels.Length - 1), Mathf.Clamp01(value2));
		Color b = this.GradientPixels[num] * ((!this.Half) ? 1f : 0.5f);
		for (int i = 0; i < this.m_rendererData.Count; i++)
		{
			LegacyMaterialColorGradientAnimator.RendererData rendererData = this.m_rendererData[i];
			Color color = Color.Lerp(rendererData.OriginalValue, b, value);
			if (rendererData.Renderer == null || rendererData.Renderer.sharedMaterial == null)
			{
				return;
			}
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

	// Token: 0x06001A56 RID: 6742 RVA: 0x00071759 File Offset: 0x0006F959
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x040016C0 RID: 5824
	public bool AnimateChildren;

	// Token: 0x040016C1 RID: 5825
	public Texture2D Gradient;

	// Token: 0x040016C2 RID: 5826
	public AnimationCurve GradientCurve;

	// Token: 0x040016C3 RID: 5827
	public bool Red = true;

	// Token: 0x040016C4 RID: 5828
	public bool Green = true;

	// Token: 0x040016C5 RID: 5829
	public bool Blue = true;

	// Token: 0x040016C6 RID: 5830
	public bool Alpha = true;

	// Token: 0x040016C7 RID: 5831
	public bool Half;

	// Token: 0x040016C8 RID: 5832
	public string Property = "_Color";

	// Token: 0x040016C9 RID: 5833
	private readonly List<LegacyMaterialColorGradientAnimator.RendererData> m_rendererData = new List<LegacyMaterialColorGradientAnimator.RendererData>();

	// Token: 0x040016CA RID: 5834
	public Color[] GradientPixels;

	// Token: 0x020003AF RID: 943
	public struct RendererData
	{
		// Token: 0x06001A57 RID: 6743 RVA: 0x00071766 File Offset: 0x0006F966
		public RendererData(Color originalValue, Renderer renderer)
		{
			this.OriginalValue = originalValue;
			this.Renderer = renderer;
		}

		// Token: 0x040016CB RID: 5835
		public Color OriginalValue;

		// Token: 0x040016CC RID: 5836
		public Renderer Renderer;
	}
}
