using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
[AddComponentMenu("Image Effects/Blur")]
[ExecuteInEditMode]
public class BlurEffect : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000003 RID: 3 RVA: 0x0000210C File Offset: 0x0000030C
	protected Material material
	{
		get
		{
			if (BlurEffect.m_Material == null)
			{
				BlurEffect.m_Material = new Material(this.blurShader);
				BlurEffect.m_Material.hideFlags = HideFlags.DontSave;
			}
			return BlurEffect.m_Material;
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002140 File Offset: 0x00000340
	protected void OnDisable()
	{
		if (BlurEffect.m_Material)
		{
			UnityEngine.Object.DestroyImmediate(BlurEffect.m_Material);
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.blurShader || !this.material.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000021A8 File Offset: 0x000003A8
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002228 File Offset: 0x00000428
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000022A0 File Offset: 0x000004A0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		temporary.name = "blurEffect1";
		temporary2.name = "blurEffect2";
		this.DownSample4x(source, temporary);
		bool flag = true;
		for (int i = 0; i < this.iterations; i++)
		{
			if (flag)
			{
				this.FourTapCone(temporary, temporary2, i);
			}
			else
			{
				this.FourTapCone(temporary2, temporary, i);
			}
			flag = !flag;
		}
		if (flag)
		{
			Graphics.Blit(temporary, destination);
		}
		else
		{
			Graphics.Blit(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x04000001 RID: 1
	public int iterations = 3;

	// Token: 0x04000002 RID: 2
	public float blurSpread = 0.6f;

	// Token: 0x04000003 RID: 3
	public Shader blurShader;

	// Token: 0x04000004 RID: 4
	private static Material m_Material;
}
