using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[AddComponentMenu("Image Effects/Contrast Stretch")]
[ExecuteInEditMode]
public class ContrastStretchEffect : MonoBehaviour
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000E RID: 14 RVA: 0x0000241C File Offset: 0x0000061C
	protected Material materialLum
	{
		get
		{
			if (this.m_materialLum == null)
			{
				this.m_materialLum = new Material(this.shaderLum);
				this.m_materialLum.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialLum;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000F RID: 15 RVA: 0x00002454 File Offset: 0x00000654
	protected Material materialReduce
	{
		get
		{
			if (this.m_materialReduce == null)
			{
				this.m_materialReduce = new Material(this.shaderReduce);
				this.m_materialReduce.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialReduce;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000010 RID: 16 RVA: 0x0000248C File Offset: 0x0000068C
	protected Material materialAdapt
	{
		get
		{
			if (this.m_materialAdapt == null)
			{
				this.m_materialAdapt = new Material(this.shaderAdapt);
				this.m_materialAdapt.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialAdapt;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000011 RID: 17 RVA: 0x000024C4 File Offset: 0x000006C4
	protected Material materialApply
	{
		get
		{
			if (this.m_materialApply == null)
			{
				this.m_materialApply = new Material(this.shaderApply);
				this.m_materialApply.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialApply;
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000024FC File Offset: 0x000006FC
	private void Start()
	{
		foreach (RenderTexture renderTexture in this.adaptRenderTex)
		{
			renderTexture.name = "contrastStrechEffect";
		}
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shaderAdapt.isSupported || !this.shaderApply.isSupported || !this.shaderLum.isSupported || !this.shaderReduce.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002590 File Offset: 0x00000790
	private void OnEnable()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!this.adaptRenderTex[i])
			{
				this.adaptRenderTex[i] = new RenderTexture(1, 1, 32);
				this.adaptRenderTex[i].name = "contrastStrechEffect";
				this.adaptRenderTex[i].hideFlags = HideFlags.HideAndDontSave;
			}
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000025F4 File Offset: 0x000007F4
	private void OnDisable()
	{
		for (int i = 0; i < 2; i++)
		{
			UnityEngine.Object.DestroyImmediate(this.adaptRenderTex[i]);
			this.adaptRenderTex[i] = null;
		}
		if (this.m_materialLum)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialLum);
		}
		if (this.m_materialReduce)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialReduce);
		}
		if (this.m_materialAdapt)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialAdapt);
		}
		if (this.m_materialApply)
		{
			UnityEngine.Object.DestroyImmediate(this.m_materialApply);
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002698 File Offset: 0x00000898
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / 1, source.height / 1);
		renderTexture.name = "contrastStretchEffect";
		Graphics.Blit(source, renderTexture, this.materialLum);
		while (renderTexture.width > 1 || renderTexture.height > 1)
		{
			int num = renderTexture.width / 2;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = renderTexture.height / 2;
			if (num2 < 1)
			{
				num2 = 1;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
			temporary.name = "rtTempDst";
			Graphics.Blit(renderTexture, temporary, this.materialReduce);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		this.CalculateAdaptation(renderTexture);
		this.materialApply.SetTexture("_AdaptTex", this.adaptRenderTex[this.curAdaptIndex]);
		Graphics.Blit(source, destination, this.materialApply);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002780 File Offset: 0x00000980
	private void CalculateAdaptation(Texture curTexture)
	{
		int num = this.curAdaptIndex;
		this.curAdaptIndex = (this.curAdaptIndex + 1) % 2;
		float num2 = 1f - Mathf.Pow(1f - this.adaptationSpeed, 30f * Time.deltaTime);
		num2 = Mathf.Clamp(num2, 0.01f, 1f);
		this.materialAdapt.SetTexture("_CurTex", curTexture);
		this.materialAdapt.SetVector("_AdaptParams", new Vector4(num2, this.limitMinimum, this.limitMaximum, 0f));
		Graphics.Blit(this.adaptRenderTex[num], this.adaptRenderTex[this.curAdaptIndex], this.materialAdapt);
	}

	// Token: 0x04000008 RID: 8
	public float adaptationSpeed = 0.02f;

	// Token: 0x04000009 RID: 9
	public float limitMinimum = 0.2f;

	// Token: 0x0400000A RID: 10
	public float limitMaximum = 0.6f;

	// Token: 0x0400000B RID: 11
	private RenderTexture[] adaptRenderTex = new RenderTexture[2];

	// Token: 0x0400000C RID: 12
	private int curAdaptIndex;

	// Token: 0x0400000D RID: 13
	public Shader shaderLum;

	// Token: 0x0400000E RID: 14
	private Material m_materialLum;

	// Token: 0x0400000F RID: 15
	public Shader shaderReduce;

	// Token: 0x04000010 RID: 16
	private Material m_materialReduce;

	// Token: 0x04000011 RID: 17
	public Shader shaderAdapt;

	// Token: 0x04000012 RID: 18
	private Material m_materialAdapt;

	// Token: 0x04000013 RID: 19
	public Shader shaderApply;

	// Token: 0x04000014 RID: 20
	private Material m_materialApply;
}
