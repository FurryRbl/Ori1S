using System;
using UnityEngine;

// Token: 0x020007F1 RID: 2033
[Serializable]
public class EnvironmentLightTexture
{
	// Token: 0x06002EAF RID: 11951 RVA: 0x000C5E88 File Offset: 0x000C4088
	public static Vector2 Rotate(Vector2 v, float degrees)
	{
		float num = Mathf.Sin(degrees * 0.017453292f);
		float num2 = Mathf.Cos(degrees * 0.017453292f);
		float x = v.x;
		float y = v.y;
		v.x = num2 * x - num * y;
		v.y = num * x + num2 * y;
		return v;
	}

	// Token: 0x06002EB0 RID: 11952 RVA: 0x000C5EDC File Offset: 0x000C40DC
	public void BindToMaterial(Material bindMaterial, int light, float randomOffset, int num)
	{
		bindMaterial.SetTexture(EnvironmentLight.LightNames[light, num, 0], this.Texture);
		bindMaterial.SetTextureScale(EnvironmentLight.LightNames[light, num, 0], this.Scale);
		bindMaterial.SetTextureOffset(EnvironmentLight.LightNames[light, num, 0], this.Offset);
		bindMaterial.SetVector(EnvironmentLight.LightNames[light, num, 1], new Vector4(0f, 0f, this.Rotation * 0.017453292f, 0f));
		bindMaterial.SetColor(EnvironmentLight.LightNames[light, num, 2], this.Color);
	}

	// Token: 0x06002EB1 RID: 11953 RVA: 0x000C5F85 File Offset: 0x000C4185
	public Vector4 GetTurbVec(float randomOffset)
	{
		return new Vector4(this.TurbulenceStrength, this.TurbulenceSpeed, randomOffset, this.TurbulenceBias);
	}

	// Token: 0x06002EB2 RID: 11954 RVA: 0x000C5F9F File Offset: 0x000C419F
	public Vector4 GetTurbScaleVec()
	{
		return new Vector4(this.TurbulenceScale.x, this.TurbulenceScale.y, 0f, 0f);
	}

	// Token: 0x040029E0 RID: 10720
	public Vector2 Offset;

	// Token: 0x040029E1 RID: 10721
	public Vector2 Scale = Vector2.one;

	// Token: 0x040029E2 RID: 10722
	public float Rotation;

	// Token: 0x040029E3 RID: 10723
	public Color Color = Color.white;

	// Token: 0x040029E4 RID: 10724
	public Texture2D Texture;

	// Token: 0x040029E5 RID: 10725
	public float TurbulenceStrength;

	// Token: 0x040029E6 RID: 10726
	public float TurbulenceSpeed = 1f;

	// Token: 0x040029E7 RID: 10727
	public float TurbulenceBias;

	// Token: 0x040029E8 RID: 10728
	public Vector2 TurbulenceScale = Vector2.one;
}
