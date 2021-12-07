using System;
using UnityEngine;

namespace Frameworks
{
	// Token: 0x020003E6 RID: 998
	public static class Shader
	{
		// Token: 0x06001B44 RID: 6980 RVA: 0x000756A4 File Offset: 0x000738A4
		public static void ConvertColorsToTexture(Texture2D texture, Color[] gradient)
		{
			texture.SetPixels(gradient);
			texture.Apply();
			texture.hideFlags = HideFlags.DontSave;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000756D0 File Offset: 0x000738D0
		public static void ConvertGradientToTexture(Texture2D texture, Gradient gradient, int resolution = 128)
		{
			for (int i = 0; i < resolution; i++)
			{
				float time = (float)i / (float)(resolution - 1);
				texture.SetPixel(i, 0, gradient.Evaluate(time));
			}
			texture.Apply();
			texture.hideFlags = HideFlags.DontSave;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0007571C File Offset: 0x0007391C
		public static void ConvertLerpedGradientToTexture(Texture2D texture, Gradient gradientA, Gradient gradientB, float time, int resolution = 128)
		{
			for (int i = 0; i < resolution; i++)
			{
				float time2 = (float)i / (float)(resolution - 1);
				Shader.m_colors[i] = Color.Lerp(gradientA.Evaluate(time2), gradientB.Evaluate(time2), time);
			}
			texture.SetPixels(Shader.m_colors, 0);
			texture.Apply();
			texture.hideFlags = HideFlags.DontSave;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0007578C File Offset: 0x0007398C
		public static Gradient ConvertTextureToGradient(Texture2D texture)
		{
			GradientColorKey[] array = new GradientColorKey[8];
			GradientAlphaKey[] array2 = new GradientAlphaKey[8];
			int num = 0;
			int num2 = texture.width / 7;
			if (num2 < 1)
			{
				num2 = 1;
			}
			for (int i = 0; i < texture.width; i += num2)
			{
				float time = (float)i / (float)texture.width;
				array[num] = new GradientColorKey(texture.GetPixel(i, 1), time);
				array2[num] = new GradientAlphaKey(array[num].color.a, time);
				array[num].color.a = 1f;
				num++;
			}
			Gradient gradient = new Gradient();
			gradient.SetKeys(array, array2);
			return gradient;
		}

		// Token: 0x040017B5 RID: 6069
		private static readonly Color[] m_colors = new Color[128];

		// Token: 0x020003E7 RID: 999
		public static class Globals
		{
			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00075862 File Offset: 0x00073A62
			// (set) Token: 0x06001B48 RID: 6984 RVA: 0x0007584F File Offset: 0x00073A4F
			public static Texture FogGradientTexture
			{
				get
				{
					return Shader.Globals.m_fogGradientTexture;
				}
				set
				{
					Shader.Globals.m_fogGradientTexture = value;
					Shader.SetGlobalTexture("_FogGradientTex", value);
				}
			}

			// Token: 0x17000484 RID: 1156
			// (set) Token: 0x06001B4A RID: 6986 RVA: 0x00075869 File Offset: 0x00073A69
			public static float FogGradientRange
			{
				set
				{
					Shader.SetGlobalFloat("_FogGradientRange", value * 2f);
				}
			}

			// Token: 0x040017B6 RID: 6070
			private static Texture m_fogGradientTexture;
		}

		// Token: 0x02000629 RID: 1577
		public static class DefaultTextures
		{
			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x060026DB RID: 9947 RVA: 0x000A9CB0 File Offset: 0x000A7EB0
			public static Texture Transparent
			{
				get
				{
					if (Shader.DefaultTextures.m_transparent == null)
					{
						Shader.DefaultTextures.m_transparent = new Texture2D(2, 2);
						Shader.DefaultTextures.m_transparent.hideFlags = HideFlags.DontSave;
						Shader.DefaultTextures.m_transparent.SetPixels(new Color[]
						{
							new Color(0f, 0f, 0f, 0f),
							new Color(0f, 0f, 0f, 0f),
							new Color(0f, 0f, 0f, 0f),
							new Color(0f, 0f, 0f, 0f)
						});
						Shader.DefaultTextures.m_transparent.Apply();
					}
					return Shader.DefaultTextures.m_transparent;
				}
			}

			// Token: 0x04002173 RID: 8563
			private static Texture2D m_transparent;
		}
	}
}
