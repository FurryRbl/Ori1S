using System;
using UnityEngine;

// Token: 0x02000224 RID: 548
public static class CCDistanceMapGenerator
{
	// Token: 0x060012B5 RID: 4789 RVA: 0x000554E0 File Offset: 0x000536E0
	public static void Generate(Texture2D source, Texture2D destination, float maxInside, float maxOutside, float postProcessDistance, CCDistanceMapGenerator.RGBMode rgbMode)
	{
		if (source.height != destination.height || source.width != destination.width)
		{
			return;
		}
		try
		{
			source.GetPixel(0, 0);
		}
		catch
		{
			return;
		}
		try
		{
			destination.GetPixel(0, 0);
		}
		catch
		{
			return;
		}
		CCDistanceMapGenerator.width = source.width;
		CCDistanceMapGenerator.height = source.height;
		CCDistanceMapGenerator.pixels = new CCDistanceMapGenerator.Pixel[CCDistanceMapGenerator.width, CCDistanceMapGenerator.height];
		Color color = (rgbMode != CCDistanceMapGenerator.RGBMode.White) ? Color.black : Color.white;
		for (int i = 0; i < CCDistanceMapGenerator.height; i++)
		{
			for (int j = 0; j < CCDistanceMapGenerator.width; j++)
			{
				CCDistanceMapGenerator.pixels[j, i] = new CCDistanceMapGenerator.Pixel();
			}
		}
		if (maxInside > 0f)
		{
			for (int i = 0; i < CCDistanceMapGenerator.height; i++)
			{
				for (int j = 0; j < CCDistanceMapGenerator.width; j++)
				{
					CCDistanceMapGenerator.pixels[j, i].alpha = 1f - source.GetPixel(j, i).a;
				}
			}
			CCDistanceMapGenerator.ComputeEdgeGradients();
			CCDistanceMapGenerator.GenerateDistanceTransform();
			if (postProcessDistance > 0f)
			{
				CCDistanceMapGenerator.PostProcess(postProcessDistance);
			}
			float num = 1f / maxInside;
			for (int i = 0; i < CCDistanceMapGenerator.height; i++)
			{
				for (int j = 0; j < CCDistanceMapGenerator.width; j++)
				{
					color.a = Mathf.Clamp01(CCDistanceMapGenerator.pixels[j, i].distance * num);
					destination.SetPixel(j, i, color);
				}
			}
		}
		if (maxOutside > 0f)
		{
			for (int i = 0; i < CCDistanceMapGenerator.height; i++)
			{
				for (int j = 0; j < CCDistanceMapGenerator.width; j++)
				{
					CCDistanceMapGenerator.pixels[j, i].alpha = source.GetPixel(j, i).a;
				}
			}
			CCDistanceMapGenerator.ComputeEdgeGradients();
			CCDistanceMapGenerator.GenerateDistanceTransform();
			if (postProcessDistance > 0f)
			{
				CCDistanceMapGenerator.PostProcess(postProcessDistance);
			}
			float num = 1f / maxOutside;
			if (maxInside > 0f)
			{
				for (int i = 0; i < CCDistanceMapGenerator.height; i++)
				{
					for (int j = 0; j < CCDistanceMapGenerator.width; j++)
					{
						color.a = 0.5f + (destination.GetPixel(j, i).a - Mathf.Clamp01(CCDistanceMapGenerator.pixels[j, i].distance * num)) * 0.5f;
						destination.SetPixel(j, i, color);
					}
				}
			}
			else
			{
				for (int i = 0; i < CCDistanceMapGenerator.height; i++)
				{
					for (int j = 0; j < CCDistanceMapGenerator.width; j++)
					{
						color.a = Mathf.Clamp01(1f - CCDistanceMapGenerator.pixels[j, i].distance * num);
						destination.SetPixel(j, i, color);
					}
				}
			}
		}
		if (rgbMode == CCDistanceMapGenerator.RGBMode.Distance)
		{
			for (int i = 0; i < CCDistanceMapGenerator.height; i++)
			{
				for (int j = 0; j < CCDistanceMapGenerator.width; j++)
				{
					color = destination.GetPixel(j, i);
					color.r = color.a;
					color.g = color.a;
					color.b = color.a;
					destination.SetPixel(j, i, color);
				}
			}
		}
		else if (rgbMode == CCDistanceMapGenerator.RGBMode.Source)
		{
			for (int i = 0; i < CCDistanceMapGenerator.height; i++)
			{
				for (int j = 0; j < CCDistanceMapGenerator.width; j++)
				{
					color = source.GetPixel(j, i);
					color.a = destination.GetPixel(j, i).a;
					destination.SetPixel(j, i, color);
				}
			}
		}
		CCDistanceMapGenerator.pixels = null;
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x000558E8 File Offset: 0x00053AE8
	private static void ComputeEdgeGradients()
	{
		float num = Mathf.Sqrt(2f);
		for (int i = 1; i < CCDistanceMapGenerator.height - 1; i++)
		{
			for (int j = 1; j < CCDistanceMapGenerator.width - 1; j++)
			{
				CCDistanceMapGenerator.Pixel pixel = CCDistanceMapGenerator.pixels[j, i];
				if (pixel.alpha > 0f && pixel.alpha < 1f)
				{
					float num2 = -CCDistanceMapGenerator.pixels[j - 1, i - 1].alpha - CCDistanceMapGenerator.pixels[j - 1, i + 1].alpha + CCDistanceMapGenerator.pixels[j + 1, i - 1].alpha + CCDistanceMapGenerator.pixels[j + 1, i + 1].alpha;
					pixel.gradient.x = num2 + (CCDistanceMapGenerator.pixels[j + 1, i].alpha - CCDistanceMapGenerator.pixels[j - 1, i].alpha) * num;
					pixel.gradient.y = num2 + (CCDistanceMapGenerator.pixels[j, i + 1].alpha - CCDistanceMapGenerator.pixels[j, i - 1].alpha) * num;
					pixel.gradient.Normalize();
				}
			}
		}
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x00055A30 File Offset: 0x00053C30
	private static float ApproximateEdgeDelta(float gx, float gy, float a)
	{
		if (gx == 0f || gy == 0f)
		{
			return 0.5f - a;
		}
		float num = Mathf.Sqrt(gx * gx + gy * gy);
		gx /= num;
		gy /= num;
		gx = Mathf.Abs(gx);
		gy = Mathf.Abs(gy);
		if (gx < gy)
		{
			float num2 = gx;
			gx = gy;
			gy = num2;
		}
		float num3 = 0.5f * gy / gx;
		if (a < num3)
		{
			return 0.5f * (gx + gy) - Mathf.Sqrt(2f * gx * gy * a);
		}
		if (a < 1f - num3)
		{
			return (0.5f - a) * gx;
		}
		return -0.5f * (gx + gy) + Mathf.Sqrt(2f * gx * gy * (1f - a));
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x00055AF4 File Offset: 0x00053CF4
	private static void UpdateDistance(CCDistanceMapGenerator.Pixel p, int x, int y, int oX, int oY)
	{
		CCDistanceMapGenerator.Pixel pixel = CCDistanceMapGenerator.pixels[x + oX, y + oY];
		CCDistanceMapGenerator.Pixel pixel2 = CCDistanceMapGenerator.pixels[x + oX - pixel.dX, y + oY - pixel.dY];
		if (pixel2.alpha == 0f || pixel2 == p)
		{
			return;
		}
		int num = pixel.dX - oX;
		int num2 = pixel.dY - oY;
		float num3 = Mathf.Sqrt((float)(num * num + num2 * num2)) + CCDistanceMapGenerator.ApproximateEdgeDelta((float)num, (float)num2, pixel2.alpha);
		if (num3 < p.distance)
		{
			p.distance = num3;
			p.dX = num;
			p.dY = num2;
		}
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x00055BA0 File Offset: 0x00053DA0
	private static void GenerateDistanceTransform()
	{
		for (int i = 0; i < CCDistanceMapGenerator.height; i++)
		{
			for (int j = 0; j < CCDistanceMapGenerator.width; j++)
			{
				CCDistanceMapGenerator.Pixel pixel = CCDistanceMapGenerator.pixels[j, i];
				pixel.dX = 0;
				pixel.dY = 0;
				if (pixel.alpha <= 0f)
				{
					pixel.distance = 1000000f;
				}
				else if (pixel.alpha < 1f)
				{
					pixel.distance = CCDistanceMapGenerator.ApproximateEdgeDelta(pixel.gradient.x, pixel.gradient.y, pixel.alpha);
				}
				else
				{
					pixel.distance = 0f;
				}
			}
		}
		for (int i = 1; i < CCDistanceMapGenerator.height; i++)
		{
			CCDistanceMapGenerator.Pixel pixel = CCDistanceMapGenerator.pixels[0, i];
			if (pixel.distance > 0f)
			{
				CCDistanceMapGenerator.UpdateDistance(pixel, 0, i, 0, -1);
				CCDistanceMapGenerator.UpdateDistance(pixel, 0, i, 1, -1);
			}
			for (int j = 1; j < CCDistanceMapGenerator.width - 1; j++)
			{
				pixel = CCDistanceMapGenerator.pixels[j, i];
				if (pixel.distance > 0f)
				{
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, -1, 0);
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, -1, -1);
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, 0, -1);
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, 1, -1);
				}
			}
			pixel = CCDistanceMapGenerator.pixels[CCDistanceMapGenerator.width - 1, i];
			if (pixel.distance > 0f)
			{
				CCDistanceMapGenerator.UpdateDistance(pixel, CCDistanceMapGenerator.width - 1, i, -1, 0);
				CCDistanceMapGenerator.UpdateDistance(pixel, CCDistanceMapGenerator.width - 1, i, -1, -1);
				CCDistanceMapGenerator.UpdateDistance(pixel, CCDistanceMapGenerator.width - 1, i, 0, -1);
			}
			for (int j = CCDistanceMapGenerator.width - 2; j >= 0; j--)
			{
				pixel = CCDistanceMapGenerator.pixels[j, i];
				if (pixel.distance > 0f)
				{
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, 1, 0);
				}
			}
		}
		for (int i = CCDistanceMapGenerator.height - 2; i >= 0; i--)
		{
			CCDistanceMapGenerator.Pixel pixel = CCDistanceMapGenerator.pixels[CCDistanceMapGenerator.width - 1, i];
			if (pixel.distance > 0f)
			{
				CCDistanceMapGenerator.UpdateDistance(pixel, CCDistanceMapGenerator.width - 1, i, 0, 1);
				CCDistanceMapGenerator.UpdateDistance(pixel, CCDistanceMapGenerator.width - 1, i, -1, 1);
			}
			for (int j = CCDistanceMapGenerator.width - 2; j > 0; j--)
			{
				pixel = CCDistanceMapGenerator.pixels[j, i];
				if (pixel.distance > 0f)
				{
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, 1, 0);
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, 1, 1);
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, 0, 1);
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, -1, 1);
				}
			}
			pixel = CCDistanceMapGenerator.pixels[0, i];
			if (pixel.distance > 0f)
			{
				CCDistanceMapGenerator.UpdateDistance(pixel, 0, i, 1, 0);
				CCDistanceMapGenerator.UpdateDistance(pixel, 0, i, 1, 1);
				CCDistanceMapGenerator.UpdateDistance(pixel, 0, i, 0, 1);
			}
			for (int j = 1; j < CCDistanceMapGenerator.width; j++)
			{
				pixel = CCDistanceMapGenerator.pixels[j, i];
				if (pixel.distance > 0f)
				{
					CCDistanceMapGenerator.UpdateDistance(pixel, j, i, -1, 0);
				}
			}
		}
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x00055EC4 File Offset: 0x000540C4
	private static void PostProcess(float maxDistance)
	{
		for (int i = 0; i < CCDistanceMapGenerator.height; i++)
		{
			for (int j = 0; j < CCDistanceMapGenerator.width; j++)
			{
				CCDistanceMapGenerator.Pixel pixel = CCDistanceMapGenerator.pixels[j, i];
				if ((pixel.dX != 0 || pixel.dY != 0) && pixel.distance < maxDistance)
				{
					float num = (float)pixel.dX;
					float num2 = (float)pixel.dY;
					CCDistanceMapGenerator.Pixel pixel2 = CCDistanceMapGenerator.pixels[j - pixel.dX, i - pixel.dY];
					Vector2 gradient = pixel2.gradient;
					if (gradient.x != 0f || gradient.y != 0f)
					{
						float num3 = CCDistanceMapGenerator.ApproximateEdgeDelta(gradient.x, gradient.y, pixel2.alpha);
						float num4 = num2 * gradient.x - num * gradient.y;
						float num5 = -num3 * gradient.x + num4 * gradient.y;
						float num6 = -num3 * gradient.y - num4 * gradient.x;
						if (Mathf.Abs(num5) <= 0.5f && Mathf.Abs(num6) <= 0.5f)
						{
							pixel.distance = Mathf.Sqrt((num + num5) * (num + num5) + (num2 + num6) * (num2 + num6));
						}
					}
				}
			}
		}
	}

	// Token: 0x0400101C RID: 4124
	private static int width;

	// Token: 0x0400101D RID: 4125
	private static int height;

	// Token: 0x0400101E RID: 4126
	private static CCDistanceMapGenerator.Pixel[,] pixels;

	// Token: 0x02000225 RID: 549
	public enum RGBMode
	{
		// Token: 0x04001020 RID: 4128
		White,
		// Token: 0x04001021 RID: 4129
		Black,
		// Token: 0x04001022 RID: 4130
		Distance,
		// Token: 0x04001023 RID: 4131
		Source
	}

	// Token: 0x02000226 RID: 550
	private class Pixel
	{
		// Token: 0x04001024 RID: 4132
		public float alpha;

		// Token: 0x04001025 RID: 4133
		public float distance;

		// Token: 0x04001026 RID: 4134
		public Vector2 gradient;

		// Token: 0x04001027 RID: 4135
		public int dX;

		// Token: 0x04001028 RID: 4136
		public int dY;
	}
}
