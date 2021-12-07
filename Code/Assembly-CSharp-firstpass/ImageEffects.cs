using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[AddComponentMenu("")]
public class ImageEffects
{
	// Token: 0x0600002A RID: 42 RVA: 0x00002DE0 File Offset: 0x00000FE0
	public static void RenderDistortion(Material material, RenderTexture source, RenderTexture destination, float angle, Vector2 center, Vector2 radius)
	{
		bool flag = source.texelSize.y < 0f;
		if (flag)
		{
			center.y = 1f - center.y;
			angle = -angle;
		}
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, angle), Vector3.one);
		material.SetMatrix("_RotationMatrix", matrix);
		material.SetVector("_CenterRadius", new Vector4(center.x, center.y, radius.x, radius.y));
		material.SetFloat("_Angle", angle * 0.017453292f);
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002E94 File Offset: 0x00001094
	[Obsolete("Use Graphics.Blit(source,dest) instead")]
	public static void Blit(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002EA0 File Offset: 0x000010A0
	[Obsolete("Use Graphics.Blit(source, destination, material) instead")]
	public static void BlitWithMaterial(Material material, RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest, material);
	}
}
