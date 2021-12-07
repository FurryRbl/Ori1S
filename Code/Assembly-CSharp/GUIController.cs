using System;
using System.Text;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class GUIController : MonoBehaviour
{
	// Token: 0x060012D5 RID: 4821 RVA: 0x000569F8 File Offset: 0x00054BF8
	private void Start()
	{
		this.textMaterial = this.text.GetComponent<Renderer>().material;
		this.textTransform = this.text.transform;
		this.stringBuilder = new StringBuilder();
		this.selectedContent = 0;
		this.selectedShader = 2;
		this.scale = 1f;
		this.useDistanceMap = true;
		this.gradientMaps = new Texture2D[this.gradients.Length];
		for (int i = 0; i < this.gradients.Length; i++)
		{
			Texture2D texture2D = new Texture2D(32, 8);
			texture2D.filterMode = FilterMode.Trilinear;
			texture2D.wrapMode = TextureWrapMode.Clamp;
			this.gradients[i].WriteToTexture(0f, 1f, texture2D);
			for (int j = 0; j < 32; j++)
			{
				Color pixel = texture2D.GetPixel(j, 0);
				texture2D.SetPixel(j, 1, pixel);
				texture2D.SetPixel(j, 2, pixel);
				texture2D.SetPixel(j, 3, pixel);
				texture2D.SetPixel(j, 4, pixel);
				texture2D.SetPixel(j, 5, pixel);
				texture2D.SetPixel(j, 6, pixel);
				texture2D.SetPixel(j, 7, pixel);
			}
			texture2D.Apply(true, true);
			this.gradientMaps[i] = texture2D;
		}
		this.text.Width = (this.width = -2f * Camera.main.ScreenToWorldPoint(new Vector3(145f, 0f, -Camera.main.transform.localPosition.z)).x);
		this.fontOptions = new string[this.fontPackages.Length];
		for (int k = 0; k < this.fontOptions.Length; k++)
		{
			this.fontOptions[k] = this.fontPackages[k].font.name;
		}
		if (this.fontOptions.Length > 0)
		{
			this.UpdateFont();
		}
		this.UpdateContent();
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x00056BE0 File Offset: 0x00054DE0
	private void Update()
	{
		if (this.selectedContent == 2)
		{
			this.stringBuilder.Length = 25;
			CCStringBuilderUtility.AppendFloatGrouped(this.stringBuilder, Time.realtimeSinceStartup * 1000f, 2, 7);
			this.stringBuilder.Append("\nTimes a wastin!");
			this.text.UpdateText();
		}
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x00056C3C File Offset: 0x00054E3C
	private float ShaderFloat(string name, float value)
	{
		float num = GUILayout.HorizontalSlider(value, 0f, 1f, new GUILayoutOption[0]);
		if (num != value)
		{
			this.textMaterial.SetFloat(name, num);
		}
		return num;
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x00056C78 File Offset: 0x00054E78
	private float ShaderFloat(string name, float value, float min, float max)
	{
		float num = GUILayout.HorizontalSlider(value, min, max, new GUILayoutOption[0]);
		if (num != value)
		{
			this.textMaterial.SetFloat(name, num);
		}
		return num;
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x00056CAC File Offset: 0x00054EAC
	private void UpdateContent()
	{
		switch (this.selectedContent)
		{
		case 0:
			this.text.Text = "The quick brown fox jumped over a lazy dog.\n\nThe dog [did not] fuss about it.\n\nThe fox thought it was [pretty cool].";
			break;
		case 1:
			this.text.Text = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			break;
		case 3:
			this.text.Text = "\tA\tB\tC\nA\t1\t22\t333\nB\t22\t333\t4444\nC\t333\t4444\t55555";
			break;
		}
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x00056D20 File Offset: 0x00054F20
	private void UpdateFont()
	{
		GUIController.FontPackage fontPackage = this.fontPackages[this.selectedFont];
		this.textMaterial.mainTexture = ((!this.useDistanceMap) ? fontPackage.atlas : fontPackage.distanceMap);
		this.text.Font = fontPackage.font;
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x00056D74 File Offset: 0x00054F74
	private void UpdateShader()
	{
		this.textMaterial.shader = this.shaders[this.selectedShader];
		if (this.selectedShader == 6)
		{
			this.textMaterial.SetTexture("_Gradient", this.gradientMaps[this.selectedGradient]);
		}
	}

	// Token: 0x04001060 RID: 4192
	private const float rotationLimit = 60f;

	// Token: 0x04001061 RID: 4193
	private const int FoxDog = 0;

	// Token: 0x04001062 RID: 4194
	private const int LoremIpsum = 1;

	// Token: 0x04001063 RID: 4195
	private const int Timer = 2;

	// Token: 0x04001064 RID: 4196
	private const int Table = 3;

	// Token: 0x04001065 RID: 4197
	private const int AlphaBlend = 0;

	// Token: 0x04001066 RID: 4198
	private const int AlphaTest = 1;

	// Token: 0x04001067 RID: 4199
	private const int Smooth = 2;

	// Token: 0x04001068 RID: 4200
	private const int SmoothFade = 3;

	// Token: 0x04001069 RID: 4201
	private const int SmoothOutline = 4;

	// Token: 0x0400106A RID: 4202
	private const int SmoothShadow = 5;

	// Token: 0x0400106B RID: 4203
	private const int Gradient = 6;

	// Token: 0x0400106C RID: 4204
	public static string[] contentOptions = new string[]
	{
		"Fox",
		"Lorem",
		"Timer",
		"Table"
	};

	// Token: 0x0400106D RID: 4205
	public static string[] alignmentOptions = new string[]
	{
		"L",
		"C",
		"R",
		"J"
	};

	// Token: 0x0400106E RID: 4206
	public static string[] modifierOptions = new string[]
	{
		"None",
		"[marked]",
		"Vertical",
		"a-z"
	};

	// Token: 0x0400106F RID: 4207
	public static string[] shaderOptions = new string[]
	{
		"Alpha Blend",
		"Alpha Test",
		"Smooth",
		"Smooth Fade",
		"Smooth Outline",
		"Smooth Shadow",
		"Gradient"
	};

	// Token: 0x04001070 RID: 4208
	public CCText text;

	// Token: 0x04001071 RID: 4209
	public GUIController.FontPackage[] fontPackages;

	// Token: 0x04001072 RID: 4210
	public CCTextModifier[] modifiers;

	// Token: 0x04001073 RID: 4211
	public Shader[] shaders;

	// Token: 0x04001074 RID: 4212
	public CCGradient[] gradients;

	// Token: 0x04001075 RID: 4213
	private int selectedContent;

	// Token: 0x04001076 RID: 4214
	private int selectedAlignment;

	// Token: 0x04001077 RID: 4215
	private int selectedFont;

	// Token: 0x04001078 RID: 4216
	private int selectedShader;

	// Token: 0x04001079 RID: 4217
	private int selectedGradient;

	// Token: 0x0400107A RID: 4218
	private int selectedModifier;

	// Token: 0x0400107B RID: 4219
	private bool useDistanceMap;

	// Token: 0x0400107C RID: 4220
	private float scale;

	// Token: 0x0400107D RID: 4221
	private float width;

	// Token: 0x0400107E RID: 4222
	private string[] fontOptions;

	// Token: 0x0400107F RID: 4223
	private Texture2D[] gradientMaps;

	// Token: 0x04001080 RID: 4224
	private Material textMaterial;

	// Token: 0x04001081 RID: 4225
	private Transform textTransform;

	// Token: 0x04001082 RID: 4226
	private Vector3 rotation;

	// Token: 0x04001083 RID: 4227
	private StringBuilder stringBuilder;

	// Token: 0x04001084 RID: 4228
	private float alphaBoundary = 0.5f;

	// Token: 0x04001085 RID: 4229
	private float edgeMin = 0.45f;

	// Token: 0x04001086 RID: 4230
	private float edgeMax = 0.5f;

	// Token: 0x04001087 RID: 4231
	private float outlineMin = 0.35f;

	// Token: 0x04001088 RID: 4232
	private float outlineMax = 0.4f;

	// Token: 0x04001089 RID: 4233
	private float shadowMin = 0.3f;

	// Token: 0x0400108A RID: 4234
	private float shadowMax = 0.5f;

	// Token: 0x0400108B RID: 4235
	private float shadowOffsetU = -0.005f;

	// Token: 0x0400108C RID: 4236
	private float shadowOffsetV = 0.005f;

	// Token: 0x0400108D RID: 4237
	private float fadeDistance = 10f;

	// Token: 0x0400108E RID: 4238
	private float fadeStrength = 1f;

	// Token: 0x02000233 RID: 563
	[Serializable]
	public class FontPackage
	{
		// Token: 0x0400108F RID: 4239
		public CCFont font;

		// Token: 0x04001090 RID: 4240
		public Texture2D atlas;

		// Token: 0x04001091 RID: 4241
		public Texture2D distanceMap;
	}
}
