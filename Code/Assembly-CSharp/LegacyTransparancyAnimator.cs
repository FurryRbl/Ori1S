using System;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class LegacyTransparancyAnimator : LegacyAnimator
{
	// Token: 0x06001A43 RID: 6723 RVA: 0x00070F2C File Offset: 0x0006F12C
	public void LinearFadeOut(float time)
	{
		this.AnimationCurve = AnimationCurve.Linear(0f, base.ValueInCurrentFrame(), time, 0f);
		this.Restart();
		base.Invoke("Die", time);
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x00070F67 File Offset: 0x0006F167
	public override void Awake()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.CacheShaderInformation();
		base.Awake();
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x00070F81 File Offset: 0x0006F181
	private void OnEnable()
	{
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x00070F8F File Offset: 0x0006F18F
	public override void Start()
	{
		this.m_collider = base.GetComponent<Collider>();
		base.Start();
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x00070FA4 File Offset: 0x0006F1A4
	private new void Restart()
	{
		this.CacheShaderInformation();
		base.Restart();
		if (this.SampleFirstFrameOnStart)
		{
			base.Sample(0f);
		}
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x00070FD4 File Offset: 0x0006F1D4
	private new void RestartReverse()
	{
		this.CacheShaderInformation();
		base.RestartReverse();
		if (this.SampleFirstFrameOnStart)
		{
			base.Sample(1f);
		}
	}

	// Token: 0x06001A49 RID: 6729 RVA: 0x00071003 File Offset: 0x0006F203
	public void OnMaterialChanged()
	{
		this.m_isDirty = true;
		this.CacheShaderInformation();
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x00071014 File Offset: 0x0006F214
	public void CacheShaderInformation()
	{
		if (!this.m_isDirty)
		{
			return;
		}
		if (this.m_renderer == null)
		{
			return;
		}
		if (this.IsInScene)
		{
			this.m_material = this.m_renderer.sharedMaterial;
		}
		else
		{
			this.m_material = this.m_renderer.material;
			this.m_madeMaterial = true;
		}
		int num = 0;
		for (int i = 0; i < LegacyTransparancyAnimator.SupportedProperties.Length; i++)
		{
			string propertyName = LegacyTransparancyAnimator.SupportedProperties[i];
			if (this.m_material.HasProperty(propertyName))
			{
				num++;
			}
		}
		if (num == 0)
		{
			base.enabled = false;
		}
		this.m_colors = new Color[num];
		this.m_originalAlphas = new float[num];
		this.m_colorPropertyNames = new string[num];
		int num2 = 0;
		for (int j = 0; j < LegacyTransparancyAnimator.SupportedProperties.Length; j++)
		{
			string text = LegacyTransparancyAnimator.SupportedProperties[j];
			if (this.m_material.HasProperty(text))
			{
				this.m_colorPropertyNames[num2] = text;
				this.m_colors[num2] = this.m_material.GetColor(text);
				this.m_originalAlphas[num2] = this.m_colors[num2].a;
				num2++;
			}
		}
		this.m_isDirty = false;
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x00071168 File Offset: 0x0006F368
	protected override void AnimateIt(float value)
	{
		if (value < 0.01f && this.DestroyWhenInvisible)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
		if (this.m_collider)
		{
			if (value < 0.5f)
			{
				if (this.DeactivateWhenInvisible && this.m_collider.enabled)
				{
					this.m_collider.enabled = false;
				}
			}
			else if (this.DeactivateWhenInvisible && !this.m_collider.enabled)
			{
				this.m_collider.enabled = true;
			}
		}
		if (this.m_renderer != null && this.m_colorPropertyNames != null)
		{
			for (int i = 0; i < this.m_colorPropertyNames.Length; i++)
			{
				this.m_colors[i].a = this.m_originalAlphas[i] * value;
				UberShaderAPI.SetColorCustom(this.m_renderer, this.m_colors[i], this.m_colorPropertyNames[i], !this.IsInScene);
			}
			if (this.OptimizeRenderEnable)
			{
				if (value <= 0f)
				{
					this.m_renderer.enabled = false;
				}
				else
				{
					this.m_renderer.enabled = true;
				}
			}
		}
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x000712B4 File Offset: 0x0006F4B4
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this.m_madeMaterial)
		{
			UnityEngine.Object.DestroyObject(this.m_material);
		}
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x000712D2 File Offset: 0x0006F4D2
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(1f);
	}

	// Token: 0x040016AA RID: 5802
	public bool DestroyWhenInvisible;

	// Token: 0x040016AB RID: 5803
	public bool DeactivateWhenInvisible;

	// Token: 0x040016AC RID: 5804
	private Material m_material;

	// Token: 0x040016AD RID: 5805
	[PooledSafe]
	private Color[] m_colors;

	// Token: 0x040016AE RID: 5806
	[PooledSafe]
	private float[] m_originalAlphas;

	// Token: 0x040016AF RID: 5807
	[PooledSafe]
	private string[] m_colorPropertyNames;

	// Token: 0x040016B0 RID: 5808
	private bool m_isDirty = true;

	// Token: 0x040016B1 RID: 5809
	private Collider m_collider;

	// Token: 0x040016B2 RID: 5810
	private static string[] SupportedProperties = new string[]
	{
		"_TintColor",
		"_Color",
		"_AdditiveTintColor",
		"_BackgroundColor"
	};

	// Token: 0x040016B3 RID: 5811
	public bool OptimizeRenderEnable = true;

	// Token: 0x040016B4 RID: 5812
	private Renderer m_renderer;

	// Token: 0x040016B5 RID: 5813
	private bool m_madeMaterial;
}
