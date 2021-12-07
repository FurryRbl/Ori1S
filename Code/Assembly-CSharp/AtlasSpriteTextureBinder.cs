using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class AtlasSpriteTextureBinder
{
	// Token: 0x060002F5 RID: 757 RVA: 0x0000C228 File Offset: 0x0000A428
	public AtlasSpriteTextureBinder(AnimationMeshingSettings settings, bool spriceSpaceuvs, Mesh mesh)
	{
		AtlasSpriteTextureBinder.InitProperties();
		this.m_mesh = mesh;
		if (this.m_mesh)
		{
			this.m_mesh.MarkDynamic();
		}
		this.AllocateBufferForSettings(settings);
		this.m_spriceSpaceuvs = spriceSpaceuvs;
		this.GenerateCorrectUvBuffer();
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0000C2CC File Offset: 0x0000A4CC
	public void OnPoolSpawned()
	{
		this.m_frame = 0;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
	private void AllocateBufferForSettings(AnimationMeshingSettings settings)
	{
		int num = (settings.XDivisions + 1) * (settings.YDivisions + 1);
		this.m_vertices = new Vector3[num];
		this.m_uvs = new Vector2[num];
		this.m_triangles = new int[settings.XDivisions * settings.YDivisions * 6];
		this.m_divX = settings.XDivisions;
		this.m_divY = settings.YDivisions;
		if (this.m_mesh != null)
		{
			this.m_mesh.vertices = this.m_vertices;
			int num2 = 0;
			int num3 = this.m_divX + 1;
			for (int i = 0; i < this.m_divY; i++)
			{
				for (int j = 0; j < this.m_divX; j++)
				{
					this.m_triangles[num2++] = j + i * num3;
					this.m_triangles[num2++] = j + (i + 1) * num3;
					this.m_triangles[num2++] = j + 1 + (i + 1) * num3;
					this.m_triangles[num2++] = j + 1 + (i + 1) * num3;
					this.m_triangles[num2++] = j + 1 + i * num3;
					this.m_triangles[num2++] = j + i * num3;
				}
			}
			this.m_mesh.triangles = this.m_triangles;
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0000C429 File Offset: 0x0000A629
	public void UpdateSettings(AnimationMeshingSettings settings)
	{
		if (settings != null && (settings.XDivisions != this.m_divX || settings.YDivisions != this.m_divY))
		{
			this.AllocateBufferForSettings(settings);
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0000C45A File Offset: 0x0000A65A
	public void UpdateSpriceSpaceUv(bool correctUvs)
	{
		if (this.m_spriceSpaceuvs != correctUvs)
		{
			this.m_spriceSpaceuvs = correctUvs;
			this.GenerateCorrectUvBuffer();
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0000C478 File Offset: 0x0000A678
	private void GenerateCorrectUvBuffer()
	{
		if (this.m_spriceSpaceuvs)
		{
			float num = 1f / (float)this.m_divX;
			float num2 = 1f / (float)this.m_divY;
			int num3 = 0;
			for (int i = 0; i < this.m_divY + 1; i++)
			{
				for (int j = 0; j < this.m_divX + 1; j++)
				{
					this.m_uvs[num3].x = (float)j * num;
					this.m_uvs[num3].y = (float)i * num2;
					num3++;
				}
			}
		}
		if (this.m_mesh)
		{
			this.m_mesh.uv = this.m_uvs;
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0000C534 File Offset: 0x0000A734
	public void BindTo(MeshFilter filter, Material material, Atlas atlas, UberScreenMode mode, AtlasSpriteTexture texture)
	{
		if (atlas.Width <= 0f || atlas.Height <= 0f)
		{
			atlas.Width = (float)atlas.Texture.width;
			atlas.Height = (float)atlas.Texture.height;
		}
		Rect normalizedRect = texture.NormalizedRect;
		bool flipped = texture.Flipped;
		Vector2 originalSize = texture.OriginalSize;
		material.SetVector(AtlasSpriteTextureBinder.s_usAtlas, new Vector4(normalizedRect.x, normalizedRect.y, normalizedRect.width, normalizedRect.height));
		material.SetVector(AtlasSpriteTextureBinder.s_screen, UberScreenManager.GetScreen(atlas.ScreenMode, 0f));
		material.SetVector(AtlasSpriteTextureBinder.s_screenMask, UberScreenManager.GetScreenMask(atlas.ScreenMode));
		this.m_lastMode = mode;
		if (!this.m_spriceSpaceuvs)
		{
			Vector2 centerOffset = texture.CenterOffset;
			float num3;
			float num4;
			float num5;
			float num6;
			if (flipped)
			{
				float num = atlas.Width * normalizedRect.width / originalSize.y / 2f;
				float num2 = atlas.Height * normalizedRect.height / originalSize.x / 2f;
				num3 = -num2 + centerOffset.x + 0.5f;
				num4 = -num + centerOffset.y + 0.5f;
				num5 = num2 + centerOffset.x + 0.5f;
				num6 = num + centerOffset.y + 0.5f;
			}
			else
			{
				float num7 = atlas.Width * normalizedRect.width / originalSize.x / 2f;
				float num8 = atlas.Height * normalizedRect.height / originalSize.y / 2f;
				num3 = -num7 + centerOffset.x + 0.5f;
				num4 = -num8 + centerOffset.y + 0.5f;
				num5 = num7 + centerOffset.x + 0.5f;
				num6 = num8 + centerOffset.y + 0.5f;
			}
			material.SetVector(AtlasSpriteTextureBinder.s_usAtlasSt, new Vector4(num3, num4, num5 - num3, num6 - num4));
		}
		else if (this.m_lastMaterial != material)
		{
			material.SetVector(AtlasSpriteTextureBinder.s_usAtlasSt, new Vector4(0f, 0f, 1f, 1f));
			this.m_lastMaterial = material;
		}
		Vector4 vector = material.GetVector(AtlasSpriteTextureBinder.s_depthFlip);
		float y = (!flipped) ? 0f : 1f;
		vector.y = y;
		material.SetVector(AtlasSpriteTextureBinder.s_depthFlip, vector);
		this.ApplyToMesh(atlas, texture);
		if (filter && this.m_lastFilter != filter)
		{
			filter.sharedMesh = this.m_mesh;
			this.m_lastFilter = filter;
		}
		if (this.m_lastAtlas != atlas)
		{
			material.SetTexture(ShaderProperties.MainTexture, atlas.Texture);
			this.m_lastAtlas = atlas;
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0000C824 File Offset: 0x0000AA24
	private static void InitProperties()
	{
		if (AtlasSpriteTextureBinder.s_usAtlas == -1 || AtlasSpriteTextureBinder.s_usAtlasSt == -1 || AtlasSpriteTextureBinder.s_depthFlip == -1)
		{
			AtlasSpriteTextureBinder.s_usAtlas = Shader.PropertyToID("_MainTex_US_ATLAS");
			AtlasSpriteTextureBinder.s_usAtlasSt = Shader.PropertyToID("_MainTex_US_ATLAS_ST");
			AtlasSpriteTextureBinder.s_depthFlip = Shader.PropertyToID("_DepthFlipScreen");
			AtlasSpriteTextureBinder.s_screen = Shader.PropertyToID("_Screen");
			AtlasSpriteTextureBinder.s_screenMask = Shader.PropertyToID("_ScreenMask");
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
	private void ApplyToMesh(Atlas atlas, AtlasSpriteTexture texture)
	{
		Rect normalizedRect = texture.NormalizedRect;
		bool flipped = texture.Flipped;
		Vector2 originalSize = texture.OriginalSize;
		int num = this.m_divX + 1;
		int num2 = this.m_divX + 1;
		if (flipped)
		{
			float num3 = atlas.Width * normalizedRect.width / originalSize.y / 2f;
			float num4 = atlas.Height * normalizedRect.height / originalSize.x / 2f;
			float num5 = 2f * num3 / (float)this.m_divX;
			float num6 = 2f * num4 / (float)this.m_divY;
			float num7 = -num3 + texture.CenterOffset.y;
			float num8 = -num4 + texture.CenterOffset.x;
			int num9 = 0;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					this.m_vertices[num9].y = num7 + (float)i * num5;
					this.m_vertices[num9].x = num8 + (float)j * num6;
					num9++;
				}
			}
		}
		else
		{
			float num10 = atlas.Width * normalizedRect.width / originalSize.x / 2f;
			float num11 = atlas.Height * normalizedRect.height / originalSize.y / 2f;
			float num12 = 2f * num10 / (float)this.m_divX;
			float num13 = 2f * num11 / (float)this.m_divY;
			float num14 = -num10 + texture.CenterOffset.x;
			float num15 = -num11 + texture.CenterOffset.y;
			int num16 = 0;
			for (int k = 0; k < num2; k++)
			{
				for (int l = 0; l < num; l++)
				{
					this.m_vertices[num16].x = num14 + (float)l * num12;
					this.m_vertices[num16].y = num15 + (float)k * num13;
					num16++;
				}
			}
		}
		if (!this.m_spriceSpaceuvs)
		{
			Vector3 b = new Vector3(0.5f, 0.5f);
			for (int m = 0; m < this.m_uvs.Length; m++)
			{
				this.m_uvs[m] = this.m_vertices[m] + b;
			}
		}
		this.m_mesh.vertices = this.m_vertices;
		if (!this.m_spriceSpaceuvs)
		{
			this.m_mesh.uv = this.m_uvs;
		}
		if (this.m_frame % 5 == 0)
		{
			this.m_mesh.RecalculateBounds();
		}
		this.m_frame++;
	}

	// Token: 0x04000225 RID: 549
	private static int s_usAtlas = -1;

	// Token: 0x04000226 RID: 550
	private static int s_usAtlasSt = -1;

	// Token: 0x04000227 RID: 551
	private static int s_depthFlip = -1;

	// Token: 0x04000228 RID: 552
	private static int s_screen = -1;

	// Token: 0x04000229 RID: 553
	private static int s_screenMask = -1;

	// Token: 0x0400022A RID: 554
	private static string[] s_keys = new string[1];

	// Token: 0x0400022B RID: 555
	private Vector3[] m_vertices = new Vector3[4];

	// Token: 0x0400022C RID: 556
	private Vector2[] m_uvs = new Vector2[4];

	// Token: 0x0400022D RID: 557
	private int[] m_triangles = new int[6];

	// Token: 0x0400022E RID: 558
	private bool m_spriceSpaceuvs;

	// Token: 0x0400022F RID: 559
	private Mesh m_mesh;

	// Token: 0x04000230 RID: 560
	private MeshFilter m_lastFilter;

	// Token: 0x04000231 RID: 561
	[PooledSafe]
	private int m_frame;

	// Token: 0x04000232 RID: 562
	private int m_divX;

	// Token: 0x04000233 RID: 563
	private int m_divY;

	// Token: 0x04000234 RID: 564
	private UberScreenMode m_lastMode = UberScreenMode.None;

	// Token: 0x04000235 RID: 565
	private Atlas m_lastAtlas;

	// Token: 0x04000236 RID: 566
	private Material m_lastMaterial;
}
