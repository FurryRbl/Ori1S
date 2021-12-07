using System;
using UnityEngine;

// Token: 0x02000870 RID: 2160
public class AreaMapCanvas : MonoBehaviour
{
	// Token: 0x060030CE RID: 12494 RVA: 0x000CF6FD File Offset: 0x000CD8FD
	public void Awake()
	{
		this.RuntimeArea = GameWorld.Instance.FindRuntimeArea(this.Area);
		this.Mask = this.Area.WorldMapTexture;
	}

	// Token: 0x060030CF RID: 12495 RVA: 0x000CF728 File Offset: 0x000CD928
	public void ResetMap()
	{
		if (this.Area.VisitableCondition && !this.Area.VisitableCondition.Validate(null))
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(true);
		}
		this.MapPlaneTexture.localScale = new Vector3(this.Bounds.size.x, this.Bounds.size.y);
		this.MapPlaneTexture.localPosition = this.Bounds.center;
		if (this.WorldMapTexture)
		{
			this.MapPlaneTexture.GetComponent<Renderer>().material.SetTexture(ShaderProperties.MainTexture, this.WorldMapTexture);
		}
		this.UpdateAreaMaskTextureA();
		if (this.m_addToMap)
		{
			InstantiateUtility.Destroy(this.m_addToMap);
		}
		this.SetFade(0f);
	}

	// Token: 0x170007C8 RID: 1992
	// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000CF82F File Offset: 0x000CDA2F
	public Texture WorldMapTexture
	{
		get
		{
			return this.Area.WorldMapTexture;
		}
	}

	// Token: 0x170007C9 RID: 1993
	// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000CF83C File Offset: 0x000CDA3C
	public Bounds Bounds
	{
		get
		{
			return this.Area.Bounds;
		}
	}

	// Token: 0x170007CA RID: 1994
	// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000CF849 File Offset: 0x000CDA49
	public CageStructureTool CageStructureTool
	{
		get
		{
			return this.Area.CageStructureTool;
		}
	}

	// Token: 0x170007CB RID: 1995
	// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000CF858 File Offset: 0x000CDA58
	public Vector2 WorldMapTextureSize
	{
		get
		{
			return new Vector2((float)this.WorldMapTexture.width, (float)this.WorldMapTexture.height);
		}
	}

	// Token: 0x060030D4 RID: 12500 RVA: 0x000CF884 File Offset: 0x000CDA84
	public RenderTexture GenerateAreaMaskMaskTexture()
	{
		int width = (int)Mathf.Min(1024f, this.Bounds.size.x * (float)this.PixelsPerUnit);
		int height = (int)Mathf.Min(1024f, this.Bounds.size.y * (float)this.PixelsPerUnit);
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
		temporary.name = "worldMapCanvas";
		Graphics.SetRenderTarget(temporary);
		GL.Clear(false, true, Color.clear);
		GL.PushMatrix();
		GL.LoadIdentity();
		GL.LoadPixelMatrix(this.Bounds.min.x + 0.5f, this.Bounds.max.x + 0.5f, this.Bounds.min.y + 0.5f, this.Bounds.max.y + 0.5f);
		Matrix4x4 localToWorldMatrix = this.CageStructureTool.transform.localToWorldMatrix;
		GL.MultMatrix(localToWorldMatrix);
		Material material = new Material(this.SetRGBAShader);
		material.SetColor(ShaderProperties.Color, Color.white / 2f);
		material.SetPass(0);
		GL.Begin(4);
		GL.Color(Color.white);
		for (int i = 0; i < this.CageStructureTool.Faces.Count; i++)
		{
			CageStructureTool.Face face = this.CageStructureTool.Faces[i];
			for (int j = 0; j < face.Triangles.Count; j++)
			{
				int index = face.Triangles[j];
				GL.Vertex(this.CageStructureTool.VertexByIndex(face.Vertices[index]).Position);
			}
		}
		GL.End();
		GL.PopMatrix();
		return temporary;
	}

	// Token: 0x060030D5 RID: 12501 RVA: 0x000CFA88 File Offset: 0x000CDC88
	public Color GetColor(WorldMapAreaState worldState)
	{
		switch (worldState)
		{
		case WorldMapAreaState.Hidden:
			if (AreaMapUI.Instance.DebugNavigation.UndiscoveredMapVisible)
			{
				return Color.white;
			}
			return Color.clear;
		case WorldMapAreaState.Discovered:
			return Color.red;
		case WorldMapAreaState.Visited:
			return Color.white;
		default:
			return Color.red;
		}
	}

	// Token: 0x060030D6 RID: 12502 RVA: 0x000CFAE0 File Offset: 0x000CDCE0
	public RenderTexture GenerateAreaMaskTexture()
	{
		int width = (int)Mathf.Min(1024f, this.Bounds.size.x * (float)this.PixelsPerUnit);
		int height = (int)Mathf.Min(1024f, this.Bounds.size.y * (float)this.PixelsPerUnit);
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
		temporary.name = "worldmapCanvas";
		Graphics.SetRenderTarget(temporary);
		GL.Clear(false, true, Color.clear);
		GL.PushMatrix();
		GL.LoadIdentity();
		GL.LoadPixelMatrix(this.Bounds.min.x + 0.5f, this.Bounds.max.x + 0.5f, this.Bounds.min.y + 0.5f, this.Bounds.max.y + 0.5f);
		Material material = new Material(this.SetRGBAShader);
		material.SetColor(ShaderProperties.Color, Color.white / 2f);
		material.SetPass(0);
		GL.Begin(4);
		Matrix4x4 localToWorldMatrix = this.CageStructureTool.transform.localToWorldMatrix;
		foreach (CageStructureTool.Face face in this.CageStructureTool.Faces)
		{
			WorldMapAreaState faceState = this.RuntimeArea.GetFaceState(face.ID);
			GL.Color(this.GetColor(faceState));
			foreach (int index in face.Triangles)
			{
				GL.Vertex(localToWorldMatrix.MultiplyPoint(this.CageStructureTool.VertexByIndex(face.Vertices[index]).Position));
			}
		}
		GL.End();
		GL.PopMatrix();
		RenderTexture result = this.BlurTextures(temporary);
		RenderTexture.ReleaseTemporary(temporary);
		return result;
	}

	// Token: 0x060030D7 RID: 12503 RVA: 0x000CFD34 File Offset: 0x000CDF34
	public void Update()
	{
		if (this.m_areaMaskTextureA && !this.m_areaMaskTextureA.IsCreated())
		{
			this.UpdateAreaMaskTextureA();
		}
		if (this.m_areaMaskTextureB && !this.m_areaMaskTextureB.IsCreated())
		{
			this.UpdateAreaMaskTextureB();
		}
	}

	// Token: 0x060030D8 RID: 12504 RVA: 0x000CFD90 File Offset: 0x000CDF90
	public void UpdateAreaMaskTextureA()
	{
		if (this.m_areaMaskTextureA)
		{
			UnityEngine.Object.DestroyObject(this.m_areaMaskTextureA);
		}
		this.m_areaMaskTextureA = this.GenerateAreaMaskTexture();
		this.MapPlaneTexture.GetComponent<Renderer>().material.SetTexture(ShaderProperties.MapMaskTextureA, this.m_areaMaskTextureA);
	}

	// Token: 0x060030D9 RID: 12505 RVA: 0x000CFDE4 File Offset: 0x000CDFE4
	public void UpdateAreaMaskTextureB()
	{
		if (this.m_areaMaskTextureB)
		{
			UnityEngine.Object.DestroyObject(this.m_areaMaskTextureB);
		}
		this.m_areaMaskTextureB = this.GenerateAreaMaskTexture();
		this.MapPlaneTexture.GetComponent<Renderer>().material.SetTexture(ShaderProperties.MapMaskTextureB, this.m_areaMaskTextureB);
	}

	// Token: 0x060030DA RID: 12506 RVA: 0x000CFE38 File Offset: 0x000CE038
	public void SetFade(float fade)
	{
		this.MapPlaneTexture.GetComponent<Renderer>().material.SetFloat(ShaderProperties.MapFade, fade);
	}

	// Token: 0x060030DB RID: 12507 RVA: 0x000CFE55 File Offset: 0x000CE055
	public void OnDestroy()
	{
		this.Release();
	}

	// Token: 0x060030DC RID: 12508 RVA: 0x000CFE60 File Offset: 0x000CE060
	public void Release()
	{
		if (this.m_areaMaskTextureA)
		{
			UnityEngine.Object.DestroyObject(this.m_areaMaskTextureA);
			this.m_areaMaskTextureA = null;
		}
		if (this.m_areaMaskTextureB)
		{
			UnityEngine.Object.DestroyObject(this.m_areaMaskTextureB);
			this.m_areaMaskTextureB = null;
		}
	}

	// Token: 0x060030DD RID: 12509 RVA: 0x000CFEB4 File Offset: 0x000CE0B4
	public RenderTexture BlurTextures(Texture originalTexture)
	{
		Texture mask = this.Mask;
		Material material = new Material(this.WorldMapBlurShader);
		material.SetTexture(ShaderProperties.MaskTex, mask);
		int width = originalTexture.width;
		int height = originalTexture.height;
		Vector2 vector = new Vector2(1.5f / this.MapPlaneTexture.localScale.x, 1.5f / this.MapPlaneTexture.localScale.y);
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
		RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
		temporary.name = "worldMapCanvas";
		temporary2.name = "worldMapCanvasB";
		Texture texture = originalTexture;
		RenderTexture renderTexture = temporary;
		RenderTexture renderTexture2 = temporary2;
		renderTexture.name = "current";
		renderTexture2.name = "next";
		int num = 5;
		for (int i = 0; i < num; i++)
		{
			material.SetVector(ShaderProperties.BlurSize, new Vector4(vector.x, vector.y, 0f, 0f) * (1f + (float)i / 6f));
			material.SetVector(ShaderProperties.TextureScalingAndOffset, new Vector4(1f, 1f, 0f, 0f));
			RenderTexture.active = renderTexture;
			Graphics.Blit(texture, renderTexture, material);
			texture = renderTexture;
			renderTexture = renderTexture2;
			renderTexture2 = (RenderTexture)texture;
		}
		RenderTexture renderTexture3 = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
		renderTexture3.hideFlags = HideFlags.DontSave;
		Graphics.Blit(texture, renderTexture3);
		RenderTexture.active = null;
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		return renderTexture3;
	}

	// Token: 0x060030DE RID: 12510 RVA: 0x000D004C File Offset: 0x000CE24C
	public void ReleaseAreaMaskTextureB()
	{
		if (this.m_areaMaskTextureB)
		{
			UnityEngine.Object.DestroyObject(this.m_areaMaskTextureB);
			this.m_areaMaskTextureB = null;
		}
	}

	// Token: 0x04002C15 RID: 11285
	public GameWorldArea Area;

	// Token: 0x04002C16 RID: 11286
	public RuntimeGameWorldArea RuntimeArea;

	// Token: 0x04002C17 RID: 11287
	public Shader WorldMapBlurShader;

	// Token: 0x04002C18 RID: 11288
	public Transform MapPlaneTexture;

	// Token: 0x04002C19 RID: 11289
	public Texture Mask;

	// Token: 0x04002C1A RID: 11290
	public int PixelsPerUnit = 5;

	// Token: 0x04002C1B RID: 11291
	private GameObject m_addToMap;

	// Token: 0x04002C1C RID: 11292
	private RenderTexture m_areaMaskTextureA;

	// Token: 0x04002C1D RID: 11293
	private RenderTexture m_areaMaskTextureB;

	// Token: 0x04002C1E RID: 11294
	public Shader SetRGBAShader;
}
