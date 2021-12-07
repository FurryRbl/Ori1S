using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

// Token: 0x02000978 RID: 2424
public class GlideFeatherAnimator : MonoBehaviour, ISeinReceiver, IUberAtlasExternal
{
	// Token: 0x17000859 RID: 2137
	// (get) Token: 0x06003520 RID: 13600 RVA: 0x000DEBD4 File Offset: 0x000DCDD4
	private Atlas[] Atlases
	{
		get
		{
			this.InitAtlases();
			return this.m_atlases;
		}
	}

	// Token: 0x06003521 RID: 13601 RVA: 0x000DEBE4 File Offset: 0x000DCDE4
	private void InitAtlases()
	{
		if (this.m_atlases == null || this.m_atlases.Length == 0)
		{
			this.m_atlases = this.Animations.SelectMany((TextureAnimation textureAnimation) => textureAnimation.Atlases).ToArray<Atlas>();
		}
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x000DEC3C File Offset: 0x000DCE3C
	public void Awake()
	{
		this.m_meshFilter = base.GetComponent<MeshFilter>();
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_mesh = new Mesh
		{
			name = "atlasSpriteTexture"
		};
		this.m_meshFilter.sharedMesh = this.m_mesh;
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x000DEC8A File Offset: 0x000DCE8A
	public void OnDestroy()
	{
		if (this.m_mesh)
		{
			UnityEngine.Object.DestroyObject(this.m_mesh);
		}
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x000DECA8 File Offset: 0x000DCEA8
	public void Start()
	{
		foreach (Atlas atlas in this.Atlases)
		{
			List<AtlasSpriteTexture> spriteTextures = atlas.SpriteTextures;
			foreach (AtlasSpriteTexture atlasSpriteTexture in spriteTextures)
			{
				string text = atlasSpriteTexture.Name;
				if (text.Contains("glideIdle_"))
				{
					text = text.Replace("glideIdle_", "glide_");
				}
				text = text.ToLower();
				if (!this.m_nameToFrames.ContainsKey(text))
				{
					this.m_nameToFrames.Add(text.ToLower(), new GlideFeatherAnimator.AtlasAndSpriteTexture
					{
						Atlas = atlas,
						AtlasSpriteTexture = atlasSpriteTexture
					});
				}
			}
		}
		this.m_binder = new AtlasSpriteTextureBinder(AnimationMeshingSettings.Default, false, this.m_mesh);
	}

	// Token: 0x1700085A RID: 2138
	// (get) Token: 0x06003525 RID: 13605 RVA: 0x000DEDAC File Offset: 0x000DCFAC
	private AtlasSpriteTexture SeinCurrentTexture
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.Animation.Animator.TextureAnimator.Texture;
		}
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x000DEDE0 File Offset: 0x000DCFE0
	private bool MatchFrame(string name, out GlideFeatherAnimator.AtlasAndSpriteTexture texture)
	{
		name = name.ToLower().Replace("spirit_", string.Empty);
		return this.m_nameToFrames.TryGetValue(name, out texture) || this.m_nameToFrames.TryGetValue("feather" + name, out texture);
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x000DEE38 File Offset: 0x000DD038
	public void FixedUpdate()
	{
		if (this.SeinCurrentTexture == null)
		{
			return;
		}
		if (!Characters.Sein.Controller.IsGliding && !this.m_renderer.enabled)
		{
			return;
		}
		GlideFeatherAnimator.AtlasAndSpriteTexture atlasAndSpriteTexture;
		if (this.MatchFrame(this.SeinCurrentTexture.Name, out atlasAndSpriteTexture))
		{
			this.m_binder.BindTo(this.m_meshFilter, this.m_renderer.sharedMaterial, atlasAndSpriteTexture.Atlas, atlasAndSpriteTexture.Atlas.ScreenMode, atlasAndSpriteTexture.AtlasSpriteTexture);
			this.m_renderer.enabled = true;
		}
		else
		{
			this.m_renderer.enabled = false;
		}
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x000DEEE1 File Offset: 0x000DD0E1
	public void Update()
	{
	}

	// Token: 0x06003529 RID: 13609 RVA: 0x000DEEE3 File Offset: 0x000DD0E3
	public UberScreenMode GetExternalUberScreenMode()
	{
		return this.Atlases[0].ScreenMode;
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x000DEEF2 File Offset: 0x000DD0F2
	public float GetUberTweakValue()
	{
		return this.Atlases[0].UberScreenTweak;
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x000DEF01 File Offset: 0x000DD101
	public bool DoesProvideAtlas()
	{
		return this.Atlases.Length != 0 && this.Atlases[0] != null;
	}

	// Token: 0x0600352C RID: 13612 RVA: 0x000DEF20 File Offset: 0x000DD120
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x04002FC5 RID: 12229
	public SeinCharacter Sein;

	// Token: 0x04002FC6 RID: 12230
	public TextureAnimation[] Animations;

	// Token: 0x04002FC7 RID: 12231
	private Atlas[] m_atlases;

	// Token: 0x04002FC8 RID: 12232
	private readonly Dictionary<string, GlideFeatherAnimator.AtlasAndSpriteTexture> m_nameToFrames = new Dictionary<string, GlideFeatherAnimator.AtlasAndSpriteTexture>();

	// Token: 0x04002FC9 RID: 12233
	private Mesh m_mesh;

	// Token: 0x04002FCA RID: 12234
	private MeshFilter m_meshFilter;

	// Token: 0x04002FCB RID: 12235
	private Renderer m_renderer;

	// Token: 0x04002FCC RID: 12236
	private AtlasSpriteTextureBinder m_binder;

	// Token: 0x02000979 RID: 2425
	public struct AtlasAndSpriteTexture
	{
		// Token: 0x04002FCE RID: 12238
		public Atlas Atlas;

		// Token: 0x04002FCF RID: 12239
		public AtlasSpriteTexture AtlasSpriteTexture;
	}
}
