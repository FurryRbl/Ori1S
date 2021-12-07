using System;
using UnityEngine;

// Token: 0x02000945 RID: 2373
public class CropUtility
{
	// Token: 0x0600345A RID: 13402 RVA: 0x000DBFC4 File Offset: 0x000DA1C4
	public static void CropMaterial(Material material)
	{
		if (material.mainTexture == null)
		{
			return;
		}
		Texture2D texture2D = material.mainTexture as Texture2D;
		Rect occupiedRect = CropUtility.GetOccupiedRect(texture2D);
		occupiedRect.xMin -= 1f;
		occupiedRect.yMin -= 1f;
		occupiedRect.xMax += 1f;
		occupiedRect.yMax += 1f;
		occupiedRect.xMin /= (float)texture2D.width;
		occupiedRect.yMin /= (float)texture2D.height;
		occupiedRect.xMax /= (float)texture2D.width;
		occupiedRect.yMax /= (float)texture2D.height;
		material.mainTextureOffset = new Vector2(occupiedRect.xMin, occupiedRect.yMin);
		material.mainTextureScale = new Vector2(occupiedRect.width, occupiedRect.height);
	}

	// Token: 0x0600345B RID: 13403 RVA: 0x000DC0C8 File Offset: 0x000DA2C8
	public static Rect GetOccupiedRectNormalized(Texture2D tex)
	{
		Rect occupiedRect = CropUtility.GetOccupiedRect(tex);
		occupiedRect.xMin /= (float)tex.width;
		occupiedRect.xMax /= (float)tex.width;
		occupiedRect.yMin /= (float)tex.height;
		occupiedRect.yMax /= (float)tex.height;
		return occupiedRect;
	}

	// Token: 0x0600345C RID: 13404 RVA: 0x000DC134 File Offset: 0x000DA334
	public static Rect GetOccupiedRect(Texture2D tex)
	{
		Rect result = new Rect(0f, 0f, 0f, 0f);
		return result;
	}
}
