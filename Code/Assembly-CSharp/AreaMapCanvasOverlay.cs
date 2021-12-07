using System;
using UnityEngine;

// Token: 0x02000873 RID: 2163
public class AreaMapCanvasOverlay : MonoBehaviour
{
	// Token: 0x060030E4 RID: 12516 RVA: 0x000D014B File Offset: 0x000CE34B
	public void OnValidate()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_transform = base.transform;
	}

	// Token: 0x060030E5 RID: 12517 RVA: 0x000D0168 File Offset: 0x000CE368
	[ContextMenu("Apply size")]
	public void ApplySize()
	{
		Rect rect = new Rect
		{
			width = this.Canvas.Bounds.size.x,
			height = this.Canvas.Bounds.size.y,
			center = this.Canvas.Bounds.center
		};
		Rect rect2 = rect;
		Rect pixelRect = this.PixelRect;
		pixelRect.y = (float)this.Height - pixelRect.y - pixelRect.height;
		rect2.x = rect2.xMin + rect2.width * (pixelRect.x / (float)this.Width);
		rect2.y = rect2.yMin + rect2.height * (pixelRect.y / (float)this.Height);
		rect2.width *= pixelRect.width / (float)this.Width;
		rect2.height *= pixelRect.height / (float)this.Height;
		this.m_transform.localPosition = rect2.center + Vector3.back * 0.01f;
		this.m_transform.localScale = new Vector3(rect2.width, rect2.height, 1f);
	}

	// Token: 0x060030E6 RID: 12518 RVA: 0x000D02E8 File Offset: 0x000CE4E8
	public void ApplyMasks()
	{
		Renderer component = this.Canvas.MapPlaneTexture.GetComponent<Renderer>();
		Texture texture = component.material.GetTexture(ShaderProperties.MapMaskTextureA);
		Texture texture2 = component.material.GetTexture(ShaderProperties.MapMaskTextureB);
		this.m_renderer.material.SetTexture(ShaderProperties.MapMaskTextureA, texture);
		this.m_renderer.material.SetTexture(ShaderProperties.MapMaskTextureB, texture2);
		Rect rect = new Rect
		{
			width = this.m_transform.localScale.x,
			height = this.m_transform.localScale.y,
			center = this.m_transform.localPosition
		};
		Rect rect2 = new Rect
		{
			width = this.Canvas.Bounds.size.x,
			height = this.Canvas.Bounds.size.y,
			center = this.Canvas.Bounds.center
		};
		float x = rect.width / rect2.width;
		float y = rect.height / rect2.height;
		float x2 = (rect.x - rect2.x) / rect2.width;
		float y2 = (rect.y - rect2.y) / rect2.height;
		Material material = this.m_renderer.material;
		material.SetTextureOffset("_MapMaskTextureA", new Vector2(x2, y2));
		material.SetTextureOffset("_MapMaskTextureB", new Vector2(x2, y2));
		material.SetTextureScale("_MapMaskTextureA", new Vector2(x, y));
		material.SetTextureScale("_MapMaskTextureB", new Vector2(x, y));
	}

	// Token: 0x060030E7 RID: 12519 RVA: 0x000D04DC File Offset: 0x000CE6DC
	public void ApplyFade()
	{
		Renderer component = this.Canvas.MapPlaneTexture.GetComponent<Renderer>();
		float @float = component.material.GetFloat(ShaderProperties.Fade);
		this.m_renderer.material.SetFloat(ShaderProperties.Fade, @float);
	}

	// Token: 0x04002C28 RID: 11304
	public AreaMapCanvas Canvas;

	// Token: 0x04002C29 RID: 11305
	public Rect PixelRect;

	// Token: 0x04002C2A RID: 11306
	public int Width;

	// Token: 0x04002C2B RID: 11307
	public int Height;

	// Token: 0x04002C2C RID: 11308
	[SerializeField]
	[HideInInspector]
	private Renderer m_renderer;

	// Token: 0x04002C2D RID: 11309
	[HideInInspector]
	[SerializeField]
	private Transform m_transform;
}
