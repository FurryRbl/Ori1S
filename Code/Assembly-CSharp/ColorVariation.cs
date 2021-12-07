using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class ColorVariation : MonoBehaviour
{
	// Token: 0x06000882 RID: 2178 RVA: 0x000249A4 File Offset: 0x00022BA4
	public void OnValidate()
	{
		this.m_renderers = base.GetComponentsInChildren<Renderer>();
		this.m_cachedColors = new Color[this.m_renderers.Length];
		for (int i = 0; i < this.m_renderers.Length; i++)
		{
			if (this.m_renderers[i].sharedMaterial.HasProperty(ShaderProperties.Color))
			{
				this.m_cachedColors[i] = this.m_renderers[i].sharedMaterial.GetColor(ShaderProperties.Color);
			}
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00024A30 File Offset: 0x00022C30
	public void Awake()
	{
		ColorVariationManager.Instance.Register(this);
		SceneRoot sceneRoot = SceneRoot.FindFromTransform(base.transform);
		this.MetaDataGUID = sceneRoot.MetaData.SceneMoonGuid;
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00024A65 File Offset: 0x00022C65
	public void OnEnable()
	{
		this.m_time = 0f;
		this.Sample();
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00024A78 File Offset: 0x00022C78
	public void OnDestroy()
	{
		if (ColorVariationManager.Instance)
		{
			ColorVariationManager.Instance.Unregister(this);
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00024A94 File Offset: 0x00022C94
	public void Show()
	{
		this.m_speed = 1f;
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00024AA1 File Offset: 0x00022CA1
	public void Hide()
	{
		this.m_speed = -1f;
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00024AB0 File Offset: 0x00022CB0
	public void FixedUpdate()
	{
		this.m_time = Mathf.Clamp01(this.m_time + this.m_speed * Time.deltaTime);
		this.Sample();
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00024AE4 File Offset: 0x00022CE4
	public void Sample()
	{
		for (int i = 0; i < this.m_renderers.Length; i++)
		{
			if (this.m_renderers[i] != null)
			{
				this.m_renderers[i].sharedMaterial.SetColor(ShaderProperties.Color, this.SetAlpha(this.m_cachedColors[i], this.m_time));
			}
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00024B51 File Offset: 0x00022D51
	public Color SetAlpha(Color color, float alpha)
	{
		color.a = alpha * color.a * 1.4f;
		return color;
	}

	// Token: 0x040006BB RID: 1723
	[HideInInspector]
	[SerializeField]
	private Renderer[] m_renderers;

	// Token: 0x040006BC RID: 1724
	[SerializeField]
	[HideInInspector]
	private Color[] m_cachedColors;

	// Token: 0x040006BD RID: 1725
	private float m_time;

	// Token: 0x040006BE RID: 1726
	private float m_speed;

	// Token: 0x040006BF RID: 1727
	public MoonGuid MetaDataGUID;
}
