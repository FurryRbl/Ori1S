using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public static class UberShaderAPI
{
	// Token: 0x06001A2B RID: 6699 RVA: 0x00070AC2 File Offset: 0x0006ECC2
	private static Material GetMaterials(Renderer go, bool prefab)
	{
		if (Application.isPlaying && prefab)
		{
			return go.material;
		}
		return go.sharedMaterial;
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x00070AE1 File Offset: 0x0006ECE1
	public static void SetColor(Renderer gameObject, Color color, bool prefab)
	{
		UberShaderAPI.SetColorCustom(gameObject, color, "_Color", prefab);
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x00070AF0 File Offset: 0x0006ECF0
	public static void SetMainTexture(Renderer gameObject, Texture texture, bool prefab)
	{
		UberShaderAPI.SetTexture(gameObject, texture, "_MainTex", prefab);
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x00070B00 File Offset: 0x0006ED00
	public static void SetColorCustom(Renderer gameObject, Color color, string bindName, bool prefab)
	{
		Material materials = UberShaderAPI.GetMaterials(gameObject, prefab);
		materials.SetColor(bindName, color);
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x00070B20 File Offset: 0x0006ED20
	public static void SetFloat(Renderer gameObject, float val, string bindName, bool prefab)
	{
		Material materials = UberShaderAPI.GetMaterials(gameObject, prefab);
		materials.SetFloat(bindName, val);
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x00070B40 File Offset: 0x0006ED40
	public static void SetVector(Renderer gameObject, Vector4 vector, string bindName, bool prefab)
	{
		Material materials = UberShaderAPI.GetMaterials(gameObject, prefab);
		materials.SetVector(bindName, vector);
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x00070B60 File Offset: 0x0006ED60
	public static void SetTexture(Renderer gameObject, Texture texture, string bindName, bool prefab)
	{
		Material materials = UberShaderAPI.GetMaterials(gameObject, prefab);
		materials.SetTexture(bindName, texture);
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x00070B80 File Offset: 0x0006ED80
	public static void SetTextureSettings(Renderer gameObject, string bindName, Vector2 scale, Vector2 offset, Vector2 scroll, float rotation, float rotationSpeed, bool prefab)
	{
		Material materials = UberShaderAPI.GetMaterials(gameObject, prefab);
		materials.SetTextureScale(bindName, scale);
		materials.SetTextureOffset(bindName, offset);
		materials.SetVector(bindName + "_US_ST", new Vector4(scroll.x, scroll.y, rotation, rotationSpeed));
	}
}
